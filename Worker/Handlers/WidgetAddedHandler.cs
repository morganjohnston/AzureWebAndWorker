using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.ServiceBus.Messaging;
using Serilog;
using Shared.Contracts;

namespace Worker.Handlers
{
    public class WidgetAddedHandler
    {
        public async Task Handle([ServiceBusTrigger(nameof(WidgetAdded), AccessRights.Listen)] BrokeredMessage message)
        {
            var body = message.GetBody<WidgetAdded>(new DataContractJsonSerializer(typeof(WidgetAdded)));
            Log.Information("Handled {@Contract}", body);
            //await Task.Delay(TimeSpan.FromMinutes(3));
        }
    }
}