using webapi.core.Entities;

namespace webapi.repositories.contracts
{
    public interface IUserRepository
    {
        Task<UserEntity> GetAsync(string login);
    }
}
