using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMaster : MonoBehaviour {
    float gridX, gridY;
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
}
