using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace S3BotCmd.Preconditions
{
    // todo: look into how to turn this attribute into a way where we can specify multiple allowed channels (||)
    public class RequireProperChannelAttribute : PreconditionAttribute
    {
        // Override the CheckPermissions method
        public async override Task<PreconditionResult> CheckPermissions(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            if ((context.Channel.Name != "dev-test" && context.Channel.Name != "set-planning" && context.Channel.Name != "leadership")) // Todo: can't seem to find out how to find Context.Message.Author's roles so we can scope particular commands to it
            {
                return PreconditionResult.FromError("This command may only be executed in development channel");
            }
            
            return PreconditionResult.FromSuccess();
        }
    }
}
