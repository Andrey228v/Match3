using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assets.Scripts.StateMachines.States
{
    public class RefillGridState : IState, IDisposable
    {
        private CancellationTokenSource _cancellationTokenSource;

        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
        }

        public void Enter()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void Exit()
        {

        }
    }
}
