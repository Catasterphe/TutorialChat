using PluginAPI.Core;
using System;
using CommandSystem;
using PlayerRoles;

namespace TutorialChat.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class ChatCommand : ICommand
    {
        public string Command { get; set; } = "chat";

        public string[] Aliases { get; set; } = { "tbc", "jbc" };

        public string Description { get; set; } = "Sends a message to other tutorials.";

        static string sanitizeTags(string input)
        {
            int startIndex = input.IndexOf('<');

            while (startIndex != -1)
            {
                int endIndex = input.IndexOf('>', startIndex + 1);
                if (endIndex != -1)
                {
                    input = input.Substring(0, startIndex) + input.Substring(endIndex + 1);
                }
                else
                {
                    break;
                }
                startIndex = input.IndexOf('<');
            }

            return input;
        }

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player senderAsPlayer = Player.Get(sender);

            if (senderAsPlayer == null || PlayerManager.IsPlayerMuted(senderAsPlayer.ReferenceHub.PlayerId))
            {
                response = "You do not have permissions to use this command.";
                return false;
            }

            if (senderAsPlayer.Role != RoleTypeId.Tutorial && !senderAsPlayer.RemoteAdminAccess)
            {
                response = "You are not a tutorial or an admin!";
                return false;
            }

            string sanitizedMessage = sanitizeTags(string.Join(" ", arguments));
            foreach (Player player in Player.GetPlayers())
            {
                if (player.Role == RoleTypeId.Tutorial || player.RemoteAdminAccess)
                {
                    if (Plugin.Singleton.Config.SendBroadcast)
                    {
                        player.SendBroadcast($"<color=green>[Chat Message]</color> <color=white>{sanitizedMessage}</color> - <color=green>{senderAsPlayer.DisplayNickname}</color>", Plugin.Singleton.Config.TimeToDisplay);
                    }

                    player.SendConsoleMessage($"<color=green>[Chat Message]</color> <color=white>{sanitizedMessage}</color> - <color=green>{senderAsPlayer.DisplayNickname}</color>");
                }
            }

            response = $"Chat message \"{string.Join(" ", arguments)}\" sent.";
            return true;
        }
    }
}