using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rpg_Player : Character_Movement,IBattle
{
    Transform myTarget = null;

    public bool IsLive
    {
        get => !Mathf.Approximately(curHp,0.0f);
    }

    //public bool isDie = false;
    // Start is called before the first frame update
    void Start()
    {
        /*MyAnim = GetComponentInChildren<Animator>();*///자기 자식 아래의 애니메이터 컴포넌트를 가져옴
                                                        //자식들의 모든 컴포넌트들을 받을 때에는 복수형으로 사용, 배열로 입력 받음.
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myAnim.SetTrigger("Attack");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            myAnim.SetTrigger("Skill");
        }
    }

    public void OnMove(Vector3 pos)
    {
        //if (isDie) return;
        if(IsLive) MoveToPos(pos);
    }
   
    public void OnDamage(float dmg)
    {
        curHp -= dmg;

        if (Mathf.Approximately(curHp, 0.0f))
        {
            Collider[] list = transform.GetComponentsInChildren<Collider>();
            foreach(Collider col in list)
            {
                col.enabled = false;
            }
            DeathAlarm?.Invoke();
            myAnim.SetTrigger("Die");
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

    //public void Die()
    //{
    //    myAnim.SetTrigger("Die");
    //    //isDie = true;
    //}

    public void OnAttack()
    {
        myTarget.GetComponent<IBattle>()?.OnDamage(AttackPoint);        
    }

    public void BeginBattle(Transform Target)
    {
        if (!IsLive) return;
        if (myTarget != null)
        {
            myTarget.GetComponent<Character_Property>().DeathAlarm -= TargetDead;
        }
        myTarget = Target;
        FollowTarget(Target);
        myTarget.GetComponent<Character_Property>().DeathAlarm += TargetDead;
    }
   

    public void TargetDead()
    {
        myTarget = null;
        StopAllCoroutines();
    }
}
