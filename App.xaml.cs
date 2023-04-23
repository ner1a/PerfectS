using Microsoft.EntityFrameworkCore;
namespace PerfectS;

public partial class App : Application
{

    private static PSDbContext _context;

    public static PSDbContext Context
    {
        get
        {
            if (_context == null)
            {
                var builder = new DbContextOptionsBuilder<PSDbContext>();
                builder.UseSqlServer("Server=tcp:perfects.database.windows.net,1433;Initial Catalog=PerfectS;Persist Security Info=False;User ID=Neria;Password=0guzh4n-4YD1N;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                _context = new PSDbContext(builder.Options);
            }

            return _context;
        }
    }
    public App()
	{
		InitializeComponent();

        MainPage = new NavigationPage(new LoginPage());
    }
}
