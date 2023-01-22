using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHighlightScript : MonoBehaviour
// Makes a button's background and text change color upon being clicked.
// Ensures only one instance of this button is highlighted at a time and dehighlights the previously selected button.
{
    Button button; // button component of this game object
    public bool highlighted; // bool to determine if this is highlighted
    static public ButtonHighlightScript currentlyHighlightedButton; // global static reference to whichever button is currently highlighted
    Color defaultBackgroundColor = new(0.235f, 0.235f, 0.235f); // color is #3C3C3C

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { HighlightOnSelect(); } );
        // make button execute HighlightOnSelect() whenever it is clicked on
        highlighted = false;
        // when a button spawns it is not highlighted
    }

    void HighlightOnSelect()
    {
        if (!highlighted)
        {
            DehighlightPreviouslySelectedButton();
            GetComponent<Image>().color = Color.white; // change button background color to white
            button.GetComponentInChildren<TextMeshProUGUI>().color = defaultBackgroundColor; // change button text to #3C3C3C
            highlighted = true; // button is now highlighted
            currentlyHighlightedButton = this; // create a reference to this button so it can be dehighlighted when another button is highlighted
        }
    }

    void DehighlightPreviouslySelectedButton()
    {
        if (currentlyHighlightedButton != null)
        // if a button was previously selected
        {
            currentlyHighlightedButton.button.GetComponentInChildren<TextMeshProUGUI>().color = Color.white; // change button text back to white
            currentlyHighlightedButton.button.GetComponent<Image>().color = defaultBackgroundColor; // change button background back to #3C3C3C
            currentlyHighlightedButton.highlighted = false; // button is now no longer highlighted
        }
    }
}
