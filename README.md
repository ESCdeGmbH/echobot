# EchoBot

The EchoBot is a simple example of the [framework](https://github.com/ESCdeGmbH/chatbot-framework.git). 

The topic of this bot is to answer on echo requests. Echo requests have the form "echo [...]". Look at the [Luis file](https://github.com/ESCdeGmbH/echobot/blob/master/data/luis-instance.json) to see what data was trained.
To see how the base dialogues work, have a look at the [EchoDialog](https://github.com/ESCdeGmbH/echobot/blob/master/echobot/Dialogs/EchoDialog.cs) and the [NoneDialog](https://github.com/ESCdeGmbH/echobot/blob/master/echobot/Dialogs/NoneDialog.cs).


For small talk it contains a [GreetingDialog](https://github.com/ESCdeGmbH/echobot/blob/master/echobot/Dialogs/SmallTalk/GreetingDialog.cs) as multiple and [Test](https://github.com/ESCdeGmbH/echobot/blob/master/echobot/Dialogs/SmallTalk-Data/Test.json) as single step small talk dialogues. 
Multiple step dialogues consist of response sets and the dialog file, which defines the steps. For single step dialogues just the response set is needed. From this set the bot response is chosen randomly and returned to the user.

We strongly recommend to name the intents and json files similar. This eases the maintainability and makes it clearer. Moreover you can access it by the same indentifier.

To model a deeper conversation in a base or multiple step dialogue just add more steps as in [GreetingDialog](https://github.com/ESCdeGmbH/echobot/blob/master/echobot/Dialogs/SmallTalk/GreetingDialog.cs).

For more informations about the framework [visit the Wiki](https://github.com/ESCdeGmbH/chatbot-framework/wiki).
