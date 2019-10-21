using Framework.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace echobot.Dialogs
{
    public class NoneDialog : BaseDialog<IBot4Dialog, BotServices>
    {
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
