# CutsceneEdit
A simple console application for moving and rotating Dark Souls 3 MQB files (cutscenes).

## Instructions

**1) Type r or m and then c or s. No space inbetween.**

  r: rotate
  m: move

  c: change - add or subtract from the current position or rotation of the cutscene
  s: set - set the position or roation of the cutscene

**2) Type x,y,z values**

  If you are changing the cutscene then enter the amount you want to change
  the cutscene on each axis.

  If you are setting the cutscene then enter the new position or rotation on each axis.

**3) (OPTIONAL) Enter a cut index**

  Enter the index of a specific cut if you only want changes to be applied to that cut.

  Note: rotation seems a little buggy and might edit other cuts too even if you only edit one.

**4) Put the path to a remobnd.dcx or mqb file.**

**EXAMPLES:**

``rc 0 0.4 0 s40_00_0070.remobnd.dcx``<br>
``rs 90 0 0 s31_00_0020.mqb``<br>
``mc 1 0 0 s35_00_0050.remobnd.dcx``<br>
``ms 410 123 56 s32_00_0010.mqb``<br>

## Special Thanks

TKGP - SoulsFormats
