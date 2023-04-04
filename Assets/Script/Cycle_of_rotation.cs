using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cycle_of_rotation : MonoBehaviour
{
    public float cycle;
    Vector3 dir = new Vector3(0, 1, 0);
    float delta;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float rotate = 360.0f / cycle;
        transform.Rotate(dir * rotate * Time.deltaTime,Space.Self);
    }
}
