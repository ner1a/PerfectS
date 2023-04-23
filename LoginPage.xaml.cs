using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.Maui.Controls;

namespace PerfectS;

public partial class LoginPage : ContentPage
{
    private readonly AuthService _authService;

    public LoginPage()
    {
        _authService = new AuthService(new PSDbContext());

        InitializeComponent();

    }

    private void LogInClicked(object sender, EventArgs e)
    {
        var isauthenticated = _authService.AuthenticateUser(usernameEntry.Text, passwordEntry.Text);

        if (isauthenticated > 0)
        {
            if (isauthenticated == 2) Navigation.PushAsync(new BrandPage());
            else Navigation.PushAsync(new MainPage());
        }
        else
        {
            logInAlert.IsVisible = true;
        }
    }

    private void UsernameEntryFocused(object sender, EventArgs e)
    {
        usernameEntry.Text = "";
        logInAlert.IsVisible = false;
    }
    private void PasswordEntryFocused(object sender, EventArgs e)
    {
        passwordEntry.Text = "";
        logInAlert.IsVisible = false;
    }

    private void UsernameEntry_Completed(object sender, EventArgs e)
    {
        passwordEntry.Focus();
    }

    private void PasswordEntry_Completed(object sender, EventArgs e)
    {
        var isauthenticated = _authService.AuthenticateUser(usernameEntry.Text, passwordEntry.Text);

        if (isauthenticated > 0)
        {
            if (isauthenticated == 2) Navigation.PushAsync(new BrandPage());
            else Navigation.PushAsync(new MainPage());
        }
        else
        {
            logInAlert.IsVisible = true;
        }
    }
}