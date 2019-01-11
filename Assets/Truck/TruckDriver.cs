using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckDriver : MonoBehaviour
{
    // Which input is checked 
    public int playerNumber = 1;
    
    // We need to store references to the Unity components we want to access
    // Alternatively, we could make them public and set them within the inspector in Unity
    // 'private' states that this variable will not be accessible from other classes, and will not show in the Unity inspector
    // WheelJoint2D is the type of the variable
    // [] denotes that this variable is an array and does not just contain one WheelJoint2D, but multiple.
    private WheelJoint2D[] wheelJoints;
    private JointMotor2D jointMotor;
    
    // We'll save the truck's initial position, so we can quickly reset it
    private Vector2 startPos;
    
    private Rigidbody2D rigidbody2D;

    // This is where we'll store the truck's max wheel motor speed
    // It is public, so it will show up in the inspector, where we can assign it a value
    // A float is just a number with decimal places (which also has an f at the end, because reasons)
    public float maxMovementSpeed;
    public float motorTorque;
    public float jumpForce;

    // The sprite renderer for our truck's body, so we can flip it.
    // No need to set this as public, since we'll set it in our code in our Awake() method
    private SpriteRenderer bodySprite;

    // This is called at the beginning of the game
    private void Awake()
    {
        // GetComponentsInChildren will search the GameObject for WheelJoint2D components, and returns everything it finds as an array of that type
        // This should find two wheel joints, one for each wheel, and save them in wheelJoints
        wheelJoints = GetComponents<WheelJoint2D>();

        // Here we're going to make a motor for our wheels
        jointMotor = new JointMotor2D();

        // Save truck's starting position
        startPos = transform.position;

        // Get our RigidBody2D (2D physics controller)
        rigidbody2D = GetComponent<Rigidbody2D>();

        // And our sprite renderer
        bodySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        ManageInput();
    }

    // Check for player input
    private void ManageInput()
    {
        // Set the maximum torque (power) of the motor to our motorTorque variable times the always positive (absolute) value of the horizontal input
        jointMotor.maxMotorTorque = motorTorque * Mathf.Abs(Input.GetAxisRaw("Horizontal " + playerNumber));

        // Set the motor's speed to our horizontal input times our movement speed (and then reversed with -1)
        jointMotor.motorSpeed = Input.GetAxis("Horizontal " + playerNumber) * maxMovementSpeed * -1f;

        // Our truck's body is flipped, if it's
        bodySprite.flipX = wheelJoints[1].jointSpeed > 0;

        // Assign this motor to our wheels' motors
        wheelJoints[0].motor = wheelJoints[1].motor = jointMotor;


        // Check for player input (Specific key)
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }

        // Check for player input (As defined in Edit > Project Settings > Input)
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void Jump()
    {
        // TODO Check if the truck is grounded

        // Add a force it the upwards direction (relative to the truck's rotation) of the size jumpForce
        rigidbody2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void Reset()
    {
        // Set the truck's position to the position saved in Awake
        transform.position = startPos;

        // Reset the truck's rotation 
        transform.rotation = Quaternion.identity;
    }
}