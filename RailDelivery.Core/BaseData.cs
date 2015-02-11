using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RailDelivery.Core.DataContext;

namespace RailDelivery.Core
{
    public class BaseData
    {
        private DVIRDataContext _db;

        public DVIRDataContext Db
        {
            get { return _db ?? (_db = new DVIRDataContext()); }
        }
    }
}
