using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedPlaylistListScript : MonoBehaviour
{
    public Button referencedButton;
    static Button audioFileButton;
    static Transform list;
    static string currentlyLoadedPlaylist;

    void Start()
    {
        list = transform;
        audioFileButton = referencedButton;
        // have to manually assign these because the below functions are static
    }

    static FileInfo[] LoadAudioFiles(string playlistName)
    {
        DirectoryInfo playlist = new($@"{Application.dataPath}/Playlists/{playlistName}");
        FileInfo[] audioFiles = playlist.GetFiles("*.ogg"); // update to support more file types in the future

        if (audioFiles.Length == 0)
        {
            Debug.Log($"No audio files found in {playlist}");
        }
        else
        {
            currentlyLoadedPlaylist = playlistName;
        }

        return audioFiles; 
    }

    static public void GenerateAudioFileList(string selectedPlaylist)
    // creates a scrollable list of audio files located in selectedPlaylist
    {
        if (selectedPlaylist == currentlyLoadedPlaylist)
        {
            return;
            // prevents already loaded audio files from loading again
        }

        FileInfo[] audioFiles = LoadAudioFiles(selectedPlaylist);

        for (int i = 0; i < audioFiles.Length; i++)
        {
            Button newAudioFileButton = Instantiate(audioFileButton, list); // create new audio file button in list
            string audioFileName = audioFiles[i].Name;
            newAudioFileButton.name = audioFileName;
            newAudioFileButton.GetComponentInChildren<TextMeshProUGUI>().text = audioFileName; // display name of audio file on new button
            //newAudioFileButton.onClick.AddListener(delegate { SelectedPlaylistListScript.GenerateAudioFileList(audioFileName); });
        }
    }
}
