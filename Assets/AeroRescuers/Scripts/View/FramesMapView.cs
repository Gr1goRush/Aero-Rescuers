using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramesMapView : ViewEntity
{
    private Transform _transform;

    public Transform Transform
    {
        get
        {
            if (_transform == null)
                _transform = transform;

            return _transform;
        }
    }

    private void Start()
    {
        _transform = transform;
    }
}
