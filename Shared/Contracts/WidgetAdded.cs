using System;

namespace Shared.Contracts
{
    [Serializable]
    public class WidgetAdded
    {
        public WidgetAdded(string widgetName)
        {
            WidgetName = widgetName;
        }
        public string WidgetName { get; private set; }
    }
}
