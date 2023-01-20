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

    static public void GenerateFileList(FileInfo[] files, Image background, Transform parent)
    // Creates a scrollable list of files.
    // Each element will have the provided background image.
    // The parent will determine which game object the elements will instantiate under.
    {
        for (int i = 0; i < files.Length; i++)
        {
            Instantiate(background, parent); // create background for each element
        }

        // MOVE THIS FUNCTION TO ITS RESPECTIVE SCRIPT EVENTUALLY
    }
}
