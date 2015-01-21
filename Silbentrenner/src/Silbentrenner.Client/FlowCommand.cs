using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Silbentrenner.Client
{
    public class FlowCommand : ICommand
    {
        private readonly Func<bool> m_CanExecute;
        public event Action Executed;

        public event EventHandler CanExecuteChanged;

        public FlowCommand() { }

        public FlowCommand(Func<bool> CanExecute)
        {
            m_CanExecute = CanExecute;
        }

        public bool CanExecute(object Parameter)
        {
            if (m_CanExecute == null)
            {
                return true;
            }

            return m_CanExecute();
        }

        public void CheckCanExecute()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged.Invoke(this, new EventArgs());
            }
        }

        public async void Execute(object Parameter)
        {
            OnExecuted();
        }

        protected virtual void OnExecuted()
        {
            var Handler = Executed;
            if (Handler != null)
            {
                Handler();
            }
        }

    }
}
