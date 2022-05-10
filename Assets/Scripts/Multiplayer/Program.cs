//using System;
//using System.Threading.Tasks;

//namespace EsneServerApp
//{
//    class Program
//    {
//        static Pool connectionsPool = new Pool(32);

//        static void Main(string[] args)
//        {
//            Task task = new Task(() => { new Listener(connectionsPool, 16384); });
//            task.Start();

//            while (true)
//            {
//                connectionsPool.Process();
//            }

//            Console.WriteLine("Hello World");
//            Console.ReadLine();
//        }
//    }
//}