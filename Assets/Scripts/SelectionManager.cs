using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour {
    public static SelectionManager me; 

    [SerializeField]
    public List<GameObject> current;

    GameObject firstTile = null;
    GameObject lastTile = null;

    bool drawBox = false;

    private void Awake()
    {
        current = new List<GameObject>();
        me = this;
    }
    	
	// Update is called once per frame
	void Update ()
    {
        checkLeftMouseClick();
	}

    public List<GameObject> getCurrent()
    {
        return current;
    }

    public void setCurrent(GameObject s)
    {
        clearCurrent();
        current.Add(s);
        //s.GetComponent<TileMaster>().OnSelect();
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

    void clearCurrent()
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


    void checkLeftMouseClick()
    {    
        if(Input.GetKey(KeyCode.LeftControl)) // holding lctrl for multiple selection
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
            Debug.Log("Click");
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


                if (hitObject.tag == "Unit")// -1 Z
                {
                    //setSelected(hitObject);
                }
            }
            catch
            {
                Debug.Log("No valid object selected");
                //clearSelected ();
            }
        }
    }

}
