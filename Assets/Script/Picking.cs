using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Picking : MonoBehaviour
{
    
    
    public LayerMask pickMask;
    public LayerMask enemyMask;
    public float MoveSpeed = 2.0f;
    public float Veclocity=2.0f;
    Vector3 targetPos;
    Vector3 targetRot;
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
            if(Physics.Raycast(ray,out RaycastHit hit,Mathf.Infinity,pickMask))
            {
                //if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
                if (((1 << hit.transform.gameObject.layer) & enemyMask) == 0)
                {
                    //c/*lickAction?.Invoke(hit.point); /*/
                    StopAllCoroutines();
                    //transform.position = hit.point;
                    targetPos = transform.position;
                    targetRot = transform.rotation.eulerAngles; //쿼터니언 값을 반환하기때문에 오일러로 변경해줘야함.
                    StartCoroutine(MovingToPos(hit.point));
                }
            }
        }
        transform.position = Vector3.Lerp(transform.position, targetPos, Veclocity * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRot), 10.0f * Time.deltaTime);
    }

   
    IEnumerator MovingToPos(Vector3 pos)
    {
        //Vector3 startPos, destPos; 
        //float t = 0.0f; 
        //float Dir = 1.0f;
        //startPos = transform.position;
        //destPos = pos;
        //bool arrival = false;
        //Vector3 temp;

        //while (!arrival)
        //{

        //    t = Mathf.Clamp(t + Dir * Time.deltaTime, 0.0f, 1.0f); 

        //    temp= Vector3.Lerp(startPos, destPos, t);
        //    transform.position = new(temp.x, 0.57f, temp.z);
        //    if (transform.position.x == destPos.x&&transform.position.z == destPos.z)
        //    {
        //        transform.position = new (destPos.x, 0.57f, destPos.z);
        //        arrival = true;
        //    }
        //    yield return null;
        //}

        Vector3 dir = pos - transform.position;
        float dist = dir.magnitude;
        if (dist > 0.0f)
        {
            //달리기 시작
        }
        dir.Normalize();

        StartCoroutine(Rotating(dir));
        //float angle = Vector3.Angle(transform.forward, dir);


        //float rotDir = 1.0f;
        //if (Vector3.Dot(transform.right, dir) < 0.0f)
        //{
        //    rotDir = -1.0f;
        //}

        //transform.Rotate(Vector3.up * rotDir * angle);

        while (dist > 0.0f)
        {
            float delta = MoveSpeed * Time.deltaTime;
            if (dist - delta < 0.0f)
            {
                delta = dist;
            }
            targetPos += dir * delta;
            //transform.Translate(dir * delta,Space.World);
            dist -= delta;

            yield return null;
        }
        //달리기 멈춤.
        
    }
    IEnumerator Rotating(Vector3 dir)
    {

        //float angle = Vector3.Angle(transform.forward, dir);


        //float rotdir = 1.0f;

        //if (Vector3.Dot(transform.right, dir) < 0.0f)
        //{
        //    rotdir = -1.0f;
        //}

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
            //transform.Rotate(Vector2.up *rotDir* delta);
            yield return null;
        }


        //while (r > 0.0f)
        //{
        //    float delta = 1.0f * Time.deltaTime;
        //    if (r - delta < 0.0f)
        //    {
        //        delta = r;
        //    }
        //    float a = 180.0f * (r / Mathf.PI);
        //    if (Vector3.Dot(transform.right, dir) < 0.0f)
        //    {
        //        a *= -1.0f;
        //    }
        //    r -= delta;
        //    transform.Rotate(Vector3.up * a*delta);
        //    yield return null;
        //}



    }
}
