using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Grid
{
    public int x {get; set;}   
    public int y {get; set;}

    public Grid(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public static bool operator == (Grid first, Grid second) {
        return first.x == second.x && first.y == second.y;
    }

    public static bool operator != (Grid first, Grid second) {
        return first.x != second.x || first.y != second.y;
    }

}
