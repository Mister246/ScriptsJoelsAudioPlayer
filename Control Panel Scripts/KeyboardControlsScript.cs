using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardControlsScript : MonoBehaviour
{
    void Update()
    {
        if (SearchOptionScript.searchField.isFocused)
        // if user is typing in search field
        {
            return; // do not execute keyboard controls
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ControlPanelScript.controlPanel.AudioManagement();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ForwardButtonScript.PlayNextAudio();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            BackwardButtonScript.PlayPreviousAudio();
        }
    }
}
