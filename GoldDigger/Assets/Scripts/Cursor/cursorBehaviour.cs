using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorBehaviour : MonoBehaviour
{
    public Texture2D defaultCursor;
    public Texture2D handCursor;
    public Texture2D stoneCursor;
    public Texture2D copperCursor;
    public Texture2D ironCursor;
    public Texture2D cobaltCursor;
    public Texture2D diamondCursor;
    public Texture2D titaniumCursor;



    void Start()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {

    }
    //GameObject.Find("Player").GetComponent<Player>().GetYPos < 0
    void OnMouseEnter()
    {
        if (gameObject.tag == "Above")
        {
            //Cursor.SetCursor(handCursor, Vector2.zero, CursorMode.Auto);

            int caseSwitch = UpgradeHandlerPickaxe.caseSwitchCursor;
            
            switch (caseSwitch)
            {
                case 1:
                    Cursor.SetCursor(stoneCursor, Vector2.zero, CursorMode.Auto);
                    break;
                case 2:
                    Cursor.SetCursor(copperCursor, Vector2.zero, CursorMode.Auto);
                    break;
                case 3:
                    Cursor.SetCursor(ironCursor, Vector2.zero, CursorMode.Auto);
                    break;
                case 4:
                    Cursor.SetCursor(cobaltCursor, Vector2.zero, CursorMode.Auto);
                    break;
                case 5:
                    Cursor.SetCursor(diamondCursor, Vector2.zero, CursorMode.Auto);
                    break;
                case 6:
                    Cursor.SetCursor(titaniumCursor, Vector2.zero, CursorMode.Auto);
                    break;
                default:
                    Cursor.SetCursor(handCursor, Vector2.zero, CursorMode.Auto);
                    break;
            }            
        }
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }
}
