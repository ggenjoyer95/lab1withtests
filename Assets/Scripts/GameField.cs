using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{
    public bool debugSpace = false;
    [SerializeField] private int width = 4;
    [SerializeField] private int height = 4;
    private List<Cell> cells = new List<Cell>();

    [SerializeField] private GameObject cellViewPrefab;
    [SerializeField] private RectTransform tilesContainer;

    private void Start()
    {
        Debug.Log("Press SPACE to create a cell.");
    }
    
    private void Update()
    {
        if (debugSpace || Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("[GameField] Space pressed, creating cell.");
            CreateCell();
            debugSpace = false;
        }
    }

    public Vector2Int GetEmptyPosition()
    {
        List<Vector2Int> emptyPositions = new List<Vector2Int>();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                bool isOccupied = false;
                foreach (var cell in cells)
                {
                    if (cell.Position.x == x && cell.Position.y == y)
                    {
                        isOccupied = true;
                        break;
                    }
                }
                if (!isOccupied)
                {
                    emptyPositions.Add(new Vector2Int(x, y));
                }
            }
        }

        if (emptyPositions.Count == 0)
        {
            Debug.LogWarning("[GameField] No empty positions.");
            return new Vector2Int(-1, -1);
        }

        int randomIndex = Random.Range(0, emptyPositions.Count);
        Vector2Int chosenPos = emptyPositions[randomIndex];
        Debug.Log($"[GameField] Chosen pos {chosenPos} from {emptyPositions.Count}.");
        return chosenPos;
    }

    public void CreateCell()
    {
        float rv = Random.value;
        int newValue = rv < 0.9f ? 1 : 2;
        Debug.Log($"[GameField] Create cell {newValue} (rand={rv}).");

        Vector2Int pos = GetEmptyPosition();
        if (pos.x == -1)
        {
            Debug.Log("[GameField] No empty pos.");
            return;
        }

        Cell newCell = new Cell(pos, newValue);
        cells.Add(newCell);

        if (cellViewPrefab == null)
        {
            Debug.LogError("[GameField] cellViewPrefab is null!");
            return;
        }

        GameObject cellGO = Instantiate(cellViewPrefab, tilesContainer);
        CellView cellView = cellGO.GetComponent<CellView>();
        if (cellView != null)
        {
            cellView.Init(newCell, this);
        }
        else
        {
            Debug.LogError("[GameField] Missing CellView script.");
        }
    }
    
}
