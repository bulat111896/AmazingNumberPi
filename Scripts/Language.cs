using UnityEngine;
using UnityEngine.UI;

public class Language : MonoBehaviour
{
    public Sprite[] sprites;
    public Text[] texts, inputFieldsText;
    public Text next, toGooglePlay, yes, no;

    public void Click()
    {
        PlayerPrefs.SetInt("Language", PlayerPrefs.GetInt("Language") == 0 ? 1 : 0);
        if (texts[0] != null)
        {
            if (PlayerPrefs.GetInt("Language") == 0)
                for (int i = 0; i < 3; i++)
                    inputFieldsText[i].text = inputFieldsText[i].text.Replace(" ", ",");
            else
                for (int i = 0; i < 3; i++)
                    inputFieldsText[i].text = inputFieldsText[i].text.Replace(",", " ");
        }
        SetLanguage();
    }

    void SetLanguage()
    {
        if (PlayerPrefs.GetInt("Language") == 0)
        {
            GetComponent<Image>().sprite = sprites[0];
            if (texts[0] != null)
            {
                texts[0].text = "The text box cannot be empty";
                texts[1].text = "Search";
                texts[4].text = "Enter the number";
                texts[5].text = "Range";
                texts[6].text = "Seed number";
                texts[7].text = "Final number";
                texts[8].text = "The entered number is greater than the downloaded number of characters";
            }
            texts[2].text = "Reference";
            texts[3].text = "The number Pi (π) is an irrational number (its decimal representation never ends and is not periodic), which is equal to the ratio of the circumference of the circle to its diameter. This app allows you to find both a specific digit and a range of decimal places out of 1 billion known ones. You need to download the number Pi to your phone and, depending on the free space in memory, specify the appropriate number of downloaded characters. It is also possible to get a number over the network, saving yourself from installing millions of characters to your device. Then, if the Pi number is not downloaded, the app will take it from the Internet. With the number Pi, you can train your memory by learning hundreds of characters, and the lack of advertising makes working in the app as comfortable as possible.";
            next.text = "Find an app";
            toGooglePlay.text = "Go to Google Play?";
            yes.text = "Yes";
            no.text = "No";
        }
        else
        {
            GetComponent<Image>().sprite = sprites[1];
            if (texts[0] != null)
            {
                texts[0].text = "Текстовое поле не может быть пустым";
                texts[1].text = "Поиск";
                texts[4].text = "Введите число";
                texts[5].text = "Диапазон";
                texts[6].text = "Начальное число";
                texts[7].text = "Конечное число";
                texts[8].text = "Введенное число больше загруженного количества знаков";
            }
            texts[2].text = "Справка";
            texts[3].text = "Число Пи (π) – иррациональное число (его десятичное представление никогда не заканчивается и не является периодическим), которое равно отношению длины окружности к её диаметру. Это приложение позволяет найти как конкретную цифру, так и диапазон знаков после запятой из 1 млрд известных. Необходимо загрузить число Пи к себе на телефон и, в зависимости от свободного места в памяти, указать подходящее кол-во скачиваемых знаков. Также есть возможность получать число по сети, избавив себя от установки миллионов знаков к себе на устройство. Тогда, если число Пи не будет скачено, приложение будет брать его из интернета. С помощью числа Пи можно тренировать память, выучив сотни знаков, а отсутствие рекламы делает работу в приложении максимально комфортной.";
            next.text = "Найти приложение";
            toGooglePlay.text = "Перейти в Google Play?";
            yes.text = "Да";
            no.text = "Нет";
        }

        if (texts[0] != null)
            GameObject.Find("Main Camera").GetComponent<PI>().SetResultText();
        else
            GameObject.Find("Main Camera").GetComponent<Downloader>().SetLanguage();
        
    }

    void Start()
    {
        SetLanguage();
    }
}