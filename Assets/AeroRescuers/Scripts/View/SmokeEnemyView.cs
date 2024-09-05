using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEnemyView : EnemyView
{
    private Transform _transform;

    [SerializeField] private float _step;
    [SerializeField] private float _maxScaleSmoke;
    [SerializeField] private float _minScaleSmoke;

    private void Start()
    {
        _transform = transform;
        _transform.localScale = new Vector3(1, _minScaleSmoke, 0);
        _transform.position = new Vector3(_transform.position.x, -4, 0);
    }

    private void FixedUpdate()
    {
        if ((_transform.localScale.y > _maxScaleSmoke && _step > 0) || (_transform.localScale.y <= _minScaleSmoke && _step < 0))
            _step *= (-1);

        _transform.localScale = new Vector3(1, _transform.localScale.y + _step, 0);
    }
}
