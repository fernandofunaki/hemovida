using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFSender
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    class Program : SimpleServiceReference.ISimpleServiceCallback, IDisposable
    {
        private SimpleServiceClient client;

        static void Main(string[] args)
        {
            ServiceHost myService = new ServiceHost(typeof(SimpleService));
            myService.Open();
            Program p = new Program();
            p.start();

            Console.ReadLine();
        }

        public void start()
        {
            InstanceContext context = new InstanceContext(this);

            client = new SimpleServiceReference.SimpleServiceClient(context, "WSDualHttpBinding_ISimpleService");

            for (int i = 0; i < 100; i++)
            {
                client.SendMessage("message " + i);
                Console.WriteLine("sending message" + i);
                Thread.Sleep(600);
            }
        }

        public void OnMessageAdded(string message, DateTime timestamp)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            client.Close();
        }
    }


    interface IMessageCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnMessageAdded(string message, DateTime timestamp);
    }


    [ServiceContract(CallbackContract = typeof(IMessageCallback))]
    public interface ISimpleService
    {
        [OperationContract]
        void SendMessage(string message);

        [OperationContract]
        bool Subscribe();

        [OperationContract]
        bool Unsubscribe();
    }

}
