using WHMSData.Models;

namespace WHMS.Utils
{
    public interface IPDFExporter
    {
        void Export(Order obj, string pdfName);
    }
}