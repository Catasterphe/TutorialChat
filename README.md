#  Tutorial Chat

This plugin is a recreation of the .chat command provided by the SwiftKraft Plugin for SCP:SL - used for talking with Tutorials / Allows anyone with Remote Admin to see said messages.

## Basic Usage

When a player is set to the `Tutorial` Role, they will get a broadcast telling them to use the .chat command in the client console (Press ~) to send a message to the other tutorials (usually admins). Admins do not have to be tutorials to use the command. You can mute players by typing `cmute [id/name]` in the Remote Admin (Press M) console.

## Default Config

```yaml
# This setting represents the message that will be displayed to the user when they get set to the tutorial role.
tutorial_message: <color=red><b>You are tutorial,</b></color> You can use <color=green>.chat [message]</color> in your console (not RA) to send a private message to other tutorials!
# This setting determines the duration in seconds for which the "Tutorial Message" broadcast stays on the screen, along with any chat messages sent with the command.
time_to_display: 5
# This setting determines whether the message only shows in the client console or also gets broadcasted to all tutorials + staff members.
send_broadcast: true
```
