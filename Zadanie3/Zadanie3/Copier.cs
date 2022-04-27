using System;

namespace Zadanie3
{
    public class Copier : BaseDevice, ICopier
    {
        public new int Counter { get; private set; } = 0;

        public Printer Printer { get; private set; }
        public Scanner Scanner { get; private set; }
        public int PrintCounter { get => Printer.Counter; }
        public int ScanCounter { get => Scanner.Counter; }
        public Copier()
        {
            Printer = new Printer();
            Scanner = new Scanner();
        }

        public new void PowerOn()
        {
            if (GetState() == IDevice.State.off)
            {
                state = IDevice.State.on;
                Printer.PowerOn();
                Scanner.PowerOn();
                Counter++;
            }
        }

        public new void PowerOff()
        {
            state = IDevice.State.off;
            Printer.PowerOff();
            Scanner.PowerOff();
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
