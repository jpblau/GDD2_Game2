using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // The current form this character is in
    public enum Form { Rock, Slime, Balloon};
    public Form playerForm;

    public float currentSpeed; // The player's current speed
    public float maxSpeed;  // The maximum speed out player can move
    public float constantSpeed; // The speed that we want the player to constantly be moving at to the right
    public float distanceToGround; // The distance at which the player is considered "on the ground." Read from the center of the player
    public bool grounded;  // Whether the player is on the ground or not

    private Rigidbody rb;   // The player's rigidbody which we will apply to forces to
    

    // Start is called before the first frame update
    void Start()
    {
        playerForm = Form.Rock;

        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // Check to see if we are grounded
        grounded = IsPlayerOnGround();
        if (grounded)
        {
            // Add a constant veloctiy to our player
            rb.AddForce(new Vector3(constantSpeed, 0.0f, 0.0f));
        }

        // Update our current speed calculations
        currentSpeed = rb.velocity.magnitude;
    }

    /// <summary>
    /// This method checks to see if the player is considered grounded
    /// </summary>
    /// <returns></returns>
    private bool IsPlayerOnGround()
    {
        Ray ray = new Ray(this.transform.position, new Vector3(0.0f, -1.0f, 0.0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)){
            if (hit.distance <= distanceToGround)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Changes the player's active form between rock(0), slime(1), and balloon(2)
    /// </summary>
    /// <param name="newForm">The new form the player is to assume</param>
    public void ChangeForm(int newForm)
    {
        this.playerForm = (Form)newForm;
        Debug.Log("Changed Form To: " + playerForm);

        switch (playerForm)
        {
            case Form.Rock:
                break;
            case Form.Slime:
                break;
            case Form.Balloon:
                break;
        }
    }
}
