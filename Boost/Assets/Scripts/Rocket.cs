using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

       private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space)) // thrust (while rotating too)
        {
            rigidBody.AddRelativeForce(Vector3.up);
        }

        if (Input.GetKey(KeyCode.A))
        {
            print("Rotate left!"); 
        }

        else if (Input.GetKey(KeyCode.D)) //else if prevents simultaneous left-right rotation
        {
            print("Roate right!");
        }
    }
}
