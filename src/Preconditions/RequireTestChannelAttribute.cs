using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace S3BotCmd.Preconditions
{
    public class RequireTestChannelAttribute : PreconditionAttribute
    {
        // Override the CheckPermissions method
        public async override Task<PreconditionResult> CheckPermissions(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            if ((context.Channel.Name != "dev-test")) // Todo: can't seem to find out how to find Context.Message.Author's roles so we can scope particular commands to it
            {
                return PreconditionResult.FromError("This command may only be executed in development channel");
            }
            
            return PreconditionResult.FromSuccess();
        }
    }

}
