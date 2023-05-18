using Unity.Netcode;
using UnityEngine;

/// <summary>
/// Handles movement and actions of the player gameobject
/// </summary>
public class CharacterController : NetworkBehaviour
{
    [SerializeField]
    private float speed = 5.0f;

    public Rigidbody rb;
    public float jumpAmount = 10.0f;
    private bool isGrounded = true;

    AudioSource m_Greeting;
    bool m_Play;
    bool m_ToggleChange;

    private void Start()
    {
        // spawn to a random position in the room
        transform.position = new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));

        //Fetch the AudioSource from the GameObject
        m_Greeting = GetComponent<AudioSource>();
        //Ensure the toggle is set to true for the music to play at start-up
        m_Play = false;
    }

    private void HandleKeyboardMovement()
    {
        // alternative movement
        /*
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(xInput, 0, yInput).normalized;
        transform.Translate(speed * Time.deltaTime * moveDirection);
        */

        // basic movement with WASD
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * speed * Time.deltaTime;
        }

        // attack with space
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Rotate(500.0f * Time.deltaTime, 0, 0);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            transform.rotation = Quaternion.identity;
        }
    }

    private void HandleJump()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 2f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
        }
    }

    private void HandleAudio()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            m_Greeting.Play();
        }
    }

    void Update()
    {
        // ownership check so that we wont move other clients gameobjects
        if (!IsOwner) return;

        HandleKeyboardMovement();
        HandleJump();
        HandleAudio();
    }
}
