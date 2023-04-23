using Microsoft.EntityFrameworkCore;

public class EmployeeUpdate
{

    public EmployeeUpdate(PSDbContext context)
    {
            
    }

    public async Task<List<Label>> UpdateEmployeeInfos()
    {
        List<Label> labellist = new List<Label>();
           
        using (var context = new PSDbContext())  
        {
            var employees = await context.Users.Where(u => u.Brand_Id == UserSession.Session_Brand_Id && u.Role_Id == 3).Select(u => new { u.User_Id, u.Username, u.Performance }).ToListAsync();
            var shiftLastUpdate = await context.ShiftChoises.Where(u => u.Brand_Id == UserSession.Session_Brand_Id).Select(u => u.Last_Update).ToListAsync();
            
            for (int i = 0; i < employees.Count; i++)
            {
                Label label = new Label();
                label.Text = employees[i].Username + ": " + shiftLastUpdate[i];
                labellist.Add(label);
            }

            return labellist;
        }
    }
}
