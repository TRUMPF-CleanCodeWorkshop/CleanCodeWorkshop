using System;

namespace GameOfLife.UI
{
    using System.Windows.Input;

    public class FlowCommand : ICommand
    {
        public event Action Executed;

        public FlowCommand()
        {
            
        }

        public FlowCommand(Action action)
        {
            this.Executed += action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.OnExecuted();
        }

        public event EventHandler CanExecuteChanged;

        protected virtual void OnExecuted()
        {
            var handler = this.Executed;
            if (handler != null)
            {
                handler();
            }
        }
    }
}
