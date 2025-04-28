public class InputManager
{
    TetrisGame game;

    public InputManager(TetrisGame g)
    {
        game = g;
    }

    public void ProcessInput()
    {
        char key = Keyboard.keyPressed();
        if (key == 0) return;

        if (key == 130) game.pieceManager.TryMovePiece(-1, 0);
        else if (key == 132) game.pieceManager.TryMovePiece(1, 0);
        else if (key == 133) game.pieceManager.TryMovePiece(0, 1);
        else if (key == 131) game.pieceManager.TryRotatePiece();
        else if (key == 'n' || key == 'N') game.Reset();

        while (Keyboard.keyPressed() != 0)
            Sys.wait(5);
    }
}
