using System;
using System.Threading.Tasks;

namespace Ensage.Common.FuncWheel
{
    public interface IWheelEntry
    {
        string DisplayName { get; }

        Func<Task> Execute { get; }

        bool IsEnabled { get; }
    }
}
