using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor_Picking : MonoBehaviour
{
    public LayerMask meteor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //0은 마우스 왼쪽, 1은 오른쪽, 2는 스크롤
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit_meteor, Mathf.Infinity))
            {
                //if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
                if (((1 << hit_meteor.transform.gameObject.layer) & meteor) != 0)
                {
                    
                    Destroy(hit_meteor.transform.gameObject);
                }
            }
        }
    }
}
