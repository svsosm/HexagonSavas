using System.Collections;
using UnityEngine;

public class HexMap : MonoBehaviour
{
    public GameObject hexPrefab;
    public Color[] colors;

    public int width = 8;
    public int height = 9;

    float xOffset = 0.538f;
    float yOffset = 0.469f;

    private void Start()
    {
        for(int x=0; x<width; x++)
        {
            for(int y=0; y<height; y++)
            {
                float xPos = x * xOffset;
                if(y % 2 == 1)
                {
                    xPos += xOffset / 2f;
                }
                hexPrefab.GetComponent<SpriteRenderer>().color = chooseRandomHexColor();
                GameObject hex_go = (GameObject) Instantiate(hexPrefab, new Vector3(-2 + xPos, -2f + y * yOffset, 0), Quaternion.identity);
                hex_go.name = "Hex " + x + "_" + y;
                hex_go.transform.SetParent(this.transform);
                

            }
        }
    }

    Color chooseRandomHexColor()
    {
        int randNum = Random.Range(0, 5);
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