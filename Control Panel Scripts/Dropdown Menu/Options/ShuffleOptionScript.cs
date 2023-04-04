using System.Collections;
using System.Collections.Generic;
using System.IO;
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
            image.sprite = DropdownMenuScript.trueToggleSprite; // audio files will shuffle after being loaded
        }
        else
        {
            image.sprite = DropdownMenuScript.falseToggleSprite; // audio files will be loaded in their original order
        }

        SelectedPlaylistListScript.GenerateAudioFileList(SelectedPlaylistListScript.currentlyLoadedPlaylist); // reload audio file list
    }
}