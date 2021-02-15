using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell : MonoBehaviour
{

    string oldClickCellName;
    public Sprite selectedCell;

    List<GameObject> oldSelectedCell;

    private void Start()
    {
        oldSelectedCell = new List<GameObject>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            SelectHexCellFunction();


        }
    }

    void SelectHexCellFunction()
    {
        Debug.Log("Mouse is down");
        RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hitInfo.collider != null)
        {
            if (hitInfo.transform.gameObject.tag == "HexCell")
            {
                //check click same object and change hex group.
                if (oldClickCellName == hitInfo.transform.gameObject.name)
                {
                    HexNeighbour.instance.clickCount++;
                }
                else
                {
                    HexNeighbour.instance.clickCount = 1;
                }
                oldClickCellName = hitInfo.transform.gameObject.name;
                HexNeighbour.instance.SelectAllNeigbourAroundSelectedCell(hitInfo.transform.gameObject, int.Parse(oldClickCellName));

                /*if player click first, select hex group.
                 * other click, change hex group.
                 */
                if(oldSelectedCell.Count == 0)
                {
                    foreach (GameObject newCell in HexNeighbour.instance.selectedHexCell)
                    {
                        newCell.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
                        oldSelectedCell.Add(newCell);
                    }
                }
                else
                {
                    ChangeSelectedGroup(oldSelectedCell, HexNeighbour.instance.selectedHexCell);
                }


            }
        }
        else
        {
            Debug.Log("no hit");
        }
    }


    //change hex group function
    void ChangeSelectedGroup(List<GameObject> oldSelected, List<GameObject> newSelected)
    {
        foreach(GameObject oldCell in oldSelected)
        {
            oldCell.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.black;
        }
        foreach(GameObject newCell in newSelected)
        {
            newCell.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
            oldSelected.Add(newCell);
        }

    }

}
