using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : Unit {

	// Use this for initialization
	void Awake ()
    {
        myActions = new string[] { "Delete", "Build" };
    }
	
	// Update is called once per frame
	void Update () {
        executeAction();
    }

    public override bool canWePerformAction(Action ac)
    {
        if (ac.getActionType().Equals("Movement"))
        {
            return true;
        }
        else if (ac.getActionType().Equals("Idle"))
        {
            return false;
        }
        else if (ac.getActionType().Equals("ResourceGather"))
        {
            return true;
        }
        else if (ac.getActionType().Equals("Building"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
