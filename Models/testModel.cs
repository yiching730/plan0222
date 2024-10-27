using GemBox.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace plan02.Models
{
    public class testModel
    {
        public string BName { get; set; } = "testname";
        public string Format { get; set; } = "DOCX";
        public SaveOptions Options => this.FormatMappingDictionary[this.Format];
        public IDictionary<string, SaveOptions> FormatMappingDictionary => new Dictionary<string, SaveOptions>()
        {
            ["DOCX"] = new DocxSaveOptions(),
            //["HTML"] = new HtmlSaveOptions() { EmbedImages = true },
            //["RTF"] = new RtfSaveOptions(),
            //["TXT"] = new TxtSaveOptions(),
            //["PDF"] = new PdfSaveOptions(),
            //["XPS"] = new XpsSaveOptions(),
            //["XML"] = new XmlSaveOptions(),
            //["BMP"] = new ImageSaveOptions(ImageSaveFormat.Bmp),
            //["PNG"] = new ImageSaveOptions(ImageSaveFormat.Png),
            //["JPG"] = new ImageSaveOptions(ImageSaveFormat.Jpeg),
            //["GIF"] = new ImageSaveOptions(ImageSaveFormat.Gif),
            //["TIF"] = new ImageSaveOptions(ImageSaveFormat.Tiff)
        };

    }
}
