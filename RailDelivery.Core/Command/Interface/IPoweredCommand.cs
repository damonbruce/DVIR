using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RailDelivery.Core.Models;

namespace RailDelivery.Core.Command.Interface
{
    public interface IPoweredCommand
    {
        void SavePowered(Powered powered);
    }
}
