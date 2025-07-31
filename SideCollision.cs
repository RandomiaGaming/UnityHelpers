using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCollision {

    public static Vector2 Direction(Collision2D collision)
    {
        Vector2 output = Vector2.zero;
        for (int i = 0; i < collision.contactCount; i++)
        {
            if(collision.GetContact(i).normal.x != 0)
            {
                output = new Vector2(collision.GetContact(i).normal.x, output.y);
            }
            if (collision.GetContact(i).normal.y != 0)
            {
                output = new Vector2(output.x, collision.GetContact(i).normal.y);
            }
        }
        return output;
    }
}
