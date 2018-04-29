using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Moving : Action {

    Vector3 targetPos;

    public override void initaliseLocation(Vector3 position)
    {
        targetPos = position;
        //Debug.Log(targetPos + "megkapott coord");
    }

    public override void startAction()
    {
        UnitMovement um = this.GetComponent<UnitMovement>();
        um.moveToLocation(targetPos);
    }

    public override bool getIsActionComplete()
    {
        if (Vector3.Distance(targetPos, this.transform.position) < 2.0f)
            return true;
        else
            return false;
    }

    public override string getActionType()
    {
        return "Movement";
    }
}
