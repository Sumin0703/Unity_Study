using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject orgTarget = null;
    public Transform target = null;
    public bool isTarget=true;
    float Speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (target == null)
            isTarget = false;

        if (isTarget == true)
        {
            
            target.Translate(Vector3.right * Speed * Time.deltaTime);
            if (target.position.x >= 9.0f)
            {
                Speed = -Speed;
            }
            else if (target.position.x <= -9.0f)
            {
                Speed = -Speed;
            }
        }


        if (isTarget == false)
        {
            GameObject obj = Instantiate(orgTarget);
            target = obj.GetComponent<Transform>();
            target.position = new Vector3(Random.Range(-9.0f, 9.0f), 1.0f, 9.45f);
            isTarget = true;
        }
    }

}
