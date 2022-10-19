using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EquationDisplay : MonoBehaviour
{
    private float originalUnknownMass = 0f;
    GameObject[] left;
    GameObject[] right;
    [SerializeField] private Text leftText;
    [SerializeField] private Text rightText;
    [SerializeField] private Image cross;
    private string leftString;
    private string rightString;
    private float leftMass;
    private float rightMass;
    private float unknownMassLeft;
    private float unknownMassRight;
    private float otherLeftMass;
    private float otherRightMass;
    private float unknownNumberLeft;
    private float unknownNumberRight;

    void Start()
    {
        originalUnknownMass = GameObject.Find("Unknown").GetComponent<Rigidbody2D>().mass;
    }

    void Update()
    {
        leftString = "";
        rightString = "";
        cross.GetComponent<Image>().enabled = false;

        left = GameObject.FindGameObjectsWithTag("Left");
        right = GameObject.FindGameObjectsWithTag("Right");

        leftMass = 0f;
        rightMass = 0f;
        unknownMassLeft = 0f;
        unknownMassRight = 0f;

        otherLeftMass = 0f;
        otherRightMass = 0f;

        unknownNumberLeft = 0f;
        unknownNumberRight = 0f;

        for (int i = 0; i < left.Length; i++)
        {
            leftMass += left[i].GetComponent<Rigidbody2D>().mass;
            if (left[i].name == "Unknown")
            {
                unknownMassLeft += left[i].GetComponent<Rigidbody2D>().mass;
            }
            else
            {
                otherLeftMass += left[i].GetComponent<Rigidbody2D>().mass;
            }
        }

        for (int i = 0; i < right.Length; i++)
        {
            rightMass += right[i].GetComponent<Rigidbody2D>().mass;
            if (right[i].name == "Unknown")
            {
                unknownMassRight += right[i].GetComponent<Rigidbody2D>().mass;
            }
            else
            {
                otherRightMass += right[i].GetComponent<Rigidbody2D>().mass;
            }
        }

        unknownNumberLeft = unknownMassLeft/originalUnknownMass;
        unknownNumberLeft = unknownMassLeft/originalUnknownMass;

        if (unknownNumberLeft == 0)
        {
            leftString = "" + string.Format("{0:0.##}", otherLeftMass);
        } 
        if (unknownNumberLeft == 1 && otherLeftMass == 0)
        {
            leftString = "X ";
        }
        if (unknownNumberLeft == 1 && otherLeftMass != 0)
        {
            leftString = "X + " + string.Format("{0:0.##}", otherLeftMass);
        }
        if (unknownNumberLeft != 0 && unknownNumberLeft != 1 && otherLeftMass == 0)
        {
            leftString = "" + string.Format("{0:0.##}", unknownNumberLeft) + "X ";
        }
        if (unknownNumberLeft != 0 && unknownNumberLeft != 1 && otherLeftMass != 0)
        {
            leftString = "" + string.Format("{0:0.##}", unknownNumberLeft) + "X + " + string.Format("{0:0.##}", otherLeftMass);
        }

        if (unknownNumberRight == 0)
        {
            rightString = "" + string.Format("{0:0.##}", otherRightMass);
        }
        if (unknownNumberRight == 1 && otherRightMass == 0)
        {
            rightString = "X ";
        }
        if (unknownNumberRight == 1 && otherRightMass != 0)
        {
            rightString = "X + " + string.Format("{0:0.##}", otherRightMass);
        }
        if (unknownNumberRight != 0 && unknownNumberRight != 1 && otherRightMass == 0)
        {
            rightString = "" + string.Format("{0:0.##}", unknownNumberRight) + "X ";
        }
        if (unknownNumberRight != 0 && unknownNumberRight != 1 && otherRightMass != 0)
        {
            rightString = "" + string.Format("{0:0.##}", unknownNumberRight) + "X + " + string.Format("{0:0.##}", otherRightMass);
        }


        leftText.text = "" + leftString;
        rightText.text = "" + rightString;

        if((leftString == "X " && rightString==string.Format("{0:0.##}", originalUnknownMass)) || (leftString==string.Format("{0:0.##}", originalUnknownMass) && rightString == "X "))
        {
            Invoke("LoadNextScene", 1f);
        }

        if (leftMass != rightMass)
        {
            cross.GetComponent<Image>().enabled = true;
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
