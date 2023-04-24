namespace PerfectS;

public partial class BrandTabbedPage : TabbedPage
{
	public BrandTabbedPage()
	{
		Title = UserSession.Session_Brandname;
		InitializeComponent();
	}
}