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
    public static List<Button> loadedButtons = new();
    static public FileInfo[] audioFiles;
    static public bool isShuffled;

    void Start()
    {
        list = transform;
        audioFileButton = referencedButton;
        // have to manually assign these because the below functions are static
    }

    static public void GenerateAudioFileList(string selectedPlaylist)
    // creates a scrollable list of audio files located in the selected playlist
    {
        if (currentlyLoadedPlaylist == selectedPlaylist)
        // if attempting to reload playlist
        {
            if (!ShuffleOptionScript.shuffle && !isShuffled)
            // if shuffle is disabled and audio files are not shuffled
            {
                return; // no need to reload
            }

            if (ShuffleOptionScript.shuffle && isShuffled)
            // if shuffle is enabled and audio files are already shuffled
            {
                return; // no need to reshuffle
            }
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
            ControlPanelScript.DisplayControlPanel();
        }

        if (ShuffleOptionScript.shuffle)
        {
            ControlPanelScript.controlPanel.StopAllCoroutines();
            Shuffle();
            isShuffled = true;
        }
        else
        {
            isShuffled = false;
        }

        for (int i = 0; i < audioFiles.Length; i++)
        {
            Button newAudioFileButton = Instantiate(audioFileButton, list); // create new audio file button in list
            newAudioFileButton.name = audioFiles[i].Name;
            newAudioFileButton.GetComponentInChildren<TextMeshProUGUI>().text = audioFiles[i].Name.Substring(0, audioFiles[i].Name.Length - 4); // display name of audio file on new button
            loadedButtons.Add(newAudioFileButton);
        }
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

    static FileInfo[] LoadAudioFiles(string playlistName)
    {
        FileInfo[] audioFiles = new FileInfo[0];
        FileInfo[] wavFiles;
        FileInfo[] oggFiles;
        FileInfo[] mp3Files;
        int iterations = 1; // for loop should only iterate once if loading a single audio file

        if (playlistName == "All Audio Files")
        // if attempting to load all audio files
        { 
            iterations = ListOfPlaylistsScript.playlists.Length; // for loop should iterate once for each playlist
            playlistName = ListOfPlaylistsScript.playlists[0].Name; // start from first playlist
        }

        currentlyLoadedPlaylist = playlistName;

        for (int i = 0; i < iterations; i++)
        {
            DirectoryInfo playlist = new($@"{Application.dataPath}/Playlists/{playlistName}");
            wavFiles = playlist.GetFiles("*.wav");
            oggFiles = playlist.GetFiles("*.ogg");
            mp3Files = playlist.GetFiles("*.mp3");
            audioFiles = audioFiles.Concat(wavFiles).ToArray().Concat(oggFiles).ToArray().Concat(mp3Files).ToArray();
            try
            {
                playlistName = ListOfPlaylistsScript.playlists[i + 1].Name; // move to next playlist
            }
            catch
            {
                break; // reached end of array
            }
        }

        return audioFiles; // return all files in one array
    }

    static public void Shuffle()
    // shuffles array of loaded audio files
    {
        for (int i = 0; i < audioFiles.Length; i++)
        // for each loaded audio file
        {
            int randomIndex = Random.Range(0, audioFiles.Length);
            FileInfo temp = audioFiles[i];
            audioFiles[i] = audioFiles[randomIndex];
            audioFiles[randomIndex] = temp;
        }
    }
}
