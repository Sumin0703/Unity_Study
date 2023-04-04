using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceEarth : MonoBehaviour
{
    public static DefenceEarth Instance=null;
    public Transform MyEarth= null;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(Spawning());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawning()
    {
        Vector3 pos = Vector3.zero;

        while (MyEarth.gameObject!=null)
        {
            

            while (Mathf.Approximately(pos.magnitude, 0.0f))
            {
                pos.x = Random.Range(-1.0f, 1.0f);
                pos.y = Random.Range(-1.0f, 1.0f);
            }

            Vector3 rndDir = (pos - MyEarth.position).normalized;
            pos = MyEarth.position + rndDir * 5.0f;

            GameObject obj = Instantiate(Resources.Load("Meteor"),pos,Quaternion.identity) as GameObject;

        }
        yield return new WaitForSeconds(1.0f);
    }
}
