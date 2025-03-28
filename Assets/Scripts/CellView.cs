using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour
{
    [SerializeField] private Text valueText;
    private Cell cell;
    private GameField gameField;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        Debug.Log("[CellView] Awake called");
    }

    public void Init(Cell newCell, GameField field)
    {
        cell = newCell;
        gameField = field;
        Debug.Log($"[CellView] Init cell pos = {cell.Position} val = {cell.Value}");
        cell.OnValueChanged += UpdateValue;
        cell.OnPositionChanged += UpdatePosition;
        UpdateValue(cell, cell.Value);
        UpdatePosition(cell, cell.Position);
    }

    private void UpdateValue(Cell changedCell, int newValue)
    {
        int displayValue = (int)Mathf.Pow(2, newValue);
        if (valueText != null)
        {
            valueText.text = displayValue.ToString();
        }
        Debug.Log($"[CellView] Update value val = {newValue}, {displayValue}");
    }

    private void UpdatePosition(Cell changedCell, Vector2Int newPos)
    {
        float cellSize = 130f;
        float spacing = 15f;
        float paddingLeft = 20f;
        float paddingTop = 20f;
        float xPos = paddingLeft + (cellSize + spacing) * newPos.x;
        float yPos = -paddingTop - (cellSize + spacing) * newPos.y;
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = new Vector2(xPos, yPos);
        }
        Debug.Log($"[CellView] Updated pos = {xPos}, {yPos}");
    }

    private void OnDestroy()
    {
        Debug.Log("[CellView] OnDestroy called");
        if (cell != null)
        {
            cell.OnValueChanged -= UpdateValue;
            cell.OnPositionChanged -= UpdatePosition;
        }
    }
}
