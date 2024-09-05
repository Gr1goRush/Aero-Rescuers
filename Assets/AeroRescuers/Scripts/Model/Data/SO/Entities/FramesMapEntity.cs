using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FramesMapEntity", menuName = "ScriptableObject/FramesMapEntity")]
public class FramesMapEntity : Entity
{
    private FramesMapView _framesMapView;

    [SerializeField] private FramesMapView _framesMapViewPrefab;
    [Header("Скорость движения")]
    [SerializeField] private float _speed;
    [Header("Шаг движения карты")]
    [SerializeField] private float _stepMove;

    public override ViewEntity View => _framesMapView;

    public override void Initialize(Transform parent)
    {
        _framesMapView = Instantiate(_framesMapViewPrefab, parent);
        _framesMapView.transform.SetAsFirstSibling();
    }

    public override void FixedUpdate()
    {
        var newPosition = Vector3.Lerp(_framesMapView.Transform.position, _framesMapView.Transform.position + Vector3.left * _stepMove, _speed);
        _framesMapView.Transform.position = newPosition;
    }

    public override void AddObjectDisposable()
    {

    }

    public override void Dispose()
    {

    }
}
