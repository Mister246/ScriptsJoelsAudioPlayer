using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanelScript : MonoBehaviour
{
    static public Text controlPanelText;

    static public GameObject pausePlayObject;
    public Sprite pauseSprite;
    public Sprite playSprite;
    Image pausePlayButtonImage;
    Button pausePlayButton;
    bool paused = true;

    void Start()
    {
        controlPanelText = GetComponentInChildren<Text>();

        pausePlayObject = GameObject.Find("Pause/Play Button");
        pausePlayObject.SetActive(false); // Pause/Play Button is hidden by default

        pausePlayButton = pausePlayObject.GetComponent<Button>();
        pausePlayButtonImage = pausePlayButton.GetComponent<Image>();
        pausePlayButton.onClick.AddListener(delegate { InvertPausePlayButtonSprite(); } );
        pausePlayButton.onClick.AddListener(delegate { PlaySelectedAudioFiles(ButtonHighlightScript2.currentlyHighlightedButton); } );
    }

    void InvertPausePlayButtonSprite()
    {
        if (ButtonHighlightScript2.currentlyHighlightedButton == null)
        // if there is no currently highlighted button
        {
            return;
            // button should not invert
        }

        if (pausePlayButtonImage.sprite == pauseSprite)
        {
            pausePlayButtonImage.sprite = playSprite;
            paused = false;
        }
        else
        {
            pausePlayButtonImage.sprite = pauseSprite;
            paused = true;
        }
    }

    void PlaySelectedAudioFiles(ButtonHighlightScript2 selectedButton)
    {
        if (paused)
        {

        }
    }
}
