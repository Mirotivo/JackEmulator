public class Memory
{
    // --------------------------------------------
    // COMPUTER MEMORY MAP (16-bit architecture)
    // --------------------------------------------

    // 0       : SP        // Stack Pointer
    // 1       : LCL       // Base address of the local segment
    // 2       : ARG       // Base address of the argument segment
    // 3       : THIS      // Base address of the this segment
    // 4       : THAT      // Base address of the that segment

    // 5–15    : temp      // General-purpose temporary registers

    // 16–255  : static    // Static variables (also used for symbolic variables in VM)

    // 256–2047   : stack      // VM call stack (global stack segment)
    // 2048–16383 : heap       // Dynamically allocated memory (used by objects, arrays)

    // 16384–24575 : screen    // Memory-mapped screen (512 x 256 pixels)
    //                        // Each word controls 16 horizontal pixels
    //                        // Row = y * 32, column word = x / 16

    // 24576        : keyboard // Memory-mapped keyboard register
    //                        // Stores ASCII value of key pressed, or 0 if none

    // 24577–32767 : unused/reserved

    public const int stackBase = 256;
    public const int heapBase = 16384;
    public const int screenBase = 16384;
    public const int keyboardBase = 24576;
    public const int width = 512;
    public const int height = 256;
    public static Array<int> RAM = new Array<int>(32768);

    public static void init()
    {

    }

    public static int peek(int address)
    {
        return RAM[address];
    }

    public static void poke(int address, int value)
    {
        RAM[address] = value;
    }

    public static int alloc(int size)
    {
        int address = 0;
        return address;
    }

    public static void deAlloc()
    {
        // no-op
    }
}
