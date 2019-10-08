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

    private void FixedUpdate()
    {
        if(this.gameObject.transform.position.x + this.GetComponent<Collider>().bounds.size.x/2 <= player.gameObject.transform.position.x + this.GetComponent<Collider>().bounds.size.x/2 
            && this.gameObject.transform.position.x - this.GetComponent<Collider>().bounds.size.x/2  >= player.gameObject.transform.position.x - this.GetComponent<Collider>().bounds.size.x/2)
        {
            Debug.Log("Intersect");
            player.gameObject.GetComponent<PlayerController>().constantSpeed += .2f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().constantSpeed += .2f;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().constantSpeed -= .2f;
        }
    }
}
