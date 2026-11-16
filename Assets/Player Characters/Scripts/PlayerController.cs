using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private BoardManager board;
    private Vector2Int cellPosition;

    public void Spawn(BoardManager boardManager, Vector2Int cell)
    {
        board = boardManager;
        cellPosition = cell; 
    }

    public void MoveTo(Vector2Int cell)
    {
        cellPosition = cell;
        transform.position = board.CellToWorld(cell);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       Vector2Int newCellTarget = cellPosition;
       bool hasMoved = false;

        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            newCellTarget.y += 1;
            hasMoved = true;
        }

        else if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            newCellTarget.y -= 1;
            hasMoved = true;
        }

        else if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            newCellTarget.x += 1;
            hasMoved = true;
        }

        else if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            newCellTarget.x -= 1;
            hasMoved = true;
        }

        if (hasMoved)
        {
            BoardManager.CellData cellData = board.GetCellData(newCellTarget);

            if(cellData != null && cellData.Passable)
            {
                MoveTo(newCellTarget);
            }
        }
    }
}
