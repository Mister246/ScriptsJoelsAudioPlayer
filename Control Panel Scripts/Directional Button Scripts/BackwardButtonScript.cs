using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackwardButtonScript : MonoBehaviour
{
    static public Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { PlayPreviousAudio(); } );
    }

    static public void PlayPreviousAudio()
    {
        if (ButtonHighlightScript2.currentlyHighlightedButton == null) return; // if no audio is currently selected, do nothing

        int index = ControlPanelScript.GetIndexOfAudio(ControlPanelScript.audioSource.clip.name);
        if (index != 0)
        // if not at the beginning of the playlist
        {
            string previousAudio = SelectedPlaylistListScript.audioFiles[index - 1].Name;
            ButtonHighlightScript2.HighlightButton(GameObject.Find(previousAudio).GetComponent<ButtonHighlightScript2>()); // highlight previous audio file's button
            ControlPanelScript.LoadAudio(previousAudio);
            ControlPanelScript.controlPanel.PlayAudio();
        }
        else
        {
            ControlPanelScript.StopAudio();
        }
    }
}
