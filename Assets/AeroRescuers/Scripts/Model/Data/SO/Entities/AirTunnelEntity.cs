using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AirTunnelEntity", menuName = "ScriptableObject/AirTunnelEntity")]
public class AirTunnelEntity : Entity
{
    private AirTunnelView _airTunnelView;

    [SerializeField] private AirTunnelView _airTunnelViewPrefab;

    public override ViewEntity View => _airTunnelView;

    public override void Initialize(Transform parent)
    {
        _airTunnelView = Instantiate(_airTunnelViewPrefab, parent);
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
