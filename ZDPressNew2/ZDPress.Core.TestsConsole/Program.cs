using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using ZDPress.Opc;
using ZDPress.UI.Common;


namespace ZDPress.Core.TestsConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Crypto22();
            BitParamsTest();

            //Logger.InitLogger();

            //try
            //{
            //    OpcBackgroundProcessorSingleton.Instance.ConfigureProcessor();
            //    OpcBackgroundProcessorSingleton.Instance.OnReceivedDataAction += OnReceivedData;
            //    OpcBackgroundProcessorSingleton.Instance.TimerStart();
            //    Console.ReadKey();
            //}
            //catch (Exception ex)
            //{
            //    Console.BackgroundColor = ConsoleColor.Red;
            //    Console.WriteLine(ex.Message);    
            //}
        }

        private static void Crypto22()
        {
            string value = "PassW0rd";
            string encriptedValue = Encriptor.EncryptAESWithBase64(value);

            string decriptedValue = Encriptor.DecryptAESFromBase64(encriptedValue, String.Empty);
            if (value != decriptedValue)
            {
                throw new Exception("value != decriptedValue");
            }
    }

        private static void BitParamsTest()
        {
            BitParameters bp = BitParameters.ShowGraph | BitParameters.AvtomatRezhim;

            int i = (int) bp;

            ShowBits(i);

            BitParameters sss = (BitParameters)i;


            BitParameters sss3 = sss & ~(BitParameters.AvtomatRezhim);

            BitParameters sss3333 = sss3 | (BitParameters.AvtomatRezhim);


            Console.ReadKey();

            BitParameters bp1 = (BitParameters) 3;

            if (bp1.HasFlag(BitParameters.ShowGraph))
            {
                Console.WriteLine("Has ShowGraph");
            }
            if (bp1.HasFlag(BitParameters.AvtomatRezhim))
            {
                Console.WriteLine("Has AvtomatRezhim");
            }
            if (bp1.HasFlag(BitParameters.RuchnoRezhim))
            {
                Console.WriteLine("Has RuchnoRezhim");
            }
            int i2 = (int) bp;
            ShowBits(i2);
            Console.ReadKey();
        }

        private static void BytesTest(byte[] bytes)
        {
            // If the system architecture is little-endian (that is, little end first),
            // reverse the byte array.
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            int i = BitConverter.ToInt32(bytes, 0);
            Console.WriteLine("int: {0}", i);
            // Output: int: 25
        }

        private static void ShowBits(int x)
        {
            string s = Convert.ToString(x, 2); //Convert to binary in a string

            //int[] bits = s.PadLeft(8, '0') // Add 0's from left
              //           .Select(c => int.Parse(c.ToString())) // convert each char to int
                //.ToArray(); // Convert IEnumerable from select to Array

            int[] bits = s.Select(c => int.Parse(c.ToString())) // convert each char to int
             .ToArray(); // Convert IEnumerable from select to Array

            //BitArray b = new BitArray(new byte[] { x });
            //int[] bits = b.Cast<bool>().Select(bit => bit ? 1 : 0).ToArray();

            foreach (int t in bits)
            {
                Console.Write(t.ToString());
            }

            Console.Write("--->" + x);
            Console.WriteLine();
        }

        private static void IntBit()
        {
            // Define an array of integers.
            //int[] values = { 0, 15, -15, 0x100000, -0x100000, 1000000000, -1000000000, int.MinValue, int.MaxValue };
            int[] values = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Convert each integer to a byte array.
            Console.WriteLine("{0,16}{1,10}{2,17}", "Integer", "Endian", "Byte Array");
            Console.WriteLine("{0,16}{1,10}{2,17}", "---", "------","----------");
            foreach (var value in values)
            {
                byte[] byteArray = BitConverter.GetBytes(value);
                for (int i = 0; i < byteArray.Length; i++)
                {
                    Console.Write(byteArray[i].ToString());
                }
                Console.WriteLine();
                //Console.WriteLine("{0,16}{1,10}{2,17}", value, BitConverter.IsLittleEndian ? "Little" : " Big", BitConverter.ToString(byteArray));
            }
        }

        public static int ReceiveCounter { get; set; }

        //private static void OnReceivedData(PressOperationData data) 
        //{
        //    Console.ForegroundColor = ConsoleColor.Gray;
        //    ReceiveCounter++;
        //    if (data.Exception == null)
        //    {
        //        string dataMsg = string.Format("{4} DispPress:{0} DlinaSopr:{1} ShowGraph:{2} SpentTimeToIteration:{3}", data.DispPress, data.DlinaSopr, data.ShowGraph, data.SpentTimeToProcess, ReceiveCounter);
        //        Console.WriteLine(dataMsg);
        //        Logger.Log.Info(dataMsg);
        //    }
        //    else 
        //    {
        //        Logger.Log.Error(data.Exception.Message);
        //        Console.BackgroundColor = ConsoleColor.Red;
        //        Console.WriteLine(data.Exception.Message);
        //    }

        //    if(ReceiveCounter % 10 == 0)
        //    {
                
        //    }
            

        //    if (ReceiveCounter == 10)
        //    {
        //        OpcBackgroundProcessorSingleton.Instance.WriteToOpc("True", OpcConsts.ShowGraph);
        //    }



        //    if (ReceiveCounter == 100)
        //    {
        //        OpcBackgroundProcessorSingleton.Instance.TimerStop();
        //        OpcBackgroundProcessorSingleton.Instance.OpcServerClose();
        //         Console.WriteLine(" end work...");
        //    }
        //}

     
    }
}
