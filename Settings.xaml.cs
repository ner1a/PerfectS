using Microsoft.EntityFrameworkCore;

namespace PerfectS;

public partial class Settings : ContentPage
{
	public Settings()
	{
		InitializeComponent();
	}

    private async void PassChangeButton_Clicked(object sender, EventArgs e)
    {
        using (var context = new PSDbContext())
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.User_Id == UserSession.Session_User_Id);
            if (user.Password == OldPassEntry.Text)
            {
                if (NewPassEntry.Text == NewPassEntryControl.Text)
                {
                    user.Password = NewPassEntry.Text;
                    await context.SaveChangesAsync();
                    await DisplayAlert("Baþarýlý", "Þifreniz baþarýyla deðiþtirildi", "Tamam");
                }
                else
                {
                    await DisplayAlert("Þifreler eþleþmiyor!", "Yeni þifrenizi doðru giriniz.", "Tamam");
                }
            }
            else
            {
                await DisplayAlert("Hatalý Þifre!", "Kullandýðýnýz þifreyi yanlýþ girdiniz.", "Tamam");
            }
        }
    }
}