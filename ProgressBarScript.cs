using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarScript : MonoBehaviour
{
    static public GameObject knob;
    // in the editor, knob starts at x = -500 and ends at x = 500
    // units in editor don't seem to evenly equate to units in script however
    // for the logic of this script, knob starts at x = 0 and ends at x = 800
    static public Text progressText; // display of clip.time out of clip.length

    static float startingPosition; // x = 0 for the knob
    float audioProgressPercentage; // represents how much of the audio file has been played

    void Start()
    {
        knob = GameObject.Find("Knob");
        startingPosition = knob.transform.position.x; // x = 0
        
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

    static public void DisplayProgressInTime()
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
