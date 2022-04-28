using System;
using System.Threading;

namespace Zadanie5
{
    public class Scanner : IScanner
    {
        public int Counter { get; private set; } = 0;
        public IDevice.State GetState() => IScanner.ScannerState;
        IDevice.State IDevice.GetState() => IScanner.ScannerState;
        public void SetState(IDevice.State state) => IScanner.ScannerState = state;
        void IDevice.SetState(IDevice.State state) => IScanner.ScannerState = state;

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            if (IScanner.ScannerState == IDevice.State.off)
            {
                document = null;
                return;
            }

            IScanner.ScannerState = IDevice.State.on;

            if (Counter > 0 && Counter % 2 == 0)
            {
                Console.WriteLine("Scanner is cooling down for 1 sec");
                Thread.Sleep(1000);
            }

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

            Counter++;

            IScanner.ScannerState = IDevice.State.standby;
        }
    }

}
