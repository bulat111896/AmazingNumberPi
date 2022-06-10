// AppData\LocalLow\FunnyCloudGames\PI

using UnityEngine;
using System.Net;
using System.ComponentModel;
using UnityEngine.UI;
using System.IO;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using System.Text;

public class Downloader : MonoBehaviour
{
    public Dropdown dropdown;
    public Slider progressBar;
    public Text progressText, text, dropdownLabel;
    public GameObject downloadButton, cancelButton, сompleteButton, buyButton, error;
    public Audio audio;
    public RawImage fill;
    WebClient client;
    string[] downloadUrl = new string[12] { "https://getfile.dokpub.com/yandex/get/https://disk.yandex.ru/i/1GyDVsr3ZvIWXg",    // 1 000 000
                                           "https://getfile.dokpub.com/yandex/get/https://disk.yandex.ru/d/kDFH-ZWqvASVpg",    // 10 000 000

                                           "https://getfile.dokpub.com/yandex/get/https://disk.yandex.ru/d/3HhIuILh9g1uVw",     // 0-100
                                           "https://getfile.dokpub.com/yandex/get/https://disk.yandex.ru/d/i2s6U1cenW0-uQ",     // 100-200
                                           "https://getfile.dokpub.com/yandex/get/https://disk.yandex.ru/d/Ulc1OiEhI_qIYw",     // 200-300
                                           "https://getfile.dokpub.com/yandex/get/https://disk.yandex.ru/d/viih9WSTs_BIyQ",     // 300-400
                                           "https://getfile.dokpub.com/yandex/get/https://disk.yandex.ru/d/yDFqSkslh8ifTw",     // 400-500
                                           "https://getfile.dokpub.com/yandex/get/https://disk.yandex.ru/d/Zgd2lc18khAuAQ",     // 500-600
                                           "https://getfile.dokpub.com/yandex/get/https://disk.yandex.ru/d/coiihZFHzv1VJA",     // 600-700
                                           "https://getfile.dokpub.com/yandex/get/https://disk.yandex.ru/d/_NPhK4X9vSBKyg",     // 700-800
                                           "https://getfile.dokpub.com/yandex/get/https://disk.yandex.ru/d/uTsBSBfndu7apg",     // 800-900
                                           "https://getfile.dokpub.com/yandex/get/https://disk.yandex.ru/d/NNL534q-dXGEgQ" };     // 900-1000
    float progress;
    static int dropdownValue;
    int number, updateDropdownLevel;

    IEnumerator UpdateFrame()
    {
        yield return null;
        progress = 0;
        progressBar.value = progress;
        progressText.text = progress + "%";
    }

    void SceneReload()
    {
        StopAllCoroutines();
        StartCoroutine(UpdateFrame());

        сompleteButton.SetActive(PlayerPrefs.HasKey("Download") || PlayerPrefs.HasKey("Buy"));
        downloadButton.SetActive(true);
        downloadButton.GetComponent<Button>().interactable = isRedownload();

        dropdown.interactable = true;
        buyButton.GetComponent<Button>().interactable = true;

        cancelButton.SetActive(false);
        cancelButton.GetComponent<Button>().interactable = true;

        progress = 0;
        progressBar.value = progress;
        progressText.text = progress + "%";

        downloadButton.GetComponent<Button>().interactable = isRedownload();

        if (PlayerPrefs.HasKey("dropdownLevel"))
            DownloadFile();
    }

    bool isRedownload()
    {
        return (dropdownValue == 0 && PlayerPrefs.GetInt("Download") != 1000000) ||
            (dropdownValue == 1 && PlayerPrefs.GetInt("Download") != 10000000) ||
            (dropdownValue == 2 && PlayerPrefs.GetInt("Download") != 100000000) ||
            (dropdownValue == 3 && PlayerPrefs.GetInt("Download") != 300000000) ||
            (dropdownValue == 4 && PlayerPrefs.GetInt("Download") != 500000000) ||
            (dropdownValue == 5 && PlayerPrefs.GetInt("Download") != 1000000000) ||
            (dropdownValue == 6 && PlayerPrefs.HasKey("Download"));
    }

    public void SaveDropdownValue()
    {
        if (dropdown.value != dropdownValue)
        {
            dropdownValue = dropdown.value;
            downloadButton.GetComponent<Button>().interactable = isRedownload();
        }
    }

    public void SetLanguage()
    {
        if (PlayerPrefs.GetInt("Language") == 0)
        {
            text.text = "Choose the maximum number of digits of π to download";
            dropdown.options[0].text = "1 million (960kb)";
            dropdown.options[1].text = "10 million (9,37mb)";
            dropdown.options[2].text = "100 million (93,7mb)";
            dropdown.options[3].text = "300 million (281mb)";
            dropdown.options[4].text = "500 million (468mb)";
            dropdown.options[5].text = "1 billion (976mb)";
            dropdown.options[6].text = "Remove all";
            dropdownLabel.text = dropdown.options[0].text;
            downloadButton.transform.GetChild(0).GetComponent<Text>().text = "Download";
            cancelButton.transform.GetChild(0).GetComponent<Text>().text = "Cancel";
            сompleteButton.transform.GetChild(0).GetComponent<Text>().text = "Complete";
            buyButton.transform.GetChild(0).GetComponent<Text>().text = "Use the internet";
        }
        else
        {
            text.text = "Выбери максимальное кол-во знаков числа π  для скачавания";
            dropdown.options[0].text = "1 млн (960кб)";
            dropdown.options[1].text = "10 млн (9,37мб)";
            dropdown.options[2].text = "100 млн (93,7мб)";
            dropdown.options[3].text = "300 млн (281мб)";
            dropdown.options[4].text = "500 млн (468мб)";
            dropdown.options[5].text = "1 млрд (976мб)";
            dropdown.options[6].text = "Удалить всё";
            dropdownLabel.text = dropdown.options[0].text;
            downloadButton.transform.GetChild(0).GetComponent<Text>().text = "Скачать";
            cancelButton.transform.GetChild(0).GetComponent<Text>().text = "Отмена";
            сompleteButton.transform.GetChild(0).GetComponent<Text>().text = "Завершить";
            buyButton.transform.GetChild(0).GetComponent<Text>().text = "Продолжить с интернетом";
        }
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("Download") || PlayerPrefs.HasKey("Buy"))
            сompleteButton.SetActive(false);
        SetLanguage();
        
        if (PlayerPrefs.HasKey("Buy"))
            buyButton.SetActive(false);

        downloadButton.GetComponent<Button>().interactable = isRedownload();

        if (!new DirectoryInfo(Application.persistentDataPath).Exists)
            Directory.CreateDirectory(Application.persistentDataPath);
        if (File.Exists(Application.persistentDataPath + "/pi"))
            File.Delete(Application.persistentDataPath + "/pi");

        PlayerPrefs.DeleteKey("dropdownLevel");
    }

    IEnumerator DeleteAndDownload()
    {
        if (PlayerPrefs.GetInt("dropdownLevel") < 1)
        {
            var files = Directory.EnumerateFiles(Application.persistentDataPath, "*.pi", SearchOption.AllDirectories);

            if (files != null)
            {
                progressText.text = PlayerPrefs.GetInt("Language") == 0 ? " Deleting Old Files..." : " Удаление старых файлов...";
                yield return null;
                int Download = PlayerPrefs.GetInt("Download"), i = 0, max = 0;
                foreach (var item in files)
                {
                    max++;
                }
                foreach (var item in files)
                {
                    try
                    {
                        File.Delete(item);
                    }
                    catch (Exception)
                    {
                        CancelDownload();
                        yield break;
                    }
                    if (i % number / 50000000 == 0)
                    {
                        progress = (float)i / max * 100;
                        progressBar.value = progress;
                        yield return null;
                    }
                    i++;
                }
            }

            PlayerPrefs.DeleteKey("Download");
        }
        cancelButton.GetComponent<Button>().interactable = true;

        progress = 100;
        progressBar.value = progress;
        if (dropdownValue < 6)
        {
            progressText.text = PlayerPrefs.GetInt("Language") == 0 ? " Connection..." : " Соединение...";
            yield return null;

            client = new WebClient();
            client.DownloadFileAsync(new Uri(downloadUrl[dropdownValue < 3 ? dropdownValue : (2 + PlayerPrefs.GetInt("dropdownLevel"))]), Application.persistentDataPath + "/pi");
            updateDropdownLevel = PlayerPrefs.GetInt("dropdownLevel") + 1;
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCompleted);
        }
        else
        {
            progressText.text = PlayerPrefs.GetInt("Language") == 0 ? " Deletion completed..." : " Удаление завершено...";
            yield return new WaitForSeconds(0.5f);
            SceneReload();
        }
    }

    IEnumerator ProgressBarAnimation()
    {
        while (true)
        {
            Rect rect = fill.uvRect;
            rect.x -= 2 * Time.deltaTime;
            rect.width = progress * 0.12f;
            fill.uvRect = rect;
            yield return null;
        }
    }

    public void DownloadFile()
    {
        error.SetActive(false);

        dropdown.interactable = false;
        downloadButton.SetActive(false);
        сompleteButton.SetActive(false);
        buyButton.GetComponent<Button>().interactable = false;

        cancelButton.SetActive(true);
        cancelButton.GetComponent<Button>().interactable = false;

        progress = 0;
        progressBar.value = progress;
        progressText.text = progress + "%";

        if (dropdownValue == 0)
            number = 10000;
        else if (dropdownValue == 1)
            number = 100000;
        else if (dropdownValue == 2)
            number = 1000000;
        else if (dropdownValue == 3)
            number = 3000000;
        else if (dropdownValue == 4)
            number = 5000000;
        else if (dropdownValue == 5)
            number = 10000000;

        StartCoroutine(ProgressBarAnimation());
        StartCoroutine(DeleteAndDownload());
    }

    IEnumerator Save()
    {
        string pi = File.ReadAllText(Application.persistentDataPath + "/pi");
        if (pi != "")
        {
            progressText.text = (updateDropdownLevel + "/" + Mathf.Clamp(number / 1000000, 1, 100)) + " |" + (PlayerPrefs.GetInt("Language") == 0 ? " Saving..." : " Сохранение...");
            yield return null;
            File.Delete(Application.persistentDataPath + "/pi");

            int dropdownLevel = PlayerPrefs.GetInt("dropdownLevel");
            for (int i = 0; i < number * 100 / 50000; i++)
            {
                string path = Application.persistentDataPath + "/" + BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes("pi" + (dropdownLevel * 2000 + i).ToString()))).Replace("-", string.Empty) + ".pi";

                try
                {
                    File.AppendAllText(path, pi.Substring(i * 50000, 50000));
                }
                catch (Exception)
                {
                    PlayerPrefs.SetInt("dropdownLevel", PlayerPrefs.GetInt("dropdownLevel") + 1);
                    if ((PlayerPrefs.GetInt("dropdownLevel") == 3 && dropdownValue == 3) || (PlayerPrefs.GetInt("dropdownLevel") == 5 && dropdownValue == 4) || (PlayerPrefs.GetInt("dropdownLevel") == 10 && dropdownValue == 5))
                    {
                        PlayerPrefs.SetInt("Download", number * 100);
                        PlayerPrefs.DeleteKey("dropdownLevel");
                    }

                    SceneReload();
                }

                if (dropdownValue == 0 || dropdownValue == 1 || dropdownValue == 2)
                {
                    PlayerPrefs.SetInt("Download", number * 100);
                }

                if (i % number / 50000 == 0)
                {
                    progress = (float)i / number * 50000 * res();
                    progressBar.value = progress;
                    yield return null;
                }
            }
        }
        else
        {
            error.SetActive(true);
            audio.PlayError();
        }

        SceneReload();
    }

    void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
    {
        client.Dispose();
        StartCoroutine(Save());
    }

    int res()
    {
        if (dropdownValue == 3)
            return 3;
        else if (dropdownValue == 4)
            return 5;
        else if (dropdownValue == 5)
            return 10;
        else
            return 1;
    }

    void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        progress = (float)e.BytesReceived / Mathf.Clamp(number, 0, 1000000);
        progressBar.value = progress;
        progressText.text = (updateDropdownLevel + "/" + Mathf.Clamp(number / 1000000, 1, 100)) + " | " + (int)progress + "%";
    }

    public void CancelDownload()
    {
        if (client != null)
            client.CancelAsync();
        SceneManager.LoadScene(1);
    }

    public void Close()
    {
        if (client != null)
            client.CancelAsync();
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && сompleteButton.activeSelf)
            SceneManager.LoadScene(0);
    }
}