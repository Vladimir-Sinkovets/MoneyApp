namespace MoneyApp.UseCases
{
    public class UserConstants
    {
        public static List<User> Users = new List<User>()
        {
            new User()
            {
                Email = "test@mail.com",
                Password = "1111",
                Role = "Admin",
                UserName = "Jason",
            }
        };
    }
}
