using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFReceiver
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    class Program : SimpleServiceReference.ISimpleServiceCallback, IDisposable
    {
        private SimpleServiceClient client;

        static void Main(string[] args)
        {
            Program p = new Program();
            p.start();
            Console.ReadLine();
            p.Dispose();
        }

        public void start()
        {
            InstanceContext context = new InstanceContext(this);

            client = new SimpleServiceReference.SimpleServiceClient(context, "WSDualHttpBinding_ISimpleService");
            client.Subscribe();
        }

        public void OnMessageAdded(string message, DateTime timestamp)
        {
            Console.WriteLine(message + " " + timestamp.ToString());
        }

        public void Dispose()
        {
            client.Unsubscribe();
            client.Close();
        }
    }
}
