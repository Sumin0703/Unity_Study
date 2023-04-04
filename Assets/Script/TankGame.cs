using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankGame : MonoBehaviour
{
    public float Speed = 1.0f; // Speed를 둬서 가속도 설정 가능.
    public float RotSpeed = 180.0f;
    public float TopRotSpeed=90.0f;
    public Transform myTop = null;
    public Transform myBarrel = null;
    public Transform MyMuzzle = null;
    public Bomb MyBomb = null;
    public GameObject orgBomb = null;
    public GameObject BombEffect = null;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Speed = 3.0f;
        }
        else
        {
            Speed = 1.0f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime); //초당 이동거리
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.down * RotSpeed * Time.deltaTime); //초당 RotSpeed만큼 회전한다.
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * RotSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            myTop.Rotate(Vector3.down * TopRotSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            myTop.Rotate(Vector3.up * TopRotSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            myBarrel.Rotate(Vector3.left * TopRotSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            myBarrel.Rotate(Vector3.right * TopRotSpeed * Time.deltaTime);
        }
        Vector3 angle = myBarrel.localRotation.eulerAngles;
        if (angle.x > 180.0f)
        {
            angle.x -= 360.0f;
        }
        //if (angle.x > 4.0f)
        //{
        //    angle.x = 4.0f;
        //}
        //if (angle.x < -20.0f)
        //{
        //    angle.x = 20.0f;
        //} //클램프로 한줄 처리
        angle.x=Mathf.Clamp(angle.x, -26.0f, 8.0f); //최솟값, 최대값을 넣으면 그 이상 그이하가 되지 않도록 해줌.
        myBarrel.localRotation = Quaternion.Euler(angle);

        if (Input.GetKeyDown(KeyCode.Space)) //눌렀을 때 한번만 true되도록.
        {
            MyBomb.OnFire();
            MyBomb = null;
            //GameObject obj = Instantiate(orgBomb); //obj는 원본 프리팹의 인스턴스를 가르키고잇음.
            //obj.transform.SetParent(MyMuzzle);
            //obj.transform.localPosition = Vector3.zero;
            //obj.transform.localRotation = MyMuzzle.localRotation;
            GameObject obj = Instantiate(orgBomb, MyMuzzle);
            MyBomb = obj.GetComponent<Bomb>();
            Instantiate(BombEffect, MyMuzzle.transform.position, Quaternion.identity);
        }
    }

}
