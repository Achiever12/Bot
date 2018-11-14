using System;
using System.Threading.Tasks;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Net.Http;


namespace CFE
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private const string VER = "0.1alpha";

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("�ȳ��ϼ��� CFE �� �Դϴ�. �ñ��Ͻ� ������ �����ϼ���");
            //context.Wait(MessageReceivedAsync);
            context.Call(new FAQDialog(), DialogResumeAfter);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;

            if (message.Text == "ver?")
            {
                await context.PostAsync("���� ������� ������ " + VER + "�Դϴ�.");
            }

            context.Call(new FAQDialog(), DialogResumeAfter);
            //context.Wait(MessageReceivedAsync);
        }

        private async Task DialogResumeAfter(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                string message = await result;

                //await context.PostAsync(WelcomeMessage); ;
                await this.MessageReceivedAsync(context, null);
            }
            catch (TooManyAttemptsException)
            {
                await context.PostAsync("������ ������ϴ�. �˼��մϴ�.");
            }
        }
    }
}