using PluginAPI.Core;
using System;
using CommandSystem;
using System.Linq;
using static TutorialChat.PlayerManager;

namespace TutorialChat.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ChatMuteCommand : ICommand
    {
        public string Command { get; set; } = "chatmute";

        public string[] Aliases { get; set; } = { "cmute", "tmute", "jmute" };

        public string Description { get; set; } = "Mutes players from using the .chat plugin.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player senderAsPlayer = Player.Get(sender);

            if (senderAsPlayer == null || !sender.CheckPermission(PlayerPermissions.PlayersManagement))
            {
                response = "You do not have permissions to use this command.";
                return false;
            }

            if (arguments.Count > 0)
            {
                Player playerToMute = FindPlayer(arguments.First());
                if (playerToMute == null)
                {
                    response = $"could not mute {arguments.First()} as they were not found.";
                    return false;
                }
                if (IsPlayerMuted(playerToMute.ReferenceHub.PlayerId))
                {
                    UnmutePlayer(playerToMute.ReferenceHub.PlayerId);
                    response = $"Player {playerToMute.LogName} was un-muted.";
                    playerToMute?.SendBroadcast("You have been <color=green>unmuted</color> from using the .chat command", 5);
                    return true;
                }
                MutePlayer(playerToMute.ReferenceHub.PlayerId);
                response = $"Player {playerToMute.LogName} was chat-muted.";
                playerToMute?.SendBroadcast("You have been <color=red>muted</color> from using the .chat command", 5);
                return true;
            }

            response = $"An unknown error has occured.";
            return true;
        }
    }
}