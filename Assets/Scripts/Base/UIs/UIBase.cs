
using UnityEngine;

public class UIBase : MonoBehaviour
{
    public UIEffect[] effects;

    public virtual void Appear()
    {
        gameObject.SetActive(true);

        foreach (var effect in effects)
        {
            effect.Appear();
        }
    }

    public virtual void Disappear()
    {
        if (effects == null || effects.Length == 0) gameObject.SetActive(false);
        foreach (var effect in effects)
        {
            effect.Disappear();
        }
    }
}
