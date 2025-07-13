using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace computerized_sports_course.Models
{
    public class UserModel
    {
        //הרשאות גישה
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public string SurName { get; set; }
        public string GivenName { get; set; }
        public string Role { get; set; }
    }

    public class UserContacts
    {
        public static List<UserModel> Db = new List<UserModel>()
        {
            new UserModel{ UserName="jason_admin", Password="myPasswORD", Mail="json.admin@gmail.com", SurName="britan", GivenName="jason", Role="administartor"},
            new UserModel{ UserName="elysa_seller", Password="myPassw1ORD", Mail="elysa.admin@gmail.com", SurName="lambert", GivenName="elysa", Role="seller"}
        };
    }
}
