using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using System.Collections.Generic;
using System.IO;
using WHMSData.Models;

namespace WHMS.Utils
{
    public class PDFExporter : IPDFExporter
    {
        private string path;
        private string fileName;

        public PDFExporter()
        {
            this.path = @"./../../../../../PDF Exports/";

        }

        public void Export(Order obj, string pdfName)
        {
            this.fileName = pdfName + ".pdf";
            var doc = this.CreateDocument(obj);
            this.DefineStyles(doc);
            this.AddHeader(doc);
            this.FillContent(doc, obj);
            var pdf = this.Render(doc);
            this.Save(pdf);
        }

        private Document CreateDocument(Order obj)
        {
            var document = new Document();
            document.Info.Title = "Warehouse Management System 2019";
            document.Info.Subject = $"Order: {obj.GetType().Name}";
            document.Info.Author = "TEAM X";

            return document;
        }

        private void DefineStyles(Document doc)
        {
            doc.Styles[StyleNames.Normal].Font.Name = "Verdana";
            doc.Styles[StyleNames.Normal].Font.Size = 10;

            doc.Styles[StyleNames.Header].Font.Size = 16;
            doc.Styles[StyleNames.Header].ParagraphFormat.Alignment = ParagraphAlignment.Center;
        }

        private void AddHeader(Document doc)
        {
            var section = doc.AddSection();

            var paragraph = section.Headers.Primary.AddParagraph();

            var img = paragraph.AddImage(@"./../../../Utils/order-stamp.jpg");
            img.LockAspectRatio = true;
            img.Height = 100;
            img.RelativeVertical = RelativeVertical.Line;
            img.RelativeHorizontal = RelativeHorizontal.Column;
            img.Top = ShapePosition.Top;
            img.Left = ShapePosition.Left;

            paragraph.AddText("TEAM X Software");
        }

        private void FillContent(Document doc, Order obj)
        {
            var section = doc.Sections[0];

            var paragraph = section.AddParagraph(obj.GetType().Name);
            paragraph.Format.SpaceBefore = 150;
            paragraph.Format.SpaceAfter = 10;
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;

            FillProperties(doc, obj);
        }

        private void FillProperties(Document doc, Order obj)
        {
            var section = doc.Sections[0];
            Paragraph paragraph;

            if (obj.GetType().GetInterface(nameof(ICollection<object>)) != null)
            {
                foreach (var item in (ICollection<Order>)obj)
                {
                    this.FillProperties(doc, item);
                }
            }
            else
            {
                var sample = new Order();
                foreach (var property in obj.GetType().GetProperties())
                {

                    if (property.Name == "Products")
                    {
                        paragraph = section.AddParagraph();
                        paragraph.Format.AddTabStop(100);
                        paragraph.AddFormattedText(property.Name, TextFormat.Underline);
                        paragraph.AddText(":");

                        foreach (var item in obj.ProductsWarehouses)
                        {
                            
                            paragraph = section.AddParagraph();
                            paragraph.Format.AddTabStop(100);
                            paragraph.AddTab();
                            paragraph.AddFormattedText(item.Product.Name.ToString());
                        }
                    }
                    else
                    {
                        paragraph = section.AddParagraph();
                        paragraph.Format.AddTabStop(100);
                        paragraph.AddFormattedText(property.Name, TextFormat.Underline);
                        paragraph.AddText(":");
                        paragraph.AddTab();
                        paragraph.AddFormattedText(property.GetValue(obj).ToString());
                    }

                }
            }
        }

        private PdfDocument Render(Document doc)
        {
            var renderer = new PdfDocumentRenderer();
            renderer.Document = doc;
            renderer.RenderDocument();
            return renderer.PdfDocument;
        }

        private void Save(PdfDocument doc)
        {
            if (!Directory.Exists(this.path))
            {
                Directory.CreateDirectory(this.path);
            }

            doc.Save(this.path + this.fileName);
        }
    }
}
