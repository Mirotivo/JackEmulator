class Keyboard
{
    public static char keyPressed()
    {
        return (char)Memory.peek(Memory.keyboardBase);
    }

    public static char readChar()
    {
        char c = '\0';
        char temp;

        Output.printChar('_'); // Draw cursor

        while (true)
        {
            temp = (char)Keyboard.keyPressed();
            if (temp > 0)
            {
                c = temp;

                while (true)
                {
                    temp = (char)Keyboard.keyPressed();

                    if (temp == 0)
                    {
                        Output.backSpace();

                        if (c != String.backSpace() && c != String.newLine() && c != String.carriageReturn())
                        {
                            Output.printChar(c);
                        }

                        return c;
                    }
                }
            }
        }
    }

    public static String readLine(String message)
    {
        String s = new String(64);
        char c = '\0';

        Output.printString(message);
        while (true)
        {
            c = readChar();
            if (c == String.newLine() || c == String.carriageReturn())
            {
                Output.println();
                return s;
            }
            else if (c == String.backSpace())
            {
                Output.backSpace();
                s.eraseLastChar();
            }
            else
            {
                s.appendChar(c);
            }
        }
    }

    public static int readInt(String message)
    {
        String s = readLine(message);
        return s.intValue();
    }
}
