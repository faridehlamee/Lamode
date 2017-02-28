using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamode.Repositiries
{
    class UserRepository
    {
        public const string LASTNAME = "LastName";
        public const string EMAIL = "Date";
        

        IEnumerable<AspNetUser> SortUsers(IEnumerable<AspNetUser> users, string sortOrder)
        {
            switch (sortOrder)
            {
                case LASTNAME:
                    users = users.OrderByDescending(s => s.UserName);
                    break;
                case EMAIL:
                    users = users.OrderBy(s => s.Email);
                    break;
               
                default:
                    users = users.OrderBy(s => s.UserName);
                    break;
            }
            return users;
        }

        public IEnumerable<AspNetUser> GetUsers(string sortOrder)
        {
            LamodeEntities context = new LamodeEntities();
            IEnumerable<AspNetUser> users = from s in context.AspNetUsers
                                            select s;
            users = SortUsers(users, sortOrder);
            return users;
        }

    }
}
