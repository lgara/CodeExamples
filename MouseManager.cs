using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
    // What objects are clickable
    public LayerMask clickableLayer;

    // Swap cursor for objects
    public Texture2D pointer; // Normal pointer
    public Texture2D target; // Pointer for clickable object
    public Texture2D doorway; // Pointer for doorways
    public Texture2D combat; // Pointer for attackable objects

    public EventVector3 OnClickEnvironment;


    void Update()
    {
        RaycastHit hit;

        // If the cursor is over a clickable layer enter
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, clickableLayer.value))
        {
            bool door = false;
            bool item = false;

            // If the cursor is over a game object with the tag Doorway set the cursor to the doorway sprite and set door to true
            if (hit.collider.gameObject.tag == "Doorway")
            {
                Cursor.SetCursor(doorway, new Vector2(16, 16), CursorMode.Auto);

                door = true;
            }
            // Else if the cursor is over a game object with the tag Item set the cursor to the combat sprite(I didnt have an item sprite available) and set item to true
            else if (hit.collider.gameObject.tag == "Item")
            {
                Cursor.SetCursor(combat, new Vector2(16, 16), CursorMode.Auto);

                item = true;
            }
            // Else set the cursor to the target sprite
            else
            {
                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
            }

            if (Input.GetMouseButtonDown(0))
            {
                //  If environment surface is clicked, Debug.Log message
                if (door)
                {
                    Transform doorway = hit.collider.gameObject.transform;

                    OnClickEnvironment.Invoke(doorway.position);
                    Debug.Log("Door!");
                }
                else if (item)
                {
                    Transform itemLocation = hit.collider.gameObject.transform;

                    OnClickEnvironment.Invoke(itemLocation.position);
                    Debug.Log("Item!");
                }
                else
                {
                    OnClickEnvironment.Invoke(hit.point);
                }
            }
        }
        else
        {
            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
        }

        
    }
}

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }