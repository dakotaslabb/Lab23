using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab_23.Models
{
    public partial class Users
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password Must be between the length of 5 and 15")]
        [MaxLength(15, ErrorMessage = "Password Must be between the length of 5 and 15")]
        [MinLength(5, ErrorMessage = "Password Must be between the length of 5 and 15")]
        public string Password { get; set; }
        public double? Money { get; set; }
    }
}
