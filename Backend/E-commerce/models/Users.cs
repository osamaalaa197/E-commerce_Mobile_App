using Microsoft.AspNetCore.Identity;

namespace E_commerce.models
{
    public class Users :IdentityUser
    {
        public string Location { get; set; }

    }
}
