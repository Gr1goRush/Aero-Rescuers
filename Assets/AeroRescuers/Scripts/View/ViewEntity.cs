using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewEntity : MonoBehaviour
{
    public void UpdateLocalPosition(Vector3 localPosition)
        => transform.localPosition = localPosition;
}
