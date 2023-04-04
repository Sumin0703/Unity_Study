using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster : Character_Movement,IPerception,IBattle
{
    public bool IsLive
    {
        get => myState != State.Death;
    }
    public enum State
    {
        Create,Normal,Battle,Death
    }

    public static int TotalCount = 0;
    public State myState = State.Create;

    Vector3 orgPos;

    public Transform myTarget;

    //bool isDie=false;

    protected void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case State.Normal:
                myAnim.SetBool("isMoving", false);
                StopAllCoroutines();
                StartCoroutine(Roaming(Random.Range(1.0f, 3.0f)));
                break;
            case State.Battle:

                StopAllCoroutines();
                FollowTarget(myTarget);
                break;
            case State.Death:
                Collider[] list = transform.GetComponentsInChildren<Collider>();
                foreach (Collider col in list) col.enabled = false;
                DeathAlarm?.Invoke();
                StopAllCoroutines();
                myAnim.SetTrigger("Die");
                break;
            default:
                Debug.Log("처리 되지 않은 상태.");
                break;
        }
    }

    protected void StateProcess()
    {
        switch (myState)
        {
            case State.Normal:
                break;
            case State.Battle:
                break;
            case State.Death:
                break;
            default:
                Debug.Log("처리 되지 않은 상태.");
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        TotalCount += 1;
        orgPos = transform.position;
        ChangeState(State.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
    }

    IEnumerator Roaming(float delay)
    {
        yield return new WaitForSeconds(delay);

       
        
           Vector3 pos = orgPos;
            pos.x += Random.Range(-5.0f, 5.0f);
            pos.z += Random.Range(-5.0f, 5.0f);
            MoveToPos(pos, () => StartCoroutine(Roaming(Random.Range(1.0f, 3.0f))));
        
       
    }

    public void Find(Transform Target)
    {
        //if (isDie) return;
        myTarget = Target;
        myTarget.GetComponent<Character_Property>().DeathAlarm += () => { if (IsLive) ChangeState(State.Normal); }; 
        ChangeState(State.Battle);
    }

    public void LostTarget()
    {
        //if (isDie) return;
        myTarget = null;
        ChangeState(State.Normal);
    }

    public void OnAttack()
    {
        Debug.Log("대상 공격!");
        myTarget.GetComponent<IBattle>()?.OnDamage(AttackPoint); //null인지 아닌지 검사.,
    }

    public void OnDamage(float dmg)
    {
        curHp -= dmg;

        if (Mathf.Approximately(curHp, 0.0f))
        {
            ChangeState(State.Death);
        }
        else
        {
            myAnim.SetTrigger("Damage");
        }
        //if (Hp - dmg < 0.0f)
        //{
        //    if (isDie) return;

        //    Die();

        //}
        //else
        //{
        //    Hp -= dmg;
        //    myAnim.SetTrigger("Damage");
        //}
    }

    public void OnDisappear()
    {
        StartCoroutine(Disappearing());
    }

    IEnumerator Disappearing()
    {
        yield return new WaitForSeconds(3.0f);
        float dist = 0.0f;

        while (dist < 1.0f)
        {
            dist += Time.deltaTime;
            transform.Translate(Vector3.down * Time.deltaTime);
            yield return null; //3초가 지난뒤에는 계속 while문 루프 
        }
        Destroy(gameObject);
        TotalCount--;
    }
    //public void Die()
    //{
    //    myAnim.SetTrigger("Die");
    //    ChangeState(State.Death);
    //    isDie = true;
    //}
}
