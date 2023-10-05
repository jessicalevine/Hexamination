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

    public event Action DispelChanged;
    private const int minDispel = 0;
    private int currentDispel = minDispel;
    public int CurrentDispel { get => currentDispel; internal set => currentDispel = value; }
    public int MinDispel => minDispel;

    public void ApplyDamage(int damage) {
        if (currentDispel > 0) {
            int oldDispel = currentDispel;
            DecreaseDispel(damage);
            damage -= oldDispel - currentDispel;
        }

        if (damage > 0) {
            DecreaseHealth(damage);
        }
    }

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

    public void IncreaseDispel(int amount) {
        currentDispel += amount;
        DispelChanged.Invoke();
    }

    public void DecreaseDispel(int amount) {
        currentDispel -= amount;
        currentDispel = Mathf.Max(currentDispel, minDispel);
        DispelChanged.Invoke();
    }

    public void SetDispel(int amount) {
        currentDispel = Mathf.Max(amount, minDispel);
        DispelChanged.Invoke();
    }
}
