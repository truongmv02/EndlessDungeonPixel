
using UnityEngine;

[System.Serializable]
public class InputInfo
{
    public bool inputValue;
}

public class InputCondition : Condition<InputInfo>, ISetOwner
{
    IGetInput input;

    protected override void CheckCondition()
    {
        SuitableCondition(input.GetInput() == Info.inputValue);
    }

    public void SetOwner(object owner)
    {
        input = owner as IGetInput;
    }

}
