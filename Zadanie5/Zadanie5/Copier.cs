using System;

namespace Zadanie5
{
    public class Copier : IPrinter, IScanner
    {
        public int Counter { get; private set; } = 0;

        public Printer Printer { get; private set; }
        public Scanner Scanner { get; private set; }
        public int PrintCounter { get => Printer.Counter; }
        public int ScanCounter { get => Scanner.Counter; }
        public Copier()
        {
            Printer = new Printer();
            Scanner = new Scanner();
        }

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
            Printer.Print(document);
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            Scanner.Scan(out document, formatType);
        }

        public void ScanAndPrint()
        {
            Scan(out IDocument document, IDocument.FormatType.JPG);
            Print(document);
        }
    }
}
