using PluginAPI.Core.Attributes;
using PluginAPI.Core;
using PluginAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using PluginAPI.Events;
using PlayerRoles;

namespace TutorialChat
{
    public class EventHandler
    {
        [PluginEvent(ServerEventType.PlayerChangeRole)]
        void OnPlayerChangeRole(PlayerChangeRoleEvent ev)
        {
            if (ev.Player == null || ev.ChangeReason == RoleChangeReason.Destroyed || ev.NewRole != RoleTypeId.Tutorial) return;
            ev.Player.SendBroadcast(Plugin.Singleton.Config.TutorialMessage, Plugin.Singleton.Config.TimeToDisplay);
        }

    }
}
