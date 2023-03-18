using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KnobScript : MonoBehaviour, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        ProgressBarScript.ChangeKnobPosition(eventData);
    }
}
