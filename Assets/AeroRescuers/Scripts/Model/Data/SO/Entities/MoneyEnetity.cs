using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoneyEnetity", menuName = "ScriptableObject/MoneyEnetity")]
public class MoneyEnetity : Entity
{
    private MoneyView _moneyView;

    [Header("Components")]
    [SerializeField] private MoneyView _moneyViewPrefab;

    public override ViewEntity View => _moneyView;

    public override void Initialize(Transform parent)
    {
        _moneyView = Instantiate(_moneyViewPrefab, parent);
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
