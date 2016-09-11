using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartBeat.Core
{
    public class HeartBeatInfo
    {
        private int _id;
        public virtual int Id { get { return _id; } }
        public virtual bool Beat { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual int WorkerId { get; set; }
    }
}
