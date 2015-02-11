using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RailDelivery.DVIRApp.Commands
{
    class RelayCommand : ICommand
    {
        private readonly Action _del;

        public RelayCommand(Action del)
        {
            _del = del;
        }

        public void Execute(object parameter)
        {
            _del();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
