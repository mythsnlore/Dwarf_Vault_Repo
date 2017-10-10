using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate_Level : MonoBehaviour 
{
    public int TileSize;

    public Texture2D sourceTex;
    private Color32[] pix;
    private int Coord;

    public string Prefab1;
    public string Prefab2;
    public string Prefab3;
    public string DefaultPrefab;

    private Color32 grn = Color.green;
    private Color32 red = Color.red;
    private Color32 blu = Color.blue;

    private int Row;
    private int Col;

    private int Width;

	void Start () 
    {
        Row = 0;
        Col = 0;

        pix = sourceTex.GetPixels32();
        Width = sourceTex.width;

        foreach(Color32 pixel in pix)
        {
            
            if (Col >= Width) //row 1
            {
                NextRow();
            }

            MakeTile(pixel, Row, Col);
            Col += 1;
        }

	}
	
    void NextRow()
    {
        Row += 1;
        Col = 0;
    }

    void MakeTile(Color32 pixColor, int pixRow, int pixCol)
    {
        Vector3 tilePos = new Vector3 (pixRow * TileSize,0,pixCol * -TileSize);
        string name;

        if (pixColor.Equals(red))
        {
            name = Prefab1;
        }
        else if (pixColor.Equals(grn))
        {
            name = Prefab2;
        }
        else if (pixColor.Equals(blu))
        {
            name = Prefab3;
        }
        else
        {
            name = DefaultPrefab;
        }

        Instantiate(Resources.Load(name), tilePos, Quaternion.identity);
        //Debug.Log("Made " + name + " at " + pixRow + ", " + pixCol);
    }
}
