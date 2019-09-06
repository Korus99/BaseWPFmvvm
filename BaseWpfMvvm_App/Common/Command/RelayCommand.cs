using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BaseWpfMvvm_App.Common.Command
{
    public class RelayCommand : ICommand
    {
        private readonly Func<bool> _targetCanExecuteMethod;
        private readonly Action _targetExecuteMethod;
        private readonly string _v;
        private readonly string _v2;

        public RelayCommand(Action executeMethod)
        {
            _targetExecuteMethod = executeMethod;
        }

        public RelayCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            _targetExecuteMethod = executeMethod;
            _targetCanExecuteMethod = canExecuteMethod;
        }

        public RelayCommand(string v)
        {
            _v = v;
        }
        public RelayCommand(string v, string v2)
        {
            _v = v;
            _v2 = v2;
        }

        public RelayCommand()
        {
        }

        // ICommand implementation

        public bool CanExecute(object parameter)
        {
            if (_targetCanExecuteMethod != null)
            {
                return _targetCanExecuteMethod();
            }

            return _targetExecuteMethod != null;
        }

        public event EventHandler CanExecuteChanged = delegate { };

        public void Execute(object parameter)
        {
            _targetExecuteMethod?.Invoke();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
    }

    public sealed class RelayCommand<T> : ICommand
    {
        private readonly Func<T, bool> _targetCanExecuteMethod;

        public RelayCommand(Action<T> executeMethod)
        {
            TargetExecuteMethod = executeMethod;
        }

        public RelayCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            TargetExecuteMethod = executeMethod;
            _targetCanExecuteMethod = canExecuteMethod;
        }

        public Action<T> TargetExecuteMethod { get; set; }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }


        #region ICommand Members

        bool ICommand.CanExecute(object parameter)
        {
            if (_targetCanExecuteMethod == null)
            {
                return TargetExecuteMethod != null;
            }

            var tparm = (T)parameter;
            return _targetCanExecuteMethod(tparm);
        }

        // Beware - should use weak references if command instance lifetime is longer than lifetime of UI objects that get hooked up to command
        // Prism commands solve this in their implementation
        public event EventHandler CanExecuteChanged = delegate { };

        void ICommand.Execute(object parameter)
        {
            TargetExecuteMethod?.Invoke((T)parameter);
        }

        #endregion
    }
}
