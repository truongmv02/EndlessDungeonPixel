using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HpBossUI : MonoBehaviour
{

    [SerializeField] Slider slider;

    BaseStat health;
    BaseStat currentHealth;
    public void Show(Stats stats)
    {
        health = stats["Health"];
        currentHealth = stats["CurrentHealth"];
        currentHealth.OnValueChange += HandleHealthChange;
        gameObject.SetActive(true);
    }

    void HandleHealthChange(float value)
    {
        slider.value = currentHealth.Value / health.Value;
    }

    private void OnDestroy()
    {
        if (currentHealth != null)
            currentHealth.OnValueChange -= HandleHealthChange;
    }

}
