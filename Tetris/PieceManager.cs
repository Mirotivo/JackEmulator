public class PieceManager
{
    public Array<int> ActivePiece;
    public int ActivePieceRows;
    public int ActivePieceCols;
    public int ActivePieceX;
    public int ActivePieceY;

    private Array<PieceTemplate> templates;
    private Random random;
    private Playfield playfield;
    private Renderer renderer;

    public PieceManager(Playfield field, Renderer rendererInstance)
    {
        playfield = field;
        renderer = rendererInstance;

        templates = new Array<PieceTemplate>(7);
        random = new Random(2000);
        InitTemplates();
    }

    private void InitTemplates()
    {
        Array<int> shape0 = new Array<int>(4);
        shape0[0] = 1; shape0[1] = 1; shape0[2] = 1; shape0[3] = 1;
        templates[0] = new PieceTemplate(shape0, 1, 4); // I

        Array<int> shape1 = new Array<int>(6);
        shape1[0] = 0; shape1[1] = 1; shape1[2] = 0;
        shape1[3] = 1; shape1[4] = 1; shape1[5] = 1;
        templates[1] = new PieceTemplate(shape1, 2, 3); // T

        Array<int> shape2 = new Array<int>(6);
        shape2[0] = 0; shape2[1] = 1; shape2[2] = 1;
        shape2[3] = 1; shape2[4] = 1; shape2[5] = 0;
        templates[2] = new PieceTemplate(shape2, 2, 3); // S

        Array<int> shape3 = new Array<int>(6);
        shape3[0] = 1; shape3[1] = 1; shape3[2] = 0;
        shape3[3] = 0; shape3[4] = 1; shape3[5] = 1;
        templates[3] = new PieceTemplate(shape3, 2, 3); // Z

        Array<int> shape4 = new Array<int>(6);
        shape4[0] = 1; shape4[1] = 0; shape4[2] = 0;
        shape4[3] = 1; shape4[4] = 1; shape4[5] = 1;
        templates[4] = new PieceTemplate(shape4, 2, 3); // L

        Array<int> shape5 = new Array<int>(6);
        shape5[0] = 0; shape5[1] = 0; shape5[2] = 1;
        shape5[3] = 1; shape5[4] = 1; shape5[5] = 1;
        templates[5] = new PieceTemplate(shape5, 2, 3); // J

        Array<int> shape6 = new Array<int>(4);
        shape6[0] = 1; shape6[1] = 1;
        shape6[2] = 1; shape6[3] = 1;
        templates[6] = new PieceTemplate(shape6, 2, 2); // O
    }

    public void SpawnNewPiece()
    {
        PieceTemplate t = templates[random.NextInt(7)];
        if (ActivePiece != null)
            ActivePiece.dispose();

        ActivePiece = t.Shape;
        ActivePieceRows = t.Rows;
        ActivePieceCols = t.Cols;
        ActivePieceX = 5 - ActivePieceCols / 2;
        ActivePieceY = 0;
    }

    public void Reset()
    {
        if (ActivePiece != null)
            ActivePiece.dispose();
        ActivePiece = null;
    }

    public bool TryMovePiece(int dx, int dy)
    {
        if (!CanMove(dx, dy)) return false;

        ClearActivePiece();
        ActivePieceX += dx;
        ActivePieceY += dy;
        RenderActivePiece();
        return true;
    }

    public void TryRotatePiece()
    {
        int newRows = ActivePieceCols;
        int newCols = ActivePieceRows;
        Array<int> rotated = new Array<int>(newRows * newCols);

        for (int i = 0; i < newRows; i++)
            for (int j = 0; j < newCols; j++)
                rotated[i * newCols + j] = ActivePiece[(newCols - j - 1) * ActivePieceCols + i];

        ClearActivePiece();

        var backup = ActivePiece;
        int oldRows = ActivePieceRows;
        int oldCols = ActivePieceCols;

        ActivePiece = rotated;
        ActivePieceRows = newRows;
        ActivePieceCols = newCols;

        if (!CanMove(0, 0))
        {
            ActivePiece.dispose();
            ActivePiece = backup;
            ActivePieceRows = oldRows;
            ActivePieceCols = oldCols;
        }
        else
        {
            backup.dispose();
        }

        RenderActivePiece();
    }

    public void LockPiece()
    {
        for (int i = 0; i < ActivePieceRows; i++)
            for (int j = 0; j < ActivePieceCols; j++)
                if (ActivePiece[i * ActivePieceCols + j] == 1 && ActivePieceY + i >= 0)
                    playfield.OccupyCell(ActivePieceX + j, ActivePieceY + i);
    }

    public bool CanMove(int dx, int dy)
    {
        for (int i = 0; i < ActivePieceRows; i++)
            for (int j = 0; j < ActivePieceCols; j++)
            {
                if (ActivePiece[i * ActivePieceCols + j] == 0) continue;

                int newX = ActivePieceX + j + dx;
                int newY = ActivePieceY + i + dy;

                if (newX < 0 || newX >= playfield.Cols || newY >= playfield.Rows)
                    return false;
                if (newY >= 0 && playfield.Grid[newY * playfield.Cols + newX] == 1)
                    return false;
            }
        return true;
    }

    public void RenderActivePiece()
    {
        for (int i = 0; i < ActivePieceRows; i++)
            for (int j = 0; j < ActivePieceCols; j++)
                if (ActivePiece[i * ActivePieceCols + j] == 1)
                    renderer.RenderCell(ActivePieceY + i, ActivePieceX + j, true);
    }

    public void ClearActivePiece()
    {
        for (int i = 0; i < ActivePieceRows; i++)
            for (int j = 0; j < ActivePieceCols; j++)
                if (ActivePiece[i * ActivePieceCols + j] == 1)
                    renderer.RenderCell(ActivePieceY + i, ActivePieceX + j, false);
    }
}
