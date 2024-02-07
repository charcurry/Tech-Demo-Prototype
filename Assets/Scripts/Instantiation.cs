using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiation : MonoBehaviour
{

    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        GameObject cube;
        GameObject newObject;
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //newObject = GameObject.Instantiate(prefab);

        cube.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
