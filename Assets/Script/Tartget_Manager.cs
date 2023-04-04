using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ���� �ڵ�.
public class Tartget_Manager : MonoBehaviour
{
    Coroutine move = null;//Ư�� �ڷ�ƾ�� �����ϰ������ �ڷ�ƾ�� ������ �ʿ���.
    public GameObject destroyEffect = null; //����Ʈ�� ����.
    public GameObject StopEffecet = null;
    bool isQuitting = false;

     Vector3 temp;
    //public bool Life = true;
    //IEnumerator coFunc;
    // Start is called before the first frame update
    void Start()
    {

        move = StartCoroutine(Moving());
        /*coFunc = Moving();*/ //��������Ʈ�� ���, �ý��ۿ����� �̷��� �ڷ�ƾ�� �����ϴ°���.
    }

    // Update is called once per frame
    void Update()
    {
        //coFunc.MoveNext();
        if (Input.GetKeyDown(KeyCode.F1))
        {
            /*StopAllCoroutines();*/ //�ش罺ũ��Ʈ�� �����Ǿ��ִ� ��� �ڷ�ƾ���� ����ǰԵ�. ����Ҷ� �����ؾ���.
            StopCoroutine(move);
            Instantiate(StopEffecet, transform.position, Quaternion.identity);
        }
        //t = Mathf.Clamp(t + Dir * Time.deltaTime, 0.0f, 1.0f); //T�� 0�̳� 1�� ��, ���� ������ �� Dir�� ������ �ٲ������.
        //if (Mathf.Approximately(t, 0.0f) || Mathf.Approximately(t, 1.0f))
        //{
        //    Dir *= -1.0f;
        //}
        //transform.position = Vector3.Lerp(startPos, destPos, t);

    }
    private void OnDestroy() //������Ʈ�� ��������� �Ӹ��ƴ϶� ������ �������� ȣ��Ǵ� �Լ��ε� ������ ������ �����ϱ� ������ ������ ����.
    {
        if (!isQuitting)
        {
            //TargetManager_mk2.Count--;
            Instantiate(destroyEffect, this.transform.position, Quaternion.identity); //���ʹϾ��� identity�� 0�� �ǹ�
        }
    }

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    IEnumerator Moving()
    {
        float Range = 2.0f;
        Vector3 startPos, destPos; //���������� ��������.
        float t = 0.0f; //���� �ð��� ������ t, T�� �ð��� ������ 0~1������ ���� ������.
        float Dir = 1.0f;

        t = 0.5f; //�߾ӿ��� �����ϹǷ� T�� 0.5������.
        destPos = startPos = transform.position; //ó������ ���� ���� ������ ��.
        startPos.x -= Range / 2.0f; //��ü�� �� ������ start?
        destPos.x += Range / 2.0f; //

        while (true)
        {
            t = Mathf.Clamp(t + Dir * Time.deltaTime, 0.0f, 1.0f); //T�� 0�̳� 1�� ��, ���� ������ �� Dir�� ������ �ٲ������.
            if (Mathf.Approximately(t, 0.0f) || Mathf.Approximately(t, 1.0f))
            {
                Dir *= -1.0f;
            }
            transform.position = Vector3.Lerp(startPos, destPos, t);
            yield return null;
        }
    }
}
