using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForwardButtonScript : MonoBehaviour
{
    static public Button button;
    static public ControlPanelScript controlPanel;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { PlayNextAudio(); });
        controlPanel = GameObject.Find("Control Panel").GetComponent<ControlPanelScript>();
    }

    static public void PlayNextAudio()
    {
        if (ButtonHighlightScript2.currentlyHighlightedButton == null) return; // if no audio is currently selected, do nothing

        string nextAudio = ControlPanelScript.GetNextAudio();
        if (nextAudio != null)
        {
            ButtonHighlightScript2.HighlightButton(GameObject.Find(nextAudio).GetComponent<ButtonHighlightScript2>()); // highlight next audio file's button
            ControlPanelScript.LoadAudio(nextAudio);
            controlPanel.PlayAudio();
        }
        else
        {
            ControlPanelScript.StopAudio();
        }
    }
}
