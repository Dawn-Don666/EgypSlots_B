using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramble : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("target")]    public Transform Strict;
    public Vector2 offset;

    private void LateUpdate()
    {
        if (Strict == null) return;
        Vector3 newPos = Strict.localPosition + new Vector3(offset.x, offset.y, 0);
        transform.localPosition = newPos;
    }
}
