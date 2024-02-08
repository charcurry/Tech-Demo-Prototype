using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private GameObject[] positions;
    public int destination;

    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        positions = GameObject.FindGameObjectsWithTag("Waypoint");

        if (positions.Length != 0)
        {
            destination = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, positions[destination].transform.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(positions[destination].transform.position, transform.position) == 0)
        {
            destination++;
        }
        if (destination == positions.Length)
        {
            positions.Reverse();
            destination = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
