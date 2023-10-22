using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    private int _currentHealth;

    public UnityEvent<float> OnChangeHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
        OnChangeHealth?.Invoke((float)_currentHealth / _maxHealth);
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            DecreaseHealth(10);
        }
    }

    private void DecreaseHealth(int amount)
    {
        _currentHealth -= amount;
        OnChangeHealth?.Invoke((float)_currentHealth / _maxHealth);
    }

    public void InCreaseHealth(int amount)
    {
        _currentHealth += amount;
        OnChangeHealth?.Invoke((float)_currentHealth / _maxHealth);
    }
}
