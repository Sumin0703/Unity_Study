using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotion : MonoBehaviour
{
    public Transform Root;
    public Animator myAnim = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAnimatorMove()
    {
        Root.position+=myAnim.deltaPosition;
        Root.rotation*=myAnim.deltaRotation;
    }
}
