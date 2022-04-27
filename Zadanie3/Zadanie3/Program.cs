using System;

namespace Zadanie3
{
    class Program
    {
        static void Main()
        {
            var xerox = new Copier();
            xerox.PowerOn();
            IDocument doc1 = new PDFDocument("aaa.pdf");
            xerox.Print(in doc1);

            IDocument doc2;
            xerox.Scan(out doc2);

            xerox.ScanAndPrint();
            System.Console.WriteLine(xerox.Counter);
            System.Console.WriteLine(xerox.PrintCounter);
            System.Console.WriteLine(xerox.ScanCounter);

            var multifunctional = new MultifunctionalDevice();
            multifunctional.PowerOn();
            IDocument doc3 = new PDFDocument("aaa.pdf");
            multifunctional.Print(in doc3);

            IDocument doc4;
            multifunctional.Scan(out doc4);

            multifunctional.Send(doc1);
            System.Console.WriteLine(multifunctional.Counter);
            System.Console.WriteLine(multifunctional.PrintCounter);
            System.Console.WriteLine(multifunctional.ScanCounter);
            System.Console.WriteLine(multifunctional.SendCounter);
        }
    }
}
