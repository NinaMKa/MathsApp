using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleRotate : MonoBehaviour
{
    GameObject[] left;
    GameObject[] right;
    private float leftMass;
    private float rightMass;

    Quaternion startRotation;
    float time;
  
      
    void Start()
    {
        startRotation = transform.rotation;
    }

    void Update()
    {
        leftMass = 0f;
        rightMass = 0f;
        left = GameObject.FindGameObjectsWithTag("Left");
        right = GameObject.FindGameObjectsWithTag("Right");
        transform.rotation = Quaternion.Slerp(startRotation, Quaternion.identity, time);
        time += Time.deltaTime;

        for(int i=0; i<left.Length; i++)
        {
            leftMass += left[i].GetComponent<Rigidbody2D>().mass;
        }

        for(int i=0; i<right.Length; i++)
        {
            rightMass += right[i].GetComponent<Rigidbody2D>().mass;
        }

        if(leftMass > rightMass)
        {
            transform.Rotate( 0, 0, 10); //*speed *Time.deltaTime);
        }
        
        if(leftMass < rightMass)
        {
            transform.Rotate( 0, 0, -10);// *speed *Time.deltaTime);
        }

        else
        {
            transform.Rotate( 0, 0, 0);
        }
    }
}