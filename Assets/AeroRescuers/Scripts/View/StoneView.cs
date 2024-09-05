using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class StoneView : EnemyView
{
    [SerializeField] private Rigidbody2D _rigidbody;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void OnBecameVisible()
    {
        Fall();
    }

    private async void Fall()
    {
        await Task.Delay(1500);
        if (_rigidbody != null)
        {
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody.gravityScale = 0.5f;
        }
    }

    private void OnValidate()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();
    }
}
