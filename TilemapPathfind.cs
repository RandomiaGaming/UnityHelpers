using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Node
{
    public Vector3Int pos;
    public Node Parent;
    public Node(Vector3Int _pos, Node _Parent)
    {
        pos = _pos;
        Parent = _Parent;
    }
}
[RequireComponent(typeof(Tilemap))]
public class TilemapPathfind : MonoBehaviour
{
    public TileBase Passable;
    private Tilemap tm;
    private void Awake()
    {
        tm = GetComponent<Tilemap>();
    }
    public List<Node> PathFind(Vector3 _Start, Vector3 _End)
    {
        Vector3Int Start = tm.WorldToCell(_Start);
        Vector3Int End = tm.WorldToCell(_End);

        if (tm.GetTile(End) != Passable || tm.GetTile(Start) != Passable)
        {
            return null;
        }
        List<Node> visited = new List<Node>();
        List<Node> CheckNow = new List<Node>() { new Node(Start, null) };
        while (true)
        {
            List<Node> check = new List<Node>(CheckNow);
            CheckNow = new List<Node>();
            foreach (Node currentnode in check)
            {
                foreach (Node neibor in GetNeibors(visited, currentnode))
                {
                    visited.Add(neibor);
                    CheckNow.Add(neibor);
                }
            }
            foreach (Node n in visited)
            {
                if (n.pos == End)
                {
                    List<Node> output = new List<Node>();
                    Node current = n;
                    while (current.Parent != null)
                    {
                        output.Add(current);
                        current = current.Parent;
                    }
                    output.Add(new Node(Start, null));
                    output.Reverse();
                    return output;
                }
            }
            if (check.Count < 1)
            {
                return null;
            }
        }
    }
    public Vector3Int NextStep(Vector3 _Start, Vector3 _End)
    {
        Vector3Int Start = tm.WorldToCell(_Start);
        Vector3Int End = tm.WorldToCell(_End);

        if (tm.GetTile(End) != Passable || tm.GetTile(Start) != Passable || Start == End)
        {
            return Start;
        }
        List<Node> visited = new List<Node>();
        List<Node> CheckNow = new List<Node>() { new Node(Start, null) };
        while (true)
        {
            List<Node> check = new List<Node>(CheckNow);
            CheckNow = new List<Node>();
            foreach (Node currentnode in check)
            {
                foreach (Node neibor in GetNeibors(visited, currentnode))
                {
                    visited.Add(neibor);
                    CheckNow.Add(neibor);
                }
            }
            foreach (Node n in visited)
            {
                if (n.pos == End)
                {
                    List<Node> output = new List<Node>();
                    Node current = n;
                    while (current.Parent != null)
                    {
                        output.Add(current);
                        current = current.Parent;
                    }
                    output.Add(new Node(Start, null));
                    output.Reverse();
                    return output[1].pos;
                }
            }
            if (check.Count < 1)
            {
                return Start;
            }
        }
    }

    public bool Contains(Node n, Node[] ns)
    {
        foreach (Node e in ns)
        {
            if (e.pos == n.pos)
            {
                return true;
            }
        }
        return false;
    }
    public List<Node> GetNeibors(List<Node> visited, Node n)
    {
        List<Node> neibors = new List<Node>();
        Vector3Int c = new Vector3Int(0, 1, 0);
        if (Valid(n.pos + c, visited))
        {
            neibors.Add(new Node(n.pos + c, n));
        }
        c = new Vector3Int(0, -1, 0);
        if (Valid(n.pos + c, visited))
        {
            neibors.Add(new Node(n.pos + c, n));
        }
        c = new Vector3Int(1, 0, 0);
        if (Valid(n.pos + c, visited))
        {
            neibors.Add(new Node(n.pos + c, n));
        }
        c = new Vector3Int(-1, 0, 0);
        if (Valid(n.pos + c, visited))
        {
            neibors.Add(new Node(n.pos + c, n));
        }
        return neibors;
    }
    public bool Valid(Vector3Int t, List<Node> visited)
    {
        if (t.x >= tm.cellBounds.min.x && t.x <= tm.cellBounds.max.x && t.y >= tm.cellBounds.min.y && t.y <= tm.cellBounds.max.y)
        {
            if (tm.GetTile(t) == Passable)
            {
                foreach (Node v in visited)
                {
                    if (v.pos == t)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
