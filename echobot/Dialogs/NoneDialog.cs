using Framework.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading;
using System.Threading.Tasks;

namespace echobot.Dialogs
{
    /// <summary>
    /// This dialog is the fallback dialog. All input which could not been classified triggers this dialog.
    /// </summary>
    public class NoneDialog : BaseDialog<IBot4Dialog, BotServices>
    {
        /// <summary>
        /// Initialize dialog.
        /// </summary>
        /// <param name="services">the bot services</param>
        /// <param name="bot">the bot itself</param>
        public NoneDialog(BotServices services, IBot4Dialog bot) : base(services, bot, nameof(NoneDialog))
        {
        }

        protected override void AddInitialSteps()
        {
            AddStep(SorryStep);
        }

        private async Task<DialogTurnResult> SorryStep(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            await TheBot.SendMessage("Sorry, I did not understand you ..", stepContext.Context);
            return await stepContext.NextAsync();
        }
    }
}
