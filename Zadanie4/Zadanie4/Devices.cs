using System;

namespace Zadanie4
{
    public interface IDevice
    {
        enum State { on, off, standby };

        void PowerOn() => SetState(State.on);
        void PowerOff() => SetState(State.off);
        void StandbyOn() => SetState(State.standby);
        void StandbyOff() => SetState(State.on);
        State GetState();

        abstract protected void SetState(State state);

        int Counter { get; }
    }

    public interface IPrinter : IDevice
    {
        public static IDevice.State PrinterState = IDevice.State.off;
        public static int PrintCounter;
        public new State GetState() => PrinterState;
        public new State SetState(IDevice.State state) => PrinterState = state;

        /// <summary>
        /// Dokument jest drukowany, jeśli urządzenie włączone. W przeciwnym przypadku nic się nie wykonuje
        /// </summary>
        /// <param name="document">obiekt typu IDocument, różny od `null`</param>
        void Print(in IDocument document);
    }

    public interface IScanner : IDevice
    {
        public static IDevice.State ScannerState = IDevice.State.off;
        public static int ScanCounter;
        public new State GetState() => ScannerState;
        public new State SetState(IDevice.State state) => ScannerState = state;

        // dokument jest skanowany, jeśli urządzenie włączone
        // w przeciwnym przypadku nic się dzieje
        void Scan(out IDocument document, IDocument.FormatType formatType);
    }
}
