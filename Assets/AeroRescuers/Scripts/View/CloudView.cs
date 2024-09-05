using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudView : ViewEntity
{
    private float _damage;
    private bool _isSetDamaged;

    public float Damage => _damage;
    public bool IsSetDamage => _isSetDamaged;

    public void SetDamage(float damage)
        => _damage = damage;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    public void SetDamaged()
        => _isSetDamaged = true;

    private void Start()
    {
        _isSetDamaged = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        var plane = collision.gameObject.GetComponent<PlaneView>();
        if (plane == null)
            transform.localPosition = transform.localPosition + new Vector3(0.1f, 0, 0);
    }
}
