using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TimezoneandWeather.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null) PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public class RelayCommand<T> : ICommand
        {
            private Action<T> methodToExecute;
            private Func<bool> canExecuteEvaluator;

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public RelayCommand(Action<T> methodToExecute, Func<bool> canExecuteEvaluator = null)
            {
                this.methodToExecute = methodToExecute;
                this.canExecuteEvaluator = canExecuteEvaluator;
            }

            public bool CanExecute(object parameter)
            {
                if (this.canExecuteEvaluator == null) return true;
                else
                {
                    bool result = this.canExecuteEvaluator.Invoke(); return result;
                }
            }

            public void Execute(object parameter) => this.methodToExecute.Invoke((T)parameter);
        }
    }
}
