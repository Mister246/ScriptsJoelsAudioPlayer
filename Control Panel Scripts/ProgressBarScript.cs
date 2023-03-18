using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProgressBarScript : MonoBehaviour
{
    static public GameObject knob;
    // in the editor, knob starts at x = -500 and ends at x = 500
    // units in editor don't seem to evenly equate to units in script however
    // for the logic of this script, knob starts at x = 0 and ends at x = 800
    static public Text progressText; // display of clip.time out of clip.length

    static float startingPosition; // x = 0 for the knob
    static float endingPosition; // x = 800 for the knob
    float audioProgressPercentage; // represents how much of the audio file has been played

    void Start()
    {
        knob = GameObject.Find("Knob");
        startingPosition = knob.transform.position.x; // x = 0
        endingPosition = knob.transform.position.x + 800; // x = 800

        progressText = FindObjectOfType<Text>();
    }

    void Update()
    {
        if (!ControlPanelScript.audioSource.isPlaying) return;

        DisplayProgressInTime();
        audioProgressPercentage = ControlPanelScript.audioSource.time / ControlPanelScript.audioSource.clip.length; // determine how much of the audio file has been played
        knob.transform.position = new Vector3(startingPosition + (800 * audioProgressPercentage), knob.transform.position.y, knob.transform.position.z); // move knob according to percentage
    }

    static public void ResetProgressBar()
    {
        knob.transform.position = new Vector3(startingPosition, knob.transform.position.y, knob.transform.position.z); // reset knob back to starting position
        progressText.text = ""; // clear progress text
    }

    static public void ChangeKnobPosition(PointerEventData eventData)
    // allows the user to click and hold on the knob to change the playback position of audio
    // function gets event data from KnobScript, which is attached to the Knob object
    {
        if (eventData.position.x < startingPosition)
        // if cursor position is further left than the starting position of the progress bar
        {
            return;
        }

        if (eventData.position.x > endingPosition)
        // if cursor position is further right than the ending position of the progress bar
        {
            return;
        }

        knob.transform.position = new Vector3(eventData.position.x, knob.transform.position.y, knob.transform.position.z); // change knob position according to cursor drag
    }

    static public void DisplayProgressInTime()
    // displays playback position using time at the bottom left of the control panel
    {
        int seconds = (int)ControlPanelScript.audioSource.time;
        int minutes = seconds / 60;
        seconds %= 60; // seconds must be modulo'd after getting minutes to allow minutes to be calculated

        if (seconds < 10)
        {
            progressText.text = $"{minutes}:0{seconds} / {ControlPanelScript.formattedAudioLength}";
        }
        else
        {
            progressText.text = $"{minutes}:{seconds} / {ControlPanelScript.formattedAudioLength}";
        }
    }
}
