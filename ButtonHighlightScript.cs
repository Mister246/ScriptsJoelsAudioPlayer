using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHighlightScript : MonoBehaviour
// Makes a button's background and text change color upon being clicked
{
    public Button button;
    public bool highlighted;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { HighlightOnSelect(); } ); 
        highlighted = false;
    }
    public void HighlightOnSelect()
    {
        if (!highlighted)
        {
            GetComponent<Image>().color = Color.white;
            // change button background color to white
            button.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0.235f, 0.235f, 0.235f);
            // change button text to #3C3C3C
            highlighted = true;
            ListOfPlaylistsScript.currentlyHighlightedButton = this;
        }
    }
}
