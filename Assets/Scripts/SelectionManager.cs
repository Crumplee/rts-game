using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour {
    public static SelectionManager sm; // ???

    [SerializeField]
    GameObject current;

    private void Awake()
    {
        //findAllSpuerClassExamples();
    }
    
	
	// Update is called once per frame
	void Update ()
    {
        checkLeftMouseClick();
	}

    public GameObject getCurrent()
    {
        return current;
    }

    public void setCurrent(GameObject s)
    {
        current = s;
        current.GetComponent<TileMaster>().OnSelect();
    }

    void clearCurrent()
    {
        current = null;
    }


    void checkLeftMouseClick()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Click");
            selectionRaycast();
        }

    }

    void selectionRaycast()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

        RaycastHit2D raycast = Physics2D.Raycast(mousePosition, Vector2.zero, 0f);
        try
        {
            GameObject hitObject = raycast.collider.gameObject;
            Debug.Log(hitObject.name);
            setCurrent(hitObject);
        }
        catch
        {
            Debug.Log("No valid object selected");
        }
    }
}
