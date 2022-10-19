using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PerformOperations : MonoBehaviour
{
    [SerializeField] GameObject leftWeight;
    [SerializeField] GameObject rightWeight;

    [SerializeField] Button plusButton;
    [SerializeField] Button minusButton;
    [SerializeField] Button multiplyButton;
    [SerializeField] Button divideButton;

    [SerializeField] Button leftSideButton;
    [SerializeField] Button bothSidesButton;
    [SerializeField] Button rightSideButton;
    [SerializeField] Button undoButton;
    [SerializeField] GameObject leftposition;
    [SerializeField] GameObject rightposition;

    GameObject[] numbersButtons;

    private float num = 0;
    private int lastOperation = 0;
    private float lastNumber = 0f;

    private int trialCount = 0;

    public void PerformOperation()
    {
        if(trialCount == 4)
        {
            Restart();
        }

        numbersButtons = GameObject.FindGameObjectsWithTag("NumberChoice");
        for(int i =0; i<numbersButtons.Length; i++)
        {
            if(numbersButtons[i].GetComponent<Button>().interactable == false)
            {
                num = i+1;
                break;
            }  
        }

        if(plusButton.interactable == false && num!=0)
        {
            if(leftSideButton.interactable == false)
            {
                AddOnLeft(num);
                lastNumber = num;
                lastOperation = 1;
                Invoke("Undo", 1f);           
            }
            if(rightSideButton.interactable == false)
            {
                AddOnRight(num);
                lastNumber = num;
                lastOperation = 2;
                Invoke("Undo", 1f);         
            }
            if(bothSidesButton.interactable == false)
            {
                AddOnRight(num);
                AddOnLeft(num);    
                trialCount++;    
            }
        }

        if(minusButton.interactable == false && num!=0)
        {
            float otherLeftMass=0f;
            float otherRightMass=0f;
            GameObject[] left = GameObject.FindGameObjectsWithTag("Left");
            GameObject[] right = GameObject.FindGameObjectsWithTag("Right");
            for (int i = 0; i < left.Length; i++)
            {
                if (left[i].name != "Unknown")
                {
                    otherLeftMass += left[i].GetComponent<Rigidbody2D>().mass;
                }
            }

            for (int i = 0; i < right.Length; i++)
            {
                if (right[i].name != "Unknown")
                {
                    otherRightMass += left[i].GetComponent<Rigidbody2D>().mass;
                }
            }

            if(leftSideButton.interactable == false)
            {
                if(otherLeftMass==0 || otherLeftMass<num)
                {
                    return;
                }
                TakeAwayOnLeft(num);
                lastNumber = num;
                lastOperation = 3;
                Invoke("Undo", 1f);         
            }
            if(rightSideButton.interactable == false)
            {
                if(otherRightMass==0 || otherRightMass<num)
                {
                    return;
                }
                TakeAwayOnRight(num);
                lastNumber = num;
                lastOperation = 4;
                Invoke("Undo", 1f);         
            }
            if(bothSidesButton.interactable == false)
            {
                if(otherLeftMass==0 || otherLeftMass<num || otherRightMass==0 || otherRightMass<num)
                {
                    return;
                }
                TakeAwayOnLeft(num);
                TakeAwayOnRight(num);
                trialCount++;
            }
        }

        if(multiplyButton.interactable == false && num!=0)
        {
            if(leftSideButton.interactable == false)
            {
                MultiplyLeft(num);
                lastNumber = num;
                lastOperation = 5;
                Invoke("Undo", 1f);         
            }
            if(rightSideButton.interactable == false)
            {
                MultiplyRight(num);
                lastNumber = num;
                lastOperation = 6;
                Invoke("Undo", 1f);         
            }
            if(bothSidesButton.interactable == false)
            {
                MultiplyLeft(num);
                MultiplyRight(num);
                trialCount++;
            }
        }

        if(divideButton.interactable == false && num!=0)
        {
            if(leftSideButton.interactable == false)
            {
                DivideLeft(num);
                lastNumber = num;
                lastOperation = 7;
                Invoke("Undo", 1f);         
            }
            if(rightSideButton.interactable == false)
            {
                DivideRight(num);
                lastNumber = num;
                lastOperation = 8;
                Invoke("Undo", 1f);         
            }
            if(bothSidesButton.interactable == false)
            {
                DivideLeft(num);
                DivideRight(num);
                trialCount++;
            }
        }
    }
    
    private void AddOnLeft(float num)
    {
        GameObject[] left = GameObject.FindGameObjectsWithTag("Left");
        for(int i=left.Length-1; i>=0; i--)
        {
            if(left[i].name != "Unknown")
            {
                left[i].GetComponent<Rigidbody2D>().mass+=num;
                return;
            }
        }
        GameObject weightClone = Instantiate(leftWeight, leftposition.transform.position, Quaternion.identity ,leftposition.transform);
        weightClone.GetComponent<Rigidbody2D>().mass = num;
        weightClone.transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);

    }
    private void AddOnRight(float num)
    {
      //  GameObject weightClone = Instantiate(rightWeight);
       // weightClone.GetComponent<Rigidbody2D>().mass = num;
        GameObject[] right = GameObject.FindGameObjectsWithTag("Right");
        for(int i=right.Length-1; i>=0; i--)
        {
            if(right[i].name != "Unknown")
            {
                right[i].GetComponent<Rigidbody2D>().mass+=num;
                return;
            }
        }
        GameObject weightClone = Instantiate(rightWeight, rightposition.transform.position, Quaternion.identity ,rightposition.transform);
        weightClone.GetComponent<Rigidbody2D>().mass = num;
        weightClone.transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
    }

    private void TakeAwayOnLeft(float num)
    {
        GameObject[] left = GameObject.FindGameObjectsWithTag("Left");
        for(int i=left.Length-1; i>=0; i--)
        {
            if(left[i].GetComponent<Rigidbody2D>().mass > num)
            {
                left[i].GetComponent<Rigidbody2D>().mass-=num;
                break;
            }
            else if(left[i].GetComponent<Rigidbody2D>().mass < num)
            {
                num -= left[i].GetComponent<Rigidbody2D>().mass;
                Destroy(left[i]);
                continue;
            }
            else
            {
               Destroy(left[i]);
               break;
            } 
        }
    }
    private void TakeAwayOnRight(float num)
    {
        GameObject[] right = GameObject.FindGameObjectsWithTag("Right");
        for(int i=right.Length-1; i>=0; i--)
        {
            if(right[i].GetComponent<Rigidbody2D>().mass > num)
            {
                right[i].GetComponent<Rigidbody2D>().mass-=num;
                break;
            }
            else if(right[i].GetComponent<Rigidbody2D>().mass < num)
            {
                num -= right[i].GetComponent<Rigidbody2D>().mass;
                Destroy(right[i]);
                continue;
            }
            else
            {
               Destroy(right[i]);
               break;
            } 
        }
    }

    private void MultiplyLeft(float num)
    {
        GameObject[] left = GameObject.FindGameObjectsWithTag("Left");
        for(int i=0; i<left.Length; i++)
        {
            left[i].GetComponent<Rigidbody2D>().mass *= num;
        }
    }
    private void MultiplyRight(float num)
    {
        GameObject[] right = GameObject.FindGameObjectsWithTag("Right");
            for(int i=0; i<right.Length; i++)
            {
                right[i].GetComponent<Rigidbody2D>().mass *= num;
            }
    }
    private void DivideLeft(float num)
    {
        GameObject[] left = GameObject.FindGameObjectsWithTag("Left");
        for(int i=0; i<left.Length; i++)
        {
            left[i].GetComponent<Rigidbody2D>().mass /= num;
        }

    }

    private void DivideRight(float num)
    {
        GameObject[] right = GameObject.FindGameObjectsWithTag("Right");
        for(int i=0; i<right.Length; i++)
        {
            right[i].GetComponent<Rigidbody2D>().mass /= num;
        }
    }

    private void Undo()
    {
        switch(lastOperation)
        {
            case 1:
                TakeAwayOnLeft(lastNumber);
                break;
            case 2:
                TakeAwayOnRight(lastNumber);
                break;
            case 3:
                AddOnLeft(lastNumber);
                break;
            case 4:
                AddOnRight(lastNumber);
                break;
            case 5:
                DivideLeft(lastNumber);
                break;
            case 6:
                DivideRight(lastNumber);
                break;
            case 7:
                MultiplyLeft(lastNumber);
                break;
            case 8:
                MultiplyRight(lastNumber);
                break;
            default:
                break;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
