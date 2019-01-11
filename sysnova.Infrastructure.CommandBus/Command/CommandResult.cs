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
        public CommandResult(string[] result)
        {
            this.Success = result;
        }

        public string[] Success { get; protected set; }
    }
}
