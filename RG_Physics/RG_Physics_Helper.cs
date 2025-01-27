using System.Collections.Generic;
using UnityEngine;
public static class RG_Physics_Helper
{
    //Change this to the number of pixels per 1 unit of distance in Unity.
    public const int Pixels_Per_Unit = 16;

    private static List<RG_Collider> Managed_Colliders = new List<RG_Collider>();
    public static void Manage_Collider(RG_Collider NewCollider)
    {
        if (Managed_Colliders == null)
        {
            Managed_Colliders = new List<RG_Collider>();
        }
        foreach (RG_Collider MRGC in Managed_Colliders)
        {
            if (MRGC == NewCollider)
            {
                return;
            }
        }
        Managed_Colliders.Add(NewCollider);
    }
    public static List<RG_Collider> Get_Managed_Colliders()
    {
        List<RG_Collider> Cleaned = new List<RG_Collider>();
        for (int i = 0; i < Managed_Colliders.Count; i++)
        {
            if (Managed_Colliders[i] == null)
            {
                Managed_Colliders.RemoveAt(i);
                i--;
            }
            else if (Managed_Colliders[i].enabled)
            {
                Cleaned.Add(Managed_Colliders[i]);
            }
        }
        return Cleaned;
    }
    public static Vector2Int World_To_Pixel(Vector2 WorldPoint)
    {
        Vector2Int Output = new Vector2Int((int)(WorldPoint.x * Pixels_Per_Unit), (int)(WorldPoint.y * Pixels_Per_Unit));
        if (WorldPoint.x < 0)
        {
            Output.x -= 1;
        }
        if (WorldPoint.y < 0)
        {
            Output.y -= 1;
        }
        return Output;
    }
    public static Vector2 Pixel_To_World(Vector2Int Point)
    {
        Vector2 Output = new Vector2(Point.x / (float)Pixels_Per_Unit, Point.y / (float)Pixels_Per_Unit);
        Output += new Vector2(0.01f / Pixels_Per_Unit, 0.01f / Pixels_Per_Unit);
        return Output;
    }
}
public struct RG_Bounds
{
    public Vector2Int Min;
    public Vector2Int Max;
    public RG_Bounds(Vector2Int Min, Vector2Int Max)
    {
        this.Min = Min;
        this.Max = Max;
    }
}
public sealed class RG_Collision
{
    public RG_Collider Other_Collider = null;
    public GameObject Other_GameObject = null;
    public RG_Side_Info Side = new RG_Side_Info();
}
public sealed class RG_Trigger_Overlap
{
    public RG_Collider Other_Collider = null;
    public GameObject Other_GameObject = null;
}
public struct RG_Side_Info
{
    public bool Top;
    public bool Bottom;
    public bool Left;
    public bool Right;
    public RG_Side_Info(bool Top, bool Bottom, bool Left, bool Right)
    {
        this.Top = Top;
        this.Bottom = Bottom;
        this.Left = Left;
        this.Right = Right;
    }
    public RG_Side_Info(Vector2Int Side_Vector)
    {
        if (Side_Vector.y > 0)
        {
            Top = true;
        }
        else
        {
            Top = false;
        }
        if (Side_Vector.y < 0)
        {
            Bottom = true;
        }
        else
        {
            Bottom = false;
        }
        if (Side_Vector.x > 0)
        {
            Right = true;
        }
        else
        {
            Right = false;
        }
        if (Side_Vector.x < 0)
        {
            Left = true;
        }
        else
        {
            Left = false;
        }
    }
}