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
            AudioSource audioSource=GetComponent<AudioSource>();
            rigidBody.AddRelativeForce(Vector3.up);
            if(!audioSource.isPlaying)
                audioSource.Play();
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);
        }

        else if (Input.GetKey(KeyCode.D)) //else if prevents simultaneous left-right rotation
        {
            transform.Rotate(-Vector3.forward);
        }
    }
}
