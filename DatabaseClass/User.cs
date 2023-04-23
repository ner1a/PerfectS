using System.ComponentModel.DataAnnotations;

namespace PerfectS
{
    public class User
    {
        [Key]
        public int User_Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role_Id { get; set; }
        public int Brand_Id { get; set; }
        public int Performance { get; set; }
    }
}