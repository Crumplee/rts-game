using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {
    public static Pathfinder me;

    void Awake()
    {
        me = this;
    }

    public List<Vector3> getPath(Vector3 startPos, Vector3 endPos)
    {
        //Debug.Log(endPos + " endpos");
        List<TileMaster> path = new List<TileMaster>();
        getPath(startPos, endPos, ref path);
        List<Vector3> retVal = convertToVectorPath(path);
        return retVal;
    }
    List<Vector3> convertToVectorPath(List<TileMaster> tiles)
    {
        List<Vector3> retVal = new List<Vector3>();
        foreach (TileMaster tile in tiles)
        {
            //Debug.Log(tile.gameObject.transform.position + " ez");
            retVal.Add (tile.gameObject.transform.position);
            //retVal.Add(tile.transform.position);
        }
        return retVal;
    }

    void getPath(Vector3 startPos, Vector3 endPos, ref List<TileMaster> store)
    {
        Vector2 sPos = new Vector2((int)startPos.x, (int)startPos.y);
        Vector2 ePos = new Vector2((int)endPos.x, (int)endPos.y);

        TileMaster startNode = MapGenerator.me.getTile((int)sPos.x, (int)sPos.y); 
        TileMaster targetNode = MapGenerator.me.getTile((int)ePos.x, (int)ePos.y);

        if (startNode == null || targetNode == null || targetNode.isWalkable() == false)
        {
            Debug.Log("not walkable");
            return;
        }

        List<TileMaster> openSet = new List<TileMaster>(); //tiles to check
        List<TileMaster> closedSet = new List<TileMaster>();//checked
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            TileMaster node = openSet[0];

            for (int i = 1; i < openSet.Count; ++i)
            {
                if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
                {
                    if (openSet[i].getH() < node.getH())
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            if (node == targetNode)
            {
                //Debug.LogError ("Finished Path " + startNode.name + " " + targetNode.name);
                RetracePath(startNode, targetNode, ref store);
                return;
            }

            foreach (TileMaster neighbour in MapGenerator.me.getTileNeighbours(node))
            {

                if (!neighbour.isWalkable() || closedSet.Contains(neighbour) || neighbour == null || node == null)
                {//not valid neighbour
                    continue;
                }

                int newCostToNeighbour = node.getG() + GetDistance(node, neighbour);//calculates gCost
                if (newCostToNeighbour < neighbour.getG() || !openSet.Contains(neighbour))
                {

                    neighbour.setG(newCostToNeighbour);
                    neighbour.setH(GetDistance(neighbour, targetNode));
                    neighbour.setParent(node);
                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }

    void RetracePath(TileMaster startNode, TileMaster targetNode, ref List<TileMaster> store)
    {
        List<TileMaster> path = new List<TileMaster>();
        TileMaster currentNode = targetNode;

        while (currentNode != startNode)
        {
            //Debug.Log ("Retracing path " + currentNode.gameObject.name);
            path.Add(currentNode);
            //currentNode.OnSelect();
            currentNode = currentNode.getParent();
        }
        path.Reverse();
        store = path;
    }

    int GetDistance(TileMaster nodeA, TileMaster nodeB)
    {

        int dstX = Mathf.Abs((int)nodeA.getCoords().x - (int)nodeB.getCoords().x);
        int dstY = Mathf.Abs((int)nodeA.getCoords().y - (int)nodeB.getCoords().y);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}