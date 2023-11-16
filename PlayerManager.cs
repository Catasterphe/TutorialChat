using PluginAPI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialChat
{
    public class PlayerManager
    {
        /* 
            ███    ███ ██    ██ ████████ ███████ ███████
            ████  ████ ██    ██    ██    ██      ██     
            ██ ████ ██ ██    ██    ██    █████   ███████
            ██  ██  ██ ██    ██    ██    ██           ██
            ██      ██  ██████     ██    ███████ ███████
        */

        private static Dictionary<int, bool> mutedPlayers = new Dictionary<int, bool>();
        public static void MutePlayer(int playerId)
        {
            if (!mutedPlayers.ContainsKey(playerId))
            {
                mutedPlayers.Add(playerId, true);
            }
        }
        public static void UnmutePlayer(int playerId)
        {
            if (mutedPlayers.ContainsKey(playerId))
            {
                mutedPlayers.Remove(playerId);
            }
        }
        public static bool IsPlayerMuted(int playerId)
        {
            return mutedPlayers.ContainsKey(playerId) && mutedPlayers[playerId];
        }
        public static Player FindPlayer(string scanVar)
        {
            foreach (Player player in Player.GetPlayers())
            {
                Log.Debug(scanVar);
                if (player.Nickname.Contains(scanVar)) return player;
                if (player.ReferenceHub.PlayerId.ToString() == scanVar) return player;
            }
            return null;
        }
    }
}