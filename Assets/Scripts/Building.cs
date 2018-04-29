using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    public int tilesWidth, tilesHeight;
    public string buldingName;
    public Sprite buildingSprite;
    public int wood, stone, gold;

    TileMaster entrance;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setTileNearMe(TileMaster tm)
    { 
        int x = (int)tm.getCoords().x;
        int y = (int)tm.getCoords().y;

        int mod = (tilesHeight / 2) + 1;
        y -= mod;

        entrance = MapGenerator.me.getTile(x, y);
        entrance.onPathSelect();
        Debug.Log("TILE NEAREST : " + entrance.name);
    }

    public TileMaster getGoToTile()
    {
        if (entrance == null)
        {
            setTileNearMe(MapGenerator.me.getTile((int)this.transform.position.x, (int)this.transform.position.y));
            return entrance;
        }
        else
        {
            return entrance;
        }
    }

}
