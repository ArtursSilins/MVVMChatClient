using MVVMChatClient.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMChatClient.Core.ViewModel.Commands
{
    public class ObjecRelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<IClosable> _Action;
        private IClosable _Closable;
        public ObjecRelayCommand(Action<IClosable> action, IClosable closable)
        {
            _Action = action;
            _Closable = closable;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _Action.Invoke(_Closable);
        }
        private void method(ICloneable win)
        {
            win.Clone();
        }
    }
}
