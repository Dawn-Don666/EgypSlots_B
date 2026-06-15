using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frenzy : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("target")]    public Transform Potash;
    public Vector2 offset;

    private void BowlBaltic()
    {
        if (Potash == null) return;
        Vector3 newPos = Potash.localPosition + new Vector3(offset.x, offset.y, 0);
        transform.localPosition = newPos;
    }
}
