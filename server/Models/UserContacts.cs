namespace computerized_sports_course.Models
{
    public class UserContacts1
    {
        //הרשאות גישה
        public static List<UserModel> Db = new List<UserModel>() {
             new UserModel{ UserName="jason_admin",Password="myPasswORD",
            Mail="json.admin@gmail.com",SurName="britan",GivenName="jason",Role="administartor"},

        new UserModel{ UserName="elysa_seller",Password="myPassw1ORD",
            Mail="elysa.admin@gmail.com",SurName="lambert",GivenName="elysa",Role="seller"}



        };



    }
}