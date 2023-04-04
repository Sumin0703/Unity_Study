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
        
        if (Input.GetKey(KeyCode.W)) //KeyCode��� ������.
        {
            /*transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime);*///���Ͱ��� self�����̱� ������ world���� space���� �����ص���.
            //transform.Translate(Vector3.forward * Time.deltaTime); //���� �ڵ�� ���� new�� �Ἥ ���͸� �������� �ʾƵ� ����ü Vector3�� ����Ͽ� �����ΰ�����.
            transform.Translate(transform.forward * Time.deltaTime,Space.World); //forward�� ���̰� 1m�� ����
        }
        else if (Input.GetKey(KeyCode.S)) //KeyCode��� ������.
        {
            transform.Translate(-transform.forward * Time.deltaTime, Space.World); //���⺤�Ͱ��� �e�� �����̱� ������ ���� �������� �������.
        }
        if (Input.GetKey(KeyCode.A)) //KeyCode��� ������.
        {
            transform.Rotate(transform.up*-360.0f * Time.deltaTime, Space.World); 
        }
        else if (Input.GetKey(KeyCode.D)) //KeyCode��� ������.
        {
            transform.Rotate(transform.up* 360.0f * Time.deltaTime, Space.World); 
        }
    }
}
