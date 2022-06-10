using UnityEngine;

public class DropdownSound : MonoBehaviour
{
    public Audio _audio;

    void OnEnable()
    {
        if (PlayerPrefs.GetInt("Sound") == 1)
            _audio.PlayOpen();
    }

    void OnDisable()
    {
        if (PlayerPrefs.GetInt("Sound") == 1)
            _audio.PlayClose();
    }
}