using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_ResourceGathering : Action {

    public Vector3 positionOfResource;
    public float resourceTimer = 5.0f;
    public float storeTimer = 1.0f;
    public Vector3 positionOfMain_keep;

    //action states
    public bool movingToResource = false; //moving to resource that the player right clicked on
    public bool gatheredResource = false; //at the location of the resource, gathering
    public bool movingToStorehouse = false; //gathered the resource, moving to the storehouse
    public bool storedResource = false; //have we reached the storehouse
    public bool loop = true; //whether we repeat the action


    
    public bool atRes = false;
    public bool atStore = false;
    public string resourceType = "";

    public override void initaliseLocation(Vector3 position)
    {
        multiPartAction = true; 
        positionOfResource = position;
        try
        {
            //positionOfMain_keep = GameObject.FindGameObjectWithTag("Building").transform.position;
            positionOfMain_keep = BuildingManager.me.getNearestBuildingOfType("Main_keep", this.gameObject.transform.position).getGoToTile().transform.position;
        }
        catch
        {
            loop = false;
        }
    }

    public override void reinitialiseAction()
    {
        initaliseLocation(positionOfResource);
    }

    public override void startAction()
    {
        atRes = atResource();
        atStore = atStorehouse();

        //Debug.Log(positionOfMain_keep);
        

        if (atResource() == false && movingToResource == false)
        {
            //Debug.Log("moving to resource");
            moveToResource();
        }

        if (atResource() == true && gatheredResource == false)
        {
            //Debug.Log("gather resource");
            gatherResource();
        }

        if (atStorehouse() == false && gatheredResource == true)
        {
            //Debug.Log("moving to storehouse");
            moveToStorehouse();
        }

        if (atStorehouse() == true && gatheredResource == true)
        {
            //Debug.Log("store resource");
            storeResources();
        }
    }

    void moveToResource()
    {
        if (movingToResource == false)
        {
            UnitMovement um = this.GetComponent<UnitMovement>();
            um.moveToLocation(positionOfResource);
            movingToResource = true;
        }
    }

    bool atResource()
    {
        Vector2 myPos = new Vector2(this.transform.position.x, this.transform.position.y);
        Vector2 targetPos = new Vector2(positionOfResource.x, positionOfResource.y);

        if (Vector2.Distance(myPos, targetPos) < 2.0f)
        {
            movingToResource = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    bool atStorehouse()
    {
        Vector2 myPos = new Vector2(this.transform.position.x, this.transform.position.y);
        Vector2 targetPos = new Vector2(positionOfMain_keep.x, positionOfMain_keep.y);

        if (Vector2.Distance(myPos, targetPos) < 2.0f)
        {
            movingToStorehouse = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    void gatherResource()
    {
        resourceTimer -= Time.deltaTime;

        if (resourceTimer <= 0)
        {
            gatheredResource = true;
            resourceTimer = 5.0f;
        }
    }

    void moveToStorehouse()
    {
        if (movingToStorehouse == false)
        {
            UnitMovement um = this.GetComponent<UnitMovement>();
            um.moveToLocation(positionOfMain_keep);            
        }
    }

    void storeResources()
    {
        storeTimer -= Time.deltaTime;

        if (storeTimer <= 0)
        {
            Unit um = this.GetComponent<Unit>();

            if (um.actions.Count > 1)
            {
                loop = false;
            }
            else
            {
                loop = true;
            }

            ResourceManager.me.increaseResource(resourceType, 100);
            storeTimer = 1.0f;
            resetAction();
        }
        
    }

    void resetAction()
    {
        movingToResource = false;
        gatheredResource = false;
        movingToStorehouse = false;
        storedResource = false;
    }

    public override bool getIsActionComplete()
    {
        return !loop;
    }

    public override string getActionType()
    {
        return "ResourceGather";
    }
}
