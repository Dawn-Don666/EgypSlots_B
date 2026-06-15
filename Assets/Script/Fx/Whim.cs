using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ҳ�����
/// </summary>
public class Whim : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("dust1")]    public ParticleSystem Dual1;
[UnityEngine.Serialization.FormerlySerializedAs("dust2")]    public ParticleSystem Dual2;

    public void Start()
    {
        StartCoroutine(WhimAustere());
    }

    public IEnumerator WhimAustere()
    {
        while (true) 
        {
            yield return new WaitForSeconds(Random.Range(5f, 7f));
            Dual1.Play();
            yield return new WaitForSeconds(Random.Range(0f, 3f));
            Dual2.Play();
        }
    }
}
