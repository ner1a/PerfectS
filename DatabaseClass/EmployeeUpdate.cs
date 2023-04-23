namespace PerfectS.DatabaseClass
{
    public class EmployeeUpdate
    {
        private readonly PSDbContext _context;

        public EmployeeUpdate(PSDbContext context)
        {
            _context = context;
        }

        public List<Label> UpdateEmployeeInfos()
        {
            var employees = _context.Users.Where(u => u.Brand_Id == UserSession.Session_Brand_Id && u.Role_Id == 3).Select(u => new { u.User_Id, u.Username, u.Performance}).ToList();
            var shiftLastUpdate = _context.ShiftChoises.Where(u => u.Brand_Id == UserSession.Session_Brand_Id).Select(u => u.Last_Update).ToList();
            List<Label> labellist = new List<Label>();

            for (int i = 0; i < employees.Count; i++)
            {
                Label label = new Label();
                label.Text = employees[i].Username + ": " + shiftLastUpdate[i].Value;
                labellist.Add(label);
            }

            return labellist;
        }
    }
}
