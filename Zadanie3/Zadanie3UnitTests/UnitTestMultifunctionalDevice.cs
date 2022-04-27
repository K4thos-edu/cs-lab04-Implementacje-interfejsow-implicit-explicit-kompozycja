using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zadanie3;
using System;

namespace Zadanie3UnitTests
{
    [TestClass]
    public class UnitTestMultifunctionalDevice
    {
        [TestMethod]
        public void MultifunctionalDevice_GetState_StateOff()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOff();

            Assert.AreEqual(IDevice.State.off, multifunctionalDevice.GetState());
        }

        [TestMethod]
        public void MultifunctionalDevice_GetState_StateOn()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            Assert.AreEqual(IDevice.State.on, multifunctionalDevice.GetState());
        }


        // weryfikacja, czy po wywołaniu metody `Print` i włączonej kopiarce w napisie pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Print_DeviceOn()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multifunctionalDevice.Print(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Print` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Print_DeviceOff()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multifunctionalDevice.Print(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Scan_DeviceOff()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                multifunctionalDevice.Scan(out doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Scan_DeviceOn()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                multifunctionalDevice.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy wywołanie metody `Scan` z parametrem określającym format dokumentu
        // zawiera odpowiednie rozszerzenie (`.jpg`, `.txt`, `.pdf`)
        [TestMethod]
        public void MultifunctionalDevice_Scan_FormatTypeDocument()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                multifunctionalDevice.Scan(out doc1, formatType: IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                multifunctionalDevice.Scan(out doc1, formatType: IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                multifunctionalDevice.Scan(out doc1, formatType: IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }


        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie pojawiają się słowa `Print`
        // oraz `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_ScanAndPrint_DeviceOn()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            IDocument doc = new PDFDocument("doc.pdf");

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multifunctionalDevice.Send(in doc);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Send fax"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("doc.pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // ani słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_ScanAndPrint_DeviceOff()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOff();

            IDocument doc = new PDFDocument("doc.pdf");

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multifunctionalDevice.Send(in doc);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Send fax"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("doc.pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_PrintCounter()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            multifunctionalDevice.Print(in doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            multifunctionalDevice.Print(in doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            multifunctionalDevice.Print(in doc3);

            IDocument doc4 = new PDFDocument("doc.pdf");

            multifunctionalDevice.PowerOff();
            multifunctionalDevice.Print(in doc3);
            multifunctionalDevice.Scan(out doc1);
            multifunctionalDevice.PowerOn();

            multifunctionalDevice.Send(in doc4);
            multifunctionalDevice.Send(in doc4);

            // 3 wydruków, gdy urządzenie włączone
            Assert.AreEqual(3, multifunctionalDevice.PrintCounter);
        }

        [TestMethod]
        public void MultifunctionalDevice_ScanCounter()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            IDocument doc1;
            multifunctionalDevice.Scan(out doc1);
            IDocument doc2;
            multifunctionalDevice.Scan(out doc2);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            multifunctionalDevice.Print(in doc3);

            IDocument doc4 = new PDFDocument("doc.pdf");

            multifunctionalDevice.PowerOff();
            multifunctionalDevice.Print(in doc3);
            multifunctionalDevice.Scan(out doc1);
            multifunctionalDevice.PowerOn();

            multifunctionalDevice.Send(in doc4);
            multifunctionalDevice.Send(in doc4);

            // 2 skany, gdy urządzenie włączone
            Assert.AreEqual(2, multifunctionalDevice.ScanCounter);
        }

        [TestMethod]
        public void MultifunctionalDevice_PowerOnCounter()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();
            multifunctionalDevice.PowerOn();
            multifunctionalDevice.PowerOn();

            IDocument doc1;
            multifunctionalDevice.Scan(out doc1);
            IDocument doc2;
            multifunctionalDevice.Scan(out doc2);

            multifunctionalDevice.PowerOff();
            multifunctionalDevice.PowerOff();
            multifunctionalDevice.PowerOff();
            multifunctionalDevice.PowerOn();

            IDocument doc3 = new ImageDocument("aaa.jpg");
            multifunctionalDevice.Print(in doc3);

            multifunctionalDevice.PowerOff();
            multifunctionalDevice.Print(in doc3);
            multifunctionalDevice.Scan(out doc1);
            multifunctionalDevice.PowerOn();

            //multifunctionalDevice.ScanAndPrint();
            //multifunctionalDevice.ScanAndPrint();

            // 3 włączenia
            Assert.AreEqual(3, multifunctionalDevice.Counter);
        }

        [TestMethod]
        public void MultifunctionalDevice_SendCounter()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            IDocument doc1;
            multifunctionalDevice.Scan(out doc1);
            IDocument doc2;
            multifunctionalDevice.Scan(out doc2);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            multifunctionalDevice.Print(in doc3);

            IDocument doc4 = new PDFDocument("doc.pdf");

            multifunctionalDevice.PowerOff();
            multifunctionalDevice.Print(in doc3);
            multifunctionalDevice.Scan(out doc1);
            multifunctionalDevice.PowerOn();

            multifunctionalDevice.Send(in doc4);
            multifunctionalDevice.Send(in doc4);
            multifunctionalDevice.PowerOff();
            multifunctionalDevice.Send(in doc4);
            multifunctionalDevice.Send(in doc4);
            multifunctionalDevice.PowerOn();

            // 2, gdy urządzenie włączone
            Assert.AreEqual(2, multifunctionalDevice.SendCounter);
        }
    }
}
