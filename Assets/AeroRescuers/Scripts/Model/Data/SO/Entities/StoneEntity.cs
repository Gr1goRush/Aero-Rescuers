using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoneEntity", menuName = "ScriptableObject/StoneEntity")]
public class StoneEntity : Entity
{
    private StoneView _stoneView;

    [SerializeField] private StoneView _stoneViewPrefab;

    public override ViewEntity View => _stoneView;

    public override void Initialize(Transform parent)
    {
        _stoneView = Instantiate(_stoneViewPrefab, parent);
    }

    public override void FixedUpdate()
    {

    }

    public override void AddObjectDisposable()
    {

    }

    public override void Dispose()
    {

    }
}
