using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Serilog;
using Shared.Contracts;

namespace Worker.Handlers
{
    public class WidgetAddedHandler
    {
        public async Task Handle([ServiceBusTrigger(nameof(WidgetAdded))] WidgetAdded message)
        {
            Log.Information("Handled {@Contract}", message);
        }
    }
}