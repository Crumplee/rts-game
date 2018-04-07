using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour {
    public static GUIManager me;
    public Texture2D blackTransBox;


    // Use this for initialization
    void Awake ()
    {
        me = this;	
	}
	
    public Texture2D getBlackTransBox()
    {
        return blackTransBox;
    }
}
