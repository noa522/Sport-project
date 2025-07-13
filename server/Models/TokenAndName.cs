namespace computerized_sports_course.Models
{
    public class TokenAndName
    {
        public string Token { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        // קונסטרוקטור
        public TokenAndName(string token, string name, string phoneNumber)
        {
            Token = token;
            Name = name;
            PhoneNumber = phoneNumber;
        }
    }
}
