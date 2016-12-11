using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Edge {

    public Vector3 p1;
    public Vector3 p2;

    public Edge(Vector3 point1, Vector3 point2) { p1 = point1; p2 = point2; }
    public float length { get { return Vector3.Distance(p1, p2); } }
    public Vector3 center { get { return p1 + (p2 - p1).normalized * (.5f * Vector3.Distance(p1, p2)); } }

}
