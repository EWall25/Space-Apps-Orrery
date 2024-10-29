using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailRendererCollider : MonoBehaviour
{
    public float ttl;
    public GameObject father;

    void Start()
    {
        StartCoroutine(DestroySelf());
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(ttl);
        Destroy(gameObject);
    }
}
