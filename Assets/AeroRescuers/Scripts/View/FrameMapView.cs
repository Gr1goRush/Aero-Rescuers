using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class FrameMapView : ViewEntity
{
    private Transform _transform;

    [SerializeField] private RectTransform _rectTransform;

    public float Width => _rectTransform.rect.width;
    public float Height => _rectTransform.rect.height;
    public Vector3 Position => _transform.position;
    public ReactiveCommand<FrameMapEntity> OffsetFrameBack = new();

    private void Awake()
    {
        _transform = transform;
    }

    private void OnValidate()
    {
        if (_rectTransform == null)
            _rectTransform = GetComponent<RectTransform>();
    }
}
