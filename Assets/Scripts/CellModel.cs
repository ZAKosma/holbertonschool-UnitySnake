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

    [SerializeField]
    private Text coordDebug;

    public void Init()
    {
        
        var go1 = transform.GetChild(0);
        thisModelBorder = go1.GetComponent<Image>();
        var go2 = transform.GetChild(1);
        thisModelColor = go2.GetComponent<Image>();

        //coordDebug = this.gameObject.GetComponentInChildren<Text>();
        coordDebug.gameObject.SetActive(false);
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

        coordDebug.rectTransform.sizeDelta = new Vector2(colorSize, colorSize);
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
        return thisCell.GetCellValue();
    }

    public Cell GetCellRaw()
    {
        return thisCell;
    }

    public void UpdateCellColor()
    {
        thisModelColor.color = GridModel.Instance.GetOccupantColor(thisCell.GetCellValue());
    }

    public void UpdateCellColor(Color c)
    {
        thisModelColor.color = c;
    }

    [ContextMenu("Show Coordinates")]
    public void DebugCoordinates()
    {
        coordDebug.gameObject.SetActive(true);
        coordDebug.text = thisCell.X() + ", " + thisCell.Y();
    }
}
