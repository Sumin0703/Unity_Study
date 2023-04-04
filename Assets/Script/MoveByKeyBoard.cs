using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByKeyBoard : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.W)) //KeyCode라는 열거형.
        {
            /*transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime);*///벡터값은 self기준이기 때문에 world말고 space값을 생략해도돔.
            //transform.Translate(Vector3.forward * Time.deltaTime); //위의 코드와 같이 new를 써서 벡터를 생성하지 않아도 구조체 Vector3를 사용하여 앞으로가게함.
            transform.Translate(transform.forward * Time.deltaTime,Space.World); //forward는 길이가 1m인 벡터
        }
        else if (Input.GetKey(KeyCode.S)) //KeyCode라는 열거형.
        {
            transform.Translate(-transform.forward * Time.deltaTime, Space.World); //방향벡터갑이 웕드 기준이기 때문에 월드 기준으로 해줘야함.
        }
        if (Input.GetKey(KeyCode.A)) //KeyCode라는 열거형.
        {
            transform.Rotate(transform.up*-360.0f * Time.deltaTime, Space.World); 
        }
        else if (Input.GetKey(KeyCode.D)) //KeyCode라는 열거형.
        {
            transform.Rotate(transform.up* 360.0f * Time.deltaTime, Space.World); 
        }
    }
}
