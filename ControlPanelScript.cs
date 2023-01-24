using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanelScript : MonoBehaviour
{
    static public Text ControlPanelText;
    void Start()
    {
        ControlPanelText = GetComponentInChildren<Text>();
    }
}
