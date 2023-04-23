using PerfectS;

public class ShiftChoiseSave
{
    private readonly PSDbContext _context;

    public ShiftChoiseSave(PSDbContext context)
    {
        _context = context;
    }

    public bool ShiftChoiseUpload(List<Picker> pickerList)
    {
        var shiftchoise = _context.ShiftChoises.Where(s => s.Brand_Id == UserSession.Session_Brand_Id && s.User_Id == UserSession.Session_User_Id).FirstOrDefault();
        var brand = _context.Brands.FirstOrDefault(b => b.Brand_Id == UserSession.Session_Brand_Id);

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
                shiftchoise.Last_Update = DateTime.Now;
            }
            _context.SaveChanges();
            return true;
        }
        else return false;
    }
}