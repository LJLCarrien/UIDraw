namespace Vectrosity
{
    using System;
    using UnityEngine;

    public class VectorPoints : VectorLine
    {
        public VectorPoints(string name, Vector2[] points, Material material, float width) : base(true, name, points, material, width)
        {
        }

        public VectorPoints(string name, Vector3[] points, Material material, float width) : base(true, name, points, material, width)
        {
        }

        public VectorPoints(string name, Vector2[] points, Color[] colors, Material material, float width) : base(true, name, points, colors, material, width)
        {
        }

        public VectorPoints(string name, Vector2[] points, Color color, Material material, float width) : base(true, name, points, color, material, width)
        {
        }

        public VectorPoints(string name, Vector3[] points, Color[] colors, Material material, float width) : base(true, name, points, colors, material, width)
        {
        }

        public VectorPoints(string name, Vector3[] points, Color color, Material material, float width) : base(true, name, points, color, material, width)
        {
        }
    }
}

