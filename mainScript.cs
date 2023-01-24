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
}
