public class Renderer
{
    int screenXOffset = 160;
    int cellWidth = 8, cellHeight = 8;

    String scoreLabel = new String(10);
    String gameOverMessage = new String(10);

    public Playfield playfield;

    public Renderer(Playfield field)
    {
        playfield = field;

        scoreLabel.appendChar('S').appendChar('c').appendChar('o').appendChar('r').appendChar('e').appendChar(':').appendChar(' ');
        gameOverMessage.appendChar('G').appendChar('a').appendChar('m').appendChar('e').appendChar(' ')
            .appendChar('O').appendChar('v').appendChar('e').appendChar('r').appendChar('!');
    }

    public void RenderFrame()
    {
        Screen.clearScreen();
        RenderPlayfield();
        RenderBorders();
    }

    public void RenderPlayfield()
    {
        for (int i = 0; i < playfield.Rows; i++)
            for (int j = 0; j < playfield.Cols; j++)
                if (playfield.Grid[i * playfield.Cols + j] == 1)
                    RenderCell(i, j, true);
    }

    public void RenderBorders()
    {
        int leftX = screenXOffset - 1;
        int rightX = screenXOffset + playfield.Cols * cellWidth;
        int topY = 0;
        int bottomY = playfield.Rows * cellHeight;

        Screen.setColor(true);
        Screen.drawLine(leftX, topY, leftX, bottomY);
        Screen.drawLine(rightX, topY, rightX, bottomY);
        Screen.drawLine(leftX, topY, rightX, topY);
        Screen.drawLine(leftX, bottomY, rightX, bottomY);
    }

    public void RenderScore(int score)
    {
        Output.moveCursor(0, 0);
        Screen.setColor(true);
        Output.printString(scoreLabel);
        Output.printInt(score);
    }

    public void RenderGameOver()
    {
        Output.moveCursor(0, 0);
        Screen.setColor(true);
        Output.printChar(' ');

        Output.moveCursor(0, 1);
        Output.printString(gameOverMessage);
    }

    public void RenderCell(int row, int col, bool filled)
    {
        int x = screenXOffset + col * cellWidth;
        int y = row * cellHeight;
        Screen.setColor(filled);
        Screen.drawRectangle(x, y, x + cellWidth - 1, y + cellHeight - 1);
    }
}
