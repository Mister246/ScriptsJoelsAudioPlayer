using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class ControlPanelScript : MonoBehaviour
{
    static public Text controlPanelText;

    public SpriteLibrary spriteLibrary;
    static public Sprite pauseSprite;
    static public Sprite playSprite;

    static public GameObject pausePlayObject;
    static Image pausePlayButtonImage;
    Button pausePlayButton;
    static bool paused = true; // by default audio is paused

    static public AudioSource audioSource;

    void Start()
    {
        controlPanelText = GetComponentInChildren<Text>();

        spriteLibrary = GetComponent<SpriteLibrary>();
        pauseSprite = spriteLibrary.GetSprite("Pause/Play Icons", "Pause");
        playSprite = spriteLibrary.GetSprite("Pause/Play Icons", "Play");

        pausePlayObject = GameObject.Find("Pause/Play Button");
        pausePlayObject.SetActive(false); // Pause/Play Button is hidden by default
        pausePlayButton = pausePlayObject.GetComponent<Button>();
        pausePlayButtonImage = pausePlayButton.GetComponent<Image>();
        pausePlayButton.onClick.AddListener(delegate { AudioManagement(ButtonHighlightScript2.currentlyHighlightedButton); } );

        audioSource = GetComponent<AudioSource>();
    }

    void AudioManagement(ButtonHighlightScript2 selectedButton)
    {
        if (ButtonHighlightScript2.currentlyHighlightedButton == null)
        // if there is no currently highlighted button
        {
            return;
            // no audio to handle
        }

        if (paused)
        {
            PlayAudio(selectedButton.name);
        }
        else // if not paused
        {
            PauseAudio();
        }
    }

    public void PlayAudio(string audioFileName)
    {
        WWW url = new WWW("file://" + $@"{Application.dataPath}/Playlists/{SelectedPlaylistListScript.currentlyLoadedPlaylist}/{audioFileName}");
        audioSource.clip = url.GetAudioClip(false, true);
        audioSource.clip.name = audioFileName;

        if (audioSource.clip == null)
        {
            Debug.Log("unable to play audio, audio is null");
        }

        pausePlayButtonImage.sprite = pauseSprite;
        audioSource.Play();
        paused = false;
    }

    static public void PauseAudio()
    {
        pausePlayButtonImage.sprite = playSprite;
        audioSource.Stop();
        paused = true;
    }

    static public void DisplayText(string text)
    {
        pausePlayObject.SetActive(false); // hide pause/play button
        controlPanelText.text = text;
    }

    static public void DisplayPausePlayButton()
    {
        pausePlayObject.SetActive(true);
        controlPanelText.text = ""; // hide text
    }
}
