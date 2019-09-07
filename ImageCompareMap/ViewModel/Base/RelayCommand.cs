using System;
using System.Windows.Input;

namespace MapComparer.viewmodel {

    class RelayCommand : ICommand {

        private Action<object>      execute;
        private Func<object, bool>  canExecute;

        public event EventHandler CanExecuteChanged {
            add     { CommandManager.RequerySuggested += value; }
            remove  { CommandManager.RequerySuggested -= value; }
        }


        public RelayCommand(Action<object> executeAction) : this(executeAction, null) {

        }

        public RelayCommand(Action<object> executeAction, Func<object, bool> canExecuteFunc) {
            canExecute  = canExecuteFunc;
            execute     = executeAction;
        }


        public bool CanExecute(object parameter) {
            if (canExecute != null)     return  canExecute(parameter);
            else                        return  true;
            
        }

        public void Execute(object parameter) {
            if (execute != null) execute(parameter);
        }


    }

}
