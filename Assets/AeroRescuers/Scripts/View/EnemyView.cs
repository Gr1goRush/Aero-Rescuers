using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : ViewEntity
{
    [SerializeField] private float _damage;
    [SerializeField] private csDestroyEffect _csDestroyEffect;

    public float Damage => _damage;

    public void CreateDestroyEffect(Transform parent)
    {
        var destroyEffect = Instantiate(_csDestroyEffect, parent);
        destroyEffect.transform.position = new Vector3(destroyEffect.transform.position.x, destroyEffect.transform.position.y, 0);
    }
}
