using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int pointValue;
    public int healthPoints;
    public Sprite weakBrick, mediumBrick, hardBrick, veryHardBrick;

    public void BreakBrick()
    {
        healthPoints--;

        switch(healthPoints)
        {
            case 4:
                GetComponent<SpriteRenderer>().sprite = veryHardBrick;
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = hardBrick;
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = mediumBrick;
                break;
            case 1:
                GetComponent<SpriteRenderer>().sprite = weakBrick;
                break;
        }
    }
}
