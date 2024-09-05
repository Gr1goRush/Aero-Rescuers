using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private List<FrameMapEntity> _framesMap;
    private Vector3 _lastFrameMapPosition;

    public LevelController()
    {
        _framesMap = new List<FrameMapEntity>();
    }

    public void AddFrameMapEntity(FrameMapEntity frameMapEntity)
        => _framesMap.Add(frameMapEntity);

    public void SetLastFrameMapEntity(Vector3 lastFrameMapPosition)
        => _lastFrameMapPosition = lastFrameMapPosition;

    public void ChangeLastFrameMap(FrameMapView frameMapView)
    {
        frameMapView.transform.localPosition = _lastFrameMapPosition + new Vector3(frameMapView.Width, 0, 0);
        _lastFrameMapPosition = frameMapView.transform.localPosition;
    }

    public void FixedUpdateFramesMap()
    {
        foreach (var frameMap in _framesMap)
            frameMap.FixedUpdate();
    }
}
