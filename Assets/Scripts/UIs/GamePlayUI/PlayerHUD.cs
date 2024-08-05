using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] Slider hpSlider;
    [SerializeField] Text hpText;

    [Header("Energy")]
    [SerializeField] Slider energySlider;
    [SerializeField] Text energyText;

    BaseStat health;
    BaseStat currentHealth;

    BaseStat energy;
    BaseStat currentEnergy;

    CharacterController player;

    void Start()
    {
        player = GameManager.Instance.Player;

        health = player.Stats["Health"];
        currentHealth = player.Stats["CurrentHealth"];

        health.OnValueChange += HandleHealthChange;
        currentHealth.OnValueChange += HandleHealthChange;

        energy = player.Stats["Energy"];
        currentEnergy = player.Stats["CurrentEnergy"];

        energy.OnValueChange += HandleEnergyChange;
        currentEnergy.OnValueChange += HandleEnergyChange;

        HandleHealthChange(0);
        HandleEnergyChange(0);

        Debug.Log("player status");

    }

    void HandleHealthChange(float value)
    {
        hpSlider.value = currentHealth.Value / health.Value;
        hpText.text = currentHealth.Value.ToString("N0") + "/" + health.Value.ToString("N0");
    }

    void HandleEnergyChange(float value)
    {
        energySlider.value = currentEnergy.Value / energy.Value;
        energyText.text = currentEnergy.Value.ToString("N0") + "/" + energy.Value.ToString("N0");
    }

    private void OnDestroy()
    {
        currentHealth.OnValueChange -= HandleHealthChange;
        health.OnValueChange -= HandleHealthChange;

        currentEnergy.OnValueChange -= HandleEnergyChange;
        energy.OnValueChange -= HandleEnergyChange;
    }

}

