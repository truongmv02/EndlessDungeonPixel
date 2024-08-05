
using UnityEngine;

[System.Serializable]
public class ChargeConditionInfo
{
    public int chargeAmountMin;
}
public class ChargeCondition : Condition<ChargeConditionInfo>, ISetOwner
{
    Charge charge;

    protected override void CheckCondition()
    {
        if (Info.chargeAmountMin == 0)
        {
            SuitableCondition(charge.CurrentCharge == 0);
        }
        else
        {
            SuitableCondition(charge.CurrentCharge >= Info.chargeAmountMin);
        }
    }

    public void SetOwner(object owner)
    {
        charge = (owner as MonoBehaviour).GetComponent<Charge>();
    }

}
