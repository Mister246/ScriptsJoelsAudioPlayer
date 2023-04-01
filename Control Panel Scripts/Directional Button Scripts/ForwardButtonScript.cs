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
    }

    static public void PlayNextAudio()
    {
        if (ButtonHighlightScript2.currentlyHighlightedButton == null) return; // if no audio is currently selected, do nothing

        int index = ControlPanelScript.GetIndexOfAudio(ControlPanelScript.audioSource.clip.name);
        if (index != SelectedPlaylistListScript.audioFiles.Length - 1)
        // if not reached the end of playlist
        {
            string nextAudio = SelectedPlaylistListScript.audioFiles[index + 1].Name;
            ButtonHighlightScript2.HighlightButton(GameObject.Find(nextAudio).GetComponent<ButtonHighlightScript2>()); // highlight next audio file's button
            ControlPanelScript.LoadAudio(nextAudio);
            ControlPanelScript.controlPanel.PlayAudio();
        }
        else
        {
            ControlPanelScript.StopAudio();
        }
    }
}
