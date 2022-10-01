using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MassDisplay : MonoBehaviour
{
    private string textMass = "";
    void Update()
    {
        textMass = "" + GetComponent<Rigidbody2D>().mass; 
        GetComponentInChildren<TMP_Text>().text = textMass;
    }
}
