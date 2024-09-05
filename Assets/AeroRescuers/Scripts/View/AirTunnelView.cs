using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirTunnelView : ViewEntity
{
    [SerializeField] private csDestroyEffect _csDestroyEffect;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    public void CreateDestroyEffect()
    {
        var destroyEffect = Instantiate(_csDestroyEffect, transform.position, Quaternion.identity);
        destroyEffect.transform.position = new Vector3(destroyEffect.transform.position.x, destroyEffect.transform.position.y, 0);
    }
}
