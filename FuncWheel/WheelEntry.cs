using System;
using System.Threading.Tasks;

namespace Ensage.Common.FuncWheel
{
    public class WheelEntry : IWheelEntry
    {
        public WheelEntry(string displayName, Func<Task> func)
            : this(displayName, func, true)
        {}

        public WheelEntry(string displayName, Func<Task> func, bool isEnabled)
        {
            this.DisplayName = displayName;
            this.Execute = func;
            this.IsEnabled = isEnabled;
        }

        public string DisplayName { get; set; }

        public Func<Task> Execute { get; set; }

        public bool IsEnabled { get; set; }
    }
}
