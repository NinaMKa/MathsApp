using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnknownNumberDisplay : MonoBehaviour
{
    private float originalUnknownMass = 0f;
    private string textMass = "";

    void Start()
    {
        originalUnknownMass = GetComponent<Rigidbody2D>().mass;
    }

    void Update()
    {
        if(GetComponent<Rigidbody2D>().mass/originalUnknownMass != 1)
        {
            textMass = "" + string.Format("{0:0.##}", GetComponent<Rigidbody2D>().mass/originalUnknownMass) + "X"; 
        }
        else
        {
             textMass = "X"; 
        }
        GetComponentInChildren<TMP_Text>().text = textMass;
    }
}
