using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
   
    public float MoveSpeed = 0.005f;
    public static float Velocity = 0;

    Vector3 targetRot;
    Vector3 targetPos;

    public LayerMask TargetMask;
    public LayerMask MeteorMask;
    public GameObject dieEffect=null;

    bool isQuitting = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Velocity = MeteorManager.Velocity;
        

        if (Physics.SphereCast(transform.position, 100.0f, Vector3.zero, out RaycastHit hit))
            targetPos = hit.point;
            
        targetRot = transform.rotation.eulerAngles;
        StartCoroutine(FoundEarth(targetPos));
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRot), 10.0f * Time.deltaTime);
    }


    IEnumerator FoundEarth(Vector3 pos)
    {
        Vector3 dir = pos - transform.position;
        float dist = dir.magnitude;

        dir.Normalize();

        StartCoroutine(Rotating(dir));

        while (dist > 0.0f)
        {
            
            float delta = MoveSpeed*Velocity * Time.deltaTime;
            if (dist - delta < 0.0f)
            {
                delta = dist;
            }
            transform.Translate(dir * delta, Space.World);
            dist -= delta;

            yield return null;
        }

    }

    IEnumerator Rotating(Vector3 dir)
    {

        float d = Vector3.Dot(transform.forward, dir);
        float r = Mathf.Acos(d);
        float angle = r * Mathf.Rad2Deg;
        float rotDir = 1.0f;
        if (Vector3.Dot(transform.right, dir) < 0.0f)
        {
            rotDir = -1.0f;
        }

        while (angle > 0.0f)
        {
            float delta = 360.0f * Time.deltaTime;
            if (angle - delta < 0.0f)
            {
                delta = angle;
            }
            targetRot.y += delta * rotDir;
            angle -= delta;
          
            yield return null;
        }
    }

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    private void OnDestroy()
    {
        if (!isQuitting)
        {
            MeteorManager.Score += 1;
            Instantiate(dieEffect, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(((1 << other.transform.gameObject.layer) & TargetMask) != 0)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
    
}
