using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings
{
    public const int BLOCK_SIZE = 55;
    public const int BLOCK_GAP = 10;
    public const int BLOCK_CELL_SIZE = BLOCK_SIZE + BLOCK_GAP;
    public const int LINE_WIDTH = 2;
    public const int LINE_GAP = 8;
    public const int MAX_BRIDGE_LINES = 2;

    public static int[] LEVEL_COUNT = new int[]{
        3,3,3,2,2,
        2,2,3,3,2,
        2,3,2,2,4,
        3,3,3,4

    };

    public static int LEVELS = LEVEL_COUNT.Length;
}
