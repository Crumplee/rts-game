using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMaster : MonoBehaviour {
    float gridX, gridY;

    //pathfind
    public bool walkable = true; 
    int gCost;//cost start-this
    int hCost;//cost this-target
    TileMaster parent;

    public void setG(int val)
    {
        gCost = val;
    }

    public void setH(int val)
    {
        hCost = val;
    }

    public int getH()
    {
        return hCost;
    }

    public int getG()
    {
        return gCost;
    }
    
    public bool isWalkable()
    {
        return walkable;
    }

    public void setWalkable(bool val)
    {
        walkable = val;
    }
    public TileMaster getParent()
    {
        return parent;
    }

    public void setParent(TileMaster val)
    {
        parent = val;
    }

    public virtual void OnSelect()
    {
        this.GetComponent<SpriteRenderer>().color = Color.red;
    }

    public virtual void OnDeSelect()
    {
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public Vector2 getCoords()
    {
        return new Vector2(gridX, gridY);
    }

    public void setCoords(Vector2 coords)
    {
        gridX = coords.x;
        gridY = coords.y;
    }

    public virtual int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

}
