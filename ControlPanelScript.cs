using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanelScript : MonoBehaviour
{
    static public Text controlPanelText;

    public GameObject pausePlayObject;
    public Sprite pauseSprite;
    public Sprite playSprite;
    Image pausePlayButtonImage;
    Button pausePlayButton;

    void Start()
    {
        controlPanelText = GetComponentInChildren<Text>();

        pausePlayButton = pausePlayObject.GetComponent<Button>();
        pausePlayButton.onClick.AddListener(delegate { InvertPausePlayButtonSprite(); } );
        pausePlayButtonImage = pausePlayButton.GetComponent<Image>();
    }

    void InvertPausePlayButtonSprite()
    {
        if (pausePlayButtonImage.sprite == pauseSprite)
        {
            pausePlayButtonImage.sprite = playSprite;
        }
        else
        {
            pausePlayButtonImage.sprite = pauseSprite;
        }
    }
}
