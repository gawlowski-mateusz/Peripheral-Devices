using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using PCSC;

namespace ConsoleApplication1
{
    class Program
    {
        private static SCardError error;
        private static SCardReader reader;
        private static System.IntPtr intptr;
        private static SCardContext hContext;
        static void Main(string[] args)
        {
            byte[] commandB;
            try
            {
                connect();

                // get in TELECOM
                commandB = new byte[] { 0xA0, 0xA4, 0x00, 0x00, 0x02, 0x7F, 0x10 }; // adress constructed with 2 chars 107F
                sendCommand(commandB, "SELECT(TELECOM)");

                commandB = new byte[] { 0xA0, 0xC0, 0x00, 0x00, 0x16 }; // wait for response with length 22(10)
                sendCommand(commandB, "\nGET RESPONSE");

                // get in SMS  
                commandB = new byte[] { 0xA0, 0xA4, 0x00, 0x00, 0x02, 0x6F, 0x3C };
                sendCommand(commandB, "\nSELECT SMS");
                commandB = new byte[] { 0xA0, 0xC0, 0x00, 0x00, 0x16 };

                // SMS read
                commandB = new byte[] { 0xA0, 0xB2, 0x01, 0x04, 0xB0 };
                sendCommand(commandB, "\nREAD RECORD");
                commandB = new byte[] { 0xA0, 0xC0, 0x00, 0x00, 0x16 };
                sendCommand(commandB, "\nGET RESPONSE");

                Console.WriteLine("\nPress any buton to continue...\n");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occured");
                Console.ReadKey();
            }
        }

        public static void toTelecom()
        {
             byte[] commandBytes = new byte[] {0xA0, 0xA4, 0x00, 0x00, 0x02, 0x7F, 0x10};
                sendCommand(commandBytes, "SELECT TELECOM");

                // GET RESPONSE
                commandBytes = new byte[] {0xA0, 0xC0, 0x00, 0x00, 0x16};
                sendCommand(commandBytes, "GET RESPONSE");

                // SELECT SMS
                commandBytes = new byte[] {0xA0, 0xA4, 0x00, 0x00, 0x02, 0x6F, 0x3C};
                sendCommand(commandBytes, "SELECT SMS");

                // GET RESPONSE
                commandBytes = new byte[] {0xA0, 0xC0, 0x00, 0x00, 0x0F};
                sendCommand(commandBytes, "GET RESPONSE");
        }

        public static void connect()
        {
            hContext = new SCardContext();
            hContext.Establish(SCardScope.System);

            string[] readerList = hContext.GetReaders(); // Load avaliable readers
            Boolean noReaders = readerList.Length <= 0;

            if (noReaders)
            {
                throw new PCSCException(SCardError.NoReadersAvailable, "Reader was not found"); 
            }

            int counter = 1;
            Console.WriteLine("Choose reader: ");

            foreach (string element in readerList)
            {
                Console.WriteLine("F" + counter + " -> " + element);
                counter++;
            }

            var input = Console.ReadKey();
            string tmp = readerList[0];

            switch (input.Key)
            {

                case ConsoleKey.F1:
                    tmp = readerList[0];
                    break;

                case ConsoleKey.F2:
                    tmp = readerList[1];
                    break;
            }

            Console.WriteLine("\nPress any button to continue...\n");
            Console.ReadKey();

            reader = new SCardReader(hContext);

            error = reader.Connect(tmp, SCardShareMode.Shared, SCardProtocol.T0 | SCardProtocol.T1);
            checkError(error);

            if (reader.ActiveProtocol == SCardProtocol.T0)
            {
                intptr = SCardPCI.T0;

            }
            else if (reader.ActiveProtocol == SCardProtocol.T1)
            {
                intptr = SCardPCI.T1;
            }
            else
            {
                Console.WriteLine("Protocol is not avaliable");
                Console.WriteLine("\nPress any button to continue...\n");
                Console.ReadKey();
            }
        }

        public static void sendCommand(byte[] command, String name) // send command to card
        {
            byte[] recivedBytes = new byte[256];
            error = reader.Transmit(intptr, command, ref recivedBytes);
            checkError(error);
            writeResponse(recivedBytes, name);
        }

        public static void writeResponse(byte[] recivedBytes, String responseCode) // read response from card
        {
            Console.Write(responseCode + ": ");

            for (int i = 0; i < recivedBytes.Length; i++)
                Console.Write("{0:X2} ", recivedBytes[i]); // print binary answer

            Console.WriteLine();
        }

        static void checkError(SCardError error) // check if card is inserted
        {
            if (error != SCardError.Success)
            {
                throw new PCSCException(error, SCardHelper.StringifyError(error));
            }
        }

    }
}
    