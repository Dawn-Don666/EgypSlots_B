using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// »Ò³¾Á£×Ó
/// </summary>
public class Dust : MonoBehaviour
{
    public ParticleSystem dust1;
    public ParticleSystem dust2;

    public void Start()
    {
        StartCoroutine(DustRestart());
    }

    public IEnumerator DustRestart()
    {
        while (true) 
        {
            yield return new WaitForSeconds(Random.Range(5f, 7f));
            dust1.Play();
            yield return new WaitForSeconds(Random.Range(0f, 3f));
            dust2.Play();
        }
    }
}
