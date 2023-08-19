using HotelListing.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto);
        Task<bool> Login(LoginDto loginDto);
    }
}
