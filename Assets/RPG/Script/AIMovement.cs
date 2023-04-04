using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIMovement : Character_Movement
{
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void MoveByPath(Vector3[] pathlist)
    {
        StopAllCoroutines();
        StartCoroutine(MovingByPath(pathlist));
        
    }

    IEnumerator MovingByPath(Vector3[] pathlist)
    {
        myAnim.SetFloat("Speed", 1.0f);
        int i = 1;
        while (i < pathlist.Length)
        {
            bool done = false;
            MoveToPos(pathlist[i], () => done = true) ;
            while (!done) { 
                for(int n = i; n < pathlist.Length; ++n)
                {
                    Debug.DrawLine(n==i?transform.position: pathlist[n - 1], pathlist[n],Color.red);
                }
                yield return null; 
            }
            i++;
        }
        myAnim.SetFloat("Speed", 0.0f) ;


    }
}
