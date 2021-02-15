using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{

    List<Color> selectedCellColor;
    int rotateCount;


    public void RotateClockwise()
    {

        StartCoroutine(RotateClockWise(1f));

    }

    public void RotateCounterClockwise()
    {

        StartCoroutine(RotateCounterClockWise(1f));
    }

    IEnumerator RotateClockWise(float time)
    {

        while (rotateCount < 3)
        {

            selectedCellColor = new List<Color>();
            foreach (GameObject col in HexNeighbour.instance.selectedHexCell)
            {
                selectedCellColor.Add(col.GetComponent<SpriteRenderer>().color); //get old selected cell color
            }

            for (int j = 0; j < 3; j++)
            {
                if (j == 2)
                {
                    HexNeighbour.instance.selectedHexCell[j].GetComponent<SpriteRenderer>().color = selectedCellColor[0];
                }
                else
                {
                    HexNeighbour.instance.selectedHexCell[j].GetComponent<SpriteRenderer>().color = selectedCellColor[j + 1]; //change color other object
                }
                if (checkExploison(HexNeighbour.instance.selectedHexCell)) { break; } //if exploison, stop rotate
            }
            yield return new WaitForSeconds(time);
            rotateCount++;
        }

        rotateCount = 0;
        

    }



    IEnumerator RotateCounterClockWise(float time)
    {

        while (rotateCount < 3)
        {

            selectedCellColor = new List<Color>();
            foreach (GameObject col in HexNeighbour.instance.selectedHexCell)
            {
                selectedCellColor.Add(col.GetComponent<SpriteRenderer>().color); //get old selected cell color
            }

            for (int j = 0; j < 3; j++)
            {
                if (j == 0)
                {
                    HexNeighbour.instance.selectedHexCell[j].GetComponent<SpriteRenderer>().color = selectedCellColor[2]; 
                }
                else
                {
                    HexNeighbour.instance.selectedHexCell[j].GetComponent<SpriteRenderer>().color = selectedCellColor[j - 1]; //change color other object
                }
                if (checkExploison(HexNeighbour.instance.selectedHexCell)) { break; } //if exploison, stop rotate
            }
            yield return new WaitForSeconds(time);
            rotateCount++;
        }

        rotateCount = 0;


    }

    private bool checkExploison(List<GameObject> selectCells)
    {
        /* to do: check all selected object neighbour and explosion.
         * destroy exploison object and create new object  
         * increase move count for each exploison.
         * create score script and increase score.
         */
        return false;
    }
       

}
