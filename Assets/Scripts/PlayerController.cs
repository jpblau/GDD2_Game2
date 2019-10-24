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
    public float minSpeed; //The minimum speed that the player constantly moves right
    public float constantSpeed; // The speed that we want the player to constantly be moving at to the right
    public float distanceToGround; // The distance at which the player is considered "on the ground." Read from the center of the player
    public float floatForce = 12.0f;
    public bool grounded;  // Whether the player is on the ground or not
    public List<PhysicMaterial> listOfPhysicsMats;  // the list of all our physics materials, for each playerForm
    public List<GameObject> listOfFormMeshes;   // the list of all our child gameobjects that we are enabling to swap the mesh of the character
    public List<float> masses = new List<float>();  // the list of all the masses of each form
    public List<float> drags = new List<float>();   // the list of all the drags of each form
    public GameObject key;          //Reference to a key in the level
    public GameObject gate;         //Reference to a gate in the level
    public List<AudioClip> soundFX; // All the sound effects that this object can produce
    public float balloonBounceMax; //Max value that the balloon can be
    public particleSystemHead particleSystemHead; // Our particle system for the game, which we will move around the scene as needed

    private bool hasKey;     //Bool to check if the player has the key with them
    private Rigidbody rb;   // The player's rigidbody which we will apply to forces to
    //private PhysicMaterial pm; // The player's current physics material, based on their playerForm
    private GameObject activeChildForm; // The player's active gameobject child, based on the player's current form. 
    private GameManager gm;
    private Form previousForm; //The form that the player was in the last run through of the code
    private UIManager ui;
    private AudioSource audioSource;    // The player's audio source for playing sound effects
    
    

    // Start is called before the first frame update
    void Start()
    {
        playerForm = Form.Rock;

        rb = this.gameObject.GetComponent<Rigidbody>();
//pm = this.gameObject.GetComponent<BoxCollider>().material;
        activeChildForm = listOfFormMeshes[0];
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        ui= GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        audioSource = this.gameObject.GetComponent<AudioSource>();

        hasKey = false;

        Debug.Log(rb);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // Check to see if we just hit the ground (also lets set grounded while we're at it)
        bool wasGrounded = grounded;
        grounded = IsPlayerOnGround();

        // Shake the camera if we just hit the ground
        if (!wasGrounded && grounded && transform.position != gm.transform.position)
        {
            switch (playerForm)
            {
                case Form.Rock:
                    ui.ShakeCamera(.05f, .1f);
                    break;
                case Form.Slime:
                    ui.ShakeCamera(.02f, .01f);
                    break;
                case Form.Balloon:
                    break;
            }
            
        }

        //Add constant force to our player
        if (grounded)
        {
            rb.velocity = new Vector3(minSpeed, rb.velocity.y);
            if (playerForm == Form.Slime)
            {
                particleSystemHead.PlaySlimeRoll(this.transform.position);
            }
        }

        //Make balloon float
        if (playerForm == Form.Balloon)
        {
            rb.AddForce(new Vector3(0.0f, floatForce *1.5f, 0.0f));
            rb.AddForce(new Vector3(constantSpeed / 5, 0.0f, 0.0f));
        }

        // Make the rock fall down super fast
        if (playerForm == Form.Rock)
        {
            rb.AddForce(new Vector3(0.0f, -9.81f * 2, 0.0f));
            //Adding extra downward force if the player just changed from another form to rock
            if (previousForm != Form.Rock)
            {
                //rb.AddForce(new Vector3(0.0f, -9.81f * 1000, 0.0f));
            }
        }

        if(rb.velocity.x >= maxSpeed)
        {
            rb.velocity = new Vector3(maxSpeed, rb.velocity.y);
        }

        if (rb.velocity.y >= maxSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, maxSpeed);
        }

        // Update our current speed calculations
        currentSpeed = rb.velocity.magnitude;
        previousForm = playerForm;
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
            //TODO there's a lot of repeated code here-- is it necessary? 
            case Form.Rock:
                rb.mass = masses[newForm];
                rb.drag = drags[newForm];
                activeChildForm.SetActive(false);
                activeChildForm = listOfFormMeshes[newForm];
                activeChildForm.SetActive(true);
                break;
            case Form.Slime:
                rb.mass = masses[newForm];
                rb.drag = drags[newForm];
                activeChildForm.SetActive(false);
                activeChildForm = listOfFormMeshes[newForm];
                activeChildForm.SetActive(true);
                break;
            case Form.Balloon:
                rb.mass = masses[newForm];
                rb.drag = drags[newForm];
                activeChildForm.SetActive(false);
                activeChildForm = listOfFormMeshes[newForm];
                activeChildForm.SetActive(true);
                SetAudioClipAndPlay(3);
                break;
        }
    }


    /// <summary>
    /// Checks for collosions between player and environment
    /// </summary>
    /// <param name="col">Collision being checked for</param>
    private void OnCollisionEnter(Collision col)
    {
        if (playerForm == Form.Balloon)
        {
            //Debug.Log(rb.velocity.y + ":upward force");
            if (col.gameObject.GetComponent<Renderer>().bounds.min.y > activeChildForm.gameObject.GetComponent<Renderer>().bounds.center.y)
            {
                //Debug.Log("Value:" + (rb.velocity.y * 500) * -.35f);
                if ((rb.velocity.y * 500) * -.35f > balloonBounceMax)
                {
                    //Debug.Log("Default");
                    rb.AddForce(new Vector3(0, balloonBounceMax));
                }
                else
                {
                    //Debug.Log("Not Default");
                    rb.AddForce(new Vector3(0, (rb.velocity.y * 500) * -.35f));
                }
            }
        }
        switch (col.gameObject.tag)
        {
            case "Spike":
                if (playerForm == Form.Balloon)
                {
                    // Let's restart the level
                    rb.velocity = Vector3.zero;
                    gm.RestartLevel();
                    //key.gameObject.SetActive(true);
                    hasKey = false;
                    SetAudioClipAndPlay(4);
                }
                break;
            case "Flag":
                rb.velocity = Vector3.zero;
                ui.winCanvas.gameObject.SetActive(true);
                break;
            case "Key":
                key.gameObject.SetActive(false);
                hasKey = true;
                break;
            case "Gate":
                if (hasKey) gate.gameObject.SetActive(false);
                else gm.RestartLevel();
                break;
            case "Wall":
                switch (playerForm)
                {
                    case Form.Rock:
                        SetAudioClipAndPlay(1);
                        break;
                    case Form.Slime:
                        SetAudioClipAndPlay(2);
                        break;
                    case Form.Balloon:
                        SetAudioClipAndPlay(0);
                        break;
                }
                audioSource.Play();
                break;
            case "Ground":
                switch (playerForm)
                {
                    case Form.Rock:
                        SetAudioClipAndPlay(1);
                        particleSystemHead.PlayGroundHit(col.GetContact(0).point);
                        break;
                    case Form.Slime:
                        SetAudioClipAndPlay(2);
                        break;
                    case Form.Balloon:
                        SetAudioClipAndPlay(0);
                        break;
                }
                break;
        }

        

        /*if(col.gameObject.tag.Equals("Spike")&&playerForm==Form.Balloon)

        {
            // Let's restart the level
            rb.velocity = Vector3.zero;
            gm.RestartLevel();
            key.gameObject.SetActive(true);
            hasKey = false;
        }
        if(col.gameObject.tag.Equals("Flag"))
        {
            rb.velocity = Vector3.zero;
            ui.winCanvas.gameObject.SetActive(true);
        }
        if(col.gameObject.tag.Equals("Key"))
        {
            key.gameObject.SetActive(false);
            hasKey = true;
        }
        if (col.gameObject.tag.Equals("Gate")&&hasKey==true)
        {
            gate.gameObject.SetActive(false);
        }
        if(col.gameObject.tag.Equals("Gate")&&hasKey==false)
        {
            gm.RestartLevel();
        }*/
    }

    /// <summary>
    /// Sets the audio source's clip to the provided index, and then plays that sound.
    /// </summary>
    /// <param name="index">The index of the sound to play in soundFX. Can pass in 0 for NULL</param>
    public void SetAudioClipAndPlay(int index)
    {
        audioSource.clip = soundFX[index];
        audioSource.Play();
    }
}
