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

    private float num = 0;
    private int lastOperation = 0;
    private float lastNumber = 0f;

    public void PerformOperation()
    {
        GameObject[] numbersButtons;
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
            }
        }

        if(minusButton.interactable == false && num!=0)
        {
            if(leftSideButton.interactable == false)
            {
                TakeAwayOnLeft(num);
                lastNumber = num;
                lastOperation = 3;
                Invoke("Undo", 1f);         
            }
            if(rightSideButton.interactable == false)
            {
                TakeAwayOnRight(num);
                lastNumber = num;
                lastOperation = 4;
                Invoke("Undo", 1f);         
            }
            if(bothSidesButton.interactable == false)
            {
                TakeAwayOnLeft(num);
                TakeAwayOnRight(num);
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
            }
        }

    }
    
    private void AddOnLeft(float num)
    {
        GameObject weightClone = Instantiate(leftWeight);
        weightClone.GetComponent<Rigidbody2D>().mass = num;
    }

    private void AddOnRight(float num)
    {
        GameObject weightClone = Instantiate(rightWeight);
        weightClone.GetComponent<Rigidbody2D>().mass = num;
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
            if(left[i].GetComponent<Rigidbody2D>().mass < num)
            {
                num -= left[i].GetComponent<Rigidbody2D>().mass;
                Destroy(left[i]);
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
            if(right[i].GetComponent<Rigidbody2D>().mass < num)
            {
                num -= right[i].GetComponent<Rigidbody2D>().mass;
                Destroy(right[i]);
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
                right[i].GetComponent<Rigidbody2D>().mass /= num;
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
