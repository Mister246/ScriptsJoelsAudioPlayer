using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedPlaylistListScript : MonoBehaviour
{
    public Button referencedButton;
    static Button audioFileButton;
    static Transform list;
    public static string currentlyLoadedPlaylist;
    static List<Button> loadedButtons = new();
    static public FileInfo[] audioFiles;

    void Start()
    {
        list = transform;
        audioFileButton = referencedButton;
        // have to manually assign these because the below functions are static
    }

    static public void GenerateAudioFileList(string selectedPlaylist)
    // creates a scrollable list of audio files located in the selected playlist
    {
        if (selectedPlaylist == currentlyLoadedPlaylist)
        {
            return;
            // prevents already loaded audio files from loading again
        }

        UnloadButtons(); // unload any buttons currently in the list
        ControlPanelScript.StopAudio(); // stop playing any audio if there is any

        audioFiles = LoadAudioFiles(selectedPlaylist);

        if (audioFiles.Length == 0)
        {
            ControlPanelScript.DisplayText($"No files located in {selectedPlaylist}");
        }
        else
        {
            ControlPanelScript.DisplayPausePlayButton();
        }

        for (int i = 0; i < audioFiles.Length; i++)
        {
            Button newAudioFileButton = Instantiate(audioFileButton, list); // create new audio file button in list
            string audioFileName = audioFiles[i].Name;
            newAudioFileButton.name = audioFileName;
            newAudioFileButton.GetComponentInChildren<TextMeshProUGUI>().text = audioFileName; // display name of audio file on new button
            loadedButtons.Add(newAudioFileButton);
        }
    }

    static FileInfo[] LoadAudioFiles(string playlistName)
    {
        DirectoryInfo playlist = new($@"{Application.dataPath}/Playlists/{playlistName}");
        currentlyLoadedPlaylist = playlistName;
        return playlist.GetFiles("*.ogg"); // update to support more file types in the future
    }

    static public void UnloadButtons()
    {
        if (loadedButtons.Count > 0)
        {
            foreach (Button button in loadedButtons.ToList())
            {
                Destroy(button.gameObject);
                loadedButtons.Remove(button);
            }
        }
        ButtonHighlightScript2.currentlyHighlightedButton = null; // once buttons are unloaded, nothing is highlighted
    }
}
