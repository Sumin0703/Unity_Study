using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : CharacterProperty2D
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate() //물리 프레임워크
    {
        AirCheck();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = Vector2.zero;
        dir.x = Input.GetAxisRaw("Horizontal"); //GetAxis는 누르고 있으면 값이 증가함-> 가속 됨 -1~1까지의 값.
        //GetAxisRaw는 0에서 1, 0에서 -1로 값을 바로 바꾸는
        if (!myAnim.GetBool("isAttacking"))
        {
            if (!Mathf.Approximately(dir.x, 0.0f))
            {
                myAnim.SetBool("isMoving", true);
                if (dir.x < 0.0f)
                {
                    myRenderer.flipX = true;
                }
                else
                {
                    myRenderer.flipX = false;
                }
            }
            else
            {
                myAnim.SetBool("isMoving", false);
            }
            //dir.y = Input.GetAxis("Vertical");
            transform.Translate(dir * MoveSpeed * Time.deltaTime);
        }
        if (Input.GetMouseButton(0))
        {
            myAnim.SetTrigger("Attack");
            
        }

        if (Input.GetKeyDown(KeyCode.Space)&&!myAnim.GetBool("isAir"))
        {
            //myRigid2D.AddForce(Vector2.up*300.0f);
            
            coJump = StartCoroutine(Jumping(1,3));
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(Dropping());
        }
    }

    Coroutine coJump = null;
    bool isDown = false;
    IEnumerator Jumping(float totalTime,float maxHeigth)
    {
        isDown = false;
        myAnim.SetTrigger("Jump");
        float t = 0.0f;
        float orgHeight = transform.position.y; //벡터 사용시 앞으로 점프 X, 제자리 점프만 됨.
        while (t <= totalTime)
        {
            if (t >= totalTime * 0.5f) isDown = true;
            t += Time.deltaTime;
            float h = Mathf.Sin((t/totalTime)*Mathf.PI)* maxHeigth;
            Vector3 pos= new Vector3(transform.position.x, orgHeight + h, transform.position.z); ;
            if (isDown)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Vector3.Distance(transform.position,pos), crashMask); //현재 지점과 이동할 지점의 거리 만큼을 Rqy.를 쏜다.
                if (hit.collider != null)
                {
                    transform.position = hit.point;
                    yield break;
                }
            }

            transform.position = pos;
            yield return null;
        }
        transform.position= new Vector3(transform.position.x, orgHeight, transform.position.z); ;
    }

    float dropDist = 0.0f;

    IEnumerator Dropping()
    {
        dropDist = 2.0f;
        
        //ignoreGround = curGround;
        yield return null;

    }

    //Transform ignoreGround = null; //Transform으로 해서 다 같은 타일맵인데(같은 위치) 이기 때문에 한번에 떨어지게됨, 그러므로 높이를 체크해서 해 보는 ..
    //Transform curGround = null;


    public LayerMask crashMask;

    void AirCheck()
    {
        Vector2 orgPos = transform.position+Vector3.up*0.25f; //바닥에서 바로 쏘면 검출될 수도 있고 안될 수도 있으니까 살짝 위에서 쏘면 됨, 바닥면이랑 충돌지점이랑 같기 때문에 조금 띄워주는거임.
        Vector2 dir = Vector2.down;
        //ContactFilter2D filter = new ContactFilter2D();
        //filter.layerMask = crashMask;
        //RaycastHit2D[] hitList = new RaycastHit2D[10];
        //if (Physics2D.Raycast(orgPos, dir,filter, hitList,0.1f)>0) //10cm만큼만 쏜다.
        //{ //여러개를 동시 검출하는, 동시에 검출하기 때문에 0보다 크면 검출되는거임
        //    myAnim.SetBool("isAir", false);
        //}
        //else
        //{
        //    myAnim.SetBool("isAir", true);
        //}    //오류가 계속남
        RaycastHit2D hit = Physics2D.Raycast(orgPos, dir, 0.5f, crashMask);
        if ( hit.collider != null&& dropDist<=0.0f /*&&ignoreGround!=hit.transform*/) //hit의 null이 아니면 충돌이난거임
        {//&&연산은 앞에게 false되면 뒤에것두 연산하지않음. or은 앞쪽에서 만족하면 뒤쪽 연산x
            if (isDown)
            {
                if (coJump != null) StopCoroutine(coJump);
                transform.position = hit.point;
            }
            myAnim.SetBool("isAir", false);
            //curGround = hit.transform;
        }
        else
        {
            myAnim.SetBool("isAir", true);
            float delta= 9.8f * Time.deltaTime;
            transform.position += Vector3.down * delta;

            if (dropDist > 0.0f)
            {
                dropDist -= delta;
                
            }
        }
    }

    

}
