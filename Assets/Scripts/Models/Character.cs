using System;
using UnityEngine;

public class Character : MonoBehaviour {
    public event Action HealthChanged;

    private const int minHealth = 0;
    private const int maxHealth = 80;
    private int currentHealth = maxHealth;

    public int CurrentHealth { get => currentHealth; internal set => currentHealth = value; }
    public int MinHealth => minHealth;
    public int MaxHealth => maxHealth;

    public void IncreaseHealth(int amount) {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
        HealthChanged.Invoke();
    }

    public void DecreaseHealth(int amount) {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
        HealthChanged.Invoke();
    }
}
