namespace JackEmulator;

public partial class JackEmulatorForm : Form
{
    public static int scale = 4;

    public JackEmulatorForm()
    {
        this.Text = "512x256 Pixel Editor";
        this.DoubleBuffered = true;
        this.ClientSize = new Size(Memory.width * scale, Memory.height * scale);
        this.KeyDown += (s, e) =>
        {
            // Skip modifier-only keys
            if (e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.Capital ||
                e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey)
            {
                return;
            }

            bool shift = e.Shift ^ Console.CapsLock;
            Keys key = e.KeyCode;

            Memory.RAM[Memory.keyboardBase] = MapKeyToAscii(key, shift);
        };
        this.KeyUp += (s, e) => Memory.RAM[Memory.keyboardBase] = 0;
        this.MouseClick += OnMouseClick;
        this.Paint += OnPaint;
        this.Shown += OnShownAsync;
    }

    private static int MapKeyToAscii(Keys key, bool shift)
    {
        if (key >= Keys.A && key <= Keys.Z)
        {
            return shift ? (int)key : (int)(key + 32); // A-Z or a-z
        }

        if (key >= Keys.D0 && key <= Keys.D9)
        {
            // 0-9 or symbols )!@#$%^&*(
            return shift ? ")!@#$%^&*("[key - Keys.D0] : (int)(key);
        }

        // Special keys mapping for Nand2Tetris
        if (key == Keys.Left) return 130;
        if (key == Keys.Up) return 131;
        if (key == Keys.Right) return 132;
        if (key == Keys.Down) return 133;

        return (int)key; // fallback
    }

    private void OnMouseClick(object? sender, MouseEventArgs e)
    {
        int x = e.X / scale;
        int y = e.Y / scale;

        if (x >= 0 && x < Memory.width && y >= 0 && y < Memory.height)
        {
            int wordIndex = Memory.screenBase + (y * 32) + (x / 16);
            int bit = x % 16;
            int mask = 1 << bit;

            // toggle the bit
            if ((Memory.RAM[wordIndex] & mask) != 0)
            {
                Memory.RAM[wordIndex] &= ~mask; // turn off
            }
            else
            {
                Memory.RAM[wordIndex] |= mask;  // turn on
            }

            Invalidate(new Rectangle(x * scale, y * scale, scale, scale));
        }
    }

    private void OnPaint(object? sender, PaintEventArgs e)
    {
        var g = e.Graphics;
        draw(g);
    }

    private async void OnShownAsync(object? sender, EventArgs e)
    {
        using (Graphics g = this.CreateGraphics())
        {
            draw(g);
        }

        await Task.Run(() => {
            Sys.init();
        });
    }

    private void draw(Graphics g)
    {
        for (int y = 0; y < Memory.height; y++)
        {
            for (int x = 0; x < Memory.width; x++)
            {
                int wordIndex = Memory.screenBase + (y * 32) + (x / 16);
                int bit = x % 16;
                bool isPixelOn = (Memory.RAM[wordIndex] & (1 << bit)) != 0;

                Brush brush = isPixelOn ? Brushes.Black : Brushes.White;
                g.FillRectangle(brush, x * scale, y * scale, scale, scale);
            }
        }
    }
}
