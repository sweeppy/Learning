using Microsoft.AspNet.Identity.EntityFramework;

namespace NotebookWPFApi.Models
{
    public static class Roles
    {
        public static IdentityRole user = new IdentityRole { Name = "User" };
        public static IdentityRole admin = new IdentityRole { Name = "Admin" };
    }
}
