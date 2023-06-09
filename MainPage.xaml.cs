﻿using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Linq;

namespace PerfectS;

public partial class MainPage : ContentPage
{
    private readonly ShiftChoiseSave _shiftChoiseSave;
    private readonly ShiftPageInformations _shiftPageInformation;

    private readonly List<Picker> pickerList = new List<Picker>();
    private readonly string[] weekDays = new string[] { "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma", "Cumartesi", "Pazar" };

    public MainPage()
    {
        Title = "Hoşgeldin, " + UserSession.Session_Username;
        InitializeComponent();

        _shiftChoiseSave = new ShiftChoiseSave(new PSDbContext());
        _shiftPageInformation = new ShiftPageInformations(new PSDbContext());
    }

    public async void ShiftSaveClicked(object sender, EventArgs e)
    {
        bool result = await _shiftChoiseSave.ShiftChoiseUpload(pickerList);
        if (result)
        {
            await DisplayAlert("Başarılı", "Tercihleriniz kaydedildi.", "Tamam");
        }
        else await DisplayAlert("Başarısız", "Bir sorun oluştu.", "Tamam");
    }

    private async void CreateShiftChoiseLayout()
    {
        Grid grid = new Grid
        {
            RowDefinitions =
            {
                new RowDefinition {Height = new GridLength(50)},
                new RowDefinition {Height = new GridLength(50)},
                new RowDefinition {Height = new GridLength(50)},
                new RowDefinition {Height = new GridLength(50)},
                new RowDefinition {Height = new GridLength(50)},
                new RowDefinition {Height = new GridLength(50)},
                new RowDefinition {Height = new GridLength(50)},
                new RowDefinition {Height = new GridLength(50)},
                new RowDefinition {Height = GridLength.Auto}
            },
            ColumnDefinitions =
            {
                new ColumnDefinition {Width = new GridLength(150)},
                new ColumnDefinition {Width = new GridLength(150)},
                new ColumnDefinition {Width = new GridLength(150)},
                new ColumnDefinition {Width = new GridLength(150)}
            }
        };

        grid.RowSpacing = 20;
        grid.ColumnSpacing = 10;

        grid.Add(new Label
        {
            Text = "Günler",
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center,
            FontAttributes = FontAttributes.Bold,
        }, 0, 0);
        grid.Add(new Label
        {
            Text = "1.Shift",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            FontAttributes = FontAttributes.Bold,
        }, 1, 0);
        grid.Add(new Label
        {
            Text = "2.Shift",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            FontAttributes = FontAttributes.Bold,
        }, 2, 0);
        grid.Add(new Label
        {
            Text = "3.Shift",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            FontAttributes = FontAttributes.Bold,
        }, 3, 0);

        int[] workingDays = await _shiftPageInformation.ShiftPageInfo();


        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < workingDays[i]; j++)
            {
                if (j == 0)
                {
                    Label label = new Label();
                    label.Text = weekDays[i];
                    label.VerticalOptions = LayoutOptions.Center;
                    label.HorizontalOptions = LayoutOptions.Start;
                    label.FontAttributes = FontAttributes.Bold;
                    grid.Add(label, 0, (i + 1));

                    Picker picker = new Picker();
                    picker.ItemsSource = new string[] { "Çok Uygun", "Uygun", "Uygun Değil", "İmkansız" };
                    picker.SelectedIndex = 3;
                    picker.AutomationId = weekDays[i] + (j + 1).ToString();
                    picker.HorizontalOptions = LayoutOptions.Center;
                    picker.VerticalOptions = LayoutOptions.Center;
                    grid.Add(picker, (j + 1), (i + 1));
                    pickerList.Add(picker);
                }
                else
                {
                    Picker picker = new Picker();
                    picker.ItemsSource = new string[] { "Çok Uygun", "Uygun", "Uygun Değil", "İmkansız" };
                    picker.SelectedIndex = 3;
                    picker.AutomationId = weekDays[i] + (j + 1).ToString();
                    picker.HorizontalOptions = LayoutOptions.Center;
                    picker.VerticalOptions = LayoutOptions.Center;
                    grid.Add(picker, (j + 1), (i + 1));
                    pickerList.Add(picker);
                }
            }
        }

        MainLayout.Add(grid);
    }

    public void RefreshButtonClicked(object sender, EventArgs e)
    {
        if (MainLayout.Children == null)
        {
            CreateShiftChoiseLayout();
            shiftSaveButton.IsVisible = true;
        }
        else
        {
            MainLayout.Children.Clear();
            CreateShiftChoiseLayout();
            shiftSaveButton.IsVisible = true;
        }
    }
}
