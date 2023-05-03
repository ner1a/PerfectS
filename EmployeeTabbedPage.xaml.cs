namespace PerfectS;

public partial class EmployeeTabbedPage : TabbedPage
{
	public EmployeeTabbedPage()
	{
		InitializeComponent();
        Title = UserSession.Session_Username;
    }
}