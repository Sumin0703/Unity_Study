using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//선형 보간 코드.
public class Tartget_Manager : MonoBehaviour
{
    Coroutine move = null;//특정 코루틴을 삭제하고싶을때 코루틴의 변수가 필요함.
    public GameObject destroyEffect = null; //이펙트의 원본.
    public GameObject StopEffecet = null;
    bool isQuitting = false;

     Vector3 temp;
    //public bool Life = true;
    //IEnumerator coFunc;
    // Start is called before the first frame update
    void Start()
    {

        move = StartCoroutine(Moving());
        /*coFunc = Moving();*/ //델리게이트와 비슷, 시스템에서는 이렇게 코루틴을 실행하는것임.
    }

    // Update is called once per frame
    void Update()
    {
        //coFunc.MoveNext();
        if (Input.GetKeyDown(KeyCode.F1))
        {
            /*StopAllCoroutines();*/ //해당스크립트와 연관되어있는 모든 코루틴들이 종료되게됨. 사용할때 조심해야함.
            StopCoroutine(move);
            Instantiate(StopEffecet, transform.position, Quaternion.identity);
        }
        //t = Mathf.Clamp(t + Dir * Time.deltaTime, 0.0f, 1.0f); //T가 0이나 1일 때, 끝에 도달한 것 Dir의 방향을 바꿔줘야함.
        //if (Mathf.Approximately(t, 0.0f) || Mathf.Approximately(t, 1.0f))
        //{
        //    Dir *= -1.0f;
        //}
        //transform.position = Vector3.Lerp(startPos, destPos, t);

    }
    private void OnDestroy() //오브젝트가 종료됐을때 뿐만아니라 어플이 끝날때도 호출되는 함수인데 어플이 끝나도 생성하기 때문에 오류가 생김.
    {
        if (!isQuitting)
        {
            //TargetManager_mk2.Count--;
            Instantiate(destroyEffect, this.transform.position, Quaternion.identity); //쿼터니언의 identity는 0을 의미
        }
    }

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    IEnumerator Moving()
    {
        float Range = 2.0f;
        Vector3 startPos, destPos; //시작지점과 도착지점.
        float t = 0.0f; //보간 시간을 저장할 t, T는 시간의 비율로 0~1사이의 값을 가진다.
        float Dir = 1.0f;

        t = 0.5f; //중앙에서 시작하므로 T는 0.5를가짐.
        destPos = startPos = transform.position; //처음에는 같은 값을 가지게 됨.
        startPos.x -= Range / 2.0f; //전체폭 중 절반이 start?
        destPos.x += Range / 2.0f; //

        while (true)
        {
            t = Mathf.Clamp(t + Dir * Time.deltaTime, 0.0f, 1.0f); //T가 0이나 1일 때, 끝에 도달한 것 Dir의 방향을 바꿔줘야함.
            if (Mathf.Approximately(t, 0.0f) || Mathf.Approximately(t, 1.0f))
            {
                Dir *= -1.0f;
            }
            transform.position = Vector3.Lerp(startPos, destPos, t);
            yield return null;
        }
    }
}
