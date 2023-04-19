using System;
using UnityEngine;

public class Health : IHealth
{
    private int _maxValue;
    private int _currentValue;

    public int Value { get => _currentValue; }

    public event Action HealthChanged;
    public event Action Died;

    public Health(int maxValue)
    {
        _maxValue = maxValue;
        _currentValue = _maxValue;
    }

    public void TakeDamage(int value)
    {
        if (value < 0)
            throw new ArgumentException("Incorrect damage value");

        _currentValue -= value;

        HealthChanged?.Invoke();

        if (_currentValue <= 0)
        {
            _currentValue = 0;
            Died?.Invoke();
        }
    }

    public void ReturnHealth()
    {
        _currentValue = _maxValue;
    }

    public void Heal(int value)
    {
        if (value < 0)
            throw new ArgumentException("HealValue is incorrect");

        _currentValue += value;

        if (_currentValue > _maxValue)
            _currentValue = _maxValue;
    }
}

public interface IHealth
{
    event Action HealthChanged;
    event Action Died;

    void TakeDamage(int value);
    void ReturnHealth();
    void Heal(int value);
}