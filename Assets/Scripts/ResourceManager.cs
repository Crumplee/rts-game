using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {
    public static ResourceManager me;

    public int food = 0, wood = 0, stone = 0, gold = 0;

    void Awake()
    {
        me = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void reduceResource(string resource, int amount)
    {
        changeResourceAmount(resource, amount * -1);
    }

    public void increaseResource(string resource, int amount)
    {
        changeResourceAmount(resource, amount);
        //Debug.Log(resource + " ---- " + amount);
    }

    public bool checkResourceAmount(string resource, int amount)
    {
        switch (resource)
        {
            case "food":
                if (food >= amount)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                break;
            case "wood":
                if (wood >= amount)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                break;
            case "stone":
                if (stone >= amount)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                break;
            case "gold":
                if (gold >= amount)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                break;
            default:
                return false;
                break;
        }
    }

    void changeResourceAmount(string resource, int amount)
    {
        switch (resource)
        {
            case "food":
                food += amount;
                break;
            case "wood":
                wood += amount;
                break;
            case "stone":
                stone += amount;
                break;
            case "gold":
                gold += amount;
                break;
            default:
                break;
        }
    }

    float originalWidth = 1920.0f; 
    float originalHeight = 1080.0f;
    Vector3 scale;
    float dispWidth = 1920.0f / 6;

    void OnGUI()
    {
        GUI.depth = 0;
        scale.x = Screen.width / originalWidth;
        scale.y = Screen.height / originalHeight;
        scale.z = 1;
        var svMat = GUI.matrix;
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);

        for (int x = 0; x < 5; x++)
        {
            Rect pos = new Rect(0 + (dispWidth * x), 0, dispWidth, 75);

            switch (x)
            {
                case 0:
                    GUI.Box(pos, "Food " + food.ToString());
                    break;
                case 1:
                    GUI.Box(pos, "Wood " + wood.ToString());
                    break;
                case 2:
                    GUI.Box(pos, "Stone " + stone.ToString());
                    break;
                case 3:
                    GUI.Box(pos, "Gold " + gold.ToString());
                    break;
                default:
                    break;
            }
        }
        GUI.matrix = svMat;
    }
}
