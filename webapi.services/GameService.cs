using AutoMapper;
using Microsoft.AspNetCore.Http;
using webapi.core.Constants;
using webapi.core.Entities;
using webapi.core.Extensions;
using webapi.core.Models;
using webapi.core.Models.Responses;
using webapi.repositories.contracts;
using webapi.services.contracts;
using webapi.services.Helpers;

namespace webapi.services
{
    public class GameService : BaseService, IGameService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFieldRepository _fieldRepository;
        private readonly IOilPumpRepository _oilPumpRepository;

        public GameService(IUserRepository userRepository,
            IFieldRepository fieldRepository,
            IOilPumpRepository oilPumpRepository,

            IHttpContextAccessor httpContextAccessor,
            IMapper mapper) : base(httpContextAccessor, mapper)
        {
            _userRepository = userRepository;
            _fieldRepository = fieldRepository;
            _oilPumpRepository = oilPumpRepository;
        }

        public async Task<GameResponse> GetGameAsync()
        {
            return new GameResponse(
                Mapper.Map<IEnumerable<Field>>(
                    await _fieldRepository.GetAsync(User.GetId())));
        }

        public async Task<CollectOilPumpResponse> CollectOilPumpAsync(int fieldId, int oilPumpId)
        {
            UserEntity user = await _userRepository.GetAsync(User.GetId());

            OilPumpEntity oilPump = await _oilPumpRepository.GetAsync(oilPumpId);

            if (oilPump is null)
            {
                throw new Exception("Oil Pump not found");
            }

            if (oilPump.Field.UserId != User.GetId())
            {
                throw new Exception("You can't collect not your Oil Pump");
            }

            if (oilPump.NextPumping > DateTime.UtcNow)
            {
                throw new Exception("Unable to collect Oil Pump");
            }

            DateTime nextPumping = DateTime.UtcNow.AddSeconds(GameHelper.GetNextPumpingSeconds(oilPump.Field.Multiplier));
            await _oilPumpRepository.UpdateNextPumpingAsync(oilPumpId, nextPumping);

            int barrels = user.Barrels + GameHelper.GetBarrels(oilPump.Field.Multiplier);
            await _userRepository.UpdateBarrelsAsync(user.Id, barrels);

            return new CollectOilPumpResponse(barrels, nextPumping);
        }

        public async Task<BuyResponse<Field>> BuyFieldAsync()
        {
            UserEntity user = await _userRepository.GetAsync(User.GetId());

            int multiplier = (int)Math.Pow(2, user.Fields.Count);
            int fieldPrice = GameHelper.GetFieldPrice(multiplier);

            if (user.Barrels < fieldPrice)
            {
                throw new Exception("Not enough Barrels");
            }

            FieldEntity addedField = await _fieldRepository.AddAsync(new FieldEntity(multiplier, user.Id));

            int barrels = user.Barrels - fieldPrice;
            await _userRepository.UpdateBarrelsAsync(user.Id, barrels);

            return new BuyResponse<Field>(barrels, Mapper.Map<Field>(addedField));
        }

        public async Task<BuyResponse<OilPump>> BuyOilPumpAsync(int fieldId)
        {
            UserEntity user = await _userRepository.GetAsync(User.GetId());
            FieldEntity field = user.Fields.FirstOrDefault(f => f.Id == fieldId);

            int multiplier = (int)Math.Pow(2, user.Fields.Count);
            int oilPumpPrice = GameHelper.GetOilPumpPrice(multiplier);

            if (field is null)
            {
                throw new Exception("Field not found");
            }

            if (field.OilPumps.Count == GameConstants.OilPumpsMax)
            {
                throw new Exception("Unable to add additional Oil Pump");
            }

            if (user.Barrels < oilPumpPrice)
            {
                throw new Exception("Not enough Barrels");
            }

            OilPumpEntity addedOilPump = await _oilPumpRepository.AddAsync(new OilPumpEntity(DateTime.UtcNow, fieldId));

            int barrels = user.Barrels - oilPumpPrice;
            await _userRepository.UpdateBarrelsAsync(user.Id, barrels);

            return new BuyResponse<OilPump>(barrels, Mapper.Map<OilPump>(addedOilPump));
        }
    }
}
