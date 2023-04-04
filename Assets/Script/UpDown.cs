using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour
{
    float dist = 2.5f;
    //float curDist = 0.0f;
    Vector3 dir; //y�� ������ ����, ��Į��� 1
    // Start is called before the first frame update
    void Start()
    {
        dir.Set(0, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //���� 2.5���� �ö� �ڿ� �ٽ� ���� �ڸ��� ���ƿ��� �� �ݺ�.

        //transform.position = transform.position + dir * Time.deltaTime;
        //transform.Translate(dir * Time.deltaTime); //���� �ڵ�� ���� �ڵ���.

        //if (transform.position.y >= 2.5f)
        //{
        //    dir.Set(0, -1, 0);
        //}
        //else if (transform.position.y <= 0.5f)
        //{
        //    dir.Set(0, 1, 0);
        //}
        //transform.Translate(dir * Time.deltaTime);

        //float delta = Time.deltaTime;
        //if (curDist + delta>= 2.5f)
        //{
        //    delta = 2.5f - curDist;
        //    curDist = 0.0f;
        //    dir = -dir;
        //}
        //else
        //{
        //    curDist += delta;
        //}

        //transform.Translate(dir * delta);

        float delta = Time.deltaTime; //�ݴ�� ���� ���� ��Ȳ, �� �� ����ȭ ��.
        if (dist - delta <= 0.0f)
        {
            delta = dist;
            dist = 2.5f;
            dir = -dir;
        }
        dist -= delta;
        transform.Translate(dir * delta,Space.Self); 

        //ȸ��

        transform.Rotate(dir * 360.0f*Time.deltaTime); //�������� �������� ȸ������.�ʴ� 360�� 
    }
}
