# EchoBot

The EchoBot is a simple example of the [framework](https://github.com/ESCdeGmbH/chatbot-framework.git). 

The topic of this bot is to answer on echo requests. Echo requests have the form "echo [...]". Look at the [Luis file](https://github.com/ESCdeGmbH/echobot/blob/master/data/luis-instance.json) to see what data was trained.
To see how the base dialogues work, have a look at the [EchoDialog](https://github.com/ESCdeGmbH/echobot/blob/master/echobot/Dialogs/EchoDialog.cs) and the [NoneDialog](https://github.com/ESCdeGmbH/echobot/blob/master/echobot/Dialogs/NoneDialog.cs).


For small talk it contains a [GreetingDialog](https://github.com/ESCdeGmbH/echobot/blob/master/echobot/Dialogs/SmallTalk/GreetingDialog.cs) as multiple and [Test](https://github.com/ESCdeGmbH/echobot/blob/master/echobot/Dialogs/SmallTalk-Data/Test.json) as single step small talk dialogues. 
Multiple step dialogues consist of response sets and the dialog file, which defines the steps. For single step dialogues just the response set is needed. From this set the bot response is chosen randomly and returned to the user.

We strongly recommend to name the intents and json files similar. This eases the maintainability and makes it clearer. Moreover you can access it by the same indentifier.

To model a deeper conversation in a base or multiple step dialogue just add more steps as in [GreetingDialog](https://github.com/ESCdeGmbH/echobot/blob/master/echobot/Dialogs/SmallTalk/GreetingDialog.cs).

For more informations about the framework [visit the Wiki](https://github.com/ESCdeGmbH/chatbot-framework/wiki).


## Structure of the echo bot repository

The following is a detailed description of how the echo bot project is built.

**Top-Level:** Contains the solution of the repo, as well as the NuGet configuration where the feed for the chatbot framework is stored.

**data:** Contains a sporadic version of the LUIS training data as framework

**echobot:** Contains the actual project; the echo bot.

- Bot.cs: The main class of the bot. Contains the definition of the assignment of intents to dialogs. 
- BotEmulator.bot: Configuration for the BotFramework emulator for testing
- BotServices.cs: Defines the service connections of a bot instance (such as DB connections)
- Program.cs: Contains the main method as usual
- Startup.cs: Common startup class as in any .NET Core WebApp
- appsettings*.json: logging options etc.
- echobot.csproj: The project file for the Echo-Bot
- luis.json: The configuration file for the used LUIS instance.

**echobot/Controller:** Contains the controller(s) for the web project
- BotController.cs: The main controller for the Echobot. Takes care of creating new bot instances and receiving messages.

**echobot/Dialogs:** Contains the various dialogs.
- EchoDialog.cs: Simple dialog that returns the last input. 
- NoneDialog.cs: Important dialog that deals with what happens if an input could not be classified.

**echobot/Dialogs/Smalltalk-Data:**
Defines the set of responses for a small talk dialog. The naming convention matches the intents according to the following scheme: *File XY.json <-> Intent st_XY*. This naming is enforced by the use of the built-in smalltalk mechanisms.

**echobot/Dialogs/SmallTalk:** Contains an exemplary multi step smalltalk dialogue

**echobot/Properties:** Launch configuration
