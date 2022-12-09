using System;
using System.ComponentModel.DataAnnotations;

namespace GameAnalytics.Model
{
    public class Users
    {
        [Key]
        public Guid UserID { get; set; }
        public string Full_Name { get; set; }
        public string Email { get; set; }
    }
}
