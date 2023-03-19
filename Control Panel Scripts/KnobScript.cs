using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class KnobScript : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        if (ButtonHighlightScript2.currentlyHighlightedButton == null) return; // if no song is currently selected
        ProgressBarScript.DragKnob(eventData);
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