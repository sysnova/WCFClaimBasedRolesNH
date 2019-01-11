using sysnova.Domain.Core.Common;
using sysnova.Infrastructure.CommandBus.Command;
using sysnova.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using sysnova.Domain.Interfaces;

namespace sysnova.Infrastructure.CommandBus.Dispatcher
{
    public class DefaultCommandBus : ICommandBus
    {
        private IKernel _kernel;
        private IUnitOfWork _uow;
        private ICommandResult _result;
        public DefaultCommandBus(IKernel kernel, IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _kernel = kernel;
        }
        public ICommandResult Submit<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            ICommandHandler<TCommand> handler = _kernel.Get<ICommandHandler<TCommand>>();

            if (!((handler != null) && handler is ICommandHandler<TCommand>))
            {
                throw new CommandHandlerNotFoundException(typeof(TCommand));
            }
                _result = handler.Execute(command);
                _uow.Commit();
            return _result;
        }

        public IEnumerable<ValidationResult> Validate<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            IValidationHandler<TCommand> handler = _kernel.Get<IValidationHandler<TCommand>>();

            if (!((handler != null) && handler is IValidationHandler<TCommand>))
            {
                throw new ValidationHandlerNotFoundException(typeof(TCommand));
            }

            return handler.Validate(command);
        }

        public ICommandResult Submit<TCommand, TCommandHandler>(TCommand command, TCommandHandler commandHandler)
            where TCommand : ICommand
            where TCommandHandler : ICommandHandler<TCommand>
        {
            if (!((commandHandler != null) && commandHandler is ICommandHandler<TCommand>))
            {
                throw new CommandHandlerNotFoundException(typeof(TCommand));
            }

            return commandHandler.Execute(command);
        }

        public IEnumerable<ValidationResult> Validate<TCommand, TValidationHandler>(TCommand command, TValidationHandler validationHandler)
            where TCommand : ICommand
            where TValidationHandler : IValidationHandler<TCommand>
        {
            if (!((validationHandler != null) && validationHandler is IValidationHandler<TCommand>))
            {
                throw new ValidationHandlerNotFoundException(typeof(TCommand));
            }

            return validationHandler.Validate(command);
        }

        public void AsyncRun<T>(Action<T> action)
        {
            Task.Factory.StartNew(delegate
            {
                //using (var container = AutofacDependencyResolver.Current.ApplicationContainer.BeginLifetimeScope("httpRequest"))
                //{
                    var service = _kernel.Get<T>();
                    action(service);
                //}
            });
        }
    }
}
