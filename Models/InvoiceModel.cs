using GemBox.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace plan02.Models
{
    public class InvoiceModel
    {
        public int Number { get; set; } = 1;
        public DateTime Date { get; set; } = DateTime.Today;
        public string Company { get; set; } = "請填單位";
        public string Address { get; set; } = "請填計畫人員類別";
        public string Name { get; set; } = "請填姓名";
        public string Format { get; set; } = "DOCX";
        public SaveOptions Options => this.FormatMappingDictionary[this.Format];
        public IDictionary<string, SaveOptions> FormatMappingDictionary => new Dictionary<string, SaveOptions>()
        {
            ["DOCX"] = new DocxSaveOptions(),
            ["HTML"] = new HtmlSaveOptions() { EmbedImages = true },
            ["RTF"] = new RtfSaveOptions(),
            ["TXT"] = new TxtSaveOptions(),
            ["PDF"] = new PdfSaveOptions(),
            ["XPS"] = new XpsSaveOptions(),
            ["XML"] = new XmlSaveOptions(),
            ["BMP"] = new ImageSaveOptions(ImageSaveFormat.Bmp),
            ["PNG"] = new ImageSaveOptions(ImageSaveFormat.Png),
            ["JPG"] = new ImageSaveOptions(ImageSaveFormat.Jpeg),
            ["GIF"] = new ImageSaveOptions(ImageSaveFormat.Gif),
            ["TIF"] = new ImageSaveOptions(ImageSaveFormat.Tiff)
        };

    }
}
