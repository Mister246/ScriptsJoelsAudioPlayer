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

    public static float startingPosition; // x = 0 for the knob
    public static float endingPosition; // x = 800 for the knob
    public static int LENGTH = 800; // length of progress bar
    static float audioProgressPercentage; // represents how much of the audio file has been played

    void Start()
    {
        knob = GameObject.Find("Knob");
        startingPosition = knob.transform.position.x; // x = 0
        endingPosition = knob.transform.position.x + LENGTH; // x = 800

        progressText = FindObjectOfType<Text>();
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

    static public void UpdateKnobPosition()
    // change knob position according to audio playback position
    // e.g. if 43% of the way through an audio file, move knob to the 43 percentile of the progress bar
    {
        audioProgressPercentage = ControlPanelScript.audioSource.time / ControlPanelScript.audioSource.clip.length; // determine how much of the audio file has been played
        knob.transform.position = new Vector3(startingPosition + (LENGTH * audioProgressPercentage), knob.transform.position.y, knob.transform.position.z); // move knob according to percentage
    }

    void Update()
    {
        if (!ControlPanelScript.audioSource.isPlaying) return;

        DisplayProgressInTime(); // continuously update progress text
        UpdateKnobPosition();
    }

    static public void ResetProgressBar()
    {
        knob.transform.position = new Vector3(startingPosition, knob.transform.position.y, knob.transform.position.z); // reset knob back to starting position
        progressText.text = ""; // clear progress text
    }

    static public void DragKnob(PointerEventData eventData)
    // allows the user to click and hold on the knob to change the playback position of audio
    // function gets event data from KnobScript, which is attached to the Knob object
    {
        if (ControlPanelScript.audioSource.isPlaying) ControlPanelScript.PauseAudio(); // pause while dragging 
        if (eventData.position.x < startingPosition) return; // if cursor position is further left than the starting position of the progress bar
        if (eventData.position.x > endingPosition) return; // if cursor position is further right than the ending position of the progress bar

        knob.transform.position = new Vector3(eventData.position.x, knob.transform.position.y, knob.transform.position.z); // change knob position according to cursor drag
    }
}
