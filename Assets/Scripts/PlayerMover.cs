using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private GridManager grid;
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private TMP_Text winText;

    private Vector2Int currentCell;
    private Vector3 targetPosition;
    private bool isMoving;

    void Start()
    {
        currentCell = grid.PlayerStartCell;
        transform.position = grid.CellToWorld(currentCell);
        targetPosition = transform.position;

        winText.gameObject.SetActive(false); // Oyun başında yazıyı gizle
    }

        void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (transform.position == targetPosition)   // Hedef noktaya vardık mı?
            {
                isMoving = false;                       // Hareket bitti, yeni tuşu dinle

                if (currentCell == grid.GoalCell)      // Vardığımız yer hedef mi?
                {
                    Debug.Log("WİNNER! 🎉");
                    GetComponent<SpriteRenderer>().color = Color.green;
                    winText.gameObject.SetActive(true);
                }
            }
            return;
        }

        Vector2Int dir = ReadDirection();
        if (dir == Vector2Int.zero) return;

        Vector2Int end = grid.GetSlideEnd(currentCell, dir);
        if (end == currentCell) return; // Duvara yapışık, hareket yok

        currentCell = end;
        targetPosition = grid.CellToWorld(end);
        isMoving = true;
    }

    private Vector2Int ReadDirection()
    {
        var kb = Keyboard.current;
        if (kb == null) return Vector2Int.zero;

        if (kb.upArrowKey.wasPressedThisFrame) return Vector2Int.up;
        if (kb.downArrowKey.wasPressedThisFrame) return Vector2Int.down;
        if (kb.leftArrowKey.wasPressedThisFrame) return Vector2Int.left;
        if (kb.rightArrowKey.wasPressedThisFrame) return Vector2Int.right;
        return Vector2Int.zero;
    }
}