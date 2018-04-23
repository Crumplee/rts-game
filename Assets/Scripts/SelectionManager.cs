using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager me;
    public selectingModes selectionMode;

    [SerializeField]
    public List<GameObject> current;

    public GameObject buildingPlaceCursor;

    GameObject firstTile = null;
    GameObject lastTile = null;

    bool drawBox = false;

    private void Awake()
    {
        current = new List<GameObject>();
        me = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (current.Count == 0 && selectionMode != selectingModes.creatingBuildings && selectionMode != selectingModes.tiles)
        {
            decideSelectionMode();
        }

        switch (selectionMode)
        {
            case selectingModes.tiles:
                checkLeftMouseClick();
                break;
            case selectingModes.units:
                checkUnitSelection();
                break;
            case selectingModes.creatingBuildings:
                createBuilding();
                break;
            default:
                break;
        }
        shouldWeDisplayBuildingConstructCursor();
        //checkLeftMouseClick();
    }

    void decideSelectionMode()
    {
        if (getSelectionRaycast() == null)
        {
            selectionMode = selectingModes.units;

        }
        else
        {
            if (getSelectionRaycast().tag == "Building")
            {
                selectionMode = selectingModes.buildings;
            }
            else
            {
                selectionMode = selectingModes.units;
            }
        }
    }

    public List<GameObject> getCurrent()
    {
        return current;
    }

    public void setCurrent(GameObject s)
    {
        clearCurrent();
        current.Add(s);
        /*
        if (s.GetComponent<TileMaster>() == true)
            s.GetComponent<TileMaster>().OnSelect();*/
    }

    public void setCurrent(List<GameObject> sList)
    {
        clearCurrent();
        current = sList;
        /*
        foreach (GameObject o in current)
        {
            o.GetComponent<TileMaster>().OnSelect();
        }*/
    }

    public void clearCurrent()
    {
        firstTile = null;
        lastTile = null;
        /*
        foreach(GameObject o in current)
        {
            o.GetComponent<TileMaster>().OnDeSelect();
        }*/
        current = new List<GameObject>();
    }

    void shouldWeDisplayBuildingConstructCursor()
    {
        if (selectionMode == selectingModes.creatingBuildings)
        {
            buildingPlaceCursor.SetActive(true);
        }
        else
        {
            buildingPlaceCursor.SetActive(false);
        }
    }

    void checkLeftMouseClick()
    {
        if (Input.GetKey(KeyCode.LeftControl)) // holding lctrl for multiple selection
        {
            Debug.Log("LeftCTRL");
            if (Input.GetMouseButton(0))
            {
                Debug.Log("Multi");
                if (firstTile == null)
                {
                    Debug.Log("firstTile");
                    selectionRaycast(ref firstTile);
                }
                else if (firstTile != null && lastTile != null)
                {
                    Vector2 startCoords = firstTile.GetComponent<TileMaster>().getCoords();
                    Vector2 endCoords = lastTile.GetComponent<TileMaster>().getCoords();
                    if (firstTile != null && lastTile != null)
                    {
                        List<GameObject> selectedTiles = MapGenerator.me.getTiles(startCoords, endCoords);
                        setCurrent(selectedTiles);
                        firstTile = null;
                        lastTile = null;
                    }
                }
            }

            if (Input.GetMouseButton(0) == false && firstTile != null)
            {
                Debug.Log("lastTile");
                drawBox = true;
                selectionRaycast(ref lastTile);
            }
            else
            {
                drawBox = false;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            firstTile = null;
            lastTile = null;
            //Debug.Log("Click");
            selectionRaycast();
            drawBox = false;
        }
        else
        {
            firstTile = null;
            lastTile = null;
            drawBox = false;
        }
    }

    void selectionRaycast()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D raycast = Physics2D.Raycast(mousePosition, Vector2.zero, 0f);
        try
        {
            GameObject hitObject = raycast.collider.gameObject;
            Debug.Log(hitObject.name + " - " + hitObject.transform.position);
            setCurrent(hitObject);
        }
        catch
        {
            Debug.Log("No valid object selected");
        }
    }

    void selectionRaycast(ref GameObject obj) //used for multiselection
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

        RaycastHit2D raycast = Physics2D.Raycast(mousePosition, Vector2.zero, 0f);
        try
        {
            GameObject hitObject = raycast.collider.gameObject;
            Debug.Log(hitObject.name);
            obj = hitObject;
        }
        catch
        {
            Debug.Log("No valid object selected");
        }
    }

    GameObject getSelectionRaycast()
    {
        Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

        RaycastHit2D raycast = Physics2D.Raycast(mousePos, Vector2.zero, 0f);
        try
        {
            GameObject hitObject = raycast.collider.gameObject;
            return hitObject;
        }
        catch
        {
            Debug.Log("No valid object selected");
        }

        return null;
    }

    private void OnGUI()
    {
        if (drawBox)
        {
            Vector3 startPos = Camera.main.WorldToScreenPoint(firstTile.transform.position);

            Vector3 endPos = Input.mousePosition;

            float width, height;
            if (startPos.x > endPos.x)
            {
                width = startPos.x - endPos.x;
            }
            else
            {
                width = endPos.x - startPos.x;
            }

            if (startPos.y > endPos.y)
            {
                height = startPos.y - endPos.y;
            }
            else
            {
                height = endPos.y - startPos.y;
            }
            Rect posToDrawBox;

            if (endPos.x > startPos.x)
            {
                if (endPos.y > startPos.y)
                {
                    posToDrawBox = new Rect(startPos.x, Screen.height - endPos.y, width, height);
                }
                else
                {
                    posToDrawBox = new Rect(startPos.x, Screen.height - startPos.y, width, height);
                }
            }
            else
            {
                if (endPos.y > startPos.y)
                {
                    posToDrawBox = new Rect(endPos.x, Screen.height - endPos.y, width, height);
                }
                else
                {
                    posToDrawBox = new Rect(endPos.x, Screen.height - startPos.y, width, height);
                }
            }

            GUI.Box(posToDrawBox, "");
            //GUI.DrawTexture(posToDrawBox, GUIManager.me.getBlackTransBox());
        }
    }

    public void checkUnitSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

            RaycastHit2D raycast = Physics2D.Raycast(mousePos, Vector2.zero, 0f);
            try
            {
                GameObject hitObject = raycast.collider.gameObject;

                Debug.Log(hitObject.name);


                if (hitObject.tag == "Unit")
                {
                    setCurrent(hitObject);
                }
            }
            catch
            {
                Debug.Log("No valid object selected");
                //clearSelected ();
            }
        }
    }

    void createBuilding()
    {
        GameObject selectedBuilding = BuildingManager.me.getToBuild();
        if (selectedBuilding != null)
        {
            Building selectedBuildScript = selectedBuilding.GetComponent<Building>();
            if (selectedBuildScript != null)
            {
                getMultipleTilesFromCoords(selectedBuildScript.tilesWidth, selectedBuildScript.tilesHeight);
                buildingPlaceCursor.GetComponent<SpriteRenderer>().sprite = selectedBuildScript.buildingSprite;
                //Debug.Log(buildingPlaceCursor + "ezitten ni");
                //CursorCollisionCheck.me.setSprite(selectedBuildScript.buildingSprite);
                buildingPlaceCursor.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

                Color cursorColour = new Color(1, 1, 1, 0.5f);
                buildingPlaceCursor.GetComponent<SpriteRenderer>().color = cursorColour;

                if (isLocationValidForConstruction())
                {
                    buildingPlaceCursor.GetComponent<SpriteRenderer>().color = cursorColour;
                }
                else
                {
                    cursorColour = new Color(1, 0, 0, 0.5f);
                    buildingPlaceCursor.GetComponent<SpriteRenderer>().color = cursorColour;
                }

                if (Input.GetMouseButton(0))
                {
                    if (isLocationValidForConstruction())
                    {
                        createBuildingAtLocation(buildingPlaceCursor.transform.position, selectedBuildScript.tilesWidth, selectedBuildScript.tilesHeight);
                    }
                }
            }
        }
    }

    void getMultipleTilesFromCoords(int width, int height)
    {
        try
        {
            width += 2;
            height += 2;
            GameObject tileAtMousePoint = null;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tileAtMousePoint = MapGenerator.me.getTile((int)mousePos.x, (int)mousePos.y).gameObject;
            //selectionRaycast (ref tileAtMousePoint);//have to change so that it doesnt hit the collider for the building placement
            TileMaster tm = tileAtMousePoint.GetComponent<TileMaster>();
            Vector2 tileGridCoords = tm.getCoords();

            if (isSelectionInGridRange(tileGridCoords, width, height) == true)
            {
                Debug.Log("Enough Space");
                Vector2 startPos = new Vector2(tileGridCoords.x - (width / 2), tileGridCoords.y - (height / 2));
                Vector2 endPos = new Vector2(tileGridCoords.x + (width / 2), tileGridCoords.y + (height / 2));
                setCurrent(MapGenerator.me.getTiles(startPos, endPos));
            }
            else
            {
                clearCurrent();
                Debug.Log("Not enough space");
            }
        }
        catch
        {
            Debug.Log("No tile at mouse position");
        }
    }

    bool isSelectionInGridRange(Vector2 centerCoords, int width, int height)
    {
        width /= 2;
        height /= 2;
        if ((centerCoords.x - width) < 0 || (centerCoords.y - height) < 0 || (centerCoords.x + width) >= MapGenerator.me.mapDimensions.x || (centerCoords.y + height) >= MapGenerator.me.mapDimensions.y)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    bool isLocationValidForConstruction()
    {
        if (current.Count <= 0)
        {
            return false;
        }


        foreach (GameObject tile in current)
        {
            TileMaster tm = tile.GetComponent<TileMaster>();
            if (tm.isWalkable() == false)
            {
                return false;
            }
        }

        int xLowBound = (int)current[0].GetComponent<TileMaster>().getCoords().x;//(int)cursorPos.x - ((width + 0)/2);
        int xHighBound = (int)current[current.Count - 1].GetComponent<TileMaster>().getCoords().x;//(int)cursorPos.x + ((width + 0)/2);
        int yLowBound = (int)current[0].GetComponent<TileMaster>().getCoords().y;//(int)cursorPos.y - ((height + 0) / 2);
        int yHighBound = (int)current[current.Count - 1].GetComponent<TileMaster>().getCoords().y;//(int)cursorPos.y + ((height + 0) / 2);

        for (int x = 0; x < current.Count - 1; x++)
        {
            GameObject tile = current[x];
            TileMaster tm = tile.GetComponent<TileMaster>();

            Vector2 curTileGrid = tm.getCoords();
            if (curTileGrid.x == xLowBound || curTileGrid.x == xHighBound)
            {
                if (curTileGrid.y == yLowBound || curTileGrid.y == yHighBound)
                {
                    if (tm.isWalkable() == false)
                    {
                        return false; //should make sure all edge tiles are walkable
                    }
                }
            }

            if (curTileGrid.y == yLowBound || curTileGrid.y == yHighBound)
            {
                if (curTileGrid.x == xLowBound || curTileGrid.x == xHighBound)
                {
                    if (tm.isWalkable() == false)
                    {
                        return false; //should make sure all edge tiles are walkable
                    }
                }
            }
        }

        return true;
    }

    void createBuildingAtLocation(Vector3 cursorPos, int width, int height) //creates a building from the one currently selected at the mouse position
    {
        //gets the lowest and highest of each axis, works cause the selected tiles are in increasing order
        int xLowBound = (int)current[0].GetComponent<TileMaster>().getCoords().x;
        int xHighBound = (int)current[current.Count - 1].GetComponent<TileMaster>().getCoords().x;
        int yLowBound = (int)current[0].GetComponent<TileMaster>().getCoords().y;
        int yHighBound = (int)current[current.Count - 1].GetComponent<TileMaster>().getCoords().y;

        for (int x = 0; x < current.Count - 1; x++)
        { //goes through all the tiles and makes the ones not on the outside (where the actual building will be placed unwalkable
            GameObject tile = current[x];
            TileMaster tm = tile.GetComponent<TileMaster>();

            Vector2 curTileGrid = tm.getCoords();
            if (curTileGrid.x == xLowBound || curTileGrid.x == xHighBound)
            {
                if (curTileGrid.y == yLowBound || curTileGrid.y == yHighBound)
                {
                    //Debug.LogError ("Making " + tile.name + " Unwalkable ");
                    Debug.LogError("Keeping " + tile.name + " Walkable");

                }
            }
            else if (curTileGrid.y == yLowBound || curTileGrid.y == yHighBound)
            {
                if (curTileGrid.x == xLowBound || curTileGrid.x == xHighBound)
                {
                    //Debug.LogError ("Making " + tile.name + " Unwalkable ");
                    Debug.LogError("Keeping " + tile.name + " Walkable");

                }
            }
            else
            {
                tm.setWalkable(false);
            }
        }

        //this code creates the building
        Vector3 spawnPos = current[(current.Count - 1) / 2].transform.position;//need to have a play with this to make it put the building nearer to the center point
        spawnPos.z = -1;
        GameObject built = (GameObject)Instantiate(BuildingManager.me.getToBuild(), spawnPos, Quaternion.Euler(0, 0, 0));
        SpriteRenderer sr = built.AddComponent<SpriteRenderer>();
        sr.sprite = built.GetComponent<Building>().buildingSprite;
        sr.sortingOrder = 10;
        built.AddComponent<BoxCollider2D>();
        built.SetActive(true);
        built.tag = "Building";

        clearCurrent();

    }


}

    public enum selectingModes
{
    tiles,
    units,
    creatingBuildings,
    buildings
}