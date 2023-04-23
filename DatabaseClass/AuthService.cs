using Microsoft.EntityFrameworkCore;

public class AuthService
{

    public AuthService(PSDbContext context)
    {

    }

    public async Task<int> AuthenticateUserAsync(string username, string password)
    {
        try
        {
            using (var context = new PSDbContext())
            {
                var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
                var brand = await context.Brands.FirstOrDefaultAsync(u => u.Brand_Id == user.Brand_Id);

                if (user != null)
                {
                    UserSession.Session_Brandname = brand.Brand_Name;
                    UserSession.Session_Username = user.Username;
                    UserSession.Session_User_Id = user.User_Id;
                    UserSession.Session_Brand_Id = user.Brand_Id;
                    UserSession.Session_Role_Id = user.Role_Id;
                    return user.Role_Id;
                }

                return -1;
            }
        }
        catch (Exception)
        {
            return -1;
        }
    }
}
