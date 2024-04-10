using System.Diagnostics;

namespace IncrementalCivilization.Messages;

[DebuggerDisplay("Page = {PageToEnable}")]
public class EnablePageMessage(EnablePageMessage.Page pageToEnable)
{
    public enum Page { Research, Upgrades, Time, All };

    public Page PageToEnable { get; private set; } = pageToEnable;
}
