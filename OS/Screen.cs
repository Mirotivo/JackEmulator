public class Screen
{
    public static bool currentColor = true;

    public static void init()
    {

    }

    public static void clearScreen()
    {
        int i;
        i = Memory.screenBase;

        while (i < Memory.keyboardBase)
        {
            Memory.poke(i, 0);
            i = i + 1;
        }
    }

    public static void setColor(bool b)
    {
        currentColor = b;
    }

    public static void drawPixel(int x, int y)
    {
        int addr;
        int bit;
        int val;
        int index;

        if (x < 0 || x >= Memory.width)
        {
            return;
        }

        if (y < 0 || y >= Memory.height)
        {
            return;
        }

        addr = Memory.screenBase + (y * 32) + (x / 16);
        val = Memory.peek(addr);
        index = x & 15;
        bit = Math.powersOfTwo[index];
        if (currentColor)
        {
            val = val | bit;
            Memory.poke(addr, val);
        }
        else
        {
            val = val & (~bit);
            Memory.poke(addr, val);
        }
        Screen.refresh();
    }

    public static void drawHorizontalLine(int x1, int x2, int y)
    {
        int x;
        int addr;
        int color;

        x = x1;

        while (x <= x2)
        {
            if ((x & 15) == 0)
            {
                if (currentColor)
                {
                    color = -1;
                }
                else
                {
                    color = 0;
                }

                while ((x + 15) <= x2)
                {
                    addr = Memory.screenBase + (y * 32) + (x / 16);
                    Memory.poke(addr, color);
                    x = x + 16;
                }
            }

            drawPixel(x, y);
            x = x + 1;
        }
    }

    public static void drawLine(int x1, int y1, int x2, int y2)
    {
        int dx;
        int dy;
        int sx;
        int sy;
        int err;
        int e2;
        int temp;

        if (y1 == y2)
        {
            if (x1 > x2)
            {
                temp = x1;
                x1 = x2;
                x2 = temp;
            }
            drawHorizontalLine(x1, x2, y1);
            return;
        }

        dx = Math.abs(x2 - x1);
        dy = Math.abs(y2 - y1);

        if (x1 < x2)
        {
            sx = 1;
        }
        else
        {
            sx = -1;
        }

        if (y1 < y2)
        {
            sy = 1;
        }
        else
        {
            sy = -1;
        }

        err = dx - dy;

        while (true)
        {
            drawPixel(x1, y1);
            if (x1 == x2 && y1 == y2)
            {
                break;
            }

            e2 = err + err;

            if (e2 > -dy)
            {
                err = err - dy;
                x1 = x1 + sx;
            }

            if (e2 < dx)
            {
                err = err + dx;
                y1 = y1 + sy;
            }
        }
    }

    public static void drawRectangle(int x1, int y1, int x2, int y2)
    {
        int y;
        y = y1;

        while (y <= y2)
        {
            drawLine(x1, y, x2, y);
            y = y + 1;
        }
    }

    public static void drawCircle(int x, int y, int r)
    {
        int dy;
        int dx;
        int rsq;

        dy = -r;
        rsq = r * r;

        while (dy <= r)
        {
            dx = (int)Math.sqrt(rsq - (dy * dy));
            drawLine(x - dx, y + dy, x + dx, y + dy);
            dy = dy + 1;
        }
    }

    // User Defined
    public static void refresh()
    {
        if (Application.OpenForms.Count > 0)
            Application.OpenForms[0]?.Invalidate(); // Redraw after character print
    }
}
