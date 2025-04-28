public class TetrisGame
{
    public Playfield playfield;
    public PieceManager pieceManager;
    public Renderer renderer;
    public InputManager inputManager;
    int gravityCounter;
    int gravityThreshold;
    int playerScore;

    public TetrisGame()
    {
        playfield = new Playfield(20, 10);
        renderer = new Renderer(playfield);
        pieceManager = new PieceManager(playfield, renderer);
        inputManager = new InputManager(this);
        gravityCounter = 0;
        gravityThreshold = 20;
        playerScore = 0;
    }

    public void run()
    {
        renderer.RenderFrame();
        renderer.RenderScore(playerScore);
        pieceManager.SpawnNewPiece();

        while (true)
        {
            inputManager.ProcessInput();
            ApplyGravity();
            Sys.wait(10);
        }
    }

    public void ApplyGravity()
    {
        gravityCounter++;
        if (gravityCounter >= gravityThreshold)
        {
            if (!pieceManager.TryMovePiece(0, 1))
            {
                pieceManager.LockPiece();
                int cleared = playfield.ClearFullLines();
                playerScore += cleared * 10;
                pieceManager.SpawnNewPiece();
                renderer.RenderFrame();
                renderer.RenderScore(playerScore);

                if (!pieceManager.CanMove(0, 0))
                {
                    renderer.RenderGameOver();
                    Sys.halt();
                }
            }
            gravityCounter = 0;
        }
    }

    public void Reset()
    {
        playfield.Reset();
        pieceManager.Reset();
        Screen.clearScreen();
        playerScore = 0;
        gravityCounter = 0;
        gravityThreshold = 20;
        pieceManager.SpawnNewPiece();
        renderer.RenderFrame();
        renderer.RenderScore(playerScore);
    }
}
