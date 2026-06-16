using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ҳ�����
/// </summary>
public class Task : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("dust1")]    public ParticleSystem Mood1;
[UnityEngine.Serialization.FormerlySerializedAs("dust2")]    public ParticleSystem Mood2;

    public void Start()
    {
        StartCoroutine(TaskEnvelop());
    }

    public IEnumerator TaskEnvelop()
    {
        while (true) 
        {
            yield return new WaitForSeconds(Random.Range(5f, 7f));
            Mood1.Play();
            yield return new WaitForSeconds(Random.Range(0f, 3f));
            Mood2.Play();
        }
    }
}
