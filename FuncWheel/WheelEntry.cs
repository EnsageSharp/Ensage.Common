using System;
using System.Threading.Tasks;

namespace Ensage.Common.FuncWheel
{
    public class WheelEntry : IWheelEntry
    {
        public WheelEntry(string displayName, Func<Task> func)
        {
            this.DisplayName = displayName;
            this.Execute = func;
        }

        public string DisplayName { get; set; }

        public Func<Task> Execute { get; set; }
    }
}
