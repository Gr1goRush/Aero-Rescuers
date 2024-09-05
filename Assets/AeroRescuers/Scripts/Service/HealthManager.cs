using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class HealthManager : MonoBehaviour
{
    private float _currentHealth;

    [SerializeField] private Slider _indicatorHealth;

    public ReactiveCommand GameOverCommand = new();

    public void Damage(float countDamage)
    {
        _currentHealth -= countDamage;
        ViewCurrentHealth();
        
        if (_currentHealth <= 0)
            GameOverCommand.Execute();
    }

    public void Die()
    {
        _currentHealth = 0;
        ViewCurrentHealth();

        if(GameOverCommand.IsDisposed == false)
            GameOverCommand.Execute();
    }

    private void Start()
    {
        ManagerUniRx.AddObjectDisposable(GameOverCommand);
        _currentHealth = 100;
        _indicatorHealth.maxValue = 100;
        ViewCurrentHealth();
    }

    private void ViewCurrentHealth()
        => _indicatorHealth.value = _currentHealth;

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(GameOverCommand);
    }
}
