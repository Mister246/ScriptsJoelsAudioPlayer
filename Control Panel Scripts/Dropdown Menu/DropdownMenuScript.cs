using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class DropdownMenuScript : MonoBehaviour
{
    static public GameObject dropdownMenu;
    Button button;
    static public bool toggle = false;

    SpriteLibrary spriteLibrary;
    static public Sprite falseToggleSprite;
    static public Sprite trueToggleSprite;

    void Start()
    {
        spriteLibrary = GetComponent<SpriteLibrary>();
        falseToggleSprite = spriteLibrary.GetSprite("Toggle Sprites", "False");
        trueToggleSprite = spriteLibrary.GetSprite("Toggle Sprites", "True");

        button = GetComponent<Button>();

        dropdownMenu = GameObject.Find("Dropdown Menu");
        dropdownMenu.SetActive(false);

        button.onClick.AddListener(delegate { DisplayDropdownMenu(); } );
    }

    public static void DisplayDropdownMenu()
    // Displays the dropdown menu when clicking on this button
    // If dropdown menu is already displayed, it instead hides the dropdown menu
    {
        toggle = !toggle;
        if (toggle)
        {
            dropdownMenu.SetActive(true);
        }
        else
        {
            dropdownMenu.SetActive(false);
        }
    }
}
