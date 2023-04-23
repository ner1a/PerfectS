using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PerfectS;
using System.Linq;

namespace PerfectS
{

    public class ShiftCreate
    {
        private readonly ShiftPageInformations _shiftPageInformation;

        public ShiftCreate(PSDbContext context)
        {
            _shiftPageInformation = new ShiftPageInformations(new PSDbContext());
        }

        public async Task<Grid> ShiftCreateCalc()
        {
            using(var context = new PSDbContext())
            {

                List<Label> labelList = new List<Label>();

                int index = 0;

                int[] dayShiftCount = await _shiftPageInformation.ShiftPageInfo();

                string[] weekDaysEN = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

                int[] weekDayDensity = new int[21];

                var brand = await context.Brands.Where(b => b.Brand_Id == UserSession.Session_Brand_Id).FirstOrDefaultAsync();

                int[] shiftWorkerCounts = new int[21];

                string[] shiftColumuns = new string[21];

                for (int i = 0; i < dayShiftCount.Length; i++)
                {
                    for (int j = 0; j < dayShiftCount[i]; j++)
                    {
                        string columnName = weekDaysEN[i] + "_" + (j + 1).ToString();
                        shiftColumuns[index] = columnName;
                        string brandColumnName = weekDaysEN[i] + (j + 1).ToString() + "_Shift_Count";
                        var brandColumn = typeof(Brand).GetProperty(brandColumnName);
                        var shiftWorker = brandColumn.GetValue(brand);
                        shiftWorkerCounts[index] = (int)shiftWorker;
                        var toplam = await context.ShiftChoises.Where(s => s.Brand_Id == UserSession.Session_Brand_Id).SumAsync(s => EF.Property<int>(s, columnName));
                        weekDayDensity[index] = toplam;
                        index++;
                    }

                    if (dayShiftCount[i] < 3)
                    {
                        index += 3 - dayShiftCount[i];
                    }
                }

                Dictionary<int, List<string>> shiftWorkersDictionary = new Dictionary<int, List<string>>();

                while (true)
                {
                    if (weekDayDensity.Max() > 0)
                    {
                        int shiftIndex = Array.IndexOf(weekDayDensity, weekDayDensity.Max());
                        string shiftColumn = shiftColumuns[shiftIndex];
                        int shiftWorkerCount = shiftWorkerCounts[shiftIndex];

                        var shiftWorkers = await context.ShiftChoises
                            .Join(context.Users,
                                sc => sc.User_Id,
                                us => us.User_Id,
                                (sc, us) => new { ShiftChoise = sc, User = us })
                            .Where(s => s.ShiftChoise.Brand_Id == UserSession.Session_Brand_Id && EF.Property<int>(s.ShiftChoise, shiftColumn) != 4)
                            .OrderBy(s => EF.Property<int>(s.ShiftChoise, shiftColumn))
                            .ThenByDescending(u => u.User.Performance)
                            .Select(s => new { shiftColumn, s.ShiftChoise.User_Id })
                            .Take(shiftWorkerCount)
                            .ToListAsync();

                        foreach (var item in shiftWorkers)
                        {
                            if (shiftWorkersDictionary.ContainsKey(item.User_Id))
                            {
                                string day = item.shiftColumn.Substring(0, item.shiftColumn.IndexOf("_"));
                                string sameDay = day + "_1";
                                string alsoSameDay = day + "_2";
                                string stillSameDay = day + "_3";
                                if (shiftWorkersDictionary[item.User_Id].Contains(sameDay) ||
                                    shiftWorkersDictionary[item.User_Id].Contains(alsoSameDay) ||
                                    shiftWorkersDictionary[item.User_Id].Contains(stillSameDay))
                                {
                                    shiftWorkers = await context.ShiftChoises
                                    .Join(context.Users,
                                        sc => sc.User_Id,
                                        us => us.User_Id,
                                        (sc, us) => new { ShiftChoise = sc, User = us })
                                    .Where(s => s.ShiftChoise.User_Id != item.User_Id && s.ShiftChoise.Brand_Id == UserSession.Session_Brand_Id && EF.Property<int>(s.ShiftChoise, shiftColumn) != 4)
                                    .OrderBy(s => EF.Property<int>(s.ShiftChoise, shiftColumn))
                                    .ThenByDescending(u => u.User.Performance)
                                    .Select(s => new { shiftColumn, s.ShiftChoise.User_Id })
                                    .Take(shiftWorkerCount)
                                    .ToListAsync();
                                }
                            }
                        }

                        foreach (var item in shiftWorkers)
                        {
                            if (shiftWorkersDictionary.ContainsKey(item.User_Id))
                            {
                                if (!shiftWorkersDictionary[item.User_Id].Contains(item.shiftColumn))
                                {
                                    shiftWorkersDictionary[item.User_Id].Add(item.shiftColumn);
                                }
                            }
                            else
                            {
                                shiftWorkersDictionary.Add(item.User_Id, new List<string> { item.shiftColumn });
                            }
                        }
                        weekDayDensity[shiftIndex] = 0;
                    }
                    else
                    {
                        break;
                    }
                }
                for (int i = 1; i <= 7; i++)
                {
                    for (int j = 1; j <= 3; j++)
                    {
                        string columnName = weekDaysEN[i - 1] + "_" + (j).ToString();
                        List<int> shiftWorkers = new List<int>();

                        Label label = new Label();

                        Grid.SetColumn(label, i);
                        Grid.SetRow(label, j);
                        label.VerticalOptions = LayoutOptions.Center;
                        label.HorizontalOptions = LayoutOptions.Center;
                        label.FontAttributes = FontAttributes.Bold;
                        label.HorizontalTextAlignment = TextAlignment.Center;

                        foreach (var userID in shiftWorkersDictionary.Keys)
                        {
                            if (shiftWorkersDictionary[userID].Contains(columnName))
                            {
                                shiftWorkers.Add(userID);
                            }
                        }

                        if (shiftWorkers.Count != 0)
                        {
                            int listIndex = 0;
                            foreach (var userID in shiftWorkers)
                            {
                                var user = await context.Users.Where(u => u.User_Id == userID).FirstOrDefaultAsync();
                                if (listIndex != shiftWorkers.Count - 1)
                                {
                                    label.Text += user.Username + "\n";
                                }
                                else
                                {
                                    label.Text += user.Username;
                                }

                                listIndex++;
                            }
                        }
                        else
                        {
                            label.Text = "---";
                        }
                        labelList.Add(label);
                    }
                }

                Grid weeksShiftPlan = new Grid
                {
                    HorizontalOptions = LayoutOptions.Center,
                    Margin = new Thickness(0, 0, 0, 30),
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(60) },
                    new RowDefinition { Height = new GridLength(60) },
                    new RowDefinition { Height = new GridLength(60) },
                    new RowDefinition { Height = new GridLength(60) }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(80) },
                    new ColumnDefinition { Width = new GridLength(80) },
                    new ColumnDefinition { Width = new GridLength(80) },
                    new ColumnDefinition { Width = new GridLength(80) },
                    new ColumnDefinition { Width = new GridLength(80) },
                    new ColumnDefinition { Width = new GridLength(80) },
                    new ColumnDefinition { Width = new GridLength(80) },
                    new ColumnDefinition { Width = new GridLength(80) }
                }
                };
                Label label1 = new Label
                {
                    Text = "Günler",
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Colors.LightGreen,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                };
                Grid.SetColumn(label1, 0);
                Grid.SetRow(label1, 0);

                Label label2 = new Label
                {
                    Text = "Shift 1",
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Colors.LightGreen,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                };
                Grid.SetColumn(label2, 0);
                Grid.SetRow(label2, 1);

                Label label3 = new Label
                {
                    Text = "Shift 2",
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Colors.LightGreen,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                };
                Grid.SetColumn(label3, 0);
                Grid.SetRow(label3, 2);

                Label label4 = new Label
                {
                    Text = "Shift 3",
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Colors.LightGreen,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                };
                Grid.SetColumn(label4, 0);
                Grid.SetRow(label4, 3);

                Label label5 = new Label
                {
                    Text = "Pazartesi",
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Colors.LightGreen,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                };
                Grid.SetColumn(label5, 1);
                Grid.SetRow(label5, 0);

                Label label6 = new Label
                {
                    Text = "Salı",
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Colors.LightGreen,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                };
                Grid.SetColumn(label6, 2);
                Grid.SetRow(label6, 0);

                Label label7 = new Label
                {
                    Text = "Çarşamba",
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Colors.LightGreen,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                };
                Grid.SetColumn(label7, 3);
                Grid.SetRow(label7, 0);

                Label label8 = new Label
                {
                    Text = "Perşembe",
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Colors.LightGreen,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                };
                Grid.SetColumn(label8, 4);
                Grid.SetRow(label8, 0);

                Label label9 = new Label
                {
                    Text = "Cuma",
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Colors.LightGreen,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                };
                Grid.SetColumn(label9, 5);
                Grid.SetRow(label9, 0);

                Label label10 = new Label
                {
                    Text = "Cumartesi",
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Colors.LightGreen,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                };
                Grid.SetColumn(label10, 6);
                Grid.SetRow(label10, 0);

                Label label11 = new Label
                {
                    Text = "Pazar",
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Colors.LightGreen,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                };
                Grid.SetColumn(label11, 7);
                Grid.SetRow(label11, 0);

                weeksShiftPlan.Children.Add(label1);
                weeksShiftPlan.Children.Add(label2);
                weeksShiftPlan.Children.Add(label3);
                weeksShiftPlan.Children.Add(label4);
                weeksShiftPlan.Children.Add(label5);
                weeksShiftPlan.Children.Add(label6);
                weeksShiftPlan.Children.Add(label7);
                weeksShiftPlan.Children.Add(label8);
                weeksShiftPlan.Children.Add(label9);
                weeksShiftPlan.Children.Add(label10);
                weeksShiftPlan.Children.Add(label11);

                foreach (var label in labelList)
                {
                    weeksShiftPlan.Children.Add(label);
                }

                return weeksShiftPlan;
            }            
        }
    }
}