using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListOfPlaylistsScript : MonoBehaviour
{
    public DirectoryInfo[] playlists; // contains each folder located in Playlists
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
        DirectoryInfo playlistsDirectory = new DirectoryInfo($@"{Application.dataPath}/Playlists");
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
            newPlaylistButton.GetComponentInChildren<TextMeshProUGUI>().text = folders[i].Name;
            // display name of playlist on new button
        }
    }
}
