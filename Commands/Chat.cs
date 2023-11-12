using PluginAPI.Core;
using System.Collections.Generic;
using System;
using TutorialChat;
using CommandSystem;
using PlayerRoles;
using RemoteAdmin;
using UnityEngine;
using PluginAPI.Commands;
using PluginAPI;
using System.Linq;
using System.Net;

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
            int startIndex = input.IndexOf("<");
            int endIndex = input.IndexOf(">");

            if (startIndex < endIndex)
            {
                return input.Substring(0, startIndex) + input.Substring(endIndex + 1);
            }

            return input;
        }

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player senderAsPlayer = Player.Get(sender);

            if (senderAsPlayer == null)
            {
                response = "You must be a player to run this command.";
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
                    response = $"Chat message \"{string.Join(" ", arguments)}\" sent.";
                }
            }

            response = $"Chat message \"{string.Join(" ", arguments)}\" sent.";
            return true;
        }
    }
}