using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PerfectS
{
    public class Brand
    {
        [Key]
        public int Brand_Id { get; set; }
        public string Brand_Name { get; set; }
        //-----------------------------------------
        public int Monday { get; set; }
        public int Monday1_Shift_Count { get; set; }
        public int Monday2_Shift_Count { get; set; }
        public int Monday3_Shift_Count { get; set; }
        //-----------------------------------------
        public int Tuesday { get; set; }
        public int Tuesday1_Shift_Count { get; set; }
        public int Tuesday2_Shift_Count { get; set; }
        public int Tuesday3_Shift_Count { get; set; }
        //-----------------------------------------
        public int Wednesday { get; set; }
        public int Wednesday1_Shift_Count { get; set; }
        public int Wednesday2_Shift_Count { get; set; }
        public int Wednesday3_Shift_Count { get; set; }
        //-----------------------------------------
        public int Thursday { get; set; }
        public int Thursday1_Shift_Count { get; set; }
        public int Thursday2_Shift_Count { get; set; }
        public int Thursday3_Shift_Count { get; set; }
        //-----------------------------------------
        public int Friday { get; set; }
        public int Friday1_Shift_Count { get; set; }
        public int Friday2_Shift_Count { get; set; }
        public int Friday3_Shift_Count { get; set; }
        //-----------------------------------------
        public int Saturday { get; set; }
        public int Saturday1_Shift_Count { get; set; }
        public int Saturday2_Shift_Count { get; set; }
        public int Saturday3_Shift_Count { get; set; }
        //-----------------------------------------
        public int Sunday { get; set; }
        public int Sunday1_Shift_Count { get; set; }
        public int Sunday2_Shift_Count { get; set; }
        public int Sunday3_Shift_Count { get; set; }
    }
}
