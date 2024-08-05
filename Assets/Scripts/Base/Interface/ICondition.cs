using System;

public interface ICondition
{
    bool IsSuitable { get; }
    Action<ICondition> OnSuitable { set; }

    void ResetCondition();
}
