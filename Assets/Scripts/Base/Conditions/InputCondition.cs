
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
        SuitableCondition(input.GetInput() == info.inputValue);
    }

    public void SetOwner(object owner)
    {
        input = owner as IGetInput;
    }

}
