public class ShiftPageInformations
{
    private readonly PSDbContext _context;

    public ShiftPageInformations(PSDbContext context)
    {
        _context = context;
    }

    public int[] ShiftPageInfo()
    {
        var userBrandID = UserSession.Session_Brand_Id;

        var page = _context.Brands.FirstOrDefault(p => p.Brand_Id == userBrandID);

        if (page != null)
        {
            int[] workDays = new int[7];
            workDays[0] = page.Monday;
            workDays[1] = page.Tuesday;
            workDays[2] = page.Wednesday;
            workDays[3] = page.Thursday;
            workDays[4] = page.Friday;
            workDays[5] = page.Saturday;
            workDays[6] = page.Sunday;
            return workDays;
        }
        else { return new int[0]; }
    }
}