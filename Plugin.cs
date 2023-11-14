using PluginAPI.Events;
using PluginAPI.Core.Attributes;
using PluginAPI.Core;

namespace TutorialChat
{
    public class Plugin
    {
        public static Plugin Singleton { get; private set; }
        public const string Version = "13.3.1";
        [PluginConfig] public Config Config;

        [PluginEntryPoint("Tutorial Chat", Version, "A recreation of the swiftkraft command that lets tutorials/admins chat with .chat", "Aster")]
        public void LoadPlugin()
        { 
            Singleton = this;
            EventManager.RegisterEvents<EventHandler>(this);
            var handler = PluginHandler.Get(this);
            handler.LoadConfig(this, nameof(PluginConfig));

            Log.Info($"Plugin {handler.PluginName} loaded.");
        }
    }
}