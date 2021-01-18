using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    private Rigidbody rigidBody;
    private AudioSource audioSource;
    [SerializeField] private float rcsThrust = 100f;
    [SerializeField] private float mainThrust = 100f;

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
            audioSource = GetComponent<AudioSource>();
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
    }

    private void Rotate()
    {
        float rotationSpeed = rcsThrust * Time.deltaTime;
        rigidBody.freezeRotation = true; //manual control of the rotation
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D)) //else if prevents simultaneous left-right rotation
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }

        rigidBody.freezeRotation = false; //resume physics controlled rotation
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
               //nothing.
                break;
            case "Finish":
                SceneManager.LoadScene(1);
                break;
            default:
                SceneManager.LoadScene(0);
                break;

        }
    }
}