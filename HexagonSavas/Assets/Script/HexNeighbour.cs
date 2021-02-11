using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexNeighbour : MonoBehaviour
{

    private void Update()
    {
        Ray worldPos = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(worldPos, out hit))
        {
            Debug.Log(hit.point);
        }
    }

    public GameObject[] GetNeighbours(int x, int y, int mapSizeX, int mapSizeY, Vector3 cellPosition)
    {
        List<GameObject> neighbours = new List<GameObject>(2);

        
        if(x > cellPosition.x && y > cellPosition.y)
        {
            //TOP RIGHT + RIGHT
            neighbours.Add(HexMap.instance.hexCells[x + 1 + y * mapSizeY]);
            neighbours.Add(HexMap.instance.hexCells[x + (y - 1) * mapSizeY]);
        }
        else if(x < cellPosition.x && y > cellPosition.y)
        {
            //TOP LEFT + LEFT
            neighbours.Add(HexMap.instance.hexCells[x - 1 + y * mapSizeY]);
            neighbours.Add(HexMap.instance.hexCells[(x - 1) + (y - 1) * mapSizeY]);
        }


        //LEFT
        if (x > 0)
            neighbours.Add(HexMap.instance.hexCells[x - 1 + y * mapSizeY]);

        //RIGHT
        if (x < mapSizeX)
            neighbours.Add(HexMap.instance.hexCells[x + 1 + y * mapSizeY]);
        
        if(y % 2 == 0)
        {
            //TOP LEFT
            if (y > 0)
                neighbours.Add(HexMap.instance.hexCells[(x - 1) + (y - 1) * mapSizeY]);
            
            
        }
        return neighbours.ToArray();
    }
}
