using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : Monster
{
    public LayerMask enemyMask;
    public Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        myTarget = AIPerception.enemyTarget;
    }

    // Update is called once per frame
    void Update()
    {
        if (myTarget != null)
        {
            MoveToPos(myTarget.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((enemyMask & 1 << other.gameObject.layer) != 0) //enemyMask�� ������ ���ӿ�����Ʈ�� ���̾ ���� ��
        {
            animator.SetTrigger("Attack");   
        }
    }
}
