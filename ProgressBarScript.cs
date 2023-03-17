using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProgressBarScript : MonoBehaviour
{
    static public GameObject knob;
    // in the editor, knob starts at x = -500 and ends at x = 500
    // units in editor don't seem to evenly equate to units in script however
    // for the logic of this script, knob starts at x = 0 and ends at x = 800

    static float startingPosition; // x = 0 for the knob
    float audioProgressPercentage; // represents how much of the audio file has been played

    void Start()
    {
        knob = GameObject.Find("Knob");
        startingPosition = knob.transform.position.x; // x = 0
    }

    void Update()
    {
        if (!ControlPanelScript.audioSource.isPlaying) return;

        audioProgressPercentage = ControlPanelScript.audioSource.time / ControlPanelScript.audioSource.clip.length; // determine how much of the audio file has been played
        knob.transform.position = new Vector3(startingPosition + (800 * audioProgressPercentage), knob.transform.position.y, knob.transform.position.z); // move knob according to percentage
    }

    static public void ResetProgressBar()
    // resets knob back to starting position
    {
        knob.transform.position = new Vector3(startingPosition, knob.transform.position.y, knob.transform.position.z);
    }
}
