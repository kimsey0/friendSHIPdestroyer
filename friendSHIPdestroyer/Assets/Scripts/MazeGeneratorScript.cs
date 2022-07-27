using UnityEngine;

public class MazeGeneratorScript : MonoBehaviour
{
    public int Width = 30;
    public int Height = 40;

    private readonly (int x, int y)[] directions = new[]
    {
            (1, 0),
            (-1, 0),
            (0, 1),
            (0, -1)
        };

    // Start is called before the first frame update
    void Start()
    {
        var maze = GenerateMaze();

        const int scaling = 10;
        for (var i = 0; i < maze.GetLength(0); i++)
        {
            for (var j = 0; j < maze.GetLength(1); j++)
            {
                if ((maze[i, j] & 0b1) > 0)
                {
                    var wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    wall.transform.parent = transform;
                    wall.name = $"Wall left {i}, {j}";
                    wall.transform.localPosition = new Vector3(i * scaling, -j * scaling - scaling / 2, 0);
                    wall.transform.localScale = new Vector3(1, scaling, 1);
                }
                if ((maze[i, j] & 0b10) > 0)
                {
                    var wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    wall.transform.parent = transform;
                    wall.name = $"Wall top {i}, {j}";
                    wall.transform.localPosition = new Vector3(i * scaling + scaling / 2, -j * scaling, 0);
                    wall.transform.localScale = new Vector3(scaling, 1, 1);
                }
            }
        }
        //e.Graphics.DrawLine(pen, new Point(maze.GetLength(0) * scaling, 0), new Point(maze.GetLength(0) * scaling, maze.GetLength(1) * scaling));
        //e.Graphics.DrawLine(pen, new Point(0, maze.GetLength(1) * scaling), new Point(maze.GetLength(0) * scaling, maze.GetLength(1) * scaling));
    }

    private byte[,] GenerateMaze()
    {
        var maze = new byte[Width, Height];
        for (var i = 0; i < maze.GetLength(0); i++)
        {
            for (var j = 0; j < maze.GetLength(1); j++)
            {
                maze[i, j] = 0b11;
            }
        }

        MakePath(maze, 0, 0);

        return maze;
    }

    private void MakePath(byte[,] maze, int x, int y)
    {
        directions.Shuffle();
        foreach (var (dx, dy) in directions)
        {
            if (x + dx < 0 || x + dx >= Width || y + dy < 0 || y + dy >= Height || (maze[x + dx, y + dy] & 0b100) > 0)
            {
                continue;
            }

            maze[x + dx, y + dy] |= 0b100;
            switch (dx, dy)
            {
                case (1, 0):
                    maze[x + 1, y] &= 0b110;
                    break;
                case (0, 1):
                    maze[x, y + 1] &= 0b101;
                    break;
                case (-1, 0):
                    maze[x, y] &= 0b110;
                    break;
                case (0, -1):
                    maze[x, y] &= 0b101;
                    break;
            }
            MakePath(maze, x + dx, y + dy);
        }
    }
}

static class RandomExtensions
{
    public static void Shuffle<T>(this T[] array)
    {
        var n = array.Length;
        while (n > 1)
        {
            int k = Random.Range(0, n--);
            (array[k], array[n]) = (array[n], array[k]);
        }
    }
}
