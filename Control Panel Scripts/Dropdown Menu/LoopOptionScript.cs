using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopOptionScript : MonoBehaviour
{
    static Image image;
    Button button;
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
        ControlPanelScript.audioSource.loop = !ControlPanelScript.audioSource.loop; // toggle loop flag

        if (ControlPanelScript.audioSource.loop)
        {
            image.sprite = DropdownMenuScript.trueToggleSprite;
        }
        else
        {
            image.sprite = DropdownMenuScript.falseToggleSprite;
            controlPanel.StartCoroutine(controlPanel.OnAudioEnd(ControlPanelScript.audioSource.clip.length - ControlPanelScript.audioSource.time)); // start coroutine from control panel in order to ensure audio properly stops upon completion
        }
    }
}
