using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestData
{
    private string modeType;
    private int stageID;
    private int gridSize;
    private string front;
    private string side;
    private string top;

    public QuestData(string _modeType, int _stageID, int _gridSize, string _front, string _side, string _top)
    {
        this.modeType = _modeType;
        this.stageID = _stageID;
        this.gridSize = _gridSize;
        this.front = string.Concat(_front.Where(x => !char.IsWhiteSpace(x)));
        this.side = string.Concat(_side.Where(x => !char.IsWhiteSpace(x)));
        this.top = string.Concat(_top.Where(x => !char.IsWhiteSpace(x)));
    }

    public string GetModeType()
    {
        return this.modeType;
    }

    public int GetStageID()
    {
        return this.stageID;
    }

    public int GetGridSize()
    {
        return this.gridSize;
    }

    public string GetFrontInfo()
    {
        return this.front;
    }

    public string GetSideInfo()
    {
        return this.side;
    }

    public string GetTopInfo()
    {
        return this.top;
    }

    public void ShowQuestData()
    {
        Debug.Log($"modeType = {modeType} ::: stageID = {stageID}" +
            $"\n gridSize = {gridSize} \n front = {front} \n side = {side} \n top = {top}");
    }
}
