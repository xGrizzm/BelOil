using webapi.core.Entities;

namespace webapi.repositories.contracts
{
    public interface IFieldRepository
    {
        Task<FieldEntity> AddAsync(FieldEntity field);

        Task<IEnumerable<FieldEntity>> GetAsync(int userId);
    }
}
