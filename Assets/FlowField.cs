using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowField : MonoBehaviour
{


    public float startPush; //The intial force that is added when the player first enters the flow field
    public float constantPush; //The constant push that the player is given the whole time they stay in the field
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  


    private void OnTriggerStay(Collider other)
    {
        
        if (other.transform.root.name == "Player")
        {
            other.transform.root.GetComponent<Rigidbody>().AddForce(new Vector3(constantPush, 0.0f, 0.0f));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.name == "Player")
        {
            other.transform.root.GetComponent<Rigidbody>().AddForce(new Vector3(startPush, 0.0f, 0.0f));
        }
    }

}
