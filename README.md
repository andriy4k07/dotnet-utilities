# dotnet-utilities

A personal .NET utility library for typed console I/O, array helpers, and extensions.  
Published to NuGet and continuously extended.

![NuGet](https://img.shields.io/nuget/v/Andiy4k.Utilities)
![License](https://img.shields.io/github/license/andriy4k07/dotnet-utilities)
![.NET](https://img.shields.io/badge/.NET-10.0-purple)

---

## Installation
```bash
dotnet add package Andiy4k.Utilities
```

---

## Namespaces
```csharp
using Utilities;
```

---

## Classes

### `ConsoleEx`
Provides static methods for enhanced console input/output, including colored output and input validation with retry loops.

| Method | Description |
|---|---|
| `ReadValue<T>(message)` | Reads and parses any `IParsable<T>` type with retry |
| `ReadChar(message)` | Reads a single character with retry |
| `ReadString(message)` | Reads a non-empty string with retry |
| `ReadDateTime(message)` | Reads a date in `dd.MM.yyyy` or `dd.MM.yyyy HH:mm` format |
| `WriteColor(color, message)` | Writes colored text (no newline) |
| `WriteLineColor(color, message)` | Writes colored text with newline |
```csharp
int age    = ConsoleEx.ReadValue<int>("Enter age:");
string name = ConsoleEx.ReadString("Enter name:");
DateTime dt = ConsoleEx.ReadDateTime("Enter date:");
ConsoleEx.WriteLineColor(ConsoleColor.Green, "Success!");
```

---

### `ArrayHelper`
Provides static helper methods for creating and populating one-dimensional and two-dimensional arrays, either with random values or with data read from the console.

| Method | Description |
|---|---|
| `Generate1DArray(count, min, max, seed?)` | Generates a random `int[]` |
| `Generate2DArray(rows, cols, min, max, seed?)` | Generates a random `int[,]` |
| `Create1DIntArray(count)` | Fills `int[]` from console input |
| `Create1DStringArray(count)` | Fills `string[]` from console input |
```csharp
int[]   arr    = ArrayHelper.Generate1DArray(10, 0, 100);
int[,]  matrix = ArrayHelper.Generate2DArray(3, 3, 0, 100, seed: 42);
```

---

### `ArrayExtensions`
Provides extension methods for arrays: printing one-dimensional and two-dimensional arrays to the console, and filling an array either from console input or with random values.

| Method | Description |
|---|---|
| `Print<T>()` | Prints array with index labels |
| `PrintOneLine<T>()` | Prints array in a single line |
| `Print2DMatrix<T>()` | Prints 2D array as a grid |
| `FillFromConsole()` | Fills `int[]` from console |
| `FillRandom(min, max, seed?)` | Fills `int[]` with random values |
```csharp
int[] arr = new int[5];
arr.FillRandom(0, 100);
arr.PrintOneLine();

int[,] matrix = ArrayHelper.Generate2DArray(3, 3, 0, 9);
matrix.Print2DMatrix();
```

---

## License

[MIT](LICENSE)
```

---
