using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //델리게이트를 쉽게 이용할 수 있게 도와줌.

public class MousePicking : MonoBehaviour
{
    //public UnityAction<Vector3> clickAction = null;
    public UnityEvent<Vector3> clickAction = null;
    public UnityEvent <Transform> AttackAction = null;
    public UnityEvent<Vector3> RightClick = null;
    public LayerMask pickMask;
    public LayerMask enemyMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, pickMask | enemyMask))
            {
                if (((1 << hit.transform.gameObject.layer) & enemyMask) != 0)
                {

                    AttackAction?.Invoke(hit.transform);
                }
                else
                {
                    clickAction?.Invoke(hit.point);
                }

            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, pickMask))
            {
                RightClick?.Invoke(hit.point);
            }
        }
    }
}
