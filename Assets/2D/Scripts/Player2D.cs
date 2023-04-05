using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : CharacterProperty2D
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate() //���� �����ӿ�ũ
    {
        AirCheck();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = Vector2.zero;
        dir.x = Input.GetAxisRaw("Horizontal"); //GetAxis�� ������ ������ ���� ������-> ���� �� -1~1������ ��.
        //GetAxisRaw�� 0���� 1, 0���� -1�� ���� �ٷ� �ٲٴ�
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
        float orgHeight = transform.position.y; //���� ���� ������ ���� X, ���ڸ� ������ ��.
        while (t <= totalTime)
        {
            if (t >= totalTime * 0.5f) isDown = true;
            t += Time.deltaTime;
            float h = Mathf.Sin((t/totalTime)*Mathf.PI)* maxHeigth;
            Vector3 pos= new Vector3(transform.position.x, orgHeight + h, transform.position.z); ;
            if (isDown)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Vector3.Distance(transform.position,pos), crashMask); //���� ������ �̵��� ������ �Ÿ� ��ŭ�� Rqy.�� ���.
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

    //Transform ignoreGround = null; //Transform���� �ؼ� �� ���� Ÿ�ϸ��ε�(���� ��ġ) �̱� ������ �ѹ��� �������Ե�, �׷��Ƿ� ���̸� üũ�ؼ� �� ���� ..
    //Transform curGround = null;


    public LayerMask crashMask;

    void AirCheck()
    {
        Vector2 orgPos = transform.position+Vector3.up*0.25f; //�ٴڿ��� �ٷ� ��� ����� ���� �ְ� �ȵ� ���� �����ϱ� ��¦ ������ ��� ��, �ٴڸ��̶� �浹�����̶� ���� ������ ���� ����ִ°���.
        Vector2 dir = Vector2.down;
        //ContactFilter2D filter = new ContactFilter2D();
        //filter.layerMask = crashMask;
        //RaycastHit2D[] hitList = new RaycastHit2D[10];
        //if (Physics2D.Raycast(orgPos, dir,filter, hitList,0.1f)>0) //10cm��ŭ�� ���.
        //{ //�������� ���� �����ϴ�, ���ÿ� �����ϱ� ������ 0���� ũ�� ����Ǵ°���
        //    myAnim.SetBool("isAir", false);
        //}
        //else
        //{
        //    myAnim.SetBool("isAir", true);
        //}    //������ ��ӳ�
        RaycastHit2D hit = Physics2D.Raycast(orgPos, dir, 0.5f, crashMask);
        if ( hit.collider != null&& dropDist<=0.0f /*&&ignoreGround!=hit.transform*/) //hit�� null�� �ƴϸ� �浹�̳�����
        {//&&������ �տ��� false�Ǹ� �ڿ��͵� ������������. or�� ���ʿ��� �����ϸ� ���� ����x
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
