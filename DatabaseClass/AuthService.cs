using Microsoft.EntityFrameworkCore;

public class AuthService
{
    private readonly PSDbContext _context;

    public AuthService(PSDbContext context)
    {
        _context = context;
    }

    public int AuthenticateUser(string username, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

        if (user != null)
        {
            UserSession.Session_User_Id = user.User_Id;
            UserSession.Session_Brand_Id = user.Brand_Id;
            UserSession.Session_Role_Id = user.Role_Id;
            return user.Role_Id;
        }
        return -1;
    }
}
