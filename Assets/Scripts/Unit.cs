using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    
    public List<Action> actions;


    //public Action action = null;
    //public Action nextAction = null;

    public string unitType;

    public string[] myActions;

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

    public void addAction(Action a)
    {
        switch (actions.Count)
        {
            case 0:
                actions.Add(a);
                break;
            case 1:
                if (actions[0].getActionType() != "ResourceGather")
                    actions.Add(a);
                else
                    Destroy(a);
                break;
            default:
                Action tmp = actions[1];
                actions[1] = a;
                Destroy(tmp);
                break;
        }
    }


    protected void executeAction()
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
        /*
        if (action != null || nextAction != null)
        {
            if (action && !action.actionStarted)
            {
                action.actionStarted = true;
                action.enabled = true;
                action.startAction();
            }

        }*/


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

    public string[] getAvailableActions()
    {
        return myActions;
    }

}
