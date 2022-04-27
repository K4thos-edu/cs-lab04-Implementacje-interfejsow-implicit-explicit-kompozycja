using System;

namespace Zadanie1
{
    public class Copier : BaseDevice, IPrinter, IScanner
    {
        public int PrintCounter { get; private set; } = 0;
        public int ScanCounter { get; private set; } = 0;
        public new int Counter { get; private set; } = 0;

        public new void PowerOn()
        {
            if (GetState() == IDevice.State.off)
            {
                Counter++;
                base.PowerOn();
            }
        }

        public new void PowerOff()
        {
            if (GetState() == IDevice.State.on)
            {
                base.PowerOff();
            }
        }

        public void Print(in IDocument document)
        {
            if (GetState() == IDevice.State.on)
            {
                Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
                PrintCounter++;
            }
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            if (GetState() == IDevice.State.off)
            {
                document = null;
                return;
            }

            ScanCounter++;

            switch (formatType)
            {
                case IDocument.FormatType.JPG:
                    document = new ImageDocument($"ImageScan{ScanCounter}.jpg");
                    break;
                case IDocument.FormatType.PDF:
                    document = new ImageDocument($"PDFScan{ScanCounter}.pdf");
                    break;
                case IDocument.FormatType.TXT:
                    document = new ImageDocument($"TextScan{ScanCounter}.txt");
                    break;
                default:
                    throw new ArgumentException("File type has not been defined");
            }

            Console.WriteLine($"{DateTime.Now} Scan: {document.GetFileName()}");
        }

        public void ScanAndPrint()
        {
            if (state == IDevice.State.on)
            {
                IDocument document;
                Scan(out document, IDocument.FormatType.JPG);
                Print(document);
            }
        }
    }
}
