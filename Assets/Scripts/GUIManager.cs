using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour {
    public static GUIManager me;
    public Texture2D blackTransBox;


    float originalWidth = 1920.0f;
    float originalHeight = 1080.0f;
    Vector3 scale;

    selectingModes selectionMode;

    // Use this for initialization
    void Awake ()
    {
        me = this;	
	}

    void Update()
    {
        selectionMode = SelectionManager.me.selectionMode;
    }

    public Texture2D getBlackTransBox()
    {
        return blackTransBox;
    }


    void OnGUI()
    {
        GUI.depth = 0;
        scale.x = Screen.width / originalWidth;
        scale.y = Screen.height / originalHeight;
        scale.z = 1;
        var svMat = GUI.matrix;
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);

        drawGUIBackground();

        if (selectionMode == selectingModes.tiles)
        {
            // n/a
        }
        else if (selectionMode == selectingModes.creatingBuildings)
        {
            //drawGUIBackground();
            //drawBuildingConstructButtons();
        }
        else if (selectionMode == selectingModes.units)
        {
          //gui for creating units
            //drawGUIBackground();
            /*
            if (SelectionManager.me.moreThanOneTypeSelected() == true)
            {
                drawFilterUnitButtons();
            }
            else
            {
                //draw selected unit info
                drawUnitInfo();
                drawUnitOrderButtons();
            }*/
        }
        else if (selectionMode == selectingModes.buildings)
        {
            //drawGUIBackground();
            //drawBuildingActionButtons();
        }
        GUI.matrix = svMat;
    }


    void drawGUIBackground()
    {
        Rect bgPos = new Rect(0, originalHeight - (originalHeight / 5), originalWidth, originalHeight / 5);
        //Rect resPos = new Rect(originalHeight - 20, );
        GUI.Box(bgPos, "");
    }
}
