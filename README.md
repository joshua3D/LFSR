# C# LFSR
 A 17-bit Linear Feedback Shift Register implemented in .Net Core 

## How To Use in SadConsole
**A 17-bit LFSR is limited to 0-511 values on one axis, and 0-255 on the other.**

**This implementation assigns 0-511 to the X-Axis, and 0-255 to the Y-Axis**

```
SadConsole.Console console = new SadConsole.Console(width, height);

LFSR lfsr = new LFSR(console.Width, console.Height);

// the lfsr always begins with the Coords value (0,0);
Coords startingCoordinate = lfsr.Value;

// we create the next value (within our width/height domain) by using Next
lfsr.Next();

// we don't create and get in the same step, to avoid confusion
Coords nextCoordinate = lfsr.Value;
```

[A demonstration of the LFSR generating 1,200 cells](https://youtu.be/77TmBRx6myM)

## Why use FizzleFade and not an RNG?

1. Every coordinate is generated only once by the algorithm
2. Every coordinate generated is stored in a struct, 'Coords'
2. Every coordinate is distributed evenly across the domain
3. The algorithm is 100% deterministic
4. No collection is used (e.g., no lists, arrays, or hashmaps)
6. Resetting the LFSR has little to no overhead

## Where did the FizzleFade come from?
[To learn more, I recommend this wonderful article on the use of FizzleFade in Wolfenstein3D](https://fabiensanglard.net/fizzlefade/index.php)
