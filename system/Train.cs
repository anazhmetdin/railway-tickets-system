using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace system
{
    public class Train
    {
        public int id { get; }
        public int seatsCount { get; protected set; }
        
        protected Train(int id, int seatsCount)
        {
            this.id = id;
            this.seatsCount = seatsCount;
        }


    }
}

}
