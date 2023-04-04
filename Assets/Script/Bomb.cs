using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    bool isFire = false;
    public float Speed = 10.0f;
   
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Speed * Time.deltaTime;
        if (isFire)
        {
            

            Ray ray = new Ray();
            ray.origin = transform.position;
            ray.direction = transform.forward;
            if (Physics.Raycast(ray, out RaycastHit hit, delta)) //부딪힌 대상의 정보가 rayHit hit에 저장.,.,ㅡ
            {
                DestroyObject(hit.transform.gameObject);
            }
            transform.Translate(Vector3.forward * delta);
        }
    }

    public void OnFire()
    {
        isFire = true;
        transform.SetParent(null);
        GetComponent<Collider>().isTrigger = false; //발사됐을때 trigger이 풀리면서 물리적 충돌을하게 됨.
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("OnCollisionExit");
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("OnCollisionStay");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        if (other.gameObject.tag == "Bomb") return;
        //other.GetComponent<Tartget_Manager>().Life = false;
        //Vector3 temp = other.transform.position;
        //Destroy(other.gameObject);

        //StartCoroutine(CreateDelay(temp));

    }

    IEnumerator CreateDelay(Vector3 temp)
    {
        yield return new WaitForSeconds(1.0f);

        GameObject org = Resources.Load("Target") as GameObject;
        if (org != null)
        {
            GameObject obj = Instantiate(org);
            temp.x = Random.Range(-9.0f, 9.0f);
            obj.transform.position = temp;
        }
    }

    private void DestroyObject(GameObject obj)
    {
        Vector3 temp = obj.transform.position;
        Destroy(obj);
        StartCoroutine(CreateDelay(temp));
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("OnTriggerStay");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit");
    }
}
