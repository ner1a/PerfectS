using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls.Shapes;
using System.Linq;
using System.Reflection.PortableExecutable;

namespace PerfectS;

public partial class BrandPage : ContentPage
{
    private readonly ShiftCreate _shiftCreate;

    public BrandPage()
    {
        _shiftCreate = new ShiftCreate(new PSDbContext());
        InitializeComponent();
        BrandNameLabel.Text = UserSession.Session_Brandname;
        BrandEmployeeLabel.Text += (UserSession.Session_EmployeeCount.ToString());
    }

    private async void ShiftCreateButtonClicked(object sender, EventArgs e)
    {
        Grid shiftLayoutGrid = await _shiftCreate.ShiftCreateCalc();
        if (ShiftLayout.Children == null)
        {
            ShiftLayout.Children.Add(shiftLayoutGrid);
            await DisplayAlert("Baþarýlý", "Çalýþma programý oluþturuldu.", "Tamam");
        }
        else
        {
            ShiftLayout.Children.Clear();
            ShiftLayout.Children.Add(shiftLayoutGrid);
            await DisplayAlert("Baþarýlý", "Çalýþma programý oluþturuldu.", "Tamam");
        }
    }
}