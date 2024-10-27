using GemBox.Document;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace plan02.Models
{
    public partial class BEmployee
    {
        public uint BId { get; set; }
        [Required(ErrorMessage = "請輸入帳號")]
        [Display(Name = "帳號")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "請輸入姓名")]
        [Display(Name = "姓名")]
        public string BName { get; set; }
        [Required(ErrorMessage = "請輸入助理人員類別")]
        [Display(Name = "助理人員類別")]
        public string BType { get; set; }
        [Required(ErrorMessage = "請輸入性別")]
        [Display(Name = "性別")]
        public string BGender { get; set; }
        [Required(ErrorMessage = "請輸入信箱")]
        [Display(Name = "信箱")]
        public string BMail { get; set; }
        [Required(ErrorMessage = "請輸入薪資")]
        [Display(Name = "薪資")]
        public int? BSalary { get; set; }
        [Required(ErrorMessage = "請輸入起聘日期")]
        [DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "起聘日")]
        public DateTime? BEmploymentDate { get; set; }
        [Required(ErrorMessage = "請輸入聘用結束日")]
        [DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "聘用結束日")]
        public DateTime? BEmploymentDate2 { get; set; }
        [Display(Name = "創建時間")]
        public DateTime? BCreatetime { get; set; }


        //public string Format { get; set; } = "DOCX";
        //public SaveOptions Options => this.FormatMappingDictionary[this.Format];
        //public IDictionary<string, SaveOptions> FormatMappingDictionary => new Dictionary<string, SaveOptions>()
        //{
        //    ["DOCX"] = new DocxSaveOptions(),
        //    ["HTML"] = new HtmlSaveOptions() { EmbedImages = true },
        //    ["RTF"] = new RtfSaveOptions(),
        //    ["TXT"] = new TxtSaveOptions(),
        //    ["PDF"] = new PdfSaveOptions(),
        //    ["XPS"] = new XpsSaveOptions(),
        //    ["XML"] = new XmlSaveOptions(),
        //    ["BMP"] = new ImageSaveOptions(ImageSaveFormat.Bmp),
        //    ["PNG"] = new ImageSaveOptions(ImageSaveFormat.Png),
        //    ["JPG"] = new ImageSaveOptions(ImageSaveFormat.Jpeg),
        //    ["GIF"] = new ImageSaveOptions(ImageSaveFormat.Gif),
        //    ["TIF"] = new ImageSaveOptions(ImageSaveFormat.Tiff)
        //};

    }
}



//using System;
//using System.Collections.Generic;

//#nullable disable

//namespace plan02.Models
//{
//    public partial class BEmployee
//    {
//        public uint BId { get; set; }
//        public string UserName { get; set; }
//        public string BName { get; set; }
//        public string BType { get; set; }
//        public string BGender { get; set; }
//        public string BMail { get; set; }
//        public int? BSalary { get; set; }
//        public DateTime? BEmploymentDate { get; set; }
//        public DateTime? BCreatetime { get; set; }
//    }
//}
