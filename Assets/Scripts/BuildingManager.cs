using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {
    public static BuildingManager me;

    public GameObject[] playerBuildings;
    [SerializeField]
    GameObject selectedBuilding;
    public List<Building> buildingsInGame;

    // Use this for initialization
    void Awake ()
    {
        me = this;	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject getToBuild()
    {
        return selectedBuilding;
    }

    private void OnGUI()
    {
        if (SelectionManager.me.selectionMode == selectingModes.creatingBuildings)
        {
            int yMod = 0;
            foreach (GameObject b in playerBuildings)
            {

                try
                {
                    Building buildingScr = b.GetComponent<Building>();
                    Rect pos = new Rect(50, 50 + (50 * yMod), 100, 50);
                    if (GUI.Button(pos, buildingScr.name))
                    {
                        selectedBuilding = b;
                    }
                    yMod += 1;
                }
                catch
                {
                    Debug.Log("Building missing a component");
                }
            }
        }
    }

    public Building getNearestBuildingOfType(string type, Vector3 myPos)
    {
        //Building b = null;
        List<Building> buildingsOfType = new List<Building>();
        foreach (Building bl in buildingsInGame)
        {
            if (bl.name == type)
            {
                buildingsOfType.Add(bl);
            }
        }
        float curDistance = 99999.0f;
        Building retVal = null;
        Vector2 myPosV2 = new Vector2(myPos.x, myPos.y);
        foreach (Building bl in buildingsOfType)
        {
            Vector2 buildPos = new Vector2(bl.gameObject.transform.position.x, bl.gameObject.transform.position.y);

            if (Vector2.Distance(buildPos, myPosV2) < curDistance)
            {
                curDistance = Vector2.Distance(buildPos, myPosV2);
                retVal = bl;
            }
        }

        if (retVal != null)
        {
            return retVal;
        }
        else
        {
            return null;
        }
    }

}
