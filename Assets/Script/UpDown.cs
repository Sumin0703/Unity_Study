using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour
{
    float dist = 2.5f;
    //float curDist = 0.0f;
    Vector3 dir; //y축 방향의 벡터, 스칼라는 1
    // Start is called before the first frame update
    void Start()
    {
        dir.Set(0, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //위로 2.5미터 올라간 뒤에 다시 원래 자리로 돌아오는 걸 반복.

        //transform.position = transform.position + dir * Time.deltaTime;
        //transform.Translate(dir * Time.deltaTime); //위의 코드랑 같은 코드임.

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

        float delta = Time.deltaTime; //반대로 빼줄 때의 상황, 좀 더 간략화 됨.
        if (dist - delta <= 0.0f)
        {
            delta = dist;
            dist = 2.5f;
            dir = -dir;
        }
        dist -= delta;
        transform.Translate(dir * delta,Space.Self); 

        //회전

        transform.Rotate(dir * 360.0f*Time.deltaTime); //기준축을 기준으로 회전을함.초당 360도 
    }
}
