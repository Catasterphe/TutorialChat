namespace TutorialChat
{
    public class Config
    {
        public string TutorialMessage { get; set; } = "<color=red><b>You are tutorial,</b></color> You can use <color=green>.chat [message]</color> in your console (not RA) to send a private message to other tutorials!";
        public ushort TimeToDisplay { get; set; } = 5;
        public bool SendBroadcast { get; set; } = true;

        public bool CheckMute { get; set; } = false;
    }
}