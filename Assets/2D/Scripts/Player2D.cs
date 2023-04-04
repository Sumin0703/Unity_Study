using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : CharacterProperty2D
{
    // Start is called before the first frame update
    void Start()
    {
        
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
            dir.y = Input.GetAxis("Vertical");
            transform.Translate(dir * MoveSpeed * Time.deltaTime);
        }
        if (Input.GetMouseButton(0))
        {
            myAnim.SetTrigger("Attack");
            
        }

        
    }
}
