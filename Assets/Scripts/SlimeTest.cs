using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NVIDIA.Flex;

public class SlimeTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Rigidbody>().velocity = new Vector3(4.0f, GetComponent<Rigidbody>().velocity.y);
        Debug.Log("I AM MOVING");
        //transform.Translate(1, 0, 0);

        //GetComponent<FlexActor>().Teleport(transform.position + new Vector3(0.1f, 0, 0), Quaternion.identity);
        GetComponent<FlexActor>().ApplyImpulse(new Vector3(100, 0, 0));
    }
}
