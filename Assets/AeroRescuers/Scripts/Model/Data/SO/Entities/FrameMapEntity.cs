using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FrameMapEntity", menuName = "ScriptableObject/FrameMapEntity")]
public class FrameMapEntity : Entity
{
    private FrameMapView _frameMapView;
    private FrameMapController _frameMapController;

    [SerializeField] private FrameMapView _frameMapPreafb;
    [SerializeField] private List<Entity> _enemyPrefabs;
    [SerializeField] private float _distanceOffset;

    public FrameMapController Controller => _frameMapController;
    public override ViewEntity View => _frameMapView;

    public override void Initialize(Transform parent)
    {
        _frameMapView = Instantiate(_frameMapPreafb, parent);

        _frameMapController = new FrameMapController(_frameMapView);
        InitializeEnemy();
    }

    public override void FixedUpdate()
    {
        if(_frameMapView.Position.x < _distanceOffset)
            _frameMapView.OffsetFrameBack.Execute(this);
    }

    public override void AddObjectDisposable()
    {
        ManagerUniRx.AddObjectDisposable(_frameMapView.OffsetFrameBack);
    }

    public override void Dispose()
    {
        ManagerUniRx.Dispose(_frameMapView.OffsetFrameBack);
    }

    public void InitializeEnemy()
    {
        var count = _enemyPrefabs.Count;

        for (int i = 0; i < count; i++)
        {
            var x = Random.Range(- _frameMapView.Width / 4, _frameMapView.Width / 4);
            float y = (_frameMapView.Height / 2) - 10;

            var indexEnemy = Random.Range(0, _enemyPrefabs.Count);

            if (_enemyPrefabs[indexEnemy] is WaveEntity)
                y = Random.Range(-_frameMapView.Height / 4, -_frameMapView.Height / 2);

            var wave = Instantiate(_enemyPrefabs[indexEnemy]);
            wave.Initialize(_frameMapView.transform);
            wave.View.transform.localPosition = new Vector3(x, y, 0);
        }
    }
}
