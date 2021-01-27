using UnityEngine;

[DisallowMultipleComponent] //allows only one script per component
public class Oscillator : MonoBehaviour
{
    [SerializeField] private Vector3 movementVector;
    private float movementFactor; //TODO remove from the inspector later
    private Vector3 startPos, offset;
    [SerializeField] private float period = 2f;

    private void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (period <= Mathf.Epsilon) { return; } //if period tends to zero
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2f; //2 pi for a full circle rotation
        float rawSinWawe = Mathf.Sin(cycles * tau);

        movementFactor = rawSinWawe / 2f + 0.5f; //keeps the movement factor between 0 and 1;
        offset = movementFactor * movementVector;
        transform.position = startPos + offset;
    }
}