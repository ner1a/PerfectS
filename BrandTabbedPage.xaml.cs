namespace PerfectS;

public partial class BrandTabbedPage : TabbedPage
{
	public BrandTabbedPage()
	{
		InitializeComponent();
		Title = UserSession.Session_Brandname;
    }
}