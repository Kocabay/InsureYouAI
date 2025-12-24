using Microsoft.AspNetCore.Identity;

namespace InsureYouAI.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Imageurl { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
