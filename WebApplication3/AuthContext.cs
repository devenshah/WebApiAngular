using Microsoft.AspNet.Identity.EntityFramework;


namespace WebApplication3
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("AuthContext")
        {
            System.Data.Entity.Database.SetInitializer<AuthContext>(new AuthDbInitializer());
        }
    }
}