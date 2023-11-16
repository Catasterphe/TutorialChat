using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;
using PlayerRoles;
using static TutorialChat.PlayerManager;

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

        [PluginEvent(ServerEventType.PlayerLeft)]
        void OnPlayerLeft(PlayerLeftEvent ev)
        {
            if (IsPlayerMuted(ev.Player.ReferenceHub.PlayerId)) {
                UnmutePlayer(ev.Player.ReferenceHub.PlayerId);
            }
        }
    }
}
