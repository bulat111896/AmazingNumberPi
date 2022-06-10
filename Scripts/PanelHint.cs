using UnityEngine;
using System.Collections;

public class PanelHint : MonoBehaviour
{
    public Audio _audio;
    public Animation flare;
    public Language language;
    int indexSave;

    IEnumerator Wait()
    {
        while (true)
        {
            flare.Play();
            yield return new WaitForSeconds(5);
        }
    }

    public void OpenURL(int index)
    {
        if (index < 2)
        {
            indexSave = index;
        }
        else
        {
            if (indexSave == 0)
            {
                Application.OpenURL("https://play.google.com/store/apps/dev?id=6652204215363498616");
            }
            else if (indexSave == 1)
            {
                Application.OpenURL("https://play.google.com/store/apps/details?id=com.funnycloudgames.pi");
            }
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            gameObject.SetActive(false);
    }

    void OnEnable()
    {
        _audio.PlayOpen();
        StartCoroutine(Wait());
    }

    void OnDisable()
    {
        _audio.PlayClose();
        StopAllCoroutines();
    }
}