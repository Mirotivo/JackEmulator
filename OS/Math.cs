public class Math
{
    public static Array<int> powersOfTwo = new Array<int>(16);

    public static void init()
    {
        powersOfTwo[0] = 1;
        powersOfTwo[1] = 2;
        powersOfTwo[2] = 4;
        powersOfTwo[3] = 8;
        powersOfTwo[4] = 16;
        powersOfTwo[5] = 32;
        powersOfTwo[6] = 64;
        powersOfTwo[7] = 128;
        powersOfTwo[8] = 256;
        powersOfTwo[9] = 512;
        powersOfTwo[10] = 1024;
        powersOfTwo[11] = 2048;
        powersOfTwo[12] = 4096;
        powersOfTwo[13] = 8192;
        powersOfTwo[14] = 16384;
        powersOfTwo[15] = 32768;
    }

    public static int multiply(int x, int y)
    {
        int bitLength = 16;
        int sum = 0;
        int shiftedx = x;
        int i = 0;

        while (i < bitLength)
        {
            bool bitAnswer = Math.bit(y, i);
            if (bitAnswer)
            {
                sum = sum + shiftedx;
            }
            shiftedx = shiftedx + shiftedx;
            i = i + 1;
        }
        return sum;
    }

    public static int divide(int x, int y)
    {
        int q, answer;
        bool isNeg;

        if ((y < 0) | (x < 0))
        {
            isNeg = true;
        }
        else
        {
            isNeg = false;
        }

        if (isNeg)
        {
            x = Math.abs(x);
            y = Math.abs(y);
        }

        if (y > x)
        {
            return 0;
        }

        if ((y + y) < 0)
        {
            return 0;
        }

        q = Math.divide(x, y + y);

        if ((x - 2 * q * y) < y)
        {
            answer = q + q;
        }
        else
        {
            answer = q + q + 1;
        }

        if (isNeg)
        {
            return -answer;
        }
        else
        {
            return answer;
        }
    }

    public static int sqrt(int x)
    {
        int y = 0;
        int i, j, exp, square;

        j = 8 - 1;

        while ((j > 0) | (j == 0))
        {
            i = j;
            exp = 1;
            while (i > 0)
            {
                exp = 2 * exp;
                i = i - 1;
            }

            square = (y + exp) * (y + exp);

            if (((square < x) | (square == x)) & (square > 0))
            {
                y = y + exp;
            }

            j = j - 1;
        }
        return y;
    }

    public static int abs(int x)
    {
        if (x < 0)
        {
            return -x;
        }
        return x;
    }

    public static int min(int x, int y)
    {
        if (x < y)
        {
            return x;
        }
        return y;
    }

    public static int max(int x, int y)
    {
        if (x > y)
        {
            return x;
        }
        return y;
    }

    // User Defined
    public static bool bit(int x, int i)
    {
        int mask = powersOfTwo[i];
        bool result;

        if ((x & mask) > 0)
        {
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }
}
