using System;

namespace Zadanie4
{
    class Program
    {
        static void Main()
        {
            var xerox = new Copier();
            Console.WriteLine($"Copier state: {xerox.GetState()}");
            xerox.PowerOn();
            Console.WriteLine($"Copier state: {xerox.GetState()}");

            var doc = new PDFDocument("aaa.pdf");
            xerox.Print(doc);
            xerox.Print(doc);
            xerox.Print(doc);
            xerox.Print(doc);
            xerox.Print(doc);

            IDocument doc1;
            IDocument doc2;
            IDocument doc3;
            xerox.Scan(out doc1);
            xerox.Scan(out doc2);
            xerox.Scan(out doc3);

            Console.WriteLine($"Copier counter: {xerox.Counter}");
            Console.WriteLine($"Scan counter: {xerox.ScanCounter}");
            Console.WriteLine($"Print counter: {xerox.PrintCounter}");

            Console.WriteLine($"Copier state: {xerox.GetState()}");
            xerox.PowerOff();
            Console.WriteLine($"Copier state: {xerox.GetState()}");
        }
    }
}
