using iTextSharp.text.pdf;
using System.IO;

namespace PdfFromTemplate
{
    public class Test
    {
        public static void ReplacePdfForm()
        {
            string fileNameExisting = @"C:\path\to\existing.pdf";
            string fileNameNew = @"C:\path\to\new.pdf";

            using (var existingFileStream = new FileStream(fileNameExisting, FileMode.Open))
            using (var newFileStream = new FileStream(fileNameNew, FileMode.Create))
            {
                // Open existing PDF
                var pdfReader = new PdfReader(existingFileStream);

                // PdfStamper, which will create
                var stamper = new PdfStamper(pdfReader, newFileStream);

                var form = stamper.AcroFields;
                var fieldKeys = form.Fields.Keys;

                foreach (string fieldKey in fieldKeys)
                {
                    form.SetField(fieldKey, "REPLACED!");
                }

                // "Flatten" the form so it wont be editable/usable anymore
                stamper.FormFlattening = true;

                // You can also specify fields to be flattened, which
                // leaves the rest of the form still be editable/usable
                stamper.PartialFormFlattening("field1");

                stamper.Close();
                pdfReader.Close();
            }
        }
    }
}
