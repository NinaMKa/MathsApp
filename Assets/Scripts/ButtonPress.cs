using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour
{
    public void ClickedButton (Button btn)
    {
        btn.interactable = false;
        GameObject[] otherButtons;
        otherButtons = GameObject.FindGameObjectsWithTag(btn.tag);
        for(int i=0; i<otherButtons.Length; i++)
        {
            if(otherButtons[i].GetComponent<Button>() != btn)
            {
               otherButtons[i].GetComponent<Button>().interactable =  true;
            }
        }
    }
}
