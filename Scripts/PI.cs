using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine.Monetization;

public class PI : MonoBehaviour
{
    public Text result, arrow, numberText, inputFieldText, startText, endText, text;
    public GameObject bg, message, inputField, start, end, wifi, errorText;
    public GameObject[] obj, rangeObj;
    public Audio audio;
    public ParticleSystem confetti;
    public GridLayouts grid;
    WWW www;
    int number, startNumber, endNumber;
    string www_result;
    bool isRange;
    byte isPos;

    IEnumerator Ads()
    {
        if (PlayerPrefs.GetInt("Views") == 7)
        {
            yield return new WaitForSeconds(1);
            while (true)
            {
                if (Monetization.IsReady("video"))
                {
                    (Monetization.GetPlacementContent("video") as ShowAdPlacementContent).Show();
                    PlayerPrefs.SetInt("Views", 0);
                    yield break;
                }
                Monetization.Initialize("4111737", false);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    public void StartIsConnected()
    {
        StartCoroutine(IsConnected());
    }

    IEnumerator IsConnected()
    {
        WWWForm form = new WWWForm();
        form.AddField("pi", "1");
        WWW www2 = new WWW("f0441928.xsph.ru", form);
        yield return www2;
        if (www2.text == "true")
        {

        }
        else if (www2.text == "")
        {
            Wifi();
        }
    }

    void Awake()
    {
        if (!PlayerPrefs.HasKey("Language"))
        {
            if (Application.systemLanguage == SystemLanguage.Russian)
                PlayerPrefs.SetInt("Language", 1);
        }
        if (!PlayerPrefs.HasKey("Download") && !PlayerPrefs.HasKey("Buy"))
            SceneManager.LoadScene(1);
        StartIsConnected();

        if (!PlayerPrefs.HasKey("Buy"))
            Monetization.Initialize("4111737", false);
    }

    public void Discharge()
    {
        number = 0;
        startNumber = 0;
        endNumber = 0;
    }

    public void Download()
    {
        SceneManager.LoadScene(1);
    }

    public void Range()
    {
        isRange = !isRange;
        inputField.SetActive(!isRange);
        start.SetActive(isRange);
        end.SetActive(isRange);
        arrow.text = isRange ? "▲" : "▼";
    }

    public void OneEndEdit()
    {
        inputFieldText.text = inputFieldText.text.Replace(" ", "").Replace(",", "");
        if (inputFieldText.text != "")
        {
            if (!PlayerPrefs.HasKey("Buy"))
            {
                if (Convert.ToUInt64(inputFieldText.text) > 1000000000)
                {
                    inputFieldText.text = 1000000000.ToString();
                    StopCoroutine("ErrorText");
                    StartCoroutine("ErrorText");
                }
                else if (Convert.ToInt32(inputFieldText.text) > (PlayerPrefs.HasKey("Download") ? PlayerPrefs.GetInt("Download") : 1000000000))
                {
                    StopCoroutine("ErrorText");
                    StartCoroutine("ErrorText");
                }
                number = Mathf.Clamp(Convert.ToInt32(inputFieldText.text), 1, PlayerPrefs.HasKey("Download") ? PlayerPrefs.GetInt("Download") : 1000000000);
            }
            else
            {
                if (Convert.ToUInt64(inputFieldText.text) > 1000000000)
                    inputFieldText.text = 1000000000.ToString();
                number = Mathf.Clamp(Convert.ToInt32(inputFieldText.text), 1, 1000000000);
            }

            inputFieldText.text = number.ToString();
            number -= 1;
        }
        inputFieldText.text = Keyboard.BeautifulNumber(inputFieldText.text);
    }

    public void _Start()
    {
        startText.text = startText.text.Replace(" ", "").Replace(",", "");
        if (startText.text != "")
        {
            if (!PlayerPrefs.HasKey("Buy"))
            {
                if (Convert.ToUInt64(startText.text) > 1000000000)
                {
                    startText.text = 1000000000.ToString();
                    StopCoroutine("ErrorText");
                    StartCoroutine("ErrorText");
                }
                else if (Convert.ToInt32(startText.text) > (PlayerPrefs.HasKey("Download") ? PlayerPrefs.GetInt("Download") : 1000000000))
                {
                    StopCoroutine("ErrorText");
                    StartCoroutine("ErrorText");
                }
                startNumber = Mathf.Clamp(Convert.ToInt32(startText.text), 1, PlayerPrefs.HasKey("Download") ? PlayerPrefs.GetInt("Download") : 1000000000);
            }
            else
            {
                if (Convert.ToUInt64(startText.text) > 1000000000)
                    startText.text = 1000000000.ToString();
                startNumber = Mathf.Clamp(Convert.ToInt32(startText.text), 1, 1000000000);
            }
            if (startNumber > endNumber && endText.text != "")
            {
                endNumber = startNumber;
                endText.text = endNumber.ToString();
            }
            else if (endNumber - startNumber > 10000)
            {
                endNumber = startNumber + 10000;
                endText.text = endNumber.ToString();
            }
            startText.text = startNumber.ToString();
            startNumber -= 1;
        }
        startText.text = Keyboard.BeautifulNumber(startText.text);
    }

    public void _End()
    {
        endText.text = endText.text.Replace(" ", "").Replace(",", "");
        if (endText.text != "")
        {
            if (!PlayerPrefs.HasKey("Buy"))
            {
                if (Convert.ToUInt64(endText.text) > 1000000000)
                {
                    endText.text = 1000000000.ToString();
                    StopCoroutine("ErrorText");
                    StartCoroutine("ErrorText");
                }
                else if (Convert.ToInt32(endText.text) > (PlayerPrefs.HasKey("Download") ? PlayerPrefs.GetInt("Download") : 1000000000))
                {
                    StopCoroutine("ErrorText");
                    StartCoroutine("ErrorText");
                }
                endNumber = Mathf.Clamp(Convert.ToInt32(endText.text), 1, PlayerPrefs.HasKey("Download") ? PlayerPrefs.GetInt("Download") : 1000000000);
            }
            else
            {
                if (Convert.ToUInt64(endText.text) > 1000000000)
                    endText.text = 1000000000.ToString();
                endNumber = Mathf.Clamp(Convert.ToInt32(endText.text), 1, 1000000000);
            }
            if (startNumber >= endNumber)
                endNumber = startNumber + 1;
            else if (endNumber - startNumber > 10000 && startText.text != "")
                endNumber = startNumber + 10000 + 1;
            endText.text = endNumber.ToString();
            endNumber -= 1;
        }
        endText.text = Keyboard.BeautifulNumber(endText.text);
    }

    public void Click()
    {
        message.SetActive(false);
        errorText.SetActive(false);
        if (!isRange)
        {
            if (inputFieldText.text != "")
            {
                OneEndEdit();
                if (PlayerPrefs.HasKey("Buy") && PlayerPrefs.GetInt("Download") <= number)
                    StartCoroutine(GetPIWWW());
                else
                    GetPI();
            }
            else
                StartCoroutine(Message());
        }
        else
        {
            if (startText.text != "" && endText.text != "")
            {
                _Start();
                _End();
                if (PlayerPrefs.HasKey("Buy") && PlayerPrefs.GetInt("Download") <= endNumber - startNumber)
                    StartCoroutine(GetRangePIWWW());
                else
                    GetRangePI();
            }
            else
                StartCoroutine(Message());
        }
    }

    public void SetActive()
    {
        for (int i = 0; i < obj.Length; i++)
            obj[i].SetActive(!obj[i].activeSelf);
        if (!obj[0].activeSelf)
        {
            inputField.SetActive(false);
            start.SetActive(false);
            end.SetActive(false);
        }
        else
        {
            if (isRange)
            {
                inputField.SetActive(false);
                start.SetActive(true);
                end.SetActive(true);
            }
            else
            {
                inputField.SetActive(true);
                start.SetActive(false);
                end.SetActive(false);
            }
            grid.gameObject.SetActive(false);
            numberText.gameObject.SetActive(false);
            text.gameObject.SetActive(false);
            for (int i = 0; i < rangeObj.Length; i++)
                rangeObj[i].SetActive(false);
        }
        wifi.SetActive(false);
    }

    public void SetResult(string pi)
    {
        numberText.gameObject.SetActive(true);
        text.gameObject.SetActive(true);

        int index = 0;
        if (number % 50000 != 0)
            index = number % 50000;
        else if ((number + 1) % 50000 != 1)
            index = 50000;
        SetResultText();
        numberText.text = pi[index].ToString();
        if (isPos == 0)
        {
            result.transform.position -= new Vector3(0, 30, 0);
            isPos = 2;
        }
    }

    public void SetResultText()
    {
        result.text = PlayerPrefs.GetInt("Language") == 0 ? "Result:" : "Результат:";
        text.text = PlayerPrefs.GetInt("Language") == 0 ? (inputFieldText.text + " decimal places") : (inputFieldText.text + " знак после запятой");
    }

    public void SetRange()
    {
        grid.gameObject.SetActive(true);
        for (int i = 0; i < rangeObj.Length; i++)
            rangeObj[i].SetActive(true);

        int index = 0;
        if (startNumber % 50000 != 0)
            index = startNumber % 50000;
        else if ((startNumber + 1) % 50000 != 1)
            index = 50000;

        SetResultText();
        grid.pi = www_result.Substring(index, endNumber - startNumber + 1);
        grid.startNumber = startNumber;
        grid.Next(0);
        if (isPos > 0)
        {
            result.transform.position += new Vector3(0, 30, 0);
            isPos = 0;
        }
    }

    IEnumerator ErrorText()
    {
        if (errorText.activeSelf)
        {
            errorText.SetActive(false);
            yield return null;
        }
        errorText.SetActive(true);
        yield return new WaitForSeconds(3f);
        errorText.SetActive(false);
    }

    IEnumerator Message()
    {
        message.SetActive(true);
        audio.PlayError();
        int count = 0;
        while (count < 100)
        {
            yield return new WaitForSeconds(0.03f);
            count++;
        }
        message.SetActive(false);
    }

    IEnumerator Wait()
    {
        wifi.SetActive(true);
        yield return new WaitForSeconds(5);
        wifi.SetActive(false);
    }

    void Wifi()
    {
        StopAllCoroutines();
        StartCoroutine(Wait());
    }

    IEnumerator GetPIWWW()
    {
        bg.SetActive(true);
        www = new WWW("f0441928.xsph.ru/files/pi" + (number / 50000 + 1) + ".txt");
        yield return www;
        bg.SetActive(false);
        if (www.text != "")
        {
            confetti.Play();
            if (PlayerPrefs.GetInt("Sound") == 1)
                confetti.GetComponent<AudioSource>().Play();
            SetActive();
            SetResult(www.text);
        }
        else
            Wifi();
    }

    IEnumerator GetRangePIWWW()
    {
        bg.SetActive(true);
        www_result = "";
        yield return null;
        www = new WWW("f0441928.xsph.ru/files/pi" + (startNumber / 50000 + 1) + ".txt");
        yield return www;
        if (www.text != "")
            www_result += www.text;
        else
        {
            bg.SetActive(false);
            Wifi();
            yield break;
        }
        if (startNumber / 50000 != endNumber / 50000)
        {
            www = new WWW("f0441928.xsph.ru/files/pi" + (endNumber / 50000 + 1) + ".txt");
            yield return www;
            if (www.text != "")
                www_result += www.text;
            else
            {
                bg.SetActive(false);
                Wifi();
                yield break;
            }
        }

        bg.SetActive(false);
        confetti.Play();
        if (PlayerPrefs.GetInt("Sound") == 1)
            confetti.GetComponent<AudioSource>().Play();
        SetActive();
        SetRange();
    }

    void GetPI()
    {
        confetti.Play();
        if (PlayerPrefs.GetInt("Sound") == 1)
            confetti.GetComponent<AudioSource>().Play();
        SetActive();

        SetResult(File.ReadAllText(Application.persistentDataPath + "/" + BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes("pi" + (number / 50000)))).Replace("-", string.Empty) + ".pi"));

        PlayerPrefs.SetInt("Views", PlayerPrefs.GetInt("Views") + 1);
        StartCoroutine(Ads());
    }

    void GetRangePI()
    {
        www_result = "";
        www_result += File.ReadAllText(Application.persistentDataPath + "/" + BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes("pi" + (startNumber / 50000)))).Replace("-", string.Empty) + ".pi");
        if (startNumber / 50000 != endNumber / 50000)
            www_result += File.ReadAllText(Application.persistentDataPath + "/" + BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes("pi" + (endNumber / 50000)))).Replace("-", string.Empty) + ".pi");
        confetti.Play();
        if (PlayerPrefs.GetInt("Sound") == 1)
            confetti.GetComponent<AudioSource>().Play();
        SetActive();
        SetRange();

        PlayerPrefs.SetInt("Views", PlayerPrefs.GetInt("Views") + 1);
        StartCoroutine(Ads());
    }
}