using System;
using System.Threading;

namespace Zadanie4
{
    public class Copier : IPrinter, IScanner
    {
        public int PrintCounter { get; private set; } = 0;
        public int ScanCounter { get; private set; } = 0;
        public int Counter { get; private set; } = 0;

        protected IDevice.State state = IDevice.State.off;

        public IDevice.State GetState()
        {
            if (state == IDevice.State.off)
            {
                return IDevice.State.off;
            }
            if (IPrinter.PrinterState == IDevice.State.standby && IScanner.ScannerState == IDevice.State.standby)
            {
                return IDevice.State.standby;
            }
            return IDevice.State.on;
        }

        void IDevice.SetState(IDevice.State state)
        {
            if (state == IDevice.State.on && this.state == IDevice.State.off)
            {
                Counter++;
            }
            this.state = state;
            IPrinter.PrinterState = state;
            IScanner.ScannerState = state;
        }

        public void StandbyOn()
        {
            this.state = IDevice.State.standby;
            IPrinter.PrinterState = IDevice.State.standby;
            IScanner.ScannerState = IDevice.State.standby;
        }

        public void StandbyOff()
        {
            this.state = IDevice.State.on;
            IPrinter.PrinterState = IDevice.State.on;
            IScanner.ScannerState = IDevice.State.on;
        }

        public void PowerOn()
        {
            if (GetState() == IDevice.State.off)
            {
                Counter++;
                this.state = IDevice.State.on;
                IPrinter.PrinterState = IDevice.State.on;
                IScanner.ScannerState = IDevice.State.on;
            }
        }

        public void PowerOff()
        {
            this.state = IDevice.State.off;
            IPrinter.PrinterState = IDevice.State.off;
            IScanner.ScannerState = IDevice.State.off;
        }

        public void Print(in IDocument document)
        {
            if (IPrinter.PrinterState == IDevice.State.off)
            {
                return;
            }

            IPrinter.PrinterState = IDevice.State.on;

            if (PrintCounter > 0 && PrintCounter % 3 == 0)
            {
                Console.WriteLine("Printer is cooling down for 1 sec");
                Thread.Sleep(1000);
            }

            Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");

            PrintCounter++;
            IPrinter.PrinterState = IDevice.State.standby;
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            if (IScanner.ScannerState == IDevice.State.off)
            {
                document = null;
                return;
            }

            IScanner.ScannerState = IDevice.State.on;

            if (ScanCounter > 0 && ScanCounter % 2 == 0)
            {
                Console.WriteLine("Scanner is cooling down for 1 sec");
                Thread.Sleep(1000);
            }

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

            ScanCounter++;

            IScanner.ScannerState = IDevice.State.standby;
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
