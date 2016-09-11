using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MailBoxServ
{
    [ServiceContract]
    interface ICalcService
    {
        [OperationContract]
        int getSum();
    }


    public class CalcService : ICalcService
    {
        public int getSum()
        {
            return 1;
        }
    }

}
