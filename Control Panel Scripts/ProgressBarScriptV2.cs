using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProgressBarScriptV2 : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    static public Text progressText; // display of clip.time out of clip.length
    static public Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.interactable = false;

        progressText = GameObject.Find("Progress Text").GetComponent<Text>();
    }

    void Update()
    {
        if (!ControlPanelScript.audioSource.isPlaying) return;

        DisplayProgressInTime(); // continuously update progress text
        UpdatePlaybackPosition();
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

    static public void UpdatePlaybackPosition()
    // change knob position according to audio playback position
    // e.g. if 43% of the way through an audio file, move knob to the 43 percentile of the progress bar
    {
        slider.value = ControlPanelScript.audioSource.time / ControlPanelScript.audioSource.clip.length;
    }

    static public void UpdateClipTimePosition()
    {
        if (ButtonHighlightScript2.currentlyHighlightedButton == null) return; // if no song is currently selected

        ControlPanelScript.audioSource.time = ControlPanelScript.audioSource.clip.length * slider.value;
        // change clip time according to handle position

        ControlPanelScript.audioSource.Play();
        DisplayProgressInTime();
        ControlPanelScript.audioSource.Pause();
        // even if changed prior, audioSource.time is always 0 until the audio source has been played at least once
        // this messes up the playback position display text
        // to get around this, I play the audio source, display playback position, then immediately pause afterwards
        // this is stupid, but it works
    }


    public void OnDrag(PointerEventData eventData)
    {
        if (ControlPanelScript.audioSource.isPlaying) ControlPanelScript.PauseAudio();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        UpdateClipTimePosition();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (ControlPanelScript.audioSource.isPlaying) ControlPanelScript.PauseAudio();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        UpdateClipTimePosition();
    }

    static public void ResetProgressBar()
    {
        slider.value = 0f;
    }
}