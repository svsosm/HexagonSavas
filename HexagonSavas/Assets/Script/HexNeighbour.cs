using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexNeighbour : MonoBehaviour
{
    public static HexNeighbour instance;

    public Sprite selectedSprite;
    List<GameObject> selectedHexGroup;
    public List<GameObject> selectedHexCell;

    int width;
    int height;
    public int clickCount;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        height = HexMap.instance.height;
        width = HexMap.instance.width;
    }
    

    #region SELECT NEIGHBOURS
    public void SelectAllNeigbourAroundSelectedCell(GameObject selectHex, int i)
    {
        selectedHexGroup = new List<GameObject>();
        selectedHexGroup.Add(HexMap.instance.hexCells[i]);
        if(i == 0) //select bottom left cell
        {
            selectedHexGroup.Add(HexMap.instance.hexCells[i + height]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i + 1]);
        }
        else if(i == height - 1) //select top left cell
        {
            selectedHexGroup.Add(HexMap.instance.hexCells[i - 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i + height]);
        }
        else if(i == Mathf.Pow(width,2) - 1) //select bottom right cell
        {
            selectedHexGroup.Add(HexMap.instance.hexCells[i + 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - height + 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - height]);
        }
        else if(i == (height * width) - 1) //select top right cell
        {
            selectedHexGroup.Add(HexMap.instance.hexCells[i - height]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - height - 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - 1]);
        }
        else if(i % 2 == 0 && i < height - 1 && i > 0) // select cell with even index in the left corner
        {
            selectedHexGroup.Add(HexMap.instance.hexCells[i - 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i + height]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i + 1]);
            
            
        }
        else if(i % 2 == 0 && i > Mathf.Pow(width,2) - 1 && i < (height * width) - 1) // select cell with even index in the right corner
        {
            selectedHexGroup.Add(HexMap.instance.hexCells[i + 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - height]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - 1]);
        }
        else if (i % 2 != 0 && i < height - 1 && i > 0) // select cell with odd index in the left corner
        {
            selectedHexGroup.Add(HexMap.instance.hexCells[i - 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i + height - 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i + height]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i + height + 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i + 1]);
        }
        else if (i % 2 != 0 && i > Mathf.Pow(width,2) - 1 && i < (height * width) - 1) // select cell with odd index in the right corner
        {
            selectedHexGroup.Add(HexMap.instance.hexCells[i + 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - height +1 ]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - height]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - height - 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - 1]);

        }
        else if(i % height == 0 && i > 0 && i < Mathf.Pow(width,2)-1) // select bottom row cell
        {
            selectedHexGroup.Add(HexMap.instance.hexCells[i + height]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i + 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - height + 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - height]);
            
        }
        else if(i % height == width && i > width && i < height*width -1) // select top row cell
        {
            selectedHexGroup.Add(HexMap.instance.hexCells[i - height]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - height - 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i + height]);
        }
        else if(i % height == 2 && i > 2 && i < Mathf.Pow(width,2) +1 || i % height == 4 && i > 4 && i < Mathf.Pow(width, 2) + 3 || i % height == 6 && i > 6 && i < Mathf.Pow(width, 2) + 5)
        {
            selectedHexGroup.Add(HexMap.instance.hexCells[i - height + 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - height]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - height - 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i + height]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i + 1]);

        }
        else //select center cell
        {
            selectedHexGroup.Add(HexMap.instance.hexCells[i + 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - height]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i + height - 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i + height]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i + height + 1]);
        }
        SelectHexCells(selectedHexGroup);


    }



    /**********************************************
     * if cell have 2 neighbours, it has 1 selected hex groups situation  (mainCell + 2 neighbours)
     * have 3 neighbours, 2 situation
     * have 4 neighbours, 3 situation
     * have 5 neighbours, 4 situation
     * have 6 neighbours, 5 situation
     * 
     * Each click select different hex group.
     * normally,neighbours in the clicked corner of the selected cell should be selected.but no time for this.
     * 
     **********************************************/
    public void SelectHexCells(List<GameObject> selectedHexGroup)
    {
        List<GameObject> selectedHexCells = new List<GameObject>();

        switch(selectedHexGroup.Count)
        {
            case 3:
                SeperateAllSituationForHexGroups(1, selectedHexCells);
                break;
            case 4:
                if(clickCount % (selectedHexGroup.Count-2) == 1) //first click
                {
                    SeperateAllSituationForHexGroups(1, selectedHexCells);
                }
                else if(clickCount % (selectedHexGroup.Count - 2) == 0 && clickCount > 0) //second click
                {
                    SeperateAllSituationForHexGroups(2, selectedHexCells);
                }
                break;
            case 5:
                if (clickCount % (selectedHexGroup.Count - 2) == 1) //first click
                {
                    SeperateAllSituationForHexGroups(1, selectedHexCells);
                }
                else if(clickCount % (selectedHexGroup.Count - 2) == 2) //second click
                {
                    SeperateAllSituationForHexGroups(2, selectedHexCells); 
                }
                else if (clickCount % (selectedHexGroup.Count - 2) == 0 && clickCount > 0) //third click
                {
                    SeperateAllSituationForHexGroups(3, selectedHexCells);
                }
                break;
            case 6:
                if (clickCount % (selectedHexGroup.Count - 2) == 1) //first click
                {
                    SeperateAllSituationForHexGroups(1, selectedHexCells);
                }
                else if (clickCount % (selectedHexGroup.Count - 2) == 2) //second click
                {
                    SeperateAllSituationForHexGroups(2, selectedHexCells);
                }
                else if(clickCount % (selectedHexGroup.Count - 2) == 3) //third click
                {
                    SeperateAllSituationForHexGroups(3, selectedHexCells);

                }
                else if (clickCount % (selectedHexGroup.Count - 2) == 0 && clickCount > 0) //fourth click
                {
                    SeperateAllSituationForHexGroups(4, selectedHexCells);
                }
                break;
            case 7:
                if (clickCount % (selectedHexGroup.Count - 2) == 1) //first click
                {
                    SeperateAllSituationForHexGroups(1, selectedHexCells);
                }
                else if (clickCount % (selectedHexGroup.Count - 2) == 2)//second click
                {
                    SeperateAllSituationForHexGroups(2, selectedHexCells);
                }
                else if (clickCount % (selectedHexGroup.Count - 2) == 3) //third click
                {
                    SeperateAllSituationForHexGroups(3, selectedHexCells);

                }
                else if (clickCount % (selectedHexGroup.Count - 2) == 4)//fourth click
                {
                    SeperateAllSituationForHexGroups(4, selectedHexCells);

                }
                else if (clickCount % (selectedHexGroup.Count - 2) == 0 && clickCount > 0) //fifth click
                {
                    SeperateAllSituationForHexGroups(5, selectedHexCells);
                }
                break;
        }

        foreach (GameObject a in selectedHexCells)
        {
            Debug.Log(a.name);
        }



    }

    public void SeperateAllSituationForHexGroups(int count, List<GameObject> hexCells)
    {
        for (int i = count; i < count + 1; i++)
        {
            hexCells.Add(selectedHexGroup[0]);
            hexCells.Add(selectedHexGroup[i]);
            hexCells.Add(selectedHexGroup[i + 1]);
        }

        selectedHexCell = hexCells;
    }

    #endregion



}
