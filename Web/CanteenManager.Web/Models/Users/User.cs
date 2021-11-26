using Microsoft.AspNetCore.Identity;

namespace CanteenManager.Web.Models.Users
{
    public abstract class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }
    }

    public class Customer : User
    {
        public int CartId { get; set; }
        public Cart Cart { get; set; }

    }

    public class Manager : User
    {

    }
}
