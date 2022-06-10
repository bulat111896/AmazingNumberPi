using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour
{
    Rigidbody2D rb;

    IEnumerator StarGravity()
    {
        while (true)
        {
            if (Touch.clickPos != Vector2.zero)
            {
                rb.MovePosition(Touch.clickPos);
                rb.velocity = Vector2.zero;
            }
            yield return new WaitForSeconds(0.02f);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.55f);
        Destroy(transform.GetChild(0).gameObject);
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        rb.AddForce(transform.up * 0.3f, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Star(Clone)")
            rb.AddForce(-(collision.transform.position - transform.position).normalized, ForceMode2D.Impulse);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Wait());
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 10));
        StartCoroutine(StarGravity());
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, 50 * Time.deltaTime);
    }
}