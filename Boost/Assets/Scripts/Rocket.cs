using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    private Rigidbody rigidBody;
    private AudioSource audioSource;

    private enum State
    { Alive, Dying, Transcending };

    private State state = State.Alive;
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
        //TODO: stop sound on death
        if (state == State.Alive)
        {
            Thrust();
            Rotate();
        }
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

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) //To stop this from executing during the period between death and reload.
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Friendly":

                break;

            case "Finish":
                state = State.Transcending;
                Invoke("LoadNextScene", 1f); //TODO: parameterise time
                break;

            default:
                state = State.Dying;
                Invoke("LoadOnDeath", 1f);
                break;
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1); //TODO : appply to n levels.
    }

    private void LoadOnDeath()
    {
        SceneManager.LoadScene(0);
    }
}