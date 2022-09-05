using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomimg : MonoBehaviour
{
    private int rand;
    private int coordsx;
    private int coordsy;
    public Sprite[] SpritePics;

    public Vector2[] coords;
    // Start is called before the first frame update
    void Start()
    {
        rand = Random.Range(0,SpritePics.Length);
        GetComponent<SpriteRenderer>().sprite = SpritePics[rand];
        coordsx = Mathf.RoundToInt(coords[rand].x);
        coordsy = Mathf.RoundToInt(coords[rand].y);
        Debug.Log(coordsx.ToString() + ", " + coordsy.ToString());
    }
}
