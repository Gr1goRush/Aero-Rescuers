using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CloudEntity", menuName = "ScriptableObject/CloudEntity")]
public class CloudEntity : Entity
{
    private CloudView _cloudView;

    [SerializeField] private CloudView _cloudViewPrefab;
    [SerializeField] private int _damage;

    public override ViewEntity View => _cloudView;

    public override void Initialize(Transform parent)
    {
        _cloudView = Instantiate(_cloudViewPrefab, parent);
        _cloudView.SetDamage(_damage);
    }

    public override void FixedUpdate()
    {
        throw new System.NotImplementedException();
    }

    public override void AddObjectDisposable()
    {
        throw new System.NotImplementedException();
    }

    public override void Dispose()
    {
        throw new System.NotImplementedException();
    }
}
