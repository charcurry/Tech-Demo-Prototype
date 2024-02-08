using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Checkpoints : MonoBehaviour
{

    private Vector3 initialPosition;
    [SerializeField] private Vector3 checkpointPosition;


    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 1f;
        initialPosition = transform.position;
    }

    void OnDeath()
    {
        if (checkpointPosition != null)
        {
            transform.position = checkpointPosition;
        }
        else
        {
            transform.position = initialPosition;
        }
    }

    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Kill Box"))
        {
            OnDeath();
        }

        if (other.gameObject.CompareTag("Checkpoint"))
        {
            checkpointPosition = other.transform.position;
        }
    }
}
