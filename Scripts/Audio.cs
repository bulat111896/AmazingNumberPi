using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    public AudioClip open, close, click, error, write;
    public AudioSource audioSource;
    AudioSource musicSource;
    public Image music, sound;
    public Sprite[] sprites;
    bool isSound;

    public void PlayOpen()
    {
        audioSource.PlayOneShot(open);
    }

    public void PlayClose()
    {
        audioSource.PlayOneShot(close);
    }

    public void PlayClick()
    {
        audioSource.PlayOneShot(click);
    }

    public void PlayError()
    {
        audioSource.PlayOneShot(error);
    }

    public void PlayWrite()
    {
        audioSource.PlayOneShot(write);
    }

    public void Music()
    {
        if (musicSource.isPlaying)
            musicSource.Pause();
        else
            musicSource.Play();
        music.sprite = sprites[musicSource.isPlaying ? 0 : 1];
        PlayerPrefs.SetInt("Music", musicSource.isPlaying ? 1 : 0);
    }

    public void Sound()
    {
        isSound = !isSound;
        audioSource.volume = isSound ? 1 : 0;
        sound.sprite = sprites[isSound ? 2 : 3];
        PlayerPrefs.SetInt("Sound", isSound ? 1 : 0);
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("Music"))
            PlayerPrefs.SetInt("Music", 1);
        if (!PlayerPrefs.HasKey("Sound"))
            PlayerPrefs.SetInt("Sound", 1);

        musicSource = GetComponent<AudioSource>();
        if (music != null)
            music.sprite = sprites[PlayerPrefs.GetInt("Music") == 1 ? 0 : 1];
        if (PlayerPrefs.GetInt("Music") == 1)
            musicSource.Play();
        sound.sprite = sprites[PlayerPrefs.GetInt("Sound") == 1 ? 2 : 3];
        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            audioSource.volume = 1;
            isSound = true;
        }
    }
}