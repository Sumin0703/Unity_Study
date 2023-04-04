using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor_2 : MonoBehaviour
{
    public LayerMask crashMask;
    public Transform myTarget;
    public float Speed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        //myTarget = GameObject.Find("Earth").transform; 
        myTarget = DefenceEarth.Instance.MyEarth;
        StartCoroutine(MovingToTarget());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & crashMask) != 0)
        {
            //Destroy(other.gameObject);
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }

    IEnumerator MovingToTarget()
    {
        Vector3 dir = myTarget.position - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();
        while (dist > 0.0f)
        {
            float delta = Speed * Time.deltaTime;
            if (dist - delta<0.0f)
            {
                delta = dist;
            }
            transform.Translate(dir * delta);
            dist -= delta;
            yield return null;

        }
    }
}
