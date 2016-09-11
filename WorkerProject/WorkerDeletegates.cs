using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerProject
{
    public delegate void OnRecoveryActioned(string message);
    public delegate void OWorkerStatusChanged(WorkerStatus status);
}
