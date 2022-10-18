using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleMovement : MonoBehaviour
{
    GameObject[] left;
    GameObject[] right;
    private float leftMass;
    private float rightMass;
    private Animator animator;

   // Quaternion startRotation;
   // float time;
  
      
    void Start()
    {
       // startRotation = transform.rotation;
       animator = GetComponent<Animator>();
      // animator.SetBool("balanced", true);
    }

    void Update()
    {
        leftMass = 0f;
        rightMass = 0f;
        left = GameObject.FindGameObjectsWithTag("Left");
        right = GameObject.FindGameObjectsWithTag("Right");
       // transform.rotation = Quaternion.Slerp(startRotation, Quaternion.identity, time);
       // time += Time.deltaTime;

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
           // transform.Rotate( 0, 0, 10); //*speed *Time.deltaTime);
           animator.SetBool("balanced", false);
           animator.SetBool("rightDown", false);
           animator.SetBool("leftDown", true);
        }
        
        if(leftMass < rightMass)
        {
            //transform.Rotate( 0, 0, -10);// *speed *Time.deltaTime);
            animator.SetBool("balanced", false);
            animator.SetBool("leftDown", false);
            animator.SetBool("rightDown", true);
        }

        else
        {
            animator.SetBool("leftDown", false);
            animator.SetBool("rightDown", false);
            animator.SetBool("balanced", true);
            //transform.Rotate( 0, 0, 0);
        }
    }
}
