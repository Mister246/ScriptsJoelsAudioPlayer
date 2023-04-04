using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoplayOptionScript : MonoBehaviour
{
    static Image image;
    Button button;
    static public bool autoPlay = false;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { ToggleAutoplay(); } );

        image = GetComponent<Image>();
    }

    public void ToggleAutoplay()
    // Determine if audio should autoplay next audio in list
    {
        autoPlay = !autoPlay;

        if (autoPlay)
        {
            image.sprite = DropdownMenuScript.trueToggleSprite;
        }
        else
        {
            image.sprite = DropdownMenuScript.falseToggleSprite;
        }
    }
}
