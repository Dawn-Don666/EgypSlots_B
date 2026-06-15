using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public Vector2 offset;

    private void LateUpdate()
    {
        if (target == null) return;
        Vector3 newPos = target.localPosition + new Vector3(offset.x, offset.y, 0);
        transform.localPosition = newPos;
    }
}
