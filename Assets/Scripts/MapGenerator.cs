using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
    public static MapGenerator me;

    TileMaster[,] map;
    public GameObject prefabTile;

    public Vector2 mapDimensions;

    void Awake()
    {
        me = this;
        map = new TileMaster[(int)mapDimensions.x, (int)mapDimensions.y];
    }

    // Use this for initialization
    void Start ()
    {
        

        generateMap();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log(map.Length);
	}

    void generateMap()
    {
        for (int x = 0; x < mapDimensions.x; ++x)
        {
            for (int y = 0; y < mapDimensions.y; ++y)
            {
                float dX = x + 0.5f;
                float dY = y + 0.5f;
                Vector3 pos = new Vector3(dX, dY, 0);
                GameObject currentTile = (GameObject)Instantiate(prefabTile, pos, Quaternion.Euler(0, 0, 0));
                currentTile.GetComponent<TileMaster>().setCoords(new Vector2(x, y));
                currentTile.transform.parent = this.gameObject.transform;
                currentTile.name = "Tile" + currentTile.GetComponent<TileMaster>().getCoords().ToString();
                map[x, y] = currentTile.GetComponent<TileMaster>();
            }
        }
    }


    public List<GameObject> getTiles(Vector2 startPos, Vector2 endPos)
    {
        //Debug.Log("Getting tiles...");
        int startX, startY, endX, endY;
        List<GameObject> retList = new List<GameObject>();

        if (startPos.x <= endPos.x)
        {
            startX = (int)startPos.x;
            endX = (int)endPos.x;
        }
        else
        {
            startX = (int)endPos.x;
            endX = (int)startPos.x;
        }

        if (startPos.y <= endPos.y)
        {
            startY = (int)startPos.y;
            endY = (int)endPos.y;
        }
        else
        {
            startY = (int)endPos.y;
            endY = (int)startPos.y;
        }


        for (int x = (int)startX; x <= (int)endX; ++x)
        {
            for (int y = (int)startY; y <= (int)endY; ++y)
            {
                retList.Add(map[x, y].gameObject);
            }
        }
        //Debug.Log(retList.Count);
        return retList;

    }


    public List<TileMaster> getTileNeighbours(TileMaster tile)
    { 
        List<TileMaster> neighbours = new List<TileMaster>();
        TileMaster t = tile;

        Vector2 pos = t.getCoords();

        if (pos.x == 0)
        {

            if (pos.y == 0)
            {
                //bottom left
                neighbours.Add(map[(int)pos.x + 1, (int)pos.y]);
                neighbours.Add(map[(int)pos.x, (int)pos.y + 1]);

            }
            else if (pos.y == mapDimensions.y - 1)
            {
                //top left
                neighbours.Add(map[(int)pos.x + 1, (int)pos.y]);
                neighbours.Add(map[(int)pos.x, (int)pos.y - 1]);
            }
            else
            {
                neighbours.Add(map[(int)pos.x + 1, (int)pos.y]);
                neighbours.Add(map[(int)pos.x, (int)pos.y + 1]);
                neighbours.Add(map[(int)pos.x, (int)pos.y - 1]);
            }

        }
        else if (pos.x == mapDimensions.x - 1)
        {

            if (pos.y == 0)
            {
                //bottom right
                neighbours.Add(map[(int)pos.x - 1, (int)pos.y]);
                neighbours.Add(map[(int)pos.x, (int)pos.y + 1]);
            }
            else if (pos.y == mapDimensions.y - 1)
            {
                //top right
                neighbours.Add(map[(int)pos.x - 1, (int)pos.y]);
                neighbours.Add(map[(int)pos.x, (int)pos.y - 1]);
            }
            else
            {
                neighbours.Add(map[(int)pos.x - 1, (int)pos.y]);
                neighbours.Add(map[(int)pos.x, (int)pos.y + 1]);
                neighbours.Add(map[(int)pos.x, (int)pos.y - 1]);
            }

        }
        else
        {
            if (pos.y == 0)
            {
                //bottom right
                neighbours.Add(map[(int)pos.x - 1, (int)pos.y]);
                neighbours.Add(map[(int)pos.x, (int)pos.y + 1]);
                neighbours.Add(map[(int)pos.x + 1, (int)pos.y]);
            }
            else if (pos.y == mapDimensions.y - 1)
            {
                neighbours.Add(map[(int)pos.x - 1, (int)pos.y]);
                neighbours.Add(map[(int)pos.x, (int)pos.y - 1]);
                neighbours.Add(map[(int)pos.x + 1, (int)pos.y]);
            }
            else
            {
                neighbours.Add(map[(int)pos.x - 1, (int)pos.y]);
                neighbours.Add(map[(int)pos.x, (int)pos.y - 1]);
                neighbours.Add(map[(int)pos.x + 1, (int)pos.y]);
                neighbours.Add(map[(int)pos.x, (int)pos.y + 1]);
            }
        }

        return neighbours;
    }

    public TileMaster getTile(int x, int y)
    {
        try
        {
            return map[x, y];
        }
        catch
        {
            return null;
        }
    }
}
