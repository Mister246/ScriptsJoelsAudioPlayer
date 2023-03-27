using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackwardButtonScript : MonoBehaviour
{
    static public Button button;
    static public ControlPanelScript controlPanel;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { PlayPreviousAudio(); } );
        controlPanel = GameObject.Find("Control Panel").GetComponent<ControlPanelScript>();
    }

    static public void PlayPreviousAudio()
    {
        if (ButtonHighlightScript2.currentlyHighlightedButton == null) return; // if no audio is currently selected, do nothing

        string previousAudio = ControlPanelScript.GetPreviousAudio();
        if (previousAudio != null)
        {
            ButtonHighlightScript2.HighlightButton(GameObject.Find(previousAudio).GetComponent<ButtonHighlightScript2>()); // highlight previous audio file's button
            ControlPanelScript.LoadAudio(previousAudio);
            controlPanel.PlayAudio();
        }
        else
        {
            ControlPanelScript.StopAudio();
        }
    }
}
