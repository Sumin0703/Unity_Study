using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbital_period : MonoBehaviour
{
    public float rotation;
    Vector3 dir = new Vector3(0, 1, 0);
    float delta;
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float rotate = 360.0f/rotation;
        transform.RotateAround(obj.transform.position,dir, rotate * Time.deltaTime);
        //transform.Rotate(dir * rotate * Time.deltaTime, Space.World);
    }
}
