using Domain.Core;
using Domain.HealthChecker;
using HeartBeat.Core;
using Services.Implementation;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using WorkerProject;

namespace Domain.Agenda
{
    public class WorkerAgendaComponent : Worker
    {
        ServiceHost serviceHost = null;
        public WorkerAgendaComponent(IHeartBeat heartBeat, IHealthChecker healthChecker)
            : base(heartBeat, healthChecker)
        {
            base.OnWorkerStatusChanged += WorkerAgenda_OnWorkerStatusChanged;
            base.WorkerId = 1001;
        }

        void WorkerAgenda_OnWorkerStatusChanged(WorkerStatus status)
        {

        }

        protected override void BuildUp()
        {
            serviceHost = new ServiceHost(typeof(MailBoxService));
            serviceHost.Open();
        }

        protected override MessageRequest ReadMessage()
        {
            return null;
        }

        protected override void SendMessage(MessageRequest message)
        {
            Binding b = new NetTcpBinding();
            EndpointAddress end = new EndpointAddress("net.tcp://localhost:8080/SampleSvc");
            IMailBoxService proxy = ChannelFactory<IMailBoxService>.CreateChannel(b, end);
            proxy.SendMessage(new SolicitacaoCadastroDeLaboratorioMessage() { Sender = "Test" });
        }

        protected override void Recover()
        {

        }

        protected override void Process()
        {
            Random radom = new Random();
            int numberOfInteractions = radom.Next(9, 100);

            for (int i = 0; i < numberOfInteractions; i++)
            {
                string text = "asifhasdpifhapisdh9ahsdfhpaisufasdiufhpawoer-qwerwqejhpzspkdjfspozfjzosidfdsfojsdíofjzs´dofzsdifpsdpsdfsdfposkfkspdkfpsfopsk";
                if (text.Contains(numberOfInteractions.ToString()))
                    throw new Exception("An error occoured right now");
            }
        }
    }
}
