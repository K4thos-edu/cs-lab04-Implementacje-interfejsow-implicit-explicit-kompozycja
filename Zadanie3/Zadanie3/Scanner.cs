using System;

namespace Zadanie3
{
    public class Scanner : BaseDevice, IScanner
    {
        public new int Counter { get; private set; } = 0;

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            if (GetState() == IDevice.State.off)
            {
                document = null;
                return;
            }

            Counter++;

            switch (formatType)
            {
                case IDocument.FormatType.JPG:
                    document = new ImageDocument($"ImageScan{Counter}.jpg");
                    break;
                case IDocument.FormatType.PDF:
                    document = new ImageDocument($"PDFScan{Counter}.pdf");
                    break;
                case IDocument.FormatType.TXT:
                    document = new ImageDocument($"TextScan{Counter}.txt");
                    break;
                default:
                    throw new ArgumentException("File type has not been defined");
            }

            Console.WriteLine($"{DateTime.Now} Scan: {document.GetFileName()}");
        }
    }

}
