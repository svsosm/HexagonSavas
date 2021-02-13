using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour
{
    public static HexMap instance;

    

    public GameObject hexPrefab;
    public Color[] colors;
    public GameObject[] hexCells;

    public int width = 8;
    public int height = 9;

    float xOffset = 0.538f;
    float yOffset = 0.469f;

    int randNum;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

        GenerateHexMap();

    }


    #region HEXAGONGRID
    //generate hex map
    void GenerateHexMap()
    {
        hexCells = new GameObject[width * height]; //store cell object
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
        hexPos.x = xPos;
        hexPos.y = y * yOffset;
        hexPos.z = 0f;
        hexPrefab.GetComponent<SpriteRenderer>().color = chooseRandomHexColor();
        GameObject hex_go = hexCells[i] = Instantiate(hexPrefab, new Vector3(hexPos.x, hexPos.y, hexPos.z), Quaternion.identity) as GameObject;
        hex_go.name = i.ToString();
        hex_go.transform.SetParent(this.transform);
        ControlNeighbourColor(hex_go, i, x, y);
        
    }
    #endregion

    #region HEXCOLOR
    void ControlNeighbourColor(GameObject mainCell, int i, int X, int Y)
    {
        List<Color> selectedCells = new List<Color>();
        selectedCells.Add(AccessGameObjectColor(mainCell));
        if (X == 1 && Y == 0)
        {
            selectedCells.Add(AccessGameObjectColor(hexCells[i - height + 1]));
            selectedCells.Add(AccessGameObjectColor(hexCells[i - height]));

            checkNeighbourColor(selectedCells, mainCell);
            
        }
        else if(X > 0 && Y == 1)
        {
            //check second column
            selectedCells.Add(AccessGameObjectColor(hexCells[i - height]));
            selectedCells.Add(AccessGameObjectColor(hexCells[i - 1]));

            checkNeighbourColor(selectedCells, mainCell);

        }
        else if(X == width - 1 && Y == height - 1)
        {
            selectedCells.Add(AccessGameObjectColor(hexCells[i - height]));
            selectedCells.Add(AccessGameObjectColor(hexCells[i - height - 1]));
            selectedCells.Add(AccessGameObjectColor(hexCells[i - 1]));

            checkNeighbourColor(selectedCells, mainCell);
        }
        else if(X == width-1 && Y % 2 != 0 && Y > 2)
        {
            //check last column with odd index
            selectedCells.Add(AccessGameObjectColor(hexCells[i - height]));
            selectedCells.Add(AccessGameObjectColor(hexCells[i - 1]));

            checkNeighbourColor(selectedCells, mainCell);
        }
        else if(X > 0 && Y > 0) 
        {
            //check other cells
            selectedCells.Add(AccessGameObjectColor(hexCells[i - height + 1]));
            selectedCells.Add(AccessGameObjectColor(hexCells[i - height]));
            selectedCells.Add(AccessGameObjectColor(hexCells[i - height - 1]));
            selectedCells.Add(AccessGameObjectColor(hexCells[i - 1]));

            checkNeighbourColor(selectedCells, mainCell);
        }


    }

    //if main object and neighbours are same color, change main cell color
    void checkNeighbourColor(List<Color> colors, GameObject mainCell)
    {
        for (int index = 1; index < colors.Count - 1; index++)
        {
            if (colors[0] == colors[index] && colors[0] == colors[index + 1])
            {
                mainCell.GetComponent<SpriteRenderer>().color = colors[0] = changeHexColor(colors[0]);
            }
        }
    }


    //acces gameobject sprite color
    Color AccessGameObjectColor(GameObject gameObject)
    {
        Color goColor = gameObject.GetComponent<SpriteRenderer>().color;
        return goColor;
    }


    //if new color is same existcolor, change it.
    Color changeHexColor(Color existColor)
    {
        Color newColor = chooseRandomHexColor();
        if (existColor == newColor)
        {
            changeHexColor(newColor);
        }

        return newColor;
    }


    //create different color cell
    Color chooseRandomHexColor()
    {
        randNum = Random.Range(0, 5); //create random number for select different color
        Color resultColor = new Color();
        resultColor = colors[randNum];
        return resultColor;
    }

    #endregion HEXCOLOR


}