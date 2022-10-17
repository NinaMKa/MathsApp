using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MassDisplay : MonoBehaviour
{
    private string textMass = "";
    void Update()
    {
        textMass = "" + string.Format("{0:0.##}", GetComponent<Rigidbody2D>().mass); //ToString("n2");
        GetComponentInChildren<TMP_Text>().text = textMass;
    }
}
