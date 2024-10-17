using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
	public float speed;
    public float zigzagFrequency = 2f; // Adjust the frequency of the zig-zag
    public float zigzagAmplitude = 1f;  // Adjust the amplitude of the zig-zag

    private GameController gameController;
    private float startTime;

    void Start ()
	{
        if (tag == "Enemy")
        {
            gameController = FindObjectOfType<GameController>(); // Get reference to the GameController
            GetComponent<Rigidbody>().velocity = transform.forward * speed;
            startTime = Time.time; // Store the start time for zig-zag calculation
        }
        else
        {
            GetComponent<Rigidbody>().velocity = transform.forward * speed;
        }
	}

    void FixedUpdate()
    {
        if (gameController.wavecount == 2 && tag == "Enemy")
        {
            // Calculate the zig-zag offset
            float zigZagOffset = Mathf.Sin((Time.time - startTime) * zigzagFrequency) * zigzagAmplitude;

            // Set the velocity: move down the Z axis and zigzag in the X direction
            Vector3 newVelocity = new Vector3(zigZagOffset, 0f, speed); // Assuming moving down the negative Z axis
            GetComponent<Rigidbody>().velocity = newVelocity; // Apply the new velocity
        }
    }
}
