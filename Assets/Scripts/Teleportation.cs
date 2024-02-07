using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public Transform destination;
    public GameObject player;
    public float heightOffset = 1;
    public bool arrived;
    public string tag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!arrived)
        {
            if (tag != "")
            {
                if (other.gameObject.CompareTag(tag))
                {
                    other.transform.position = destination.position + new Vector3(0, heightOffset, 0);
                    destination.GetComponent<Teleportation>().arrived = true;
                }
            }
            else
            {
                other.transform.position = destination.position + new Vector3(0, heightOffset, 0);
                destination.GetComponent<Teleportation>().arrived = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        arrived = false;
    }
}
