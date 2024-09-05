using System;
using System.Threading.Tasks;
using UnityEngine;
using UniRx;

public class Plane
{
    private Vector2 _forceGravity;
    private float _maxHeight;
    private float _currentHeight;
    private bool _isFall;
    private bool _isFly;

    public Plane(Vector2 forceGravity, float maxHeight)
    {
        _forceGravity = forceGravity;
        _maxHeight = maxHeight;
        _isFall = false;
        _isFly = false;
    }

    public Vector2 ForceGravity => _forceGravity;
    public float CurrentHeight => _currentHeight;
    public bool IsFall { get { return _isFall; } set { _isFall = value; } }
    public bool IsFly { get { return _isFly; } set { _isFly = value; } }

    public void LossHeight(float speedIncreaseGravity)
        => _currentHeight -= speedIncreaseGravity;

    public void ChangeRoute()
    {
        if(IsFly)
            _forceGravity *= -1;
    }

    public void Up()
    {
        if (_isFly)
            _forceGravity = new Vector2(_forceGravity.x, Math.Abs(_forceGravity.y));
    }

    public void Down()
    {
        if (_isFly)
            _forceGravity = new Vector2(_forceGravity.x, -Math.Abs(_forceGravity.y));
    }

    public async void RecoveryHeight(float speedIncrease)
    {
        while (_currentHeight < _maxHeight)
        {
            _currentHeight += speedIncrease;
            await Task.Delay(100);
        }
    }
}
