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
    [SerializeField] private float levelLoadDelay = 2f;
    [SerializeField] private AudioClip mainEngine, deathSFX, successSFX;
    [SerializeField] private ParticleSystem engineParticles, successParticles, deathParticles;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (state == State.Alive)
        {
            GetThrustInput();
            GetRotateInput();
        }
    }

    private void GetThrustInput()
    {
        if (Input.GetKey(KeyCode.Space)) // thrust (while rotating too)
        {
            ApplyThrust(audioSource);
        }
        else
        {
            audioSource.Stop();
            engineParticles.Stop();
        }
    }

    private void ApplyThrust(AudioSource audioSource) //applies thrust with sound
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(mainEngine);

        if (!engineParticles.isPlaying)
        {
            engineParticles.Play();
        }
    }

    private void GetRotateInput()
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
                //do nothing
                break;

            case "Finish":
                state = State.Transcending;
                audioSource.Stop();
                audioSource.PlayOneShot(successSFX);
                successParticles.Play();
                Invoke("LoadNextScene", levelLoadDelay);
                break;

            default:
                state = State.Dying;
                audioSource.Stop();
                audioSource.PlayOneShot(deathSFX);
                deathParticles.Play();
                Invoke("LoadOnDeath", levelLoadDelay);
                break;
        }
    }

    private void LoadNextScene() //loads the next level in queue
    {
        SceneManager.LoadScene(1); //TODO : appply to n levels.
    }

    private void LoadOnDeath() //loads level 1
    {
        SceneManager.LoadScene(0);
    }
}