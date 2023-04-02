using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarSelectedPlaylistListSectionScript : MonoBehaviour
{
    static public Scrollbar scrollbar;

    void Start()
    {
        scrollbar = GetComponent<Scrollbar>();
    }

    static public void SnapScrollbarToButton(int buttonIndex)
    {
        float percentage = (float)buttonIndex / SelectedPlaylistListScript.audioFiles.Length;
        float newPosition = 1 - (percentage + (percentage * 0.022f)); // floating point error correction
        scrollbar.value = newPosition;
    }
}