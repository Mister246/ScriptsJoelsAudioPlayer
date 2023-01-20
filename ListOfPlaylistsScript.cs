using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListOfPlaylistsScript : MonoBehaviour
{
    public DirectoryInfo[] playlists; // contains each folder located in Playlists
    public FileInfo[] audioFiles;
    public Button playlistButton; // button template each playlist button will use

    void Start()
    {
        LoadPlaylists();
        GenerateFolderList(playlists, playlistButton, transform);
        // create list of playlists located Playlist folder in Assets (pre-built) or Joel'sAudioPlayer_Data (built)
    }

    void LoadPlaylists()
    // loads all folders from {Application.dataPath}/Playlists into an array
    {
        DirectoryInfo playlistsDirectory = new($@"{Application.dataPath}/Playlists");
        playlists = playlistsDirectory.GetDirectories();
        if (playlists.Length == 0)
        {
            Debug.Log($"No folders found in {playlistsDirectory}");
        }
    }

    private void GenerateFolderList(DirectoryInfo[] folders, Button playlistButton, Transform list)
    // Creates a scrollable list of folders. 
    // list determines which scrollable list the elements will instantiate under.
    {
        for (int i = 0; i < folders.Length; i++)
        // for each folder in folders
        {
            Button newPlaylistButton = Instantiate(playlistButton, list);
            // create new playlist button
            string playlistName = folders[i].Name;
            newPlaylistButton.GetComponentInChildren<TextMeshProUGUI>().text = playlistName;
            // display name of playlist on new button
            newPlaylistButton.onClick.AddListener(delegate { PreviewFiles(playlistName); });
            // make button execute PreviewFiles function on click
        }
    }

    void LoadAudioFiles(string playlistName)
    {
        string playlistFilePath = $@"{Application.dataPath}/Playlists/{playlistName}"; 
        DirectoryInfo playlist = new(playlistFilePath);
        audioFiles = playlist.GetFiles();
        if (audioFiles.Length == 0)
        {
            Debug.Log($"No audio files found in {playlistFilePath}");
        }
    }

    private void PreviewFiles(string selectedPlaylist)
    {
        LoadAudioFiles(selectedPlaylist);
        Debug.Log(audioFiles.Length);
    }
}
