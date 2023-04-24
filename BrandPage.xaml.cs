using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls.Shapes;
using System.Linq;
using System.Reflection.PortableExecutable;

namespace PerfectS;

public partial class BrandPage : ContentPage
{
    private readonly ShiftCreate _shiftCreate;
    private readonly EmployeeUpdate _employeeUpdate;

    public BrandPage()
    {
        Title = "Hoþgeldin, " + UserSession.Session_Brandname;
        _shiftCreate = new ShiftCreate(new PSDbContext());
        _employeeUpdate = new EmployeeUpdate(new PSDbContext());
      
        InitializeComponent();
    }

    private async void Update_Button_Clicked(object sender, EventArgs e)
    {
        employeeLayout.Children.Clear();
        List<Label> employeeInfos = await _employeeUpdate.UpdateEmployeeInfos();
        for (int i = 0; i < employeeInfos.Count; i++)
        {
            employeeLayout.Children.Add(employeeInfos[i]);
        }
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

    private void Shift_Settings_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new BrandShiftSettings());
    }
}