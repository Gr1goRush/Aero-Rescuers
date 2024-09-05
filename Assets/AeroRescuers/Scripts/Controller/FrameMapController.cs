using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameMapController
{
    private FrameMapView _frameMapView;

    public FrameMapController(FrameMapView frameMapView)
    {
        _frameMapView = frameMapView;
    }

    public void RotateBack()
        => _frameMapView.transform.Rotate(0, 180, 0);

    public void UpdatePosition(Vector3 newPosition)
        => _frameMapView.transform.localPosition = newPosition;
}
