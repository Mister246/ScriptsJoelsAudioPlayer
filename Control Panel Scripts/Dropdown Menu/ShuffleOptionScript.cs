using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShuffleOptionScript : MonoBehaviour
{
    static Image image;
    Button button;
    static public bool shuffle;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { ToggleShuffle(); });

        image = GetComponent<Image>();
    }

    static void ToggleShuffle()
    {
        shuffle = !shuffle;

        if (shuffle)
        {
            image.sprite = DropdownMenuScript.trueToggleSprite;
            ControlPanelScript.Shuffle();
        }
        else
        {
            image.sprite = DropdownMenuScript.falseToggleSprite;
        }
    }
}
