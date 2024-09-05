using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveEntity", menuName = "ScriptableObject/WaveEntity")]
public class WaveEntity : Entity
{
    private WaveView _waveView;

    [SerializeField] private WaveView _waveViewPrefab;

    public override ViewEntity View => _waveView;

    public override void Initialize(Transform parent)
    {
        _waveView = Instantiate(_waveViewPrefab, parent);
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
