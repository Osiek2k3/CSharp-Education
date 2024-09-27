using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositories
{
    public interface ITokenRepsitory
    {
        string CreateJWTToken(IdentityUser user,List<string> roles);
    }
}
