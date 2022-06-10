using UnityEngine;
using System.Collections;

public class Touch : MonoBehaviour
{
    public static Vector2 clickPos;

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.15f);
        while (true)
        {
            clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            yield return null;
        }
    }

    void OnMouseDown()
    {
        StartCoroutine(Wait());
    }

    void OnMouseUp()
    {
        StopAllCoroutines();
        clickPos = Vector2.zero;
    }
}
