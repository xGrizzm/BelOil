using webapi.core.Models.Requests;
using webapi.core.Models.Responses;

namespace webapi.services.contracts
{
    public interface IAuthorizationService
    {
        Task<LoginResponse> LoginAsync(LoginRequest loginModel);
    }
}
