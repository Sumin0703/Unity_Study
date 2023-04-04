using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorManager : MonoBehaviour
{
   
    Vector3 temp;
    //public GameObject orgMeteor;

    public static int Score = 0;
    public static float Velocity = 2.0f;
    public Transform Target;
    public int s = 0; //score보기위한 임시변수
    public float v = 0; //속도를 보기위한 임시변수
    public float Time = 5.0f;
    public float closeDistance = 4.0f;
    // Start is called before the first frame update
    void Start()
    {
        temp = Vector3.zero;
        StartCoroutine(CreateMeteor());
    }

    // Update is called once per frame
    void Update()
    {
        v = Velocity;
        s = Score;
    }

   
    IEnumerator CreateMeteor()
    {
        while (true)
        {
            yield return new WaitForSeconds(Time);
            
            if (Score >= 5)
            {
                Score = 0;
                Velocity *= 2.0f;
                Time -= 0.2f;
            }

            if(Target!= null)
            {
                GameObject org = Resources.Load("Meteor") as GameObject;
                if (org != null)
                {
                    GameObject obj = Instantiate(org);

                    Vector3 TargetPosition = Target.position;

                    float a = TargetPosition.x;
                    float b = TargetPosition.y;

                    float x=Random.Range(-closeDistance+a,closeDistance+b);
                    float y_2 = Mathf.Sqrt(Mathf.Pow(closeDistance, 2) - Mathf.Pow(x - a, 2));
                    y_2 *= Random.Range(0, 2) == 0 ? -1 : 1;
                    float y= y_2 + b;

                    temp = new Vector3(x, y, 0);
                    obj.transform.position = temp;

                }
            }
            
            
            //GameObject obj = Instantiate(orgMeteor);
            
            
            //    temp.x = Random.Range(-7.0f, 7.0f);
            //    temp.y = Random.Range(-7.0f, 7.0f);
            //    obj.transform.position = temp;
            
        }
    }
}
