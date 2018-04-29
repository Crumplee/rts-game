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

        Vector2 myPos = new Vector2(this.transform.position.x, this.transform.position.y);
        Vector2 tarPos = new Vector2(path[pathCounter].x, path[pathCounter].y);

        if (Vector2.Distance(myPos, tarPos) > 0.5f)
        {
            Vector3 dir = path[pathCounter] - this.transform.position;
            dir.z = 0;
            transform.Translate(dir * 5 * Time.deltaTime);
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
        //Debug.Log(targetPos + " movetolocation");
        path = Pathfinder.me.getPath(this.transform.position, targetPos);
        isMoving = true;
        if (path.Count == 0)
        {
            isMoving = false;
            this.GetComponent<Unit>().removeCurrentAction();
        }
    }
}
