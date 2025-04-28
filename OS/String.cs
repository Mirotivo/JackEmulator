public class String
{
    public Array<char> chars;
    public int count;
    public int maxLen;

    public String(int maxLength)
    {
        chars = new Array<char>(maxLength);
        count = 0;
        maxLen = maxLength;
    }

    public void dispose()
    {
        chars.dispose();
        count = 0;
        maxLen = 0;
    }

    public int length()
    {
        return count;
    }

    public char charAt(int j)
    {
        return chars[j];
    }

    public void setCharAt(int j, char c)
    {
        chars[j] = c;
    }

    public String appendChar(char c)
    {
        if (count < maxLen)
        {
            chars[count] = c;
            count = count + 1;
        }
        return this;
    }

    public void eraseLastChar()
    {
        if (count > 0)
        {
            count = count - 1;
            chars[count] = '\0';
        }
    }

    public int intValue()
    {
        int value = 0;
        int index = 0;
        bool isNeg;

        if (count > 0 && chars[index] == '-') // ASCII 45 = '-'
        {
            isNeg = true;
            index = 1;
        }
        else
        {
            isNeg = false;
            index = 0;
        }

        while (index < count && !(chars[index] < '0') && !(chars[index] > '9')) // ASCII 48-57 = '0'-'9'
        {
            value = (value * 10) + (chars[index] - 48);
            index = index + 1;
        }

        if (isNeg)
        {
            return -value;
        }
        else
        {
            return value;
        }
    }

    public void setInt(int val)
    {
        int n, i;
        Array<char> digits = new Array<char>(10);

        count = 0;

        if (val < 0)
        {
            appendChar('-');
            val = -val;
        }

        n = 0;

        if (val == 0)
        {
            digits[0] = '0';
            n = 1;
        }
        else
        {
            while (val > 0)
            {
                digits[n] = (char)('0' + (val % 10));
                val = val / 10;
                n = n + 1;
            }
        }

        i = n - 1;
        while (i >= 0)
        {
            appendChar(digits[i]);
            i = i - 1;
        }
    }


    public static char carriageReturn()
    {
        return '\r';
    }

    public static char newLine()
    {
        return '\n';
    }

    public static char backSpace()
    {
        return '\b';
    }

    public static char doubleQuote()
    {
        return '"';
    }
}
