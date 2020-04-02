using Framework.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading;
using System.Threading.Tasks;

namespace echobot.Dialogs
{
    /// <summary>
    /// This echo dialog is a sample for a simple Topic Dialog.
    /// It consists of one step which sends the users last message to the user.
    /// </summary>
    public class EchoDialog : BaseDialog<IBot4Dialog, BotServices>
    {
        /// <summary>
        /// Initialize dialog.
        /// </summary>
        /// <param name="services">the bot services</param>
        /// <param name="bot">the bot itself</param>

        public EchoDialog(BotServices services, IBot4Dialog bot) : base(services, bot, nameof(EchoDialog))
        {
        }

        protected override void AddInitialSteps()
        {
            AddStep(PingStep);
        }

        private async Task<DialogTurnResult> PingStep(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // Send Pong
            await TheBot.SendMessage($"[PONG]: {stepContext.Context.Activity.Text}", stepContext.Context);
            // Continue
            return await ProceedWithDialog(stepContext);
        }
    }
}
