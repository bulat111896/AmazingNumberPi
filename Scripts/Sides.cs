using UnityEngine;
using System.Collections;

public class Sides : MonoBehaviour
{
    public GameObject star;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Star(Clone)")
            Destroy(collision.gameObject);
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            Instantiate(star);
            yield return new WaitForSeconds(Random.Range(1f, 4f));
        }
    }

    void Start()
    {
        StartCoroutine(Spawn());
    }
}