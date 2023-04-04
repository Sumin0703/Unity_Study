using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavPlayer : Character_Property
{
    public NavMeshAgent myNav;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        myAnim.SetFloat("Speed", myNav.velocity.magnitude / myNav.speed);
    }

    public void OnWarp(Vector3 pos)
    {
        myNav.Warp(pos);
    }

    public void OnMove(Vector3 pos)
    {
        if (myAnim.GetBool("isAir")) return;
        //StartCoroutine(Moving(pos));
        StopAllCoroutines();
        StartCoroutine((JumpalbeMoving(pos)));
    }

    IEnumerator Moving(Vector3 pos)
    {
        myNav.SetDestination(pos);
        //myAnim.SetBool("isMoving", true);
        while (myNav.pathPending || myNav.remainingDistance > myNav.stoppingDistance)
        {
            yield return null;
        }
        //myAnim.SetBool("isMoving", false);
    }

    IEnumerator JumpalbeMoving(Vector3 pos)
    {
        myNav.SetDestination(pos);
        while (myNav.pathPending || myNav.remainingDistance > myNav.stoppingDistance)
        {
            if (myNav.isOnOffMeshLink)
            {
                myAnim.SetBool("isAir", true);
                myNav.isStopped=true;
                Vector3 endpos = myNav.currentOffMeshLinkData.endPos;
                Vector3 dir = endpos - transform.position;
                float dist = dir.magnitude;
                dir.Normalize();

                while (dist > 0.0f)
                {
                    float delta = myNav.speed * Time.deltaTime;
                    if (dist < delta) delta = dist;
                    dist -= delta;
                    transform.Translate(dir * delta,Space.World);
                    yield return null;
                }
                myAnim.SetBool("isAir", false);
                myNav.CompleteOffMeshLink(); //수동처리를 했기 때문에 도착했는지 알수 없음 도착했다는 것을 알려준다.
                myNav.isStopped = false; //다시 연결.
                myNav.velocity = dir * myNav.speed; //Nav를 껏다 키기 떄문에 방향벡터가 0이기 때문에 idle로 바뀌어서 도착했을 때 이상하게됨.
            }
            yield return null;
        }
    }
}
