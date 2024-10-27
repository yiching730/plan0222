using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace plan02.Models
{
    public partial class PunchIn
    {
        public uint Pid { get; set; }
        [Display(Name = "年")]
        public int? Year { get; set; }
        [Display(Name = "月")]
        public int? Month { get; set; }
        [Display(Name = "日")]
        [Range(1,31,ErrorMessage ="日期範圍為1至31日")]
        public int? Day { get; set; }
        [Display(Name = "上班時間")]
        [DataType(DataType.Time)]
        public DateTime? PunchIn1 { get; set; }
        [Display(Name = "下班時間")]
        [DataType(DataType.Time)]
        public DateTime? PunchOut { get; set; }
        [Display(Name = "請假備註")]
        public string Remark2 { get; set; }
        [Display(Name = "星期")]
        public string Weekday { get; set; }
    }
}

//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;

//#nullable disable

//namespace plan02.Models
//{
//    public partial class PunchIn
//    {
//        public uint Pid { get; set; }
//        public int? Year { get; set; }
//        public int? Month { get; set; }
//        [Range(1, 31, ErrorMessage = "日期必須介於1日至31日")]
//        public int? Day { get; set; }
//        [Display(Name = "上班時間")]
//        [DataType(DataType.Time)]
//        public DateTime? PunchIn1 { get; set; }
//        [Display(Name = "下班時間")]
//        [DataType(DataType.Time)]
//        public DateTime? PunchOut { get; set; }
//        [Display(Name = "請假備註")]
//        public string Remark2 { get; set; }
//    }
//}


//using System;
//using System.Collections.Generic;

//#nullable disable

//namespace plan02.Models
//{
//    public partial class PunchIn
//    {
//        public uint Pid { get; set; }
//        public int? Year { get; set; }
//        public int? Month { get; set; }
//        public int? Day { get; set; }
//        public DateTime? PunchIn1 { get; set; }
//        public DateTime? PunchOut { get; set; }
//        public string Remark2 { get; set; }
//    }
//}
