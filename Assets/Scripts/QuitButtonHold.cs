using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuitButtonHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    int PRSD=0;
    public void OnPointerDown(PointerEventData eventData)
    {
        Time.timeScale = 1.0f;
        PRSD = 1;
    }

    public void OnPointerUp(PointerEventData eventdata)
    {
        PRSD = 0;
    }



    private void Update()
    {
        if (PRSD == 1)
        {
            QuitFadeOutController.Completion += 0.05f;
        }
        else if (QuitFadeOutController.Completion > 0.0f)
        {
            QuitFadeOutController.Completion -= 0.05f;
        }
    }
}
