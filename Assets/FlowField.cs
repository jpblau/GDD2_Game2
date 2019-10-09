using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowField : MonoBehaviour
{

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
            other.transform.root.GetComponent<Rigidbody>().AddForce(new Vector3(100f, 0.0f, 0.0f));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.name == "Player")
        {
            other.transform.root.GetComponent<Rigidbody>().AddForce(new Vector3(10000f, 0.0f, 0.0f));
        }
    }

}
