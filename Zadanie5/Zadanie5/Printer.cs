using System;
using System.Threading;

namespace Zadanie5
{
    public class Printer : IPrinter
    {
        public int Counter { get; private set; } = 0;
        public IDevice.State GetState() => IPrinter.PrinterState;
        IDevice.State IDevice.GetState() => IPrinter.PrinterState;
        public void SetState(IDevice.State state) => IPrinter.PrinterState = state;
        void IDevice.SetState(IDevice.State state) => IPrinter.PrinterState = state;

        public void Print(in IDocument document)
        {
            if (IPrinter.PrinterState == IDevice.State.off)
            {
                return;
            }

            IPrinter.PrinterState = IDevice.State.on;

            if (Counter > 0 && Counter % 3 == 0)
            {
                Console.WriteLine("Printer is cooling down for 1 sec");
                Thread.Sleep(1000);
            }

            Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");

            Counter++;
            IPrinter.PrinterState = IDevice.State.standby;
        }

    }
}
