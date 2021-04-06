using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardData : MonoBehaviour
{
    public GameObject grid03;
    public GameObject grid04;
    public GameObject grid05;
    private GameObject currGrid;
    private int spriteNum = 0;

    private Sprite[] gridSprites;

    public void SetCardData(int gridSize, string stageData)
    {
        // 알맞은 크기의 grid 활성화
        switch(gridSize)
        {
            case 3:
                grid03.SetActive(true);
                grid04.SetActive(false);
                grid05.SetActive(false);
                currGrid = grid03;
                break;
            case 4:
                grid03.SetActive(false);
                grid04.SetActive(true);
                grid05.SetActive(false);
                currGrid = grid04;
                break;
            case 5:
                grid03.SetActive(false);
                grid04.SetActive(false);
                grid05.SetActive(true);
                currGrid = grid05;
                break;
        }

        // Grid Image에 색 채우기
        int gridCount = gridSize * gridSize;
        for (int i = 0; i < gridCount; i++)
        {
            Image gridImage = currGrid.transform.GetChild(i).GetComponent<Image>();
            int _stageData = int.Parse(stageData.Substring(i, 1));

            SetGridImage(gridImage, _stageData, gridSize, i);
        }
    }

    void SetGridImage(Image gridImage, int stageData, int gridSize, int gridNum)
    {
        int share = gridNum / gridSize;
        int remainder = gridNum % gridSize;

        gridSprites = stageData == 0 ? QuestManager.Instance.emptySprites : QuestManager.Instance.fillSprites;

        // 첫 줄
        if (share == 0)
        {
            if (remainder == 0)
            {
                spriteNum = 0;
            }
            else if (remainder == gridSize - 1)
            {
                spriteNum = 2;
            }
            else
            {
                spriteNum = 1;
            }
        }
        // 마지막 줄
        else if (share == gridSize - 1)
        {
            if (remainder == 0)
            {
                spriteNum = 6;
            }
            else if (remainder == gridSize - 1)
            {
                spriteNum = 8;
            }
            else
            {
                spriteNum = 7;
            }
        }
        // 중간
        else
        {
            if (remainder == 0)
            {
                spriteNum = 3;
            }
            else if (remainder == gridSize - 1)
            {
                spriteNum = 5;
            }
            else
            {
                spriteNum = 4;
            }
        }

        gridImage.sprite = gridSprites[spriteNum];
    }
}
