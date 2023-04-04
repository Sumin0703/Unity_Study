using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager_mk2 : MonoBehaviour
{
    public GameObject orgTarget = null;
    public Transform Target = null;
    Vector3 temp;
    static public int Count = 1;
    // Start is called before the first frame update
    void Start()
    {
        temp = Target.position;
        //StartCoroutine(NewTarget());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator NewTarget()
    {
        while (Count < 3)
        {
            yield return new WaitForSeconds(5.0f);

            Count += 1;
            GameObject obj = Instantiate(orgTarget);
            temp.x = Random.Range(-9.0f, 9.0f);
            obj.transform.position = temp;
        }
    }
}
