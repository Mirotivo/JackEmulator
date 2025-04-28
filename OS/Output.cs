class Output
{
    // Screen size = 512 pixels (width) x 256 pixels (height)
    // Character size = 8 pixels (width) x 11 pixels (height)
    // -----------------------
    // SCREEN (Nand2Tetris)
    // -----------------------

    // Memory mapping:
    // Each 16-bit word controls 16 horizontal pixels
    // 1 bit per pixel (1 = black, 0 = white)

    // Memory layout:
    // Screen starts at Memory address 16384
    // Number of words per screen row = 512 pixels / 16 pixels = 32 words
    // Total number of words = 32 words * 256 rows = 8192 words

    // -----------------------
    // CHARACTER MAP
    // -----------------------

    // Each character is defined as 11 rows of 8 bits
    // Each row is one 8-bit integer

    // -----------------------
    // MEMORY WORD
    // -----------------------

    // 1 memory word = 16 bits
    // Represents 16 consecutive horizontal pixels
    // Bit 15 = leftmost pixel
    // Bit 0 = rightmost pixel

    // To draw pixel at (x, y):
    // wordAddress = screenBase + (y * 32) + (x / 16)
    // bitIndex = 15 - (x % 16)

    public static int cursorY = 0;
    public static int cursorX = 0;
    public static Array<Array<int>> charMaps = new Array<Array<int>>(127);

    public static void init()
    {
        cursorY = 0;
        cursorX = 0;

        create(0,63,63,63,63,63,63,63,63,63,0,0);

        create(32, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);          //
        create(33, 12, 30, 30, 30, 12, 12, 0, 12, 12, 0, 0);  // !
        create(34, 54, 54, 20, 0, 0, 0, 0, 0, 0, 0, 0);       // "
        create(35, 0, 18, 18, 63, 18, 18, 63, 18, 18, 0, 0);  // #
        create(36, 12, 30, 51, 3, 30, 48, 51, 30, 12, 12, 0); // $
        create(37, 0, 0, 35, 51, 24, 12, 6, 51, 49, 0, 0);    // %
        create(38, 12, 30, 30, 12, 54, 27, 27, 27, 54, 0, 0); // &
        create(39, 12, 12, 6, 0, 0, 0, 0, 0, 0, 0, 0);        // '
        create(40, 24, 12, 6, 6, 6, 6, 6, 12, 24, 0, 0);      // (
        create(41, 6, 12, 24, 24, 24, 24, 24, 12, 6, 0, 0);   // )
        create(42, 0, 0, 0, 51, 30, 63, 30, 51, 0, 0, 0);     // *
        create(43, 0, 0, 0, 12, 12, 63, 12, 12, 0, 0, 0);     // +
        create(44, 0, 0, 0, 0, 0, 0, 0, 12, 12, 6, 0);        // ,
        create(45, 0, 0, 0, 0, 0, 63, 0, 0, 0, 0, 0);         // -
        create(46, 0, 0, 0, 0, 0, 0, 0, 12, 12, 0, 0);        // .    
        create(47, 0, 0, 32, 48, 24, 12, 6, 3, 1, 0, 0);      // /

        create(48, 12, 30, 51, 51, 51, 51, 51, 30, 12, 0, 0); // 0
        create(49, 12, 14, 15, 12, 12, 12, 12, 12, 63, 0, 0); // 1
        create(50, 30, 51, 48, 24, 12, 6, 3, 51, 63, 0, 0);   // 2
        create(51, 30, 51, 48, 48, 28, 48, 48, 51, 30, 0, 0); // 3
        create(52, 16, 24, 28, 26, 25, 63, 24, 24, 60, 0, 0); // 4
        create(53, 63, 3, 3, 31, 48, 48, 48, 51, 30, 0, 0);   // 5
        create(54, 28, 6, 3, 3, 31, 51, 51, 51, 30, 0, 0);    // 6
        create(55, 63, 49, 48, 48, 24, 12, 12, 12, 12, 0, 0); // 7
        create(56, 30, 51, 51, 51, 30, 51, 51, 51, 30, 0, 0); // 8
        create(57, 30, 51, 51, 51, 62, 48, 48, 24, 14, 0, 0); // 9

        create(58, 0, 0, 12, 12, 0, 0, 12, 12, 0, 0, 0);      // :
        create(59, 0, 0, 12, 12, 0, 0, 12, 12, 6, 0, 0);      // ;
        create(60, 0, 0, 24, 12, 6, 3, 6, 12, 24, 0, 0);      // <
        create(61, 0, 0, 0, 63, 0, 0, 63, 0, 0, 0, 0);        // =
        create(62, 0, 0, 3, 6, 12, 24, 12, 6, 3, 0, 0);       // >
        create(63, 30, 51, 51, 24, 12, 12, 0, 12, 12, 0, 0);  // ?
        create(64, 30, 51, 51, 59, 59, 59, 27, 3, 30, 0, 0);  // @

        create(65, 8, 28, 54, 99, 127, 127, 99, 99, 99, 0, 0);// A
        create(66, 31, 51, 51, 51, 31, 51, 51, 51, 31, 0, 0); // B
        create(67, 28, 54, 35, 3, 3, 3, 35, 54, 28, 0, 0);    // C
        create(68, 15, 27, 51, 51, 51, 51, 51, 27, 15, 0, 0); // D
        create(69, 63, 51, 35, 11, 15, 11, 35, 51, 63, 0, 0); // E
        create(70, 63, 51, 35, 11, 15, 11, 3, 3, 3, 0, 0);    // F
        create(71, 28, 54, 35, 3, 59, 51, 51, 54, 44, 0, 0);  // G
        create(72, 51, 51, 51, 51, 63, 51, 51, 51, 51, 0, 0); // H
        create(73, 30, 12, 12, 12, 12, 12, 12, 12, 30, 0, 0); // I
        create(74, 60, 24, 24, 24, 24, 24, 27, 27, 14, 0, 0); // J
        create(75, 51, 51, 51, 27, 15, 27, 51, 51, 51, 0, 0); // K
        create(76, 3, 3, 3, 3, 3, 3, 35, 51, 63, 0, 0);       // L
        create(77, 33, 51, 63, 63, 51, 51, 51, 51, 51, 0, 0); // M
        create(78, 51, 51, 55, 55, 63, 59, 59, 51, 51, 0, 0); // N
        create(79, 30, 51, 51, 51, 51, 51, 51, 51, 30, 0, 0); // O
        create(80, 31, 51, 51, 51, 31, 3, 3, 3, 3, 0, 0);     // P
        create(81, 30, 51, 51, 51, 51, 51, 63, 59, 30, 48, 0);// Q
        create(82, 31, 51, 51, 51, 31, 27, 51, 51, 51, 0, 0); // R
        create(83, 30, 51, 51, 6, 28, 48, 51, 51, 30, 0, 0);  // S
        create(84, 63, 63, 45, 12, 12, 12, 12, 12, 30, 0, 0); // T
        create(85, 51, 51, 51, 51, 51, 51, 51, 51, 30, 0, 0); // U
        create(86, 51, 51, 51, 51, 51, 30, 30, 12, 12, 0, 0); // V
        create(87, 51, 51, 51, 51, 51, 63, 63, 63, 18, 0, 0); // W
        create(88, 51, 51, 30, 30, 12, 30, 30, 51, 51, 0, 0); // X
        create(89, 51, 51, 51, 51, 30, 12, 12, 12, 30, 0, 0); // Y
        create(90, 63, 51, 49, 24, 12, 6, 35, 51, 63, 0, 0);  // Z

        create(91, 30, 6, 6, 6, 6, 6, 6, 6, 30, 0, 0);          // [
        create(92, 0, 0, 1, 3, 6, 12, 24, 48, 32, 0, 0);        // \
        create(93, 30, 24, 24, 24, 24, 24, 24, 24, 30, 0, 0);   // ]
        create(94, 8, 28, 54, 0, 0, 0, 0, 0, 0, 0, 0);          // ^
        create(95, 0, 0, 0, 0, 0, 0, 0, 0, 0, 63, 0);           // _
        create(96, 6, 12, 24, 0, 0, 0, 0, 0, 0, 0, 0);          // `

        create(97, 0, 0, 0, 14, 24, 30, 27, 27, 54, 0, 0);      // a
        create(98, 3, 3, 3, 15, 27, 51, 51, 51, 30, 0, 0);      // b
        create(99, 0, 0, 0, 30, 51, 3, 3, 51, 30, 0, 0);        // c
        create(100, 48, 48, 48, 60, 54, 51, 51, 51, 30, 0, 0);  // d
        create(101, 0, 0, 0, 30, 51, 63, 3, 51, 30, 0, 0);      // e
        create(102, 28, 54, 38, 6, 15, 6, 6, 6, 15, 0, 0);      // f
        create(103, 0, 0, 30, 51, 51, 51, 62, 48, 51, 30, 0);   // g
        create(104, 3, 3, 3, 27, 55, 51, 51, 51, 51, 0, 0);     // h
        create(105, 12, 12, 0, 14, 12, 12, 12, 12, 30, 0, 0);   // i
        create(106, 48, 48, 0, 56, 48, 48, 48, 48, 51, 30, 0);  // j
        create(107, 3, 3, 3, 51, 27, 15, 15, 27, 51, 0, 0);     // k
        create(108, 14, 12, 12, 12, 12, 12, 12, 12, 30, 0, 0);  // l
        create(109, 0, 0, 0, 29, 63, 43, 43, 43, 43, 0, 0);     // m
        create(110, 0, 0, 0, 29, 51, 51, 51, 51, 51, 0, 0);     // n
        create(111, 0, 0, 0, 30, 51, 51, 51, 51, 30, 0, 0);     // o
        create(112, 0, 0, 0, 30, 51, 51, 51, 31, 3, 3, 0);      // p
        create(113, 0, 0, 0, 30, 51, 51, 51, 62, 48, 48, 0);    // q
        create(114, 0, 0, 0, 29, 55, 51, 3, 3, 7, 0, 0);        // r
        create(115, 0, 0, 0, 30, 51, 6, 24, 51, 30, 0, 0);      // s
        create(116, 4, 6, 6, 15, 6, 6, 6, 54, 28, 0, 0);        // t
        create(117, 0, 0, 0, 27, 27, 27, 27, 27, 54, 0, 0);     // u
        create(118, 0, 0, 0, 51, 51, 51, 51, 30, 12, 0, 0);     // v
        create(119, 0, 0, 0, 51, 51, 51, 63, 63, 18, 0, 0);     // w
        create(120, 0, 0, 0, 51, 30, 12, 12, 30, 51, 0, 0);     // x
        create(121, 0, 0, 0, 51, 51, 51, 62, 48, 24, 15, 0);    // y
        create(122, 0, 0, 0, 63, 27, 12, 6, 51, 63, 0, 0);      // z

        create(123, 56, 12, 12, 12, 7, 12, 12, 12, 56, 0, 0);   // {
        create(124, 12, 12, 12, 12, 12, 12, 12, 12, 12, 0, 0);  // |
        create(125, 7, 12, 12, 12, 56, 12, 12, 12, 7, 0, 0);    // }
        create(126, 38, 45, 25, 0, 0, 0, 0, 0, 0, 0, 0);        // ~
    }

    public static void moveCursor(int i, int j)
    {
        cursorX = i;
        cursorY = j;
    }

    public static void printChar(char c)
    {
        Array<int> map;
        int baseAddr;
        int i;
        int lowBits, highBits;
        int val;

        map = getMap(c);
        baseAddr = Memory.screenBase + cursorX * 352 + (cursorY / 2);
        if ((cursorY & 1) == 0)
        {
            i = 0;
            while (i < 11)
            {
                // Peek current value from memory
                val = Memory.peek(baseAddr);
                // Mask to keep only high 8 bits
                highBits = val & 65280; // 65280 = 0xFF00
                // Get new low 8 bits
                lowBits = map[i];
                // Combine high bits with new low bits
                val = highBits | lowBits;
                // Write back to memory
                Memory.poke(baseAddr, val);
                baseAddr = baseAddr + 32;
                i = i + 1;
            }
        }
        else
        {
            i = 0;
            while (i < 11)
            {
                // Get current memory word
                val = Memory.peek(baseAddr);
                // Keep only lower 8 bits
                lowBits = val & 255; // 255 = 0x00FF
                // Shift map[i] left by 8 to go into high bits
                highBits = map[i] * 256; // left shift 8 bits = multiply by 2^8 = 256
                // Combine
                val = lowBits | highBits;
                // Write back
                Memory.poke(baseAddr, val);
                baseAddr = baseAddr + 32;
                i = i + 1;
            }
        }

        if (cursorY == 63)
        {
            if (cursorX == 22)
            {
                Screen.clearScreen();
                cursorY = 0;
                cursorX = 0;
            }
            else
            {
                cursorY = 0;
                cursorX = cursorX + 1;
            }
        }
        else
        {
            cursorY = cursorY + 1;
        }

        Screen.refresh();
    }

    public static void printString(String s)
    {
        int len, i = 0;

        len = s.length();

        while (len > i)
        {
            Output.printChar(s.charAt(i));
            i = i + 1;
        }
    }

    public static void printInt(int i)
    {
        String s = new String(6);
        s.setInt(i);
        printString(s);
    }

    public static void println()
    {
        if (cursorX == 22)
        {
            Screen.clearScreen();
            cursorY = 0;
            cursorX = 0;
        }
        else
        {
            cursorY = 0;
            cursorX = cursorX + 1;
        }
    }

    public static void backSpace()
    {
        if (cursorY > 0)
        {
            cursorY = cursorY - 1;
            printChar(' ');
            cursorY = cursorY - 1;
        }
    }

    // User Defined
    private static void create(int index, int a, int b, int c, int d, int e, int f, int g, int h, int i, int j, int k)
    {
        Array<int> charRow;

        charRow = new Array<int>(11);
        charRow[0] = a;
        charRow[1] = b;
        charRow[2] = c;
        charRow[3] = d;
        charRow[4] = e;
        charRow[5] = f;
        charRow[6] = g;
        charRow[7] = h;
        charRow[8] = i;
        charRow[9] = j;
        charRow[10] = k;

        charMaps[index] = charRow;
    }

    public static Array<int> getMap(char c)
    {
        int ascii;
        ascii = (int)c;

        if (ascii < 32 || ascii > 126)
        {
            return charMaps[0];
        }
        else
        {
            return charMaps[ascii];
        }
    }
}
