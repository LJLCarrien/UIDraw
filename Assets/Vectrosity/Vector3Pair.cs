namespace Vectrosity
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;

    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3Pair
    {
        public Vector3 p1;
        public Vector3 p2;
        public Vector3Pair(Vector3 point1, Vector3 point2)
        {
            this.p1 = point1;
            this.p2 = point2;
        }
    }
}

