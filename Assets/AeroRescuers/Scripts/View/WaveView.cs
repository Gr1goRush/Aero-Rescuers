using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveView : EnemyView
{
    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void OnCollisionStay2D(Collision2D collision)
    {
        var wave = collision.gameObject.GetComponent<WaveView>();
        if (wave != null)
            transform.localPosition = transform.localPosition + new Vector3(0.1f, 0, 0);
    }
}
