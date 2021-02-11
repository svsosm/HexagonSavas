using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour
{
    public static HexMap instance;

    public GameObject hexPrefab;
    public Color[] colors;
    public GameObject[] hexCells;
    public Vector3[] cellPositions;

    public int width = 8;
    public int height = 9;

    float xOffset = 0.538f;
    float yOffset = 0.469f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

        GenerateHexMap();

    }

    //generate hex map
    void GenerateHexMap()
    {
        hexCells = new GameObject[width * height]; //store cell object
        cellPositions = new Vector3[width * height]; //store cell position
        for(int x=0, i=0; x < width; x++)
        {
            for (int y=0; y < height; y++)
            {
                CreateHexCell(x, y, i++);
            }
        }
    }

    //create cell in hexmap
    void CreateHexCell(int x, int y, int i)
    {
        Vector3 hexPos;
        float xPos = x * xOffset;
        if (y % 2 == 1)
        {
            xPos += xOffset / 2f;
        }
        hexPos.x = -2f + xPos;
        hexPos.y = -2f + y * yOffset;
        hexPos.z = 0f;
        hexPrefab.GetComponent<SpriteRenderer>().color = chooseRandomHexColor();
        GameObject hex_go = hexCells[i] = Instantiate(hexPrefab, new Vector3(hexPos.x, hexPos.y, hexPos.z), Quaternion.identity) as GameObject;
        cellPositions[i] = hexPos;
        hex_go.name = x + "_" + y;
        hex_go.transform.SetParent(this.transform);
    }


    //create different color cell
    Color chooseRandomHexColor()
    {
        int randNum = Random.Range(0, 5); //create random number for select different color
        Color32 resultColor = new Color32();
        switch(randNum)
        {
            case 0:
                resultColor = colors[0];
                break;
            case 1:
                resultColor = colors[1];
                break;
            case 2:
                resultColor = colors[2];
                break;
            case 3:
                resultColor = colors[3];
                break;
            case 4:
                resultColor = colors[4];
                break;
        }
        return resultColor;
    }




}