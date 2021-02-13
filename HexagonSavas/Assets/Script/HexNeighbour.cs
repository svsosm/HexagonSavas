using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexNeighbour : MonoBehaviour
{

    public Sprite selectedSprite;
    List<GameObject> selectedHexGroup;

    int width;
    int height;
    public int clickCount;
    string oldClickCellName;

    private void Start()
    {
        height = HexMap.instance.height;
        width = HexMap.instance.width;
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {

            SelectHexCell();

        }
    }

    void SelectHexCell()
    {
        Debug.Log("Mouse is down");
        RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hitInfo.collider != null)
        {
            if (hitInfo.transform.gameObject.tag == "HexCell")
            {
                
                if (oldClickCellName == hitInfo.transform.gameObject.name)
                {
                    clickCount++;
                }
                else
                {
                    clickCount = 1;
                }
                oldClickCellName = hitInfo.transform.gameObject.name;
                SelectAllNeigbourAroundSelectedCell(hitInfo.transform.gameObject, int.Parse(oldClickCellName));
                
            }
            else
            {
                Debug.Log("nop");
            }
        }
        else
        {
            Debug.Log("no hit");
        }
    }

    #region SELECT NEIGHBOURS
    void SelectAllNeigbourAroundSelectedCell(GameObject selectHex, int i)
    {
        selectedHexGroup = new List<GameObject>();
        selectedHexGroup.Add(HexMap.instance.hexCells[i]);
        if(i == 0) //select bottom left cell
        {
            selectedHexGroup.Add(HexMap.instance.hexCells[i + 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i + width + 1]);
        }
        else if(i == height - 1) //select top left cell
        {
            selectedHexGroup.Add(HexMap.instance.hexCells[i - 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i + width + 1]);
        }
        else if(i == Mathf.Pow(width,2) - 1) //select bottom right cell
        {
            selectedHexGroup.Add(HexMap.instance.hexCells[i + 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - width]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - width - 1]);
        }
        else if(i == (height * width) - 1) //select top right cell
        {
            selectedHexGroup.Add(HexMap.instance.hexCells[i - height]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - height - 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - 1]);
        }
        else if(i % 2 == 0 && i < height - 1 && i > 0) // select cell with even index in the left corner
        {
            selectedHexGroup.Add(HexMap.instance.hexCells[i + 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i + width +1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - 1]);
        }
        else if(i % 2 == 0 && i > Mathf.Pow(width,2) - 1 && i < height * width - 1) // select cell with even index in the right corner
        {
            selectedHexGroup.Add(HexMap.instance.hexCells[i + 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - width - 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - 1]);
        }
        else if (i % 2 != 0 && i < height - 1 && i > 0) // select cell with odd index in the left corner
        {
            selectedHexGroup.Add(HexMap.instance.hexCells[i + 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i + width + 2]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i +width + 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i + width]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - 1]);
        }
        else if (i % 2 != 0 && i > Mathf.Pow(width,2) - 1 && i < height * width - 1) // select cell with odd index in the right corner
        {
            selectedHexGroup.Add(HexMap.instance.hexCells[i + 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i -width]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - width -1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - width - 2]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - 1]);

        }
        else if(i % height == 0 && i > 0 && i < Mathf.Pow(width,2)-1) // select bottom row cell
        {
            selectedHexGroup.Add(HexMap.instance.hexCells[i - height]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - height + 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i + 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i + height]);
        }
        else if(i % height == width && i > width && i < height*width -1) // select top row cell
        {
            selectedHexGroup.Add(HexMap.instance.hexCells[i - height]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - height - 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i - 1]);
            selectedHexGroup.Add(HexMap.instance.hexCells[i + height]);
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
    void SelectHexCells(List<GameObject> selectedHexGroup)
    {
        List<GameObject> selectedHexCells = new List<GameObject>();

        switch(selectedHexGroup.Count)
        {
            case 3:
                selectedHexCells = selectedHexGroup;
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

    void SeperateAllSituationForHexGroups(int count, List<GameObject> hexCells)
    {
        for (int i = count; i < count + 1; i++)
        {
            hexCells.Add(selectedHexGroup[0]);
            hexCells.Add(selectedHexGroup[i]);
            hexCells.Add(selectedHexGroup[i + 1]);
        }

    }

    #endregion



}
