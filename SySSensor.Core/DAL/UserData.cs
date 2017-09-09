using System.Collections.Generic;
using System.Linq;
using SySSensor.Core.Entities;

namespace SySSensor.Core.DAL
{
    public class UserData
    {
        public static IEnumerable<User> GetUsers()
        {
            var users = new List<User>
            {
                new User {Id = 1, Email = "mauriciojsola11@gmail.com", Password = "987654", Status = true},
                new User {Id = 1, Email = "msola", Password = "987654", Status = true}
            };

            return users.ToList();
        }
    }
}
