using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkydiverView : ViewEntity
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private csDestroyEffect _csDestroyEffect;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    public void CreateDestroyEffect()
    {
        var destroyEffect = Instantiate(_csDestroyEffect, transform.position, Quaternion.identity);
        destroyEffect.transform.position = new Vector3(destroyEffect.transform.position.x, destroyEffect.transform.position.y, 0);
    }

    private void OnBecameVisible()
    {
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody.gravityScale = 0.1f;
    }

    private void OnValidate()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();
    }
}
