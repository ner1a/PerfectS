using Microsoft.EntityFrameworkCore;
using PerfectS;

public class ShiftChoiseSave
{
    public ShiftChoiseSave(PSDbContext context)
    {
        
    }

    public async Task<bool> ShiftChoiseUpload(List<Picker> pickerList)
    {
        using (var context = new PSDbContext())
        {
            var shiftchoise = await context.ShiftChoises.Where(s => s.Brand_Id == UserSession.Session_Brand_Id && s.User_Id == UserSession.Session_User_Id).FirstOrDefaultAsync();
            var brand = await context.Brands.FirstOrDefaultAsync(b => b.Brand_Id == UserSession.Session_Brand_Id);

            string[] weekDays = new string[] { "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma", "Cumartesi", "Pazar" };
            string[] weekDaysEN = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

            if (shiftchoise != null)
            {
                foreach (Picker picker in pickerList)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (picker.AutomationId == weekDays[i] + (j + 1).ToString())
                            {
                                string columnname = weekDaysEN[i] + "_" + (j + 1).ToString();
                                var column = typeof(ShiftChoise).GetProperty(columnname);
                                column.SetValue(shiftchoise, (picker.SelectedIndex + 1));
                            }
                        }
                    }
                    shiftchoise.Last_Update = DateTime.Now.ToString("dd/MM/yy HH:mm");
                }
                context.SaveChanges();
                return true;
            }
            else return false;
        }        
    }
}