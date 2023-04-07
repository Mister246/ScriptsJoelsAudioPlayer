using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SearchOptionScript : MonoBehaviour
{
    Button button;
    static GameObject searchMenu;
    static bool toggle = false;
    static public TMP_InputField searchField;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { ToggleSearchMenu(); });

        searchMenu = GameObject.Find("Search Menu");
        searchMenu.SetActive(false); // hide search menu by default

        searchField = searchMenu.GetComponentInChildren<TMP_InputField>();
        searchField.onSubmit.AddListener(delegate { Search(searchField.text); });
    }

    static public void ToggleSearchMenu()
    {
        toggle = !toggle;
        searchMenu.SetActive(toggle);
        if (toggle) EventSystem.current.SetSelectedGameObject(searchField.gameObject); // make search field focused upon opening
    }

    static public void Search(string input)
    {
        SelectedPlaylistListScript.UnloadButtons();
        SelectedPlaylistListScript.audioFiles = SelectedPlaylistListScript.LoadAudioFiles(SelectedPlaylistListScript.currentlyLoadedPlaylist); // reload audio files

        if (input.Length == 0)
        {
            for (int i = 0; i < SelectedPlaylistListScript.audioFiles.Length; i++)
            // for each audio file
            {
                SelectedPlaylistListScript.CreateButton(SelectedPlaylistListScript.audioFiles[i].Name); // reload buttons for this playlist
            }

            return;
        }

        List<FileInfo> matchingAudioFiles = new List<FileInfo>();   
        input = input.ToLower();

        for (int i = 0; i < SelectedPlaylistListScript.audioFiles.Length; i++)
        // for each audio file
        {
            int matchingCharacters = 0;
            string audioFileName = SelectedPlaylistListScript.audioFiles[i].Name.ToLower();
            int shorterLength = Math.Min(audioFileName.Length, input.Length);

            for (int j = 0; j < shorterLength; j++)
            // for each character that both strings have the same amount of
            {
                if (audioFileName[j] == input[j])
                // if this character matches the character at the same position in the input
                {
                    matchingCharacters++;
                }
            }

            if (matchingCharacters == 0)
            {
                continue;
            }

            float percentMatch = (float)matchingCharacters / input.Length;
            float minimumMatch = 0.6f;

            if (percentMatch >= minimumMatch)
            // if this audio file is at least 60% similar to the user input
            {
                SelectedPlaylistListScript.CreateButton(SelectedPlaylistListScript.audioFiles[i].Name); // create button to select this audio file
                matchingAudioFiles.Add(SelectedPlaylistListScript.audioFiles[i]);
            }
        }

        SelectedPlaylistListScript.audioFiles = matchingAudioFiles.ToArray();
    }
}