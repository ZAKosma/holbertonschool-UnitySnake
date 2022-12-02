using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellModel : MonoBehaviour
{
    private Cell thisCell;
    private Image thisModelColor;
    private Image thisModelBorder;

    public void Init()
    {
        
        var go1 = transform.GetChild(0);
        thisModelBorder = go1.GetComponent<Image>();
        var go2 = transform.GetChild(1);
        thisModelColor = go2.GetComponent<Image>();
        
    }
    public void Init(Cell c)
    {
        Init();
        thisCell = c;
    }

    public void SetSize(float colorSize, float borderSize)
    {
        /*thisModelColor.rectTransform.rect.Set(thisModelColor.rectTransform.rect.x,
            thisModelColor.rectTransform.hierarchyCapacity,
            colorSize, colorSize);
        thisModelBorder.rectTransform.rect.Set(thisModelBorder.rectTransform.rect.x,
            thisModelBorder.rectTransform.hierarchyCapacity,
            borderSize, borderSize);*/
        thisModelColor.rectTransform.sizeDelta = new Vector2(colorSize, colorSize);
        thisModelBorder.rectTransform.sizeDelta = new Vector2(borderSize, borderSize);
    }

    private void Start()
    {
        Init();
    }

    public Cell SetCellValue(Occupant occupant)
    {
        thisCell.SetCellValue(occupant);
        
        //Colors
        UpdateCellColor();

        return thisCell;
    }

    public Cell SetCellValue(int occupantValue)
    {
        return SetCellValue(occupantValue);
    }
    
    public Occupant GetCellValue()
    {
        return this.GetCellValue();
    }

    public void UpdateCellColor()
    {
        thisModelColor.color = GridModel.Instance.GetOccupantColor(thisCell.GetCellValue());
    }

    public void UpdateCellColor(Color c)
    {
        thisModelColor.color = c;
    }
}
