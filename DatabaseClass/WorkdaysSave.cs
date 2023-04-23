public class WorkdaysSave
{
    private readonly PSDbContext _context;

    public WorkdaysSave(PSDbContext context)
    {
        _context = context;
    }

    public bool BrandInfoUpdate(int[] shiftCount, int[] mondayShiftWorker, int[] tuesdayShiftWorker, int[] wednesdayShiftWorker, int[] thursdayShiftWorker, int[] fridayShiftWorker, int[] saturdayShiftWorker, int[] sundayShiftWorker)
    {
        var brand = _context.Brands.FirstOrDefault(b => b.Brand_Id == UserSession.Session_Brand_Id);
        if (brand != null)
        {
            brand.Monday = shiftCount[0];
            brand.Monday1_Shift_Count = mondayShiftWorker[0];
            brand.Monday2_Shift_Count = mondayShiftWorker[1];
            brand.Monday3_Shift_Count = mondayShiftWorker[2];
            //---------------------------------------
            brand.Tuesday = shiftCount[1];
            brand.Tuesday1_Shift_Count = tuesdayShiftWorker[0];
            brand.Tuesday2_Shift_Count = tuesdayShiftWorker[1];
            brand.Tuesday3_Shift_Count = tuesdayShiftWorker[2];
            //---------------------------------------
            brand.Wednesday = shiftCount[2];
            brand.Wednesday1_Shift_Count = wednesdayShiftWorker[0];
            brand.Wednesday2_Shift_Count = wednesdayShiftWorker[1];
            brand.Wednesday3_Shift_Count = wednesdayShiftWorker[2];
            //---------------------------------------
            brand.Thursday = shiftCount[3];
            brand.Thursday1_Shift_Count = thursdayShiftWorker[0];
            brand.Thursday2_Shift_Count = thursdayShiftWorker[1];
            brand.Thursday3_Shift_Count = thursdayShiftWorker[2];
            //---------------------------------------
            brand.Friday = shiftCount[4];
            brand.Friday1_Shift_Count = fridayShiftWorker[0];
            brand.Friday2_Shift_Count = fridayShiftWorker[1];
            brand.Friday3_Shift_Count = fridayShiftWorker[2];
            //---------------------------------------
            brand.Saturday = shiftCount[5];
            brand.Saturday1_Shift_Count = saturdayShiftWorker[0];
            brand.Saturday2_Shift_Count = saturdayShiftWorker[1];
            brand.Saturday3_Shift_Count = saturdayShiftWorker[2];
            //---------------------------------------
            brand.Sunday = shiftCount[6];
            brand.Sunday1_Shift_Count = saturdayShiftWorker[0];
            brand.Sunday2_Shift_Count = saturdayShiftWorker[1];
            brand.Sunday3_Shift_Count = saturdayShiftWorker[2];

            _context.SaveChanges();
            return true;
        }
        else return false;
    }
}