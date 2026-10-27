using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject goalPrefab;

    // Seviye haritası:  # = duvar   . = boş   P = oyuncu başlangıcı   G = hedef
    private readonly string[] levelMap =
    {
        "#########",
        "#P..#...#",
        "#.......#",
        "#..#..#.#",
        "#.....#G#",
        "#########",
    };

    private bool[,] walls;

    public Vector2Int PlayerStartCell { get; private set; }
    public Vector2Int GoalCell { get; private set; }
    public int Width => walls.GetLength(0);
    public int Height => walls.GetLength(1);

    void Awake() // Start'tan önce çalışır; seviye, oyuncu başlamadan hazır olur
    {
        BuildLevel();
    }

    private void BuildLevel()
    {
        int h = levelMap.Length;
        int w = levelMap[0].Length;
        walls = new bool[w, h];

        for (int row = 0; row < h; row++)
        {
            for (int x = 0; x < w; x++)
            {
                int y = h - 1 - row; // Haritanın üst satırı ekranda da üstte olsun
                Vector2Int cell = new Vector2Int(x, y);
                char c = levelMap[row][x];

                if (c == '#')
                {
                    walls[x, y] = true;
                    Instantiate(wallPrefab, CellToWorld(cell), Quaternion.identity, transform);
                }
                else if (c == 'G')
                {
                    GoalCell = cell;
                    Instantiate(goalPrefab, CellToWorld(cell), Quaternion.identity, transform);
                }
                else if (c == 'P')
                {
                    PlayerStartCell = cell;
                }
            }
        }
    }

    // Izgara koordinatını (x, y) dünya konumuna çevirir; seviyeyi ekranda ortalar
    public Vector3 CellToWorld(Vector2Int cell)
    {
        return new Vector3(cell.x - Width / 2f + 0.5f, cell.y - Height / 2f + 0.5f, 0f);
    }

    public bool IsWall(Vector2Int cell)
    {
        if (cell.x < 0 || cell.y < 0 || cell.x >= Width || cell.y >= Height)
            return true; // Harita dışı da duvar sayılır
        return walls[cell.x, cell.y];
    }

    // Oyunun kuralı: duvara çarpana kadar kay; hedefin üstünden geçersen orada dur
    public Vector2Int GetSlideEnd(Vector2Int start, Vector2Int dir)
    {
        Vector2Int current = start;
        while (!IsWall(current + dir))
        {
            current += dir;
            if (current == GoalCell) break;
        }
        return current;
    }
}