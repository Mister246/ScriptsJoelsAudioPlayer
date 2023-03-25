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
            image.sprite = DropdownMenuScript.trueToggleSprite;
        }
        else
        {
            image.sprite = DropdownMenuScript.falseToggleSprite;
        }

        SelectedPlaylistListScript.GenerateAudioFileList(SelectedPlaylistListScript.currentlyLoadedPlaylist); // reload audio file list
    }

    static public void Shuffle()
    // shuffles array of loaded audio files
    {
        for (int i = 0; i < SelectedPlaylistListScript.audioFiles.Length; i++)
        // for each loaded audio file
        {
            int randomIndex1 = Random.Range(0, SelectedPlaylistListScript.audioFiles.Length);
            int randomIndex2 = Random.Range(0, SelectedPlaylistListScript.audioFiles.Length);

            FileInfo temp = SelectedPlaylistListScript.audioFiles[randomIndex1];
            SelectedPlaylistListScript.audioFiles[randomIndex1] = SelectedPlaylistListScript.audioFiles[randomIndex2];
            SelectedPlaylistListScript.audioFiles[randomIndex2] = temp;
        }
    }
}
