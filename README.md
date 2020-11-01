# LFSR
 17-bit Linear Feedback Shift Register in C#

# How To Use in SadConsole
**A 17-bit LFSR is limited to 0-511 values on one axis, and 0-255 on the other**
**This implementation assigns 0-511 to the X, and 0-255 to the y**

```cs
SadConsole.Console console = new SadConsole.Console(width, height);

LFSR lfsr = new LFSR(console.Width, console.Height);

// the lfsr starts out with a value, coordinate (0,0);
(uint, uint) startingCoordinate = lfsr.Value;

// we create the next value (within our width/height domain) by using Next
lfsr.Next();

// we don't create and get in the same step, to avoid confusion
(uint, uint) nextCoordinate = lfsr.Value;
```
