using Microsoft.EntityFrameworkCore;

namespace PerfectS
{
    [PrimaryKey(nameof(Brand_Id), nameof(User_Id))]
    public class ShiftChoise
    {
        public int Brand_Id { get; set; }
        public int User_Id { get; set; }
        public DateTime? Last_Update { get; set; }
        //-----------------------------------------
        public int? Monday_1 { get; set; }
        public int? Monday_2 { get; set; }
        public int? Monday_3 { get; set; }
        //-----------------------------------------
        public int? Tuesday_1 { get; set; }
        public int? Tuesday_2 { get; set; }
        public int? Tuesday_3 { get; set; }
        //-----------------------------------------
        public int? Wednesday_1 { get; set; }
        public int? Wednesday_2 { get; set; }
        public int? Wednesday_3 { get; set; }
        //-----------------------------------------
        public int? Thursday_1 { get; set; }
        public int? Thursday_2 { get; set; }
        public int? Thursday_3 { get; set; }
        //-----------------------------------------
        public int? Friday_1 { get; set; }
        public int? Friday_2 { get; set; }
        public int? Friday_3 { get; set; }
        //-----------------------------------------
        public int? Saturday_1 { get; set; }
        public int? Saturday_2 { get; set; }
        public int? Saturday_3 { get; set; }
        //-----------------------------------------
        public int? Sunday_1 { get; set; }
        public int? Sunday_2 { get; set; }
        public int? Sunday_3 { get; set; }
    }
}
