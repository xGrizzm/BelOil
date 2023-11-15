using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace webapi.services
{
    public abstract class BaseService
    {
        private readonly ClaimsPrincipal _user;
        public ClaimsPrincipal User => _user;

        private readonly IMapper _mapper;
        public IMapper Mapper => _mapper;

        public BaseService(IHttpContextAccessor httpContextAccessor, 
            IMapper mapper)
        {
            _user = httpContextAccessor.HttpContext.User;
            _mapper = mapper;
        }
    }
}
