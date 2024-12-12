using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Project
{
    public class CommandInvoker : ICommandInvoker, ITickable
    {
        private static readonly Queue<ICommand> _commandBuffer = new Queue<ICommand>();

        private ICommand _executableCommand;
        
        public void Tick()
        {
            if (_executableCommand != null && _executableCommand.IsCancellation == false)
                return;

            if (_commandBuffer.Count > 0)
            {
                _executableCommand = _commandBuffer.Dequeue();
                _executableCommand.Execute();
            }
        }
        
        public void AddCommand(ICommand command)
        {
            _commandBuffer.Enqueue(command);
            
            Debug.Log($"<color=yellow>[Command Invoker]</color> Buffer size: {_commandBuffer.Count}");
        }

        public void RemoveLastCommand()
        {
            if (_executableCommand != null)
            {
                _executableCommand?.Undo();
                _executableCommand = null;
            }
            
            if (_commandBuffer.Count > 0)
            {
                var command = _commandBuffer.Dequeue();

                Debug.Log($"<color=yellow>[Command Invoker]</color> Remove {command} Buffer size: {_commandBuffer.Count}");
            }
            else
                Debug.Log($"<color=yellow>[Command Invoker]</color> Buffer size: {_commandBuffer.Count}");
        }
    }
}