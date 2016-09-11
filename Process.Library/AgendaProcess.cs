using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerProject;
using Domain.Core;

namespace Process.Library
{
    public class ProcessAgenda : Domain.Core.Process
    {
        public override void Processing(MessageRequest message)
        {
        }

        protected override void PreProcessing(MessageRequest message)
        {
        }

        protected override void PostProcessing(MessageRequest message)
        {
        }

        public override void CancelRecovery()
        {
        }

        public override void Recovery()
        {

        }
    }
}
