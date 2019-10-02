using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject objectToFollow;  // This is the object the camera will be following

    public float zDistanceFromObject;  // The z distance at which we will be following our object

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = objectToFollow.transform.position;
        this.transform.position = new Vector3(newPos.x, newPos.y, zDistanceFromObject);
    }
}
