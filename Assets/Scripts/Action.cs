using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    public bool multiPartAction = false;
    public bool actionStarted = false;

    public virtual void initaliseLocation(Vector3 position)
    {

    }

    public virtual void initaliseTarget(GameObject target)
    {

    }

    public virtual void initialiseTile(TileMaster tm)
    {

    }


    public virtual void startAction()
    {

    }

    public virtual void reinitialiseAction()
    {

    }

    public virtual bool getIsActionComplete()
    {
        return false;
    }

    public virtual string getActionType()
    {
        return "Idle";
    }
}
