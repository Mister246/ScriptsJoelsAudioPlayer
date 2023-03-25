using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;
using UnityEngine.Networking;
using Unity.VisualScripting;
using System.IO;

public class ControlPanelScript : MonoBehaviour
{
    static public Text controlPanelText;

    public SpriteLibrary spriteLibrary;
    static public Sprite pauseSprite;
    static public Sprite playSprite;

    static public GameObject pausePlayObject;
    static Image pausePlayButtonImage;
    Button pausePlayButton;

    static public GameObject progressBar;
    static public string formattedAudioLength; // audio length formatted in minutes:seconds

    static public GameObject dropdownMenu;

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
        pausePlayButton.onClick.AddListener(delegate { AudioManagement(); } );

        progressBar = GameObject.Find("Progress Bar");
        progressBar.SetActive(false); // Progress Bar is hidden by default

        dropdownMenu = GameObject.Find("Dropdown Menu");
        dropdownMenu.SetActive(false); // Dropdown Menu button is hidden by default

        audioSource = GetComponent<AudioSource>();
    }

    void AudioManagement()
    {
        if (!audioSource.isPlaying && ButtonHighlightScript2.currentlyHighlightedButton != null)
        // if nothing is playing and an audio file is selected
        {
            PlayAudio();
        }
        else
        {
            PauseAudio();
        }
    }

    static public void LoadAudio(string audioFileName)
    // loads an audio file from a directory and sets it as the audio source's clip
    {
        /*
        UnityWebRequest url = UnityWebRequestMultimedia.GetAudioClip("file://" + $@"{Application.dataPath}/Playlists/{SelectedPlaylistListScript.currentlyLoadedPlaylist}/{audioFileName}", AudioType.OGGVORBIS);
        audioSource.clip = DownloadHandlerAudioClip.GetContent(url);
        */

        // mess with this later ^^

        if (!audioSource.clip.IsUnityNull())
        // if a clip is currently loaded
        {
            if (audioSource.clip.name == audioFileName)
            // if this clip is already loaded
            {
                return;
            }
        }

        WWW url = new WWW("file://" + $@"{Application.dataPath}/Playlists/{SelectedPlaylistListScript.currentlyLoadedPlaylist}/{audioFileName}");
        audioSource.clip = url.GetAudioClip(false, true);

        if (audioSource.clip == null)
        {
            Debug.Log($"unable to load {audioFileName}; audio is null");
            return;
        }

        audioSource.clip.name = audioFileName;
        audioSource.time = 0; // ensures playback position always starts at 0 

        if ((audioSource.clip.length % 60) < 10)
        {
            formattedAudioLength = $"{(int)audioSource.clip.length / 60}:0{(int)audioSource.clip.length % 60}";
        }
        else
        {
            formattedAudioLength = $"{(int)audioSource.clip.length / 60}:{(int)audioSource.clip.length % 60}";
        }
    }

    public void PlayAudio()
    {
        if (audioSource.clip.IsUnityNull()) return; // if no clip is loaded

        pausePlayButtonImage.sprite = pauseSprite;
        audioSource.Play();
        StartCoroutine(OnAudioEnd(audioSource.clip.length - audioSource.time));
        // start coroutine to execute when audio file is finished playing
        // (audioSource.clip.length - audioSource.time) is the remaining time for the audio file
    }

    static public void PauseAudio()
    {
        pausePlayButtonImage.sprite = playSprite;
        audioSource.Pause();
    }

    static public void StopAudio()
    {
        pausePlayButtonImage.sprite = playSprite;
        audioSource.Stop();
    }

    static public void DisplayText(string text)
    {
        HideControlPanel();
        controlPanelText.text = text;
    }
    static public void HideControlPanel()
    {
        pausePlayObject.SetActive(false);
        dropdownMenu.SetActive(false);
        ProgressBarScript.ResetProgressBar();
        progressBar.SetActive(false);
    }

    static public void DisplayControlPanel()
    {
        pausePlayObject.SetActive(true);
        dropdownMenu.SetActive(true);
        progressBar.SetActive(true);
        controlPanelText.text = ""; // hide text
    }

    public IEnumerator OnAudioEnd(float audioDuration)
    // executes once audioDuration seconds have passed
    {
        string audioFile = audioSource.clip.name; 
        // save reference to audio file that was playing when starting the coroutine
        float currentTime = audioSource.time; 
        // save reference to the current time of the audio file when starting the coroutine

        yield return new WaitForSeconds(audioDuration);
        // wait for audio file to end

        if (audioFile != audioSource.clip.name)
        // if at some point started playing another clip
        {
            yield break;
        }

        if (audioSource.time > 0)
        {
            if ((audioSource.time - currentTime) <= (audioDuration - 0.3))
            // if at some point interrupted the audio clip
            // this can happen either by pausing the audio file or selecting another playlist
            // 0.3 is the tolerance
            {
                yield break;
            }
        }

        if (audioSource.loop)
        {
            yield break;
        }

        if (AutoplayOptionScript.autoPlay)
        {
            for (int i = 0; i < SelectedPlaylistListScript.audioFiles.Length - 1; i++)
            // for each loaded audio file except the last
            {
                if (SelectedPlaylistListScript.audioFiles[i].Name == audioSource.clip.name)
                // if this is the currently loaded audio file
                {
                    string nextAudioFile = SelectedPlaylistListScript.audioFiles[i + 1].Name;
                    ButtonHighlightScript2.HighlightButton(GameObject.Find(nextAudioFile).GetComponent<ButtonHighlightScript2>()); // highlight next audio file's button
                    LoadAudio(nextAudioFile);
                    PlayAudio(); 
                    yield break;
                }
            }
        }

        StopAudio();
        audioSource.time = 0f; // reset playback position
    }
}