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
    static string currentlyLoadedPlaylist;
    static List<Button> loadedButtons = new();

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
        currentlyLoadedPlaylist = playlistName;

        if (audioFiles.Length == 0)
        {
            // no audio files in playlist
            // add functionality later
        }

        return audioFiles; 
    }

    static public void GenerateAudioFileList(string selectedPlaylist)
    // creates a scrollable list of audio files located in the selected playlist
    {
        if (selectedPlaylist == currentlyLoadedPlaylist)
        {
            return;
            // prevents already loaded audio files from loading again
        }

        FileInfo[] audioFiles = LoadAudioFiles(selectedPlaylist);

        UnloadButtons(); // unload any buttons currently in the list

        for (int i = 0; i < audioFiles.Length; i++)
        {
            Button newAudioFileButton = Instantiate(audioFileButton, list); // create new audio file button in list
            string audioFileName = audioFiles[i].Name;
            newAudioFileButton.name = audioFileName;
            newAudioFileButton.GetComponentInChildren<TextMeshProUGUI>().text = audioFileName; // display name of audio file on new button
            loadedButtons.Add(newAudioFileButton);
            //newAudioFileButton.onClick.AddListener(delegate { SelectedPlaylistListScript.GenerateAudioFileList(audioFileName); });
        }
    }

    static void UnloadButtons()
    {
        if (loadedButtons.Count > 0)
        {
            foreach (Button button in loadedButtons.ToList())
            {
                Destroy(button.gameObject);
                loadedButtons.Remove(button);
            }
        }
    }
}
