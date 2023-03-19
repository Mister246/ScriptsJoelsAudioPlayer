using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHighlightScript2 : MonoBehaviour
// Makes a button's background and text change color upon being clicked.
// Only one instance of this button is highlighted at a time.
// Upon a new button being highlighted, the previous button dehighlights.
// Apply this script to the button that will be used to spawn more buttons in the list.
// If you want to apply this script to a separate list, make a copy of this class in a new script and apply it to
// the template button for that list.
// This class is currently in use by the right list of audio files.
{
    Button button; // button component of this game object
    public bool highlighted; // bool to determine if this is highlighted
    static public ButtonHighlightScript2 currentlyHighlightedButton; // global static reference to whichever button is currently highlighted
    Color defaultBackgroundColor = new(0.235f, 0.235f, 0.235f); // color is #3C3C3C

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { HighlightButton(this); } );
        // make button highlight or dehighlight itself on click
        highlighted = false;
        // when a button spawns it is not highlighted
    }

    void HighlightButton(ButtonHighlightScript2 button)
    // highlights the button if it is not already highlighted
    // dehighlights the button if it is already highlighted
    {
        if (button.highlighted)
        {
            DehighlightButton(button);
        }
        else 
        // if button is not highlighted, highlight button:
        {
            DehighlightButton(currentlyHighlightedButton); // dehighlight previous button
            button.GetComponent<Image>().color = Color.white; // change button background color to white
            button.GetComponentInChildren<TextMeshProUGUI>().color = defaultBackgroundColor; // change button text to #3C3C3C
            button.highlighted = true; // button is now highlighted
            currentlyHighlightedButton = this; // create a reference to this button so it can be dehighlighted when another button is highlighted
            ControlPanelScript.LoadAudio(currentlyHighlightedButton.name); 
            ControlPanelScript.PauseAudio(); // change pause/play button sprite to play sprite
            ProgressBarScript.ResetProgressBar(); // reset knob position
            ProgressBarScript.DisplayProgressInTime(); // display playback info of loaded audio
        }
    }

    void DehighlightButton(ButtonHighlightScript2 button)
    {
        if (currentlyHighlightedButton != null)
        // if a button was previously selected
        {
            button.GetComponent<Image>().color = defaultBackgroundColor; // change button background color to default
            button.GetComponentInChildren<TextMeshProUGUI>().color = Color.white; // change button text to default
            button.highlighted = false;
            currentlyHighlightedButton = null;
        }
    }
}
