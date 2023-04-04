using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoopOptionScript : MonoBehaviour
{
    static Image image;
    Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { ToggleLoop(); } );

        image = GetComponent<Image>();
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
            if (ControlPanelScript.audioSource.isPlaying)
            {
                ControlPanelScript.controlPanel.StartCoroutine(ControlPanelScript.controlPanel.OnAudioEnd(ControlPanelScript.audioSource.clip.length - ControlPanelScript.audioSource.time)); // start coroutine from control panel in order to ensure audio properly stops upon completion
            }
        }
    }
}