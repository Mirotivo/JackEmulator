# Jack OS SDK Emulator in C#

This project implements a Software Development Kit (SDK) in **C#** to emulate the **Jack Operating System** from the **nand2tetris** curriculum.  
The goal is to recreate Jack's OS functionality in C#, providing developers with a familiar environment to understand, extend, and build Jack-based applications.

As a demonstration, a complete **Tetris game** was also written in **C#**, running on top of the Jack OS SDK Emulator.

This document highlights the **key syntax differences** between **Jack** and **C#** to help developers transition between the two languages easily.

---

# Syntax Differences: Jack vs. C#

This section summarizes the important syntactic differences between Jack and C#.  
It focuses on class structure, data types, control flow, function calling, string manipulation, and operators.

---

## 1. Class Declaration and Structure

### Jack Example

```jack
class MyClass {
    field int myField;
    static boolean classVariable;

    constructor MyClass new(int initialValue) {
        let myField = initialValue;
        return this;
    }

    method void myMethod(int arg) {
        var int localVariable;
        let localVariable = myField + arg;
        do Output.printInt(localVariable);
        return;
    }

    function void classFunction(int value) {
        let classVariable = value > 0;
        return;
    }
}
```

### C# Example

```csharp
public class MyClass
{
    private int myField;
    private static bool classVariable;

    public MyClass(int initialValue)
    {
        this.myField = initialValue;
    }

    public void MyMethod(int arg)
    {
        int localVariable = myField + arg;
        Output.printInt(localVariable);
    }

    public static void ClassFunction(int value)
    {
        classVariable = value > 0;
    }
}
```

### Key Structural Differences

- **Keywords**: Jack uses `field`, `static`, `constructor`, `method`, `function`, `var`, `let`, `do`. C# uses `private`, `public`, `static`, `void`, etc.
- **Access Modifiers**: Jack does not explicitly use access modifiers. C# uses `private`, `public`.
- **Constructor Style**: Jack constructors use `constructor` and `return this;`. C# constructors use the class name without returning.
- **Method Types**: Jack uses `method` for instance methods, `function` for static methods. C# uses the `static` keyword.
- **Variable Declaration**: Jack uses `var`. C# declares and initializes directly.
- **Assignment Syntax**: Jack uses `let`. C# uses `=`.
- **Subroutine Calls**: Jack uses `do`. C# calls methods directly.

---

## 2. Data Types

| Concept          | Jack Syntax                  | C# Syntax                   | Notes                          |
|------------------|-------------------------------|------------------------------|--------------------------------|
| Integer          | `int`                         | `int` (`System.Int32`)       | Jack int is 16-bit, C# int is 32-bit |
| Boolean          | `boolean`                     | `bool` (`System.Boolean`)    | Different keyword |
| Character        | `char`                        | `char` (`System.Char`)       | Same Unicode 16-bit |
| Array Declaration| `Array name;`                 | `type[] name;`               | Manual allocation in Jack |
| Reference Types  | Class names (e.g., `Point`)    | Class names (e.g., `Point`)  | Same idea |

### Key Type Differences

- **Integer Size**: Jack's `int` is 16-bit, C#'s `int` is 32-bit.
- **Boolean Keyword**: Jack uses `boolean`, C# uses `bool`.
- **Array Management**: Jack requires manual allocation with `Array.new(size)`.  
  C# arrays are automatically allocated and managed.

---

## 3. Control Flow

### Jack Example

```jack
if (expression) {
    // statements
}
if (expression) {
    // statements
} else {
    // statements
}

while (expression) {
    // statements
}
```

### C# Example

```csharp
if (expression)
{
    // statements
}
if (expression)
{
    // statements
}
else
{
    // statements
}

while (expression)
{
    // statements
}
```

### Key Control Flow Differences

- **Braces**: Jack always requires `{}` for code blocks.  
  C# allows omitting `{}` for single-line blocks but using them is strongly recommended.
- **Keywords**: `if`, `else`, and `while` are identical.

---

## 4. Function Calling

### Jack Example

```jack
do Output.printInt(1234);
let result = Math.multiply(6, 7);
```

### C# Example

```csharp
Output.printInt(1234);
int result = Math.Multiply(6, 7);
```

### Key Function Calling Differences

- **Use of `do`**: Jack uses `do` when calling a procedure whose return value is ignored.
- **Direct Calling**: In C#, you just call the method directly without any extra keyword.
- **Assignment**: In both Jack and C#, if the function returns a value, you assign it directly to a variable.
- **Void Functions**: Jack always requires `do` for calling `void` functions. C# does not.

---

## 5. Operators and Expressions

| Operation         | Jack Syntax | C# Syntax |
|-------------------|-------------|-----------|
| Addition           | `+`         | `+`       |
| Subtraction        | `-`         | `-`       |
| Multiplication     | `*`         | `*`       |
| Division           | `/`         | `/`       |
| Equality Check     | `=`         | `==`      |
| Inequality Check   | `~=`        | `!=`      |
| Greater Than       | `>`         | `>`       |
| Less Than          | `<`         | `<`       |
| Logical And        | `&`         | `&&`      |
| Logical Or         | `|`         | `||`      |
| Logical Not        | `~`         | `!`       |

### Key Operator Differences

- **Equality and Inequality**: Jack uses `=` for equality and `~=` for inequality.  
  C# uses `==` and `!=`.
- **Logical Operators**: Jack uses `&`, `|`, `~` for logical expressions.  
  C# uses `&&`, `||`, `!` for logical operations and also supports `&`, `|`, `^`, `~` for bitwise operations.

---

## 6. String Creation and Manipulation

In Jack, strings are built manually.  
You cannot assign a string literal directly.  
Instead, you must:

- Create a new String object with `String.new(length)`
- Append characters one by one using `appendChar(character)`

In the C# emulator, this behavior is also emulated manually to match Jack.

Example in Jack:

```jack
var String gameOverMessage;
let gameOverMessage = String.new(10);
do gameOverMessage.appendChar('G');
do gameOverMessage.appendChar('a');
do gameOverMessage.appendChar('m');
do gameOverMessage.appendChar('e');
do gameOverMessage.appendChar(' ');
do gameOverMessage.appendChar('O');
do gameOverMessage.appendChar('v');
do gameOverMessage.appendChar('e');
do gameOverMessage.appendChar('r');
do gameOverMessage.appendChar('!');
```

Equivalent example in C#:

```csharp
String gameOverMessage = new String(10)
    .appendChar('G')
    .appendChar('a')
    .appendChar('m')
    .appendChar('e')
    .appendChar(' ')
    .appendChar('O')
    .appendChar('v')
    .appendChar('e')
    .appendChar('r')
    .appendChar('!');
```

### Key Points

- Jack does not support string literals like `"Game Over!"`
- String objects must be created manually
- Characters must be appended individually
- The C# emulator uses a custom `String` class that replicates this behavior

---

# Conclusion

This document provides a clear overview of the syntactic differences between **Jack** and **C#**.  
It helps developers familiar with one language understand how to design, implement, and port applications to the other.

The **Jack OS SDK Emulator** enables writing, testing, and running Jack-based software in a modern C# environment while remaining faithful to the original nand2tetris architecture.
