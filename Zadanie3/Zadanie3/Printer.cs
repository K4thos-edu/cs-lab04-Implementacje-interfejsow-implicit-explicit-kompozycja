using System;

namespace Zadanie3
{
    public class Printer : BaseDevice, IPrinter
    {
        public new int Counter { get; private set; } = 0;

        public void Print(in IDocument document)
        {
            if (GetState() == IDevice.State.on)
            {
                Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
                Counter++;
            }
        }
    }
}
