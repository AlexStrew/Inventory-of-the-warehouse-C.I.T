using Microsoft.EntityFrameworkCore;

namespace InvAPI.Auth
{
    [Keyless]
    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string User = "User";
    }
}
