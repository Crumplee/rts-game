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
                Vector3 mousePos = Input.mousePosition;
                Vector3 mouseInWorld = Camera.main.ScreenToWorldPoint(mousePos);
                mouseInWorld.z = 0;
                /*
                Debug.Log(mousePos + " - mouseposition");
                Debug.Log(mouseInWorld + " - inworld");*/


                foreach (GameObject g in SelectionManager.me.getCurrent())
                {
                    if (g.GetComponent<Unit>() != null)
                    {
                        Unit u = g.GetComponent<Unit>();
                        Action a = g.AddComponent<Action_Moving>();
                        a.initaliseLocation(mouseInWorld);
                        u.actions.Add(a);
                    }
                }
            }

        }
    }
}
