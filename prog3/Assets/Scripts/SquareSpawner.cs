using UnityEngine;
using System.Collections;

public class SquareSpawner : MonoBehaviour
{
    void Update()
    {
        //get mouse position each frame
        Vector2 mouseClick = Input.mousePosition;

        mouseClick = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //set coordinates of the square's 4 corners
        Vector2 topL = new Vector2(mouseClick.x - 1, mouseClick.y + 1);
        Vector2 topR = new Vector2(mouseClick.x + 1, mouseClick.y + 1);
        Vector2 botR = new Vector2(mouseClick.x + 1, mouseClick.y - 1);
        Vector2 botL = new Vector2(mouseClick.x - 1, mouseClick.y - 1);

        //draw white square at mouse click pos
        if (Input.GetMouseButtonDown(0))
        {
            //draw square using said coordinates
            Debug.DrawLine(topL, topR, Color.white, 1);
            Debug.DrawLine(topR, botR, Color.white, 1);
            Debug.DrawLine(botR, botL, Color.white, 1);
            Debug.DrawLine(botL, topL, Color.white, 1);
        }

        //semi-transparent square following mouse pos
        Debug.DrawLine(topL, topR, new Color(1,1,1,0.5f));
        Debug.DrawLine(topR, botR, new Color(1,1,1,0.5f));
        Debug.DrawLine(botR, botL, new Color(1,1,1,0.5f));
        Debug.DrawLine(botL, topL, new Color(1,1,1,0.5f));

        //scroll wheel increase/decrease size of squares

    }
}


