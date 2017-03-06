using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityGameServer;

namespace Example1 {
    class Program {
        static void Main(string[] args) {
            try {
                using (ExampleServer server = new ExampleServer(args)) {
                    server.Start();
                }
            }
            catch (Exception ex) {
                Console.WriteLine("{0}: {1}\n{2}", ex.GetType(), ex.Message, ex.StackTrace);
            }
        }
    }
}
