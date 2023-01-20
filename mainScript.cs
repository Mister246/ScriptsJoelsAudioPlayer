using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class mainScript : MonoBehaviour
{
    // audio related data
    public AudioSource audioSource;

    void Start()
    {
        Application.runInBackground = true;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    static public void GenerateFolderList(DirectoryInfo[] folders, Button playlistButton, Transform list)
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

    static public void GenerateFileList(FileInfo[] files, Image background, Transform parent)
    // Creates a scrollable list of files.
    // Each element will have the provided background image.
    // The parent will determine which game object the elements will instantiate under.
    {
        for (int i = 0; i < files.Length; i++)
        {
            Instantiate(background, parent); // create background for each element
        }
    }
}
