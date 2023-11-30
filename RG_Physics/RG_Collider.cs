using System.Collections.Generic;
using UnityEngine;
public class RG_Collider : MonoBehaviour
{
    private List<RG_Collision> Collisions = new List<RG_Collision>();
    private List<RG_Trigger_Overlap> Trigger_Overlaps = new List<RG_Trigger_Overlap>();
    protected List<RG_Bounds> Collider_Shape = new List<RG_Bounds>();

    public Vector2Int Offset = Vector2Int.zero;
    public bool Is_Trigger = false;
    [Header("Directional Collision Mask")]
    public bool Top = true;
    public bool Bottom = true;
    public bool Right = true;
    public bool Left = true;
    private void Awake()
    {
        RG_Physics_Helper.Manage_Collider(this);
    }
    private void OnDisable()
    {
        Collisions = new List<RG_Collision>();
        Trigger_Overlaps = new List<RG_Trigger_Overlap>();
    }
    private void OnEnable()
    {
        Collisions = new List<RG_Collision>();
        Trigger_Overlaps = new List<RG_Trigger_Overlap>();
    }
    private void LateUpdate()
    {
        Collisions = new List<RG_Collision>();
        Trigger_Overlaps = new List<RG_Trigger_Overlap>();
    }
    private void Update()
    {
        transform.position = RG_Physics_Helper.Pixel_To_World(RG_Physics_Helper.World_To_Pixel(transform.position));
    }
    public virtual void Log_Collision(RG_Collider OtherRGC, RG_Side_Info SideInfo)
    {
        RG_Collision New_Collision = new RG_Collision();
        New_Collision.Other_GameObject = OtherRGC.gameObject;
        New_Collision.Other_Collider = OtherRGC;
        New_Collision.Side = SideInfo;
        for (int i = 0; i < Collisions.Count; i++)
        {
            if (Collisions[i].Other_Collider == OtherRGC)
            {
                if (Collisions[i].Side.Bottom)
                {
                    New_Collision.Side.Bottom = true;
                }
                if (Collisions[i].Side.Top)
                {
                    New_Collision.Side.Top = true;
                }
                if (Collisions[i].Side.Right)
                {
                    New_Collision.Side.Right = true;
                }
                if (Collisions[i].Side.Left)
                {
                    New_Collision.Side.Left = true;
                }
                Collisions.RemoveAt(i);
                i--;
            }
        }
        Collisions.Add(New_Collision);
    }
    public virtual void Log_Trigger_Overlap(RG_Collider OtherRGC)
    {
        RG_Trigger_Overlap New_Trigger_Overlap = new RG_Trigger_Overlap();
        New_Trigger_Overlap.Other_GameObject = OtherRGC.gameObject;
        New_Trigger_Overlap.Other_Collider = OtherRGC;
        for (int i = 0; i < Collisions.Count; i++)
        {
            if (Collisions[i].Other_Collider == OtherRGC)
            {
                Collisions.RemoveAt(i);
            }
        }
        Trigger_Overlaps.Add(New_Trigger_Overlap);
    }
    public virtual List<RG_Bounds> Get_Collider_Shape_Local()
    {
        List<RG_Bounds> Output = new List<RG_Bounds>();
        foreach (RG_Bounds This_Bounds in Collider_Shape)
        {
            Output.Add(new RG_Bounds(This_Bounds.Min + Offset, This_Bounds.Max + Offset));
        }
        return Output;
    }
    public virtual List<RG_Bounds> Get_Collider_Shape_World()
    {
        List<RG_Bounds> Output = new List<RG_Bounds>();
        foreach (RG_Bounds This_Bounds in Collider_Shape)
        {
            Output.Add(new RG_Bounds(Delocalize(This_Bounds.Min + Offset), Delocalize(This_Bounds.Max + Offset)));
        }
        return Output;
    }
    public virtual List<RG_Collision> Get_Collisions()
    {
        return new List<RG_Collision>(Collisions);
    }
    public virtual List<RG_Trigger_Overlap> Get_Trigger_Overlaps()
    {
        return new List<RG_Trigger_Overlap>(Trigger_Overlaps);
    }
    public virtual Vector2Int Localize(Vector2Int PixelPoint)
    {
        return PixelPoint - RG_Physics_Helper.World_To_Pixel(transform.position);
    }
    public virtual Vector2Int Delocalize(Vector2Int LocalPoint)
    {
        return LocalPoint + RG_Physics_Helper.World_To_Pixel(transform.position);
    }
    protected virtual void OnDrawGizmos()
    {
        if (isActiveAndEnabled)
        {
            if (Is_Trigger)
            {
                Gizmos.color = new Color(0, 1, 1, 0.5f);
            }
            else
            {
                Gizmos.color = new Color(0, 1, 0, 0.5f);
            }
            foreach (RG_Bounds CSB in Get_Collider_Shape_Local())
            {
                Vector2 Size = new Vector2((CSB.Max.x - CSB.Min.x + 1) / (float)RG_Physics_Helper.Pixels_Per_Unit, (CSB.Max.y - CSB.Min.y + 1) / (float)RG_Physics_Helper.Pixels_Per_Unit);
                Vector2 Center = (RG_Physics_Helper.Pixel_To_World(Delocalize(CSB.Min)) + RG_Physics_Helper.Pixel_To_World(Delocalize(CSB.Max))) / 2f;
                Center += new Vector2(0.5f / RG_Physics_Helper.Pixels_Per_Unit, 0.5f / RG_Physics_Helper.Pixels_Per_Unit);
                Gizmos.DrawCube(Center, Size);
            }
        }
    }
}