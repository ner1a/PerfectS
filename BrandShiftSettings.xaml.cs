namespace PerfectS;

public partial class BrandShiftSettings : ContentPage
{
    private readonly WorkdaysSave _workdaysSave;
    public BrandShiftSettings()
	{
        Title = UserSession.Session_Brandname + " > Shift Ayarlarý";

        _workdaysSave = new WorkdaysSave(new PSDbContext());

        InitializeComponent();
	}

    private async void WorkingDaysSaveButtonClicked(object sender, EventArgs e)
    {
        int[] shiftCount = new int[7];
        int[] mondayShiftWorker = new int[3];
        int[] tuesdayShiftWorker = new int[3];
        int[] wednesdayShiftWorker = new int[3];
        int[] thursdayShiftWorker = new int[3];
        int[] fridayShiftWorker = new int[3];
        int[] saturdayShiftWorker = new int[3];
        int[] sundayShiftWorker = new int[3];
        if (mondayWorkSwich.IsToggled)
        {
            shiftCount[0] = Int32.Parse(mondayShiftPicker.SelectedItem.ToString());
            mondayShiftWorker[0] = Int32.Parse(monday_1_employeeCount.Text);
            mondayShiftWorker[1] = Int32.Parse(monday_2_employeeCount.Text);
            mondayShiftWorker[2] = Int32.Parse(monday_3_employeeCount.Text);
        }
        if (tuesdayWorkSwich.IsToggled)
        {
            shiftCount[1] = Int32.Parse(tuesdayShiftPicker.SelectedItem.ToString());
            tuesdayShiftWorker[0] = Int32.Parse(tuesday_1_employeeCount.Text);
            tuesdayShiftWorker[1] = Int32.Parse(tuesday_2_employeeCount.Text);
            tuesdayShiftWorker[2] = Int32.Parse(tuesday_3_employeeCount.Text);
        }
        if (wednesdayWorkSwich.IsToggled)
        {
            shiftCount[2] = Int32.Parse(wednesdayShiftPicker.SelectedItem.ToString());
            wednesdayShiftWorker[0] = Int32.Parse(wednesday_1_employeeCount.Text);
            wednesdayShiftWorker[1] = Int32.Parse(wednesday_2_employeeCount.Text);
            wednesdayShiftWorker[2] = Int32.Parse(wednesday_3_employeeCount.Text);
        }
        if (thursdayWorkSwich.IsToggled)
        {
            shiftCount[3] = Int32.Parse(thursdayShiftPicker.SelectedItem.ToString());
            thursdayShiftWorker[0] = Int32.Parse(thursday_1_employeeCount.Text);
            thursdayShiftWorker[1] = Int32.Parse(thursday_2_employeeCount.Text);
            thursdayShiftWorker[2] = Int32.Parse(thursday_3_employeeCount.Text);
        }
        if (fridayWorkSwich.IsToggled)
        {
            shiftCount[4] = Int32.Parse(fridayShiftPicker.SelectedItem.ToString());
            fridayShiftWorker[0] = Int32.Parse(friday_1_employeeCount.Text);
            fridayShiftWorker[1] = Int32.Parse(friday_2_employeeCount.Text);
            fridayShiftWorker[2] = Int32.Parse(friday_3_employeeCount.Text);
        }
        if (saturdayWorkSwich.IsToggled)
        {
            shiftCount[5] = Int32.Parse(saturdayShiftPicker.SelectedItem.ToString());
            saturdayShiftWorker[0] = Int32.Parse(saturday_1_employeeCount.Text);
            saturdayShiftWorker[1] = Int32.Parse(saturday_2_employeeCount.Text);
            saturdayShiftWorker[2] = Int32.Parse(saturday_3_employeeCount.Text);
        }
        if (sundayWorkSwich.IsToggled)
        {
            shiftCount[6] = Int32.Parse(sundayShiftPicker.SelectedItem.ToString());
            sundayShiftWorker[0] = Int32.Parse(sunday_1_employeeCount.Text);
            sundayShiftWorker[1] = Int32.Parse(sunday_2_employeeCount.Text);
            sundayShiftWorker[2] = Int32.Parse(sunday_3_employeeCount.Text);
        }
        bool isUpdated = await _workdaysSave.BrandInfoUpdate(shiftCount, mondayShiftWorker, tuesdayShiftWorker, wednesdayShiftWorker,
            thursdayShiftWorker, fridayShiftWorker, saturdayShiftWorker, sundayShiftWorker);

        if (isUpdated) await DisplayAlert("Baþarýlý", "Tercihleriniz baþarýyla kaydedildi.", "Tamam");
        else await DisplayAlert("Baþarýsýz!", "Bir hata oluþtu!", "Tamam");
    }

    private void mondayWorkSwich_Toggled(object sender, ToggledEventArgs e)
    {
        if (mondayWorkSwich.IsToggled)
        {
            mondayShiftPicker.IsVisible = true;
            mondayShiftPicker.SelectedIndex = 0;
        }
        else
        {
            mondayShiftPicker.IsVisible = false;
            mondayShiftPicker.SelectedIndex = -1;
            monday_1_employeeCount.Text = "0";
            monday_2_employeeCount.Text = "0";
            monday_3_employeeCount.Text = "0";
            monday_1_employeeCount.IsVisible = false;
            monday_2_employeeCount.IsVisible = false;
            monday_3_employeeCount.IsVisible = false;
        }
    }

    private void tuesdayWorkSwich_Toggled(object sender, ToggledEventArgs e)
    {
        if (tuesdayWorkSwich.IsToggled)
        {
            tuesdayShiftPicker.IsVisible = true;
            tuesdayShiftPicker.SelectedIndex = 0;
        }
        else
        {
            tuesdayShiftPicker.IsVisible = false;
            tuesdayShiftPicker.SelectedIndex = -1;
            tuesday_1_employeeCount.Text = "0";
            tuesday_2_employeeCount.Text = "0";
            tuesday_3_employeeCount.Text = "0";
            tuesday_1_employeeCount.IsVisible = false;
            tuesday_2_employeeCount.IsVisible = false;
            tuesday_3_employeeCount.IsVisible = false;
        }
    }

    private void wednesdayWorkSwich_Toggled(object sender, ToggledEventArgs e)
    {
        if (wednesdayWorkSwich.IsToggled)
        {
            wednesdayShiftPicker.IsVisible = true;
            wednesdayShiftPicker.SelectedIndex = 0;
        }
        else
        {
            wednesdayShiftPicker.IsVisible = false;
            wednesdayShiftPicker.SelectedIndex = -1;
            wednesday_1_employeeCount.Text = "0";
            wednesday_2_employeeCount.Text = "0";
            wednesday_3_employeeCount.Text = "0";
            wednesday_1_employeeCount.IsVisible = false;
            wednesday_2_employeeCount.IsVisible = false;
            wednesday_3_employeeCount.IsVisible = false;
        }
    }

    private void thursdayWorkSwich_Toggled(object sender, ToggledEventArgs e)
    {
        if (thursdayWorkSwich.IsToggled)
        {
            thursdayShiftPicker.IsVisible = true;
            thursdayShiftPicker.SelectedIndex = 0;
        }
        else
        {
            thursdayShiftPicker.IsVisible = false;
            thursdayShiftPicker.SelectedIndex = -1;
            thursday_1_employeeCount.Text = "0";
            thursday_2_employeeCount.Text = "0";
            thursday_3_employeeCount.Text = "0";
            thursday_1_employeeCount.IsVisible = false;
            thursday_2_employeeCount.IsVisible = false;
            thursday_3_employeeCount.IsVisible = false;
        }
    }

    private void fridayWorkSwich_Toggled(object sender, ToggledEventArgs e)
    {
        if (fridayWorkSwich.IsToggled)
        {
            fridayShiftPicker.IsVisible = true;
            fridayShiftPicker.SelectedIndex = 0;
        }
        else
        {
            fridayShiftPicker.IsVisible = false;
            fridayShiftPicker.SelectedIndex = -1;
            friday_1_employeeCount.Text = "0";
            friday_2_employeeCount.Text = "0";
            friday_3_employeeCount.Text = "0";
            friday_1_employeeCount.IsVisible = false;
            friday_2_employeeCount.IsVisible = false;
            friday_3_employeeCount.IsVisible = false;
        }
    }

    private void saturdayWorkSwich_Toggled(object sender, ToggledEventArgs e)
    {
        if (saturdayWorkSwich.IsToggled)
        {
            saturdayShiftPicker.IsVisible = true;
            saturdayShiftPicker.SelectedIndex = 0;
        }
        else
        {
            saturdayShiftPicker.IsVisible = false;
            saturdayShiftPicker.SelectedIndex = -1;
            saturday_1_employeeCount.Text = "0";
            saturday_2_employeeCount.Text = "0";
            saturday_3_employeeCount.Text = "0";
            saturday_1_employeeCount.IsVisible = false;
            saturday_2_employeeCount.IsVisible = false;
            saturday_3_employeeCount.IsVisible = false;
        }
    }

    private void sundayWorkSwich_Toggled(object sender, ToggledEventArgs e)
    {
        if (sundayWorkSwich.IsToggled)
        {
            sundayShiftPicker.IsVisible = true;
            sundayShiftPicker.SelectedIndex = 0;
        }
        else
        {
            sundayShiftPicker.IsVisible = false;
            sundayShiftPicker.SelectedIndex = -1;
            sunday_1_employeeCount.Text = "0";
            sunday_2_employeeCount.Text = "0";
            sunday_3_employeeCount.Text = "0";
            sunday_1_employeeCount.IsVisible = false;
            sunday_2_employeeCount.IsVisible = false;
            sunday_3_employeeCount.IsVisible = false;
        }
    }

    private void mondayShiftPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (mondayShiftPicker.SelectedIndex >= 0)
        {
            monday_1_employeeCount.IsVisible = true;
        }
        else
        {
            monday_1_employeeCount.IsVisible = false;
        }

        if (mondayShiftPicker.SelectedIndex >= 1)
        {
            monday_2_employeeCount.IsVisible = true;
        }
        else
        {
            monday_2_employeeCount.IsVisible = false;
        }

        if (mondayShiftPicker.SelectedIndex >= 2)
        {
            monday_3_employeeCount.IsVisible = true;
        }
        else
        {
            monday_3_employeeCount.IsVisible = false;
        }
    }

    private void tuesdayShiftPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (tuesdayShiftPicker.SelectedIndex >= 0)
        {
            tuesday_1_employeeCount.IsVisible = true;
        }
        else
        {
            tuesday_1_employeeCount.IsVisible = false;
        }

        if (tuesdayShiftPicker.SelectedIndex >= 1)
        {
            tuesday_2_employeeCount.IsVisible = true;
        }
        else
        {
            tuesday_2_employeeCount.IsVisible = false;
        }

        if (tuesdayShiftPicker.SelectedIndex >= 2)
        {
            tuesday_3_employeeCount.IsVisible = true;
        }
        else
        {
            tuesday_3_employeeCount.IsVisible = false;
        }
    }

    private void wednesdayShiftPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (wednesdayShiftPicker.SelectedIndex >= 0)
        {
            wednesday_1_employeeCount.IsVisible = true;
        }
        else
        {
            wednesday_1_employeeCount.IsVisible = false;
        }

        if (wednesdayShiftPicker.SelectedIndex >= 1)
        {
            wednesday_2_employeeCount.IsVisible = true;
        }
        else
        {
            wednesday_2_employeeCount.IsVisible = false;
        }

        if (wednesdayShiftPicker.SelectedIndex >= 2)
        {
            wednesday_3_employeeCount.IsVisible = true;
        }
        else
        {
            wednesday_3_employeeCount.IsVisible = false;
        }
    }

    private void thursdayShiftPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (thursdayShiftPicker.SelectedIndex >= 0)
        {
            thursday_1_employeeCount.IsVisible = true;
        }
        else
        {
            thursday_1_employeeCount.IsVisible = false;
        }

        if (thursdayShiftPicker.SelectedIndex >= 1)
        {
            thursday_2_employeeCount.IsVisible = true;
        }
        else
        {
            thursday_2_employeeCount.IsVisible = false;
        }

        if (thursdayShiftPicker.SelectedIndex >= 2)
        {
            thursday_3_employeeCount.IsVisible = true;
        }
        else
        {
            thursday_3_employeeCount.IsVisible = false;
        }
    }

    private void fridayShiftPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (fridayShiftPicker.SelectedIndex >= 0)
        {
            friday_1_employeeCount.IsVisible = true;
        }
        else
        {
            friday_1_employeeCount.IsVisible = false;
        }

        if (fridayShiftPicker.SelectedIndex >= 1)
        {
            friday_2_employeeCount.IsVisible = true;
        }
        else
        {
            friday_2_employeeCount.IsVisible = false;
        }

        if (fridayShiftPicker.SelectedIndex >= 2)
        {
            friday_3_employeeCount.IsVisible = true;
        }
        else
        {
            friday_3_employeeCount.IsVisible = false;
        }
    }

    private void saturdayShiftPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (saturdayShiftPicker.SelectedIndex >= 0)
        {
            saturday_1_employeeCount.IsVisible = true;
        }
        else
        {
            saturday_1_employeeCount.IsVisible = false;
        }

        if (saturdayShiftPicker.SelectedIndex >= 1)
        {
            saturday_2_employeeCount.IsVisible = true;
        }
        else
        {
            saturday_2_employeeCount.IsVisible = false;
        }

        if (saturdayShiftPicker.SelectedIndex >= 2)
        {
            saturday_3_employeeCount.IsVisible = true;
        }
        else
        {
            saturday_3_employeeCount.IsVisible = false;
        }
    }

    private void sundayShiftPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (sundayShiftPicker.SelectedIndex >= 0)
        {
            sunday_1_employeeCount.IsVisible = true;
        }
        else
        {
            sunday_1_employeeCount.IsVisible = false;
        }

        if (sundayShiftPicker.SelectedIndex >= 1)
        {
            sunday_2_employeeCount.IsVisible = true;
        }
        else
        {
            sunday_2_employeeCount.IsVisible = false;
        }

        if (sundayShiftPicker.SelectedIndex >= 2)
        {
            sunday_3_employeeCount.IsVisible = true;
        }
        else
        {
            sunday_3_employeeCount.IsVisible = false;
        }
    }

    private void EmployeeCount_Unfocused(object sender, FocusEventArgs e)
    {
        Entry focusedEntry = (Entry)sender;
        try
        {
            Int32.Parse(focusedEntry.Text);
        }
        catch (Exception)
        {
            DisplayAlert("Hatalý giriþ", "Lütfen sadece sayý giriniz!", "Tamam");
            focusedEntry.Text = "0";
        }
    }

    private void Entry_Focused(object sender, FocusEventArgs e)
    {
        Entry focusedEntry = (Entry)sender;
        focusedEntry.Text = "";
    }

}