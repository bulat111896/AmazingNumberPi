using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
    public GameObject[] inputField, stick;
    public Text[] text;
    public PI pi;
    public Audio audio;
    int index;
    bool isStart;

    public static string BeautifulNumber(string num)
    {
        num = num.Replace(" ", "").Replace(",", "");
        string resultStr = string.Empty;
        for (int i = num.Length - 1; i >= 0; i--)
        {
            resultStr += num[i];
        }
        resultStr = string.Empty;

        char charLang = (PlayerPrefs.GetInt("Language") == 0) ? ',' : ' ';
        for (int i = 0; i < num.Length; i++)
        {
            if (i == 3 || i == 6 || i == 9)
            {
                resultStr += charLang;
            }
            resultStr += num[num.Length - 1 - i];
        }
        num = string.Empty;
        for (int i = resultStr.Length - 1; i >= 0; i--)
        {
            num += resultStr[i];
        }
        return num;
    }

    IEnumerator Wait()
    {
        while (true)
        {
            stick[index].SetActive(!stick[index].activeSelf);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void OneEndEdit()
    {
        if (isStart)
            pi._Start();
        else
            pi._End();
    }

    public void SetBool(bool value)
    {
        isStart = value;
    }

    public void Write(int value)
    {
        inputField[index].transform.GetChild(0).gameObject.SetActive(false);
        OnDisable();
        if (text[index].text.Replace(" ", "").Length < 10)
        {
            text[index].text += value;

            text[index].text = BeautifulNumber(text[index].text);
            audio.PlayWrite();
        }
    }

    public void Delete()
    {
        if (text[index].text.Length > 0)
        {
            text[index].text = text[index].text.Replace(" ", "").Replace(",", "").Substring(0, text[index].text.Replace(" ", "").Replace(",", "").Length - 1);
            text[index].text = BeautifulNumber(text[index].text);

            if (text[index].text == "")
                inputField[index].transform.GetChild(0).gameObject.SetActive(true);
            audio.PlayWrite();
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            gameObject.SetActive(false);
    }

    void OnEnable()
    {
        index = inputField[0].activeSelf ? 0 : (isStart ? 1 : 2);
        if (text[index].text.Length == 0)
            StartCoroutine(Wait());
    }

    void OnDisable()
    {
        StopAllCoroutines();
        stick[index].SetActive(false);
    }
}