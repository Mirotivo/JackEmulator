public class Playfield
{
    public int Rows { get; private set; }
    public int Cols { get; private set; }
    public Array<int> Grid;

    public Playfield(int rows, int cols)
    {
        Rows = rows;
        Cols = cols;
        Grid = new Array<int>(rows * cols);
    }

    public void Reset()
    {
        Grid.dispose();
        Grid = new Array<int>(Rows * Cols);
    }

    public bool IsCellOccupied(int x, int y)
    {
        return Grid[y * Cols + x] == 1;
    }

    public void OccupyCell(int x, int y)
    {
        Grid[y * Cols + x] = 1;
    }

    public int ClearFullLines()
    {
        int linesCleared = 0;
        for (int i = 0; i < Rows; i++)
        {
            bool full = true;
            for (int j = 0; j < Cols; j++)
            {
                if (Grid[i * Cols + j] == 0)
                {
                    full = false;
                    break;
                }
            }

            if (full)
            {
                linesCleared++;
                for (int k = i; k > 0; k--)
                    for (int j = 0; j < Cols; j++)
                        Grid[k * Cols + j] = Grid[(k - 1) * Cols + j];

                for (int j = 0; j < Cols; j++)
                    Grid[j] = 0;
            }
        }
        return linesCleared;
    }
}
