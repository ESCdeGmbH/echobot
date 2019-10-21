using Framework.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading;
using System.Threading.Tasks;

namespace echobot.Dialogs
{
    public class EchoDialog : BaseDialog<IBot4Dialog, BotServices>
    {

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
            return await stepContext.NextAsync();
        }
    }
}
