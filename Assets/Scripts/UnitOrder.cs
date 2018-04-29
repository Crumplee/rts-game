using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitOrder : MonoBehaviour {

       
    // Update is called once per frame
    void Update () {
        commandToMove();        
	}
   

    bool isUnitSelected()
    {
        if (SelectionManager.me.getCurrent().Count > 0)
        {
            return true;
        }
        else
            return false;
    }

    void commandToMove()
    {
        if (isUnitSelected())
        {
            if (Input.GetMouseButton(1))
            {
                /*
                Vector3 mousePos = Input.mousePosition;
                Vector3 mouseInWorld = Camera.main.ScreenToWorldPoint(mousePos);
                mouseInWorld.z = 0;*/

                Vector2 mousePosray = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

                GameObject hitObject = null;
                string objectTag = "";
                RaycastHit2D raycast = Physics2D.Raycast(mousePosray, Vector2.zero, 0f);
                try
                {
                    hitObject = raycast.collider.gameObject;
                    objectTag = hitObject.tag;
                }
                catch
                {
                    Debug.Log("Nothing Hit");
                }

                if (objectTag == "Resource")
                {
                    Debug.Log("Hit a resource");
                    Vector3 mousePos = Input.mousePosition;
                    Vector3 mouseInWorld = Camera.main.ScreenToWorldPoint(mousePos);

                    TileMaster tm = MapGenerator.me.getTile((int)mouseInWorld.x, (int)mouseInWorld.y);
                    if (tm != null)
                    {
                        foreach (GameObject g in SelectionManager.me.getCurrent())
                        {
                            if (g.GetComponent<Unit>() != null)
                            {
                                Unit u = g.GetComponent<Unit>();
                                Action a = g.AddComponent<Action_ResourceGathering>();
                                
                                if (u.canWePerformAction(a))
                                {
                                    a.initaliseLocation(mouseInWorld);
                                    u.actions.Add(a);/*
                                    Action_ResourceGathering res = g.GetComponent<Action_ResourceGathering>();
                                    res.resourceType = hitObject.GetComponent<Resource>().myType;*/
                                    a.enabled = false;
                                }
                                else
                                {
                                    Destroy(a);
                                    moveUnitToLocation(mouseInWorld, g);
                                }
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("Did not hit a resource");
                    Debug.Log(objectTag);
                    Vector3 mousePos = Input.mousePosition;
                    Vector3 mouseInWorld = Camera.main.ScreenToWorldPoint(mousePos);
                    mouseInWorld.z = 0;

                    TileMaster tm = MapGenerator.me.getTile((int)mouseInWorld.x, (int)mouseInWorld.y);
                    if (tm != null)
                    {
                        foreach (GameObject g in SelectionManager.me.getCurrent())
                        {
                            moveUnitToLocation(mouseInWorld, g);
                        }
                    }
                }
                /*
                foreach (GameObject g in SelectionManager.me.getCurrent())
                {
                    if (g.GetComponent<Unit>() != null)
                    {
                        Unit u = g.GetComponent<Unit>();
                        Action a = g.AddComponent<Action_Moving>();
                        a.initaliseLocation(mouseInWorld);
                        u.actions.Add(a);
                    }
                }*/
            }

        }
    }

    void moveUnitToLocation(Vector3 mouseInWorld, GameObject g)
    { 
        if (g.GetComponent<Unit>() != null)
        {
            Unit u = g.GetComponent<Unit>();
            Action a = g.AddComponent<Action_Moving>();
            Debug.Log(u.canWePerformAction(a));
            if (u.canWePerformAction(a) == true)
            {
                a.initaliseLocation(mouseInWorld);
                u.actions.Add(a);
                a.enabled = false;
            }
            else
            {
                Destroy(a);
            }
        }
    }
}
