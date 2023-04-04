using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPerception
{
    void Find(Transform Tratget);
    void LostTarget();

}

public class AIPerception : MonoBehaviour
{
    public static Transform enemyTarget;
    public List<Transform> myEnemylist;
    public IPerception myParent = null;
    public LayerMask enemyMask;
  
    Transform myTarget = null;
    // Start is called before the first frame update
    void Start()
    {
        myParent = transform.parent.GetComponent<IPerception>();
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if((enemyMask& 1 << other.gameObject.layer) != 0) //enemyMask�� ������ ���ӿ�����Ʈ�� ���̾ ���� ��
        {
            if (!myEnemylist.Contains(other.transform))
            {
                myEnemylist.Add(other.transform);
            }
            if (myTarget == null)
            {
                myTarget = other.transform;
                myParent.Find(myTarget);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((enemyMask & 1 << other.gameObject.layer) != 0) //enemyMask�� ������ ���ӿ�����Ʈ�� ���̾ ���� ��
        {
            if (myEnemylist.Contains(other.transform)) //enemyMask�� ������ ���ӿ�����Ʈ�� ���̾ ���� ��
            {
               
                myEnemylist.Remove(other.transform);
            }
            if (myTarget == other.transform)
            {
                myTarget = null;
                myParent.LostTarget();
            }
        }
    }
}
