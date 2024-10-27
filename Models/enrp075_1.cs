using GemBox.Document;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace plan02.Models
{
    public class enrp075_1
    {
        public string pkno { get; set; }

        [DisplayName("休學原因")]
        public string resons { get; set; }
        [DisplayName("學號")]
        public string STNO1 { get; set; }
        [DisplayName("核准日期")]
        public string APP_DATE1 { get; set; }
        [DisplayName("姓名")]
        public string CH_NAME1 { get; set; }
        [DisplayName("系所")]
        public string TEACH_GROUP_NAME1 { get; set; }
        [DisplayName("年級")]
        public string GRADE1 { get; set; }
        [DisplayName("班級")]
        public string CLASSNO1 { get; set; }
        [DisplayName("休學年度起")]
        public string SUSPEND_DROPOUT_S_AYEAR1 { get; set; }
        [DisplayName("休學學期")]
        public string SUSPEND_DROPOUT_S_SMS1 { get; set; }
        [DisplayName("休學原因")]
        public string SUSPEND_DROPOUT_REASON1 { get; set; }
        [DisplayName("休學學期數")]
        public string SUSPEND_DROPOUT_start1 { get; set; }
        [DisplayName("休學訖日")]
        public string SUSPEND_DROPOUT_date1 { get; set; }
        [DisplayName("字號")]
        public string SUSPEND_DROPOUT_word1 { get; set; }
        public string Format { get; set; } = "DOCX";
        public SaveOptions Options => this.FormatMappingDictionary[this.Format];
        public IDictionary<string, SaveOptions> FormatMappingDictionary => new Dictionary<string, SaveOptions>()
        {
            ["DOCX"] = new DocxSaveOptions(),
            ["PDF"] = new PdfSaveOptions()
        };

}
}
