using AssetManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Application.Users.Commands.Create
{
    public class UserDTOs
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Location { get; set; } = null!;
        public DateTime DateOfBirth { get; init; } = DateTime.UtcNow;
        public Gender Gender { get; set; } = Gender.Male;
        public DateTime JoinDate { get; set; }
        public string Role { get; set; } = null!;
    }
}
