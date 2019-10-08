using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject objectToFollow;  // This is the object the camera will be following

    public float zDistanceFromObject;  // The z distance at which we will be following our object

    private Vector3 positionToFollow;   // The actual position we are tracking.
    private Vector3 offset = new Vector3(1.0f, 1.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        positionToFollow = objectToFollow.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Update our position to follow
        positionToFollow = objectToFollow.transform.position;

        offset = objectToFollow.GetComponent<Rigidbody>().velocity.normalized;
        offset = new Vector3(offset.x * 0.01f, offset.y * 0.01f, offset.z * 0.01f); // We can change this multiplier based on what form the player is in

        

        Vector3 newPos = positionToFollow - offset;
        this.transform.position = new Vector3(newPos.x, newPos.y, zDistanceFromObject);
    }
}
