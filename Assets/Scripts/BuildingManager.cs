using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {
    public static BuildingManager me;

    public GameObject[] playerBuildings;
    [SerializeField]
    GameObject selectedBuilding;

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

}
