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
            UserEntity user = await _userRepository.GetAsync(User.GetId());

            IEnumerable<Field> fields = Mapper.Map<IEnumerable<Field>>(user.Fields);

            foreach (Field field in fields)
            {
                field.OilPumpPrice = GameHelper.GetOilPumpPrice(field.Multiplier);

                foreach (OilPump oilPump in field.OilPumps)
                {
                    oilPump.Barrels = GameHelper.GetBarrels(field.Multiplier);
                }
            }

            return new GameResponse(user.Barrels, fields, GameHelper.GetFieldPrice((int)Math.Pow(2, user.Fields.Count)));
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

            DateTimeOffset nextPumping = DateTime.UtcNow.AddSeconds(GameHelper.GetNextPumpingSeconds(oilPump.Field.Multiplier));
            await _oilPumpRepository.UpdateNextPumpingAsync(oilPumpId, nextPumping);

            int barrels = user.Barrels + GameHelper.GetBarrels(oilPump.Field.Multiplier);
            await _userRepository.UpdateBarrelsAsync(user.Id, barrels);

            return new CollectOilPumpResponse(barrels, nextPumping.DateTime);
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

            Field field = Mapper.Map<Field>(addedField);
            field.OilPumpPrice = GameHelper.GetOilPumpPrice(multiplier);
            return new BuyResponse<Field>(barrels, field);
        }

        public async Task<BuyResponse<OilPump>> BuyOilPumpAsync(int fieldId)
        {
            UserEntity user = await _userRepository.GetAsync(User.GetId());
            FieldEntity field = user.Fields.FirstOrDefault(f => f.Id == fieldId);

            int multiplier = field.Multiplier;
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

            OilPump oilPump = Mapper.Map<OilPump>(addedOilPump);
            oilPump.Barrels = GameHelper.GetBarrels(multiplier);
            return new BuyResponse<OilPump>(barrels, oilPump);
        }
    }
}
