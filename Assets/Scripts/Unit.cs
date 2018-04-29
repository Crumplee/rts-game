﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public List<Action> actions;

	void Awake ()
    {
        actions = new List<Action>();
	}
	
	// Update is called once per frame
	void Update () {
        executeAction();
	}

    public void removeCurrentAction()
    {
        Action a = actions[0];
        actions.Remove(a);
        Destroy(a);
    }

    void executeAction()
    {
        if (actions.Count > 0)
        {
            if (!actions[0].actionStarted)
            {
                actions[0].actionStarted = true;
                actions[0].enabled = true;
                actions[0].startAction();
            }
            else
            {
                if (actions[0].getIsActionComplete())
                    removeCurrentAction();
            }
            if (actions[0].multiPartAction)
            {
                actions[0].startAction();
            }
        }
    }

    public virtual bool canWePerformAction(Action ac)
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
        else
        {
            return false;
        }
    }


}
