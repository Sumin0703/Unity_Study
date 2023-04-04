using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPlayer : AIMovement
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(Vector3 pos)
    {
        NavMeshPath path = new NavMeshPath();
        if(NavMesh.CalculatePath(transform.position,pos,NavMesh.AllAreas, path))
        {
            switch (path.status)
            {
                case NavMeshPathStatus.PathPartial: //중간에 막혀서, 
                    break;
                case NavMeshPathStatus.PathInvalid: //d아예못가
                    break;
                case NavMeshPathStatus.PathComplete:
                    break;
            }
            MoveByPath(path.corners);
        }
    }
}
