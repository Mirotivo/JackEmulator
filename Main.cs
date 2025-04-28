public class Main
{
    static Array<int> playfieldGrid;
    static Array<int> activePiece;
    static int activePieceRows, activePieceCols;
    static int activePieceX, activePieceY;

    static int cellWidth = 8, cellHeight = 8;
    static int playfieldRows = 20, playfieldCols = 10;
    static int screenXOffset = 160;

    static int playerScore = 0;
    static int gravityCounter = 0;
    static int gravityThreshold = 20;

    static Random random = new Random(2000);
    static Array<PieceTemplate> pieceTemplates = new Array<PieceTemplate>(7);

    static String scoreLabel = new String(10);
    static String scoreValue = new String(6);
    static String gameOverMessage = new String(10);

    public static void main()
    {
        scoreLabel.appendChar('S').appendChar('c').appendChar('o').appendChar('r').appendChar('e').appendChar(':').appendChar(' ');
        gameOverMessage.appendChar('G').appendChar('a').appendChar('m').appendChar('e').appendChar(' ')
                       .appendChar('O').appendChar('v').appendChar('e').appendChar('r').appendChar('!');

        InitPieceTemplates();
        playfieldGrid = new Array<int>(playfieldRows * playfieldCols);
        SpawnNewPiece();
        RenderScore();
        RenderFrame();

        while (true)
        {
            ProcessPlayerInput();
            ApplyGravityPhysics();
            Sys.wait(10);
        }
    }

    static void InitPieceTemplates()
    {
        pieceTemplates[0] = new PieceTemplate(CreatePiece(new int[] {1,1,1,1}, 1, 4), 1, 4); // I
        pieceTemplates[1] = new PieceTemplate(CreatePiece(new int[] {0,1,0,1,1,1}, 2, 3), 2, 3); // T
        pieceTemplates[2] = new PieceTemplate(CreatePiece(new int[] {0,1,1,1,1,0}, 2, 3), 2, 3); // S
        pieceTemplates[3] = new PieceTemplate(CreatePiece(new int[] {1,1,0,0,1,1}, 2, 3), 2, 3); // Z
        pieceTemplates[4] = new PieceTemplate(CreatePiece(new int[] {1,0,0,1,1,1}, 2, 3), 2, 3); // L
        pieceTemplates[5] = new PieceTemplate(CreatePiece(new int[] {0,0,1,1,1,1}, 2, 3), 2, 3); // J
        pieceTemplates[6] = new PieceTemplate(CreatePiece(new int[] {1,1,1,1}, 2, 2), 2, 2);     // O
    }

    static Array<int> CreatePiece(int[] values, int rows, int cols)
    {
        Array<int> piece = new Array<int>(rows * cols);
        for (int i = 0; i < values.Length; i++)
            piece[i] = values[i];
        return piece;
    }

    static void ProcessPlayerInput()
    {
        char key = Keyboard.keyPressed();
        if (key == 0) return;

        if (key == 130) TryMovePiece(-1, 0);
        else if (key == 132) TryMovePiece(1, 0);
        else if (key == 133) TryMovePiece(0, 1);
        else if (key == 131) TryRotatePiece();
        else if (key == 'n' || key == 'N') ResetGame();

        while (Keyboard.keyPressed() != 0)
            Sys.wait(5);
    }

    static void ApplyGravityPhysics()
    {
        gravityCounter++;
        if (gravityCounter >= gravityThreshold)
        {
            if (!TryMovePiece(0, 1))
            {
                LockPieceIntoPlayfield();
                ApplyGameRules();
                SpawnNewPiece();
                RenderFrame();
            }
            gravityCounter = 0;
        }
    }

    static void ResetGame()
    {
        if (playfieldGrid != null)
            playfieldGrid.dispose();

        if (activePiece != null)
            activePiece.dispose();

        playfieldGrid = new Array<int>(playfieldRows * playfieldCols);
        Screen.clearScreen();
        playerScore = 0;
        gravityCounter = 0;
        gravityThreshold = 20;
        SpawnNewPiece();
        RenderScore();
        RenderFrame();
    }

    static void SpawnNewPiece()
    {
        if (activePiece != null)
            activePiece.dispose();

        PieceTemplate template = pieceTemplates[random.NextInt(7)];
        activePiece = template.data;
        activePieceRows = template.rows;
        activePieceCols = template.cols;

        activePieceX = playfieldCols / 2 - activePieceCols / 2;
        activePieceY = 0;

        if (!CanPieceMove(0, 0))
            RenderGameOver();
    }

    static bool TryMovePiece(int dx, int dy)
    {
        if (!CanPieceMove(dx, dy)) return false;

        ClearActivePiece();
        activePieceX += dx;
        activePieceY += dy;
        RenderActivePiece();
        return true;
    }

    static void TryRotatePiece()
    {
        int newRows = activePieceCols;
        int newCols = activePieceRows;
        Array<int> rotated = new Array<int>(newRows * newCols);

        for (int i = 0; i < newRows; i++)
            for (int j = 0; j < newCols; j++)
                rotated[i * newCols + j] = activePiece[(newCols - j - 1) * activePieceCols + i];

        ClearActivePiece();
        var backup = activePiece;
        int oldRows = activePieceRows;
        int oldCols = activePieceCols;

        activePiece = rotated;
        activePieceRows = newRows;
        activePieceCols = newCols;

        if (!CanPieceMove(0, 0))
        {
            activePiece.dispose();
            activePiece = backup;
            activePieceRows = oldRows;
            activePieceCols = oldCols;
        }
        else
        {
            backup.dispose();
        }

        RenderActivePiece();
    }

    static void LockPieceIntoPlayfield()
    {
        for (int i = 0; i < activePieceRows; i++)
            for (int j = 0; j < activePieceCols; j++)
                if (activePiece[i * activePieceCols + j] == 1 && activePieceY + i >= 0)
                    playfieldGrid[(activePieceY + i) * playfieldCols + (activePieceX + j)] = 1;
    }

    static void ApplyGameRules()
    {
        for (int i = 0; i < playfieldRows; i++)
        {
            bool full = true;
            for (int j = 0; j < playfieldCols; j++)
            {
                if (playfieldGrid[i * playfieldCols + j] == 0)
                {
                    full = false;
                    break;
                }
            }

            if (full)
            {
                playerScore += 10;

                for (int k = i; k > 0; k--)
                    for (int j = 0; j < playfieldCols; j++)
                        playfieldGrid[k * playfieldCols + j] = playfieldGrid[(k - 1) * playfieldCols + j];

                for (int j = 0; j < playfieldCols; j++)
                    playfieldGrid[j] = 0;

                RenderScore();
            }
        }
    }

    static void RenderFrame()
    {
        Screen.clearScreen();
        RenderScore();

        for (int i = 0; i < playfieldRows; i++)
            for (int j = 0; j < playfieldCols; j++)
                if (playfieldGrid[i * playfieldCols + j] == 1)
                    RenderCell(i, j, true);

        RenderPlayfieldBorders();
    }

    static void RenderActivePiece()
    {
        for (int i = 0; i < activePieceRows; i++)
            for (int j = 0; j < activePieceCols; j++)
                if (activePiece[i * activePieceCols + j] == 1)
                    RenderCell(activePieceY + i, activePieceX + j, true);
    }

    static void ClearActivePiece()
    {
        for (int i = 0; i < activePieceRows; i++)
            for (int j = 0; j < activePieceCols; j++)
                if (activePiece[i * activePieceCols + j] == 1)
                    RenderCell(activePieceY + i, activePieceX + j, false);
    }

    static void RenderCell(int row, int col, bool filled)
    {
        int x = screenXOffset + col * cellWidth;
        int y = row * cellHeight;
        Screen.setColor(filled);
        Screen.drawRectangle(x, y, x + cellWidth - 1, y + cellHeight - 1);
    }

    static void RenderPlayfieldBorders()
    {
        int leftX = screenXOffset - 1;
        int rightX = screenXOffset + playfieldCols * cellWidth;
        int topY = 0;
        int bottomY = playfieldRows * cellHeight;

        Screen.setColor(true);
        Screen.drawLine(leftX, topY, leftX, bottomY);
        Screen.drawLine(rightX, topY, rightX, bottomY);
        Screen.drawLine(leftX, topY, rightX, topY);
        Screen.drawLine(leftX, bottomY, rightX, bottomY);
    }

    static void RenderScore()
    {
        Output.moveCursor(0, 0);
        Screen.setColor(true);
        Output.printString(scoreLabel);

        scoreValue.setInt(playerScore);
        Output.printString(scoreValue);
    }

    static void RenderGameOver()
    {
        Output.moveCursor(0, 0);
        Output.printChar(' ');
        Output.moveCursor(0, 1);
        Output.printString(gameOverMessage);
        Sys.halt();
    }

    static bool CanPieceMove(int dx, int dy)
    {
        for (int i = 0; i < activePieceRows; i++)
            for (int j = 0; j < activePieceCols; j++)
            {
                if (activePiece[i * activePieceCols + j] == 0) continue;

                int newX = activePieceX + j + dx;
                int newY = activePieceY + i + dy;

                if (newX < 0 || newX >= playfieldCols || newY < 0 || newY >= playfieldRows)
                    return false;
                if (newY >= 0 && playfieldGrid[newY * playfieldCols + newX] == 1)
                    return false;
            }
        return true;
    }
}
