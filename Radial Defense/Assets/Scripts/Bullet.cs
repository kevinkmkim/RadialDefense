using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(WaitAndDestroy());
    }

    // public GameObject hitEffect;
    void OnCollisionEnter2D(Collision2D collision)
    {
        // GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        // Destroy(effect, 5f);
        Destroy(gameObject);
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
