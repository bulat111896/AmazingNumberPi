using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GridLayouts : MonoBehaviour
{
    public GameObject info;
    public Text progress;
    public Button back, next, backThree, nextThree;
    public Audio audio;
    public string pi;
    public int startNumber;
    int list, cells;

    IEnumerator Info()
    {
        info.SetActive(true);
        audio.PlayError();
        yield return new WaitForSeconds(3);
        info.SetActive(false);
    }

    public void Click(Transform button)
    {
        StopAllCoroutines();
        StartCoroutine(Info());
        if (button.GetChild(0).GetComponent<Text>().text != "")
        {
            if (PlayerPrefs.GetInt("Language") == 0)
                info.transform.GetChild(0).GetComponent<Text>().text = "Number " + (list * cells + button.GetSiblingIndex() + 1 + button.parent.GetSiblingIndex() * transform.childCount + startNumber) + " after the decimal point – " + button.GetChild(0).GetComponent<Text>().text;
            else
                info.transform.GetChild(0).GetComponent<Text>().text = "Число №" + (list * cells + button.GetSiblingIndex() + 1 + button.parent.GetSiblingIndex() * transform.childCount + startNumber) + " после запятой – " + button.GetChild(0).GetComponent<Text>().text;
        }
        else
        {
            if (PlayerPrefs.GetInt("Language") == 0)
                info.transform.GetChild(0).GetComponent<Text>().text = "There is nothing";
            else
                info.transform.GetChild(0).GetComponent<Text>().text = "Ничего нет";
        }
    }

    public void Next(int value)
    {
        list = Mathf.Clamp(list + value, 0, pi.Length / cells);
        progress.text = list + "/" + (pi.Length / cells);
        next.interactable = list == (pi.Length / cells) ? false : true;
        nextThree.interactable = next.interactable;
        back.interactable = (list == 0) ? false : true;
        backThree.interactable = back.interactable;
        SetNumber();
    }

    public void SetNumber()
    {
        for (int i = 0; i < transform.childCount; i++)
            for (int j = 0; j < transform.childCount; j++)
                transform.GetChild(i).GetChild(j).GetChild(0).GetComponent<Text>().text = "";

        for (int i = 0; i < transform.childCount; i++)
            for (int j = 0; j < transform.childCount; j++)
                if (pi.Length > cells * list + j + i * transform.childCount)
                    transform.GetChild(i).GetChild(j).GetChild(0).GetComponent<Text>().text = pi[cells * list + j + i * transform.childCount].ToString();
                else
                    return;
    }

    void Awake()
    {
        cells = transform.childCount * transform.GetChild(0).childCount;
    }

    void OnDisable()
    {
        list = 0;
    }
}