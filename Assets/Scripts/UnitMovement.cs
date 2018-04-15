using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour {

    public bool isMoving = false;
    public List<Vector3> path;
    int pathCounter = 0;

	// Update is called once per frame
	void Update ()
    {
		if (isMoving)
        {
            move();
        }

	}

    void move()
    {
        if (Vector3.Distance(this.transform.position, path[pathCounter]) > 0.2f)
        {
            Vector3 dir = path[pathCounter] - this.transform.position;
            transform.Translate(dir * 5 * Time.deltaTime);
            //transform.position = Vector3.MoveTowards(transform.position, path[pathCounter], 5 * Time.deltaTime);
            //transform.position = new Vector3(path[pathCounter].x, path[pathCounter].y);
        }
        else
        {
            if (pathCounter < path.Count -1)
            {
                pathCounter++;
            }
            else
            {
                isMoving = false;
            }
        }
    }

    public void moveToLocation(Vector3 targetPos)
    {
        pathCounter = 0;
        path = Pathfinder.me.getPath(this.transform.position, targetPos);
        isMoving = true;
        if (path.Count == 0)
        {
            isMoving = false;
            this.GetComponent<Unit>().removeCurrentAction();
        }
    }
}
