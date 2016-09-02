using sysnova.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sysnova.Infrastructure.CommandBus.Command
{
    public class CommandResult : ICommandResult
    {
        public CommandResult(bool success)
        {
            this.Success = success;
        }

        public bool Success { get; protected set; }
    }
}
