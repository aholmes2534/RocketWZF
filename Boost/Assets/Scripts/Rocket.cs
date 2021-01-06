using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Rigidbody rigidBody;

    // Start is called before the first frame update
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        Thrust();
        Rotate();
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space)) // thrust (while rotating too)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            rigidBody.AddRelativeForce(Vector3.up);
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true; //manual control of the rotation
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D)) //else if prevents simultaneous left-right rotation
        {
            transform.Rotate(-Vector3.forward);
        }

        rigidBody.freezeRotation = false; //resume physics controlled rotation
    }
}