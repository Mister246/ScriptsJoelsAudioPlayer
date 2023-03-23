using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopOptionScript : MonoBehaviour
{
    static Image image;
    Button button;
    static public bool loop = false;
    static public ControlPanelScript controlPanel;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { ToggleLoop(); } );

        image = GetComponent<Image>();
        image.sprite = DropdownMenuScript.falseToggleSprite;

        controlPanel = GameObject.Find("Control Panel").GetComponent<ControlPanelScript>();
    }

    public void ToggleLoop()
    // Determine if audio should loop
    {
        loop = !loop;

        if (loop)
        {
            image.sprite = DropdownMenuScript.trueToggleSprite;
        }
        else
        {
            image.sprite = DropdownMenuScript.falseToggleSprite;
            StartCoroutine(controlPanel.OnAudioEnd(ControlPanelScript.audioSource.clip.length - ControlPanelScript.audioSource.time));
        }

        ControlPanelScript.audioSource.loop = loop;
    }
}
