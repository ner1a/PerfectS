using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.GTKSpecific;
using Microsoft.Maui.Controls.Shapes;
using System.Linq;
using System.Xml;


namespace PerfectS;

public partial class BrandEmployees : ContentPage
{
	public BrandEmployees()
	{
        EmployeeRefresh();
        InitializeComponent();
	}

    private async void EmployeeRefresh()
    {
        using (var context = new PSDbContext())
        {
            var employeeList = await context.Users.Where(u => u.Brand_Id == UserSession.Session_Brand_Id && u.Role_Id == 3).OrderByDescending(u => u.Performance).ToListAsync();

            foreach (var employee in employeeList)
            {
                Grid layout = new Grid()
                {
                    ColumnSpacing = 15,
                    RowDefinitions =
                    {
                        new RowDefinition(),
                        new RowDefinition()
                    },
                    ColumnDefinitions =
                    {
                        new ColumnDefinition{ Width = new GridLength(120) },
                        new ColumnDefinition{ Width = new GridLength(50) },
                        new ColumnDefinition{ Width = new GridLength(120) },
                        new ColumnDefinition{ Width = new GridLength(70) },
                        new ColumnDefinition{ Width = new GridLength(70) },
                        new ColumnDefinition{ Width = new GridLength(50) }
                    }
                };
                layout.Margin = new Thickness(25, 2, 25, 2);

                Label nameLabel = new Label();
                nameLabel.Text = employee.Username;
                if (employee.Username.Length <= 6)
                {
                    nameLabel.Text = employee.Username;
                }
                nameLabel.VerticalOptions = LayoutOptions.Center;

                Label perfLabel = new Label();
                perfLabel.Text = employee.Performance.ToString();
                perfLabel.VerticalOptions = LayoutOptions.Center;

                Label newPerfLabel = new Label();
                newPerfLabel.Text = "Performans ata:";
                newPerfLabel.VerticalOptions = LayoutOptions.Center;

                Entry newPerfEntry = new Entry();
                newPerfEntry.VerticalOptions = LayoutOptions.Center;
                newPerfEntry.Placeholder = "0-100";

                Button perfSaveButton = new Button();
                perfSaveButton.Text = "Kaydet";
                perfSaveButton.HeightRequest = 30;
                perfSaveButton.WidthRequest = 70;
                perfSaveButton.Padding = new Thickness(1);
                perfSaveButton.AutomationId = employee.User_Id.ToString();
                perfSaveButton.CommandParameter = newPerfEntry;
                perfSaveButton.Clicked += PerfSaveButton_Clicked;

                Button empDeleteButton = new Button();
                empDeleteButton.Text = "SÝL";
                empDeleteButton.HeightRequest = 30;
                empDeleteButton.WidthRequest = 30;
                empDeleteButton.Padding = new Thickness(1);
                empDeleteButton.BackgroundColor = Colors.Red;
                empDeleteButton.TextColor = Colors.White;
                empDeleteButton.Clicked += async (sender, e) =>
                {
                    using (var context = new PSDbContext())
                    {
                        var user = await context.Users.FindAsync(employee.User_Id);
                        var empShiftChoise = await context.ShiftChoises.FirstOrDefaultAsync(e => e.User_Id == employee.User_Id && e.Brand_Id == employee.Brand_Id);
                        context.ShiftChoises.Remove(empShiftChoise);
                        context.Users.Remove(user);
                        await context.SaveChangesAsync();
                        await DisplayAlert("Çalýþan Silindi!", "Kullanýcý adý: " + nameLabel.Text, "Tamam");
                    }
                };

                var empShiftChoise = await context.ShiftChoises.FirstOrDefaultAsync(e => e.User_Id == employee.User_Id && e.Brand_Id == employee.Brand_Id);

                Label lastUpdateLabel = new Label();
                lastUpdateLabel.TextColor = Colors.LightGreen;
                lastUpdateLabel.Text = "    Güncel Shift Tercihi:\t " + empShiftChoise.Last_Update;

                layout.Add(nameLabel, 0, 0);
                layout.Add(perfLabel, 1, 0);
                layout.Add(newPerfLabel, 2, 0);
                layout.Add(newPerfEntry, 3, 0);
                layout.Add(perfSaveButton, 4, 0);
                layout.Add(empDeleteButton, 5, 0);
                Grid.SetColumnSpan(lastUpdateLabel, 6);
                layout.Add(lastUpdateLabel, 0, 1);

                Border border = new Border
                {
                    ClassId = employee.User_Id.ToString(),
                    Padding = new Thickness(3),
                    Margin = new Thickness(5),
                    StrokeShape = new RoundRectangle
                    {
                        CornerRadius = new CornerRadius(0, 40, 40, 0)
                    },
                    Stroke = Colors.LightGreen,
                    Content = layout
                };
                EmployeeLayout.Children.Add(border);
            }

            Grid newEmpLayout = new Grid()
            {
                ColumnSpacing = 25,
                RowDefinitions =
                    {
                        new RowDefinition()
                    },
                ColumnDefinitions =
                    {
                        new ColumnDefinition{ Width = new GridLength(105) },
                        new ColumnDefinition{ Width = new GridLength(110) },
                        new ColumnDefinition{ Width = new GridLength(110) },
                        new ColumnDefinition{ Width = new GridLength(70) }
                    }
            };
            newEmpLayout.Margin = new Thickness(25, 2, 25, 2);

            Label newEmployeeLabel = new Label();
            newEmployeeLabel.Text ="Çalýþan Ekle:";
            newEmployeeLabel.VerticalOptions = LayoutOptions.Center;

            Entry newEmpUsername = new Entry();
            newEmpUsername.VerticalOptions = LayoutOptions.Center;
            newEmpUsername.Placeholder = "Kullanýcý Adý";

            Entry newEmpPerf = new Entry();
            newEmpPerf.VerticalOptions = LayoutOptions.Center;
            newEmpPerf.Placeholder = "Performans";
            newEmpPerf.Margin = new Thickness(0, 0, 5, 0);

            Button newEmpAddButton = new Button();
            newEmpAddButton.Text = "Kaydet";
            newEmpAddButton.HeightRequest = 30;
            newEmpAddButton.WidthRequest = 70;
            newEmpAddButton.Padding = new Thickness(1);
            newEmpAddButton.Clicked += async (sender, e) =>
            {
                using (var context = new PSDbContext())
                {
                    try
                    {
                        await context.Users.AddAsync(new User { Username = newEmpUsername.Text, Performance = int.Parse(newEmpPerf.Text), Brand_Id = UserSession.Session_Brand_Id, Password = "123456", Role_Id = 3 });
                        await context.SaveChangesAsync();
                        var user = await context.Users.FirstOrDefaultAsync(e => e.Username == newEmpUsername.Text && e.Brand_Id == UserSession.Session_Brand_Id && e.Performance == int.Parse(newEmpPerf.Text) && e.Password == "123456" && e.Role_Id == 3);
                        await context.ShiftChoises.AddAsync(new ShiftChoise {User_Id = user.User_Id , Brand_Id = UserSession.Session_Brand_Id });
                        await context.SaveChangesAsync();
                        await DisplayAlert("Çalýþan eklendi", "Kullanýcý adý: " + newEmpUsername.Text + "\n" + "Geçici Þifre: 123456", "Tamam");
                    }
                    catch (Exception)
                    {
                        await DisplayAlert("Bir hata oluþtu!", "Lütfen girdiðiniz deðerleri kontrol edip tekrar deneyiniz", "Tamam");
                    }
                }                    
            };

            newEmpLayout.Add(newEmployeeLabel, 0, 0);
            newEmpLayout.Add(newEmpUsername, 1, 0);
            newEmpLayout.Add(newEmpPerf, 2, 0);
            newEmpLayout.Add(newEmpAddButton, 3, 0);

            Border newEmpBorder = new Border
            {
                Padding = new Thickness(3),
                Margin = new Thickness(5),
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = new CornerRadius(0, 40, 40, 0)
                },
                Stroke = Colors.Yellow,
                Content = newEmpLayout
            };
            EmployeeLayout.Children.Add(newEmpBorder);
        }
    }

    private async void PerfSaveButton_Clicked(object sender, EventArgs e)
    {
        using (var context = new PSDbContext())
        {
            try
            {
                Button saveButton = (Button)sender;
                var employee = await context.Users.FirstOrDefaultAsync(u => u.Brand_Id == UserSession.Session_Brand_Id && u.Role_Id == 3 && u.User_Id == Int32.Parse(saveButton.AutomationId));
                Entry newPerfEntry = (Entry)saveButton.CommandParameter;
                employee.Performance = Int32.Parse(newPerfEntry.Text);
                await context.SaveChangesAsync();
                await DisplayAlert("Baþarýlý", "Performans deðeri deðiþtirildi", "Tamam");
            }
            catch (Exception)
            {
                await DisplayAlert("Baþarýsýz", "Girdiðiniz deðeri kontrol ediniz.", "Tamam");
            }
        }
    }
    private void EmployeeRefreshButtonClicked(object sender, EventArgs e)
    {
        EmployeeLayout.Children.Clear();
        EmployeeRefresh();
    }
}