using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class KnobScript : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public void OnDrag(PointerEventData eventData)
    // allows the user to click and hold on the knob to change the playback position of audio
    // function gets event data from KnobScript, which is attached to the Knob object
    {
        if (ButtonHighlightScript2.currentlyHighlightedButton == null) return; // if no song is currently selected
        if (ControlPanelScript.audioSource.isPlaying) ControlPanelScript.PauseAudio(); // pause while dragging 
        if (eventData.position.x < ProgressBarScript.startingPosition) return; // if cursor position is further left than the starting position of the progress bar
        if (eventData.position.x > ProgressBarScript.endingPosition) return; // if cursor position is further right than the ending position of the progress bar

        transform.position = new Vector3(eventData.position.x, transform.position.y, transform.position.z); // change knob position according to cursor drag
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (ButtonHighlightScript2.currentlyHighlightedButton == null) return; // if no song is currently selected

        ControlPanelScript.audioSource.time = ControlPanelScript.audioSource.clip.length * ((transform.position.x - ProgressBarScript.startingPosition) / ProgressBarScript.LENGTH);
        // change playback position according to knob position
        // easier to read expression:
        // clipLength * ((knobPosition - startingPosition) / progressBarLength)

        ControlPanelScript.audioSource.Play();
        ProgressBarScript.DisplayProgressInTime();
        ControlPanelScript.audioSource.Pause();
        // even if changed prior, audioSource.time is always 0 until the audio source has been played at least once
        // this messes up the playback position display text
        // to get around this, I play the audio source, display playback position, then immediately pause afterwards
        // this is stupid, but it works
    }
}