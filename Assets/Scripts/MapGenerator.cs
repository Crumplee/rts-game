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
                Vector3 pos = new Vector3(x, y, 0);
                GameObject currentTile = (GameObject)Instantiate(prefabTile, pos, Quaternion.Euler(0, 0, 0));
                currentTile.GetComponent<TileMaster>().setCoords(new Vector2(x, y));
                currentTile.transform.parent = this.gameObject.transform;
                currentTile.name = "Tile" + currentTile.GetComponent<TileMaster>().getCoords().ToString();
                map[x, y] = currentTile.GetComponent<TileMaster>();
            }
        }
    }
}
