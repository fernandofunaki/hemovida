//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Domain.Core
//{
//    public abstract class Process : IRecovery
//    {
//        public abstract void Processing(MessageRequest message);
//        protected abstract void PreProcessing(MessageRequest message);
//        protected abstract void PostProcessing(MessageRequest message);
//        public void Execute()
//        {
//            try
//            {
//                MessageRequest message = null;
//                PreProcessing(message);
//                Processing(message);
//                PostProcessing(message);
//            }
//            catch (Exception ex)
//            {
//                throw;
//            }
//        }
//        public abstract void CancelRecovery();
//        public abstract void Recovery();
//    }


//}
