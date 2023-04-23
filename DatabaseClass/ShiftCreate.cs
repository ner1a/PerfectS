using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PerfectS;
using System.Linq;

namespace PerfectS
{

    public class ShiftCreate
    {
        private readonly PSDbContext _context;
        private readonly ShiftPageInformations _shiftPageInformation;
        private readonly List<Label> labelList = new List<Label>();

        public ShiftCreate(PSDbContext context)
        {
            _context = context;
            _shiftPageInformation = new ShiftPageInformations(new PSDbContext());
        }

        public List<Label> ShiftCreateCalc()
        {
            int index = 0;

            int[] dayShiftCount = _shiftPageInformation.ShiftPageInfo();
            
            string[] weekDaysEN = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            
            int[] weekDayDensity = new int[21];
            
            var brand = _context.Brands.Where(b => b.Brand_Id == UserSession.Session_Brand_Id).FirstOrDefault();
            
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
                    var toplam = _context.ShiftChoises.Where(s => s.Brand_Id == UserSession.Session_Brand_Id).Sum(s => EF.Property<int>(s, columnName));
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

                    var shiftWorkers = _context.ShiftChoises
                        .Join(_context.Users,
                            sc => sc.User_Id,
                            us => us.User_Id,
                            (sc, us) => new { ShiftChoise = sc, User = us })
                        .Where(s => s.ShiftChoise.Brand_Id == UserSession.Session_Brand_Id && EF.Property<int>(s.ShiftChoise, shiftColumn) != 4)
                        .OrderBy(s => EF.Property<int>(s.ShiftChoise, shiftColumn))
                        .ThenByDescending(u => u.User.Performance)
                        .Select(s => new { shiftColumn, s.ShiftChoise.User_Id })
                        .Take(shiftWorkerCount)
                        .ToList();
                        
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
                                shiftWorkers = _context.ShiftChoises
                                .Join(_context.Users,
                                    sc => sc.User_Id,
                                    us => us.User_Id,
                                    (sc, us) => new { ShiftChoise = sc, User = us })
                                .Where(s => s.ShiftChoise.User_Id != item.User_Id && s.ShiftChoise.Brand_Id == UserSession.Session_Brand_Id && EF.Property<int>(s.ShiftChoise, shiftColumn) != 4)
                                .OrderBy(s => EF.Property<int>(s.ShiftChoise, shiftColumn))
                                .ThenByDescending(u => u.User.Performance)
                                .Select(s => new { shiftColumn, s.ShiftChoise.User_Id })
                                .Take(shiftWorkerCount)
                                .ToList();
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
                    string columnName = weekDaysEN[i-1] + "_" + (j).ToString();
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
                            var user = _context.Users.Where(u => u.User_Id == userID).FirstOrDefault();
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
            return labelList;
        }
    }
}