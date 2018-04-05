namespace Vectrosity
{
    using System;
    using UnityEngine;

    public class VectorChar
    {
        public const int numberOfCharacters = 0x100;
        private static Vector2[][] points;

        public static Vector2[][] data
        {
            get
            {
                if (points == null)
                {
                    points = new Vector2[0x100][];
                    points[0x21] = new Vector2[] { new Vector2(0f, -0.9f), new Vector2(0f, -1f), new Vector2(0f, 0f), new Vector2(0f, -0.75f) };
                    points[0x22] = new Vector2[] { new Vector2(0.15f, 0f), new Vector2(0.15f, -0.25f), new Vector2(0.45f, -0.25f), new Vector2(0.45f, 0f) };
                    points[0x23] = new Vector2[] { new Vector2(0.2f, 0f), new Vector2(0.2f, -1f), new Vector2(0f, -0.33f), new Vector2(0.6f, -0.33f), new Vector2(0.4f, 0f), new Vector2(0.4f, -1f), new Vector2(0f, -0.66f), new Vector2(0.6f, -0.66f) };
                    points[0x25] = new Vector2[] { 
                        new Vector2(0f, 0f), new Vector2(0f, -0.25f), new Vector2(0.15f, 0f), new Vector2(0.15f, -0.25f), new Vector2(0f, -0.25f), new Vector2(0.15f, -0.25f), new Vector2(0f, 0f), new Vector2(0.15f, 0f), new Vector2(0.6f, -0.75f), new Vector2(0.45f, -0.75f), new Vector2(0.6f, -1f), new Vector2(0.45f, -1f), new Vector2(0.45f, -1f), new Vector2(0.45f, -0.75f), new Vector2(0.6f, -1f), new Vector2(0.6f, -0.75f),
                        new Vector2(0f, -1f), new Vector2(0.6f, 0f)
                    };
                    points[0x26] = new Vector2[] { new Vector2(0.2f, -0.5f), new Vector2(0.2f, 0f), new Vector2(0f, -0.5f), new Vector2(0f, -1f), new Vector2(0.6f, -1f), new Vector2(0f, -1f), new Vector2(0.2f, -0.5f), new Vector2(0.6f, -1f), new Vector2(0.6f, -1f), new Vector2(0.6f, -0.7f), new Vector2(0.2f, 0f), new Vector2(0.5f, 0f), new Vector2(0.5f, -0.5f), new Vector2(0.5f, 0f), new Vector2(0f, -0.5f), new Vector2(0.5f, -0.5f) };
                    points[0x27] = new Vector2[] { new Vector2(0.3f, -0.25f), new Vector2(0.45f, 0f) };
                    points[40] = new Vector2[] { new Vector2(0.45f, 0f), new Vector2(0.15f, -0.25f), new Vector2(0.15f, -0.25f), new Vector2(0.15f, -0.75f), new Vector2(0.45f, -1f), new Vector2(0.15f, -0.75f) };
                    points[0x29] = new Vector2[] { new Vector2(0.15f, 0f), new Vector2(0.45f, -0.25f), new Vector2(0.45f, -0.25f), new Vector2(0.45f, -0.75f), new Vector2(0.15f, -1f), new Vector2(0.45f, -0.75f) };
                    points[0x2a] = new Vector2[] { new Vector2(0.3f, -1f), new Vector2(0.3f, 0f), new Vector2(0.6f, -0.5f), new Vector2(0f, -0.5f), new Vector2(0.5f, -0.1f), new Vector2(0.1f, -0.9f), new Vector2(0.5f, -0.9f), new Vector2(0.1f, -0.1f) };
                    points[0x2b] = new Vector2[] { new Vector2(0f, -0.5f), new Vector2(0.6f, -0.5f), new Vector2(0.3f, -0.9f), new Vector2(0.3f, -0.1f) };
                    points[0x2c] = new Vector2[] { new Vector2(0f, -1f), new Vector2(0.15f, -0.75f) };
                    points[0x2d] = new Vector2[] { new Vector2(0f, -0.5f), new Vector2(0.6f, -0.5f) };
                    points[0x2e] = new Vector2[] { new Vector2(0f, -0.9f), new Vector2(0f, -1f) };
                    points[0x2f] = new Vector2[] { new Vector2(0.6f, 0f), new Vector2(0f, -1f) };
                    points[0x30] = new Vector2[] { new Vector2(0f, -1f), new Vector2(0f, 0f), new Vector2(0.6f, -1f), new Vector2(0.6f, 0f), new Vector2(0.6f, 0f), new Vector2(0f, 0f), new Vector2(0.6f, -1f), new Vector2(0f, -1f) };
                    points[0x31] = new Vector2[] { new Vector2(0.3f, -1f), new Vector2(0.3f, 0f) };
                    points[50] = new Vector2[] { new Vector2(0.6f, 0f), new Vector2(0f, 0f), new Vector2(0.6f, 0f), new Vector2(0.6f, -0.5f), new Vector2(0f, -0.5f), new Vector2(0.6f, -0.5f), new Vector2(0f, -0.5f), new Vector2(0f, -1f), new Vector2(0.6f, -1f), new Vector2(0f, -1f) };
                    points[0x33] = new Vector2[] { new Vector2(0.6f, 0f), new Vector2(0f, 0f), new Vector2(0.6f, -1f), new Vector2(0.6f, 0f), new Vector2(0f, -1f), new Vector2(0.6f, -1f), new Vector2(0f, -0.5f), new Vector2(0.6f, -0.5f) };
                    points[0x34] = new Vector2[] { new Vector2(0f, -0.5f), new Vector2(0f, 0f), new Vector2(0.6f, -1f), new Vector2(0.6f, 0f), new Vector2(0.6f, -0.5f), new Vector2(0f, -0.5f) };
                    points[0x35] = new Vector2[] { new Vector2(0f, 0f), new Vector2(0.6f, 0f), new Vector2(0f, 0f), new Vector2(0f, -0.5f), new Vector2(0.6f, -0.5f), new Vector2(0f, -0.5f), new Vector2(0.6f, -1f), new Vector2(0.6f, -0.5f), new Vector2(0.6f, -1f), new Vector2(0f, -1f) };
                    points[0x36] = new Vector2[] { new Vector2(0f, 0f), new Vector2(0.6f, 0f), new Vector2(0.6f, -0.5f), new Vector2(0f, -0.5f), new Vector2(0.6f, -1f), new Vector2(0.6f, -0.5f), new Vector2(0.6f, -1f), new Vector2(0f, -1f), new Vector2(0f, 0f), new Vector2(0f, -1f) };
                    points[0x37] = new Vector2[] { new Vector2(0f, 0f), new Vector2(0.6f, 0f), new Vector2(0.6f, -1f), new Vector2(0.6f, 0f) };
                    points[0x38] = new Vector2[] { new Vector2(0f, 0f), new Vector2(0.6f, 0f), new Vector2(0.6f, -1f), new Vector2(0.6f, 0f), new Vector2(0f, 0f), new Vector2(0f, -1f), new Vector2(0.6f, -1f), new Vector2(0f, -1f), new Vector2(0.6f, -0.5f), new Vector2(0f, -0.5f) };
                    points[0x39] = new Vector2[] { new Vector2(0f, 0f), new Vector2(0.6f, 0f), new Vector2(0.6f, -1f), new Vector2(0.6f, 0f), new Vector2(0.6f, -1f), new Vector2(0f, -1f), new Vector2(0.6f, -0.5f), new Vector2(0f, -0.5f), new Vector2(0f, 0f), new Vector2(0f, -0.5f) };
                    points[0x3a] = new Vector2[] { new Vector2(0f, -0.9f), new Vector2(0f, -1f), new Vector2(0f, -0.3f), new Vector2(0f, -0.4f) };
                    points[0x3b] = new Vector2[] { new Vector2(0f, -1f), new Vector2(0.15f, -0.75f), new Vector2(0.1f, -0.3f), new Vector2(0.1f, -0.4f) };
                    points[60] = new Vector2[] { new Vector2(0.6f, 0f), new Vector2(0f, -0.5f), new Vector2(0.6f, -1f), new Vector2(0f, -0.5f) };
                    points[0x3d] = new Vector2[] { new Vector2(0.6f, -0.25f), new Vector2(0f, -0.25f), new Vector2(0.6f, -0.75f), new Vector2(0f, -0.75f) };
                    points[0x3e] = new Vector2[] { new Vector2(0f, 0f), new Vector2(0.6f, -0.5f), new Vector2(0f, -1f), new Vector2(0.6f, -0.5f) };
                    points[0x3f] = new Vector2[] { new Vector2(0f, -0.9f), new Vector2(0f, -1f), new Vector2(0f, -0.75f), new Vector2(0f, -0.5f), new Vector2(0f, -0.5f), new Vector2(0.3f, -0.5f), new Vector2(0.3f, 0f), new Vector2(0.3f, -0.5f), new Vector2(0f, 0f), new Vector2(0.3f, 0f) };
                    points[0x41] = new Vector2[] { new Vector2(0f, -1f), new Vector2(0f, -0.3f), new Vector2(0.6f, -0.3f), new Vector2(0.6f, -1f), new Vector2(0.3f, 0f), new Vector2(0f, -0.3f), new Vector2(0.3f, 0f), new Vector2(0.6f, -0.3f), new Vector2(0f, -0.5f), new Vector2(0.6f, -0.5f) };
                    points[0x42] = new Vector2[] { 
                        new Vector2(0f, -1f), new Vector2(0f, 0f), new Vector2(0.447f, 0f), new Vector2(0f, 0f), new Vector2(0.447f, 0f), new Vector2(0.6f, -0.155f), new Vector2(0.6f, -0.347f), new Vector2(0.6f, -0.155f), new Vector2(0.448f, -0.5f), new Vector2(0.6f, -0.347f), new Vector2(0.448f, -0.5f), new Vector2(0f, -0.5f), new Vector2(0.6f, -0.653f), new Vector2(0.448f, -0.5f), new Vector2(0.6f, -0.653f), new Vector2(0.6f, -0.845f),
                        new Vector2(0.447f, -1f), new Vector2(0.6f, -0.845f), new Vector2(0f, -1f), new Vector2(0.447f, -1f)
                    };
                    points[0x43] = new Vector2[] { new Vector2(0.6f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, -1f), new Vector2(0.6f, -1f), new Vector2(0f, -1f) };
                    points[0x44] = new Vector2[] { new Vector2(0f, 0f), new Vector2(0f, -1f), new Vector2(0.447f, 0f), new Vector2(0f, 0f), new Vector2(0.447f, 0f), new Vector2(0.6f, -0.155f), new Vector2(0.6f, -0.845f), new Vector2(0.6f, -0.155f), new Vector2(0.6f, -0.845f), new Vector2(0.447f, -1f), new Vector2(0.447f, -1f), new Vector2(0f, -1f) };
                    points[0x45] = new Vector2[] { new Vector2(0f, 0f), new Vector2(0.6f, 0f), new Vector2(0f, 0f), new Vector2(0f, -1f), new Vector2(0.6f, -1f), new Vector2(0f, -1f), new Vector2(0f, -0.5f), new Vector2(0.3f, -0.5f) };
                    points[70] = new Vector2[] { new Vector2(0f, 0f), new Vector2(0.6f, 0f), new Vector2(0f, 0f), new Vector2(0f, -1f), new Vector2(0f, -0.5f), new Vector2(0.3f, -0.5f) };
                    points[0x47] = new Vector2[] { new Vector2(0.6f, 0f), new Vector2(0f, 0f), new Vector2(0f, -1f), new Vector2(0f, 0f), new Vector2(0.6f, -1f), new Vector2(0.6f, -0.5f), new Vector2(0.6f, -1f), new Vector2(0f, -1f), new Vector2(0.3f, -0.5f), new Vector2(0.6f, -0.5f) };
                    points[0x48] = new Vector2[] { new Vector2(0f, 0f), new Vector2(0f, -1f), new Vector2(0.6f, 0f), new Vector2(0.6f, -1f), new Vector2(0f, -0.5f), new Vector2(0.6f, -0.5f) };
                    points[0x49] = new Vector2[] { new Vector2(0.6f, 0f), new Vector2(0f, 0f), new Vector2(0.6f, -1f), new Vector2(0f, -1f), new Vector2(0.3f, -1f), new Vector2(0.3f, 0f) };
                    points[0x4a] = new Vector2[] { new Vector2(0.6f, -1f), new Vector2(0f, -1f), new Vector2(0.6f, -1f), new Vector2(0.6f, 0f), new Vector2(0f, -1f), new Vector2(0f, -0.725f) };
                    points[0x4b] = new Vector2[] { new Vector2(0f, 0f), new Vector2(0f, -1f), new Vector2(0f, -0.5f), new Vector2(0.6f, 0f), new Vector2(0f, -0.5f), new Vector2(0.6f, -1f) };
                    points[0x4c] = new Vector2[] { new Vector2(0f, 0f), new Vector2(0f, -1f), new Vector2(0.6f, -1f), new Vector2(0f, -1f) };
                    points[0x4d] = new Vector2[] { new Vector2(0f, 0f), new Vector2(0f, -1f), new Vector2(0f, 0f), new Vector2(0.3f, -0.5f), new Vector2(0.6f, 0f), new Vector2(0.3f, -0.5f), new Vector2(0.6f, 0f), new Vector2(0.6f, -1f) };
                    points[0x4e] = new Vector2[] { new Vector2(0f, 0f), new Vector2(0f, -1f), new Vector2(0.6f, 0f), new Vector2(0.6f, -1f), new Vector2(0.6f, -1f), new Vector2(0f, 0f) };
                    points[0x4f] = new Vector2[] { new Vector2(0f, 0f), new Vector2(0f, -1f), new Vector2(0.6f, 0f), new Vector2(0.6f, -1f), new Vector2(0.6f, -1f), new Vector2(0f, -1f), new Vector2(0.6f, 0f), new Vector2(0f, 0f) };
                    points[80] = new Vector2[] { new Vector2(0f, 0f), new Vector2(0f, -1f), new Vector2(0.6f, 0f), new Vector2(0f, 0f), new Vector2(0f, -0.5f), new Vector2(0.6f, -0.5f), new Vector2(0.6f, 0f), new Vector2(0.6f, -0.5f) };
                    points[0x51] = new Vector2[] { new Vector2(0.6f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, -1f), new Vector2(0.6f, -1f), new Vector2(0f, -1f), new Vector2(0.6f, 0f), new Vector2(0.6f, -1f), new Vector2(0.6f, -1f), new Vector2(0.3f, -0.5f) };
                    points[0x52] = new Vector2[] { new Vector2(0f, 0f), new Vector2(0f, -1f), new Vector2(0.6f, 0f), new Vector2(0f, 0f), new Vector2(0f, -0.5f), new Vector2(0.6f, -0.5f), new Vector2(0.6f, 0f), new Vector2(0.6f, -0.5f), new Vector2(0.15f, -0.5f), new Vector2(0.6f, -1f) };
                    points[0x53] = new Vector2[] { new Vector2(0f, 0f), new Vector2(0.6f, 0f), new Vector2(0f, 0f), new Vector2(0f, -0.5f), new Vector2(0.6f, -0.5f), new Vector2(0f, -0.5f), new Vector2(0.6f, -1f), new Vector2(0.6f, -0.5f), new Vector2(0.6f, -1f), new Vector2(0f, -1f) };
                    points[0x54] = new Vector2[] { new Vector2(0.6f, 0f), new Vector2(0f, 0f), new Vector2(0.3f, -1f), new Vector2(0.3f, 0f) };
                    points[0x55] = new Vector2[] { new Vector2(0f, 0f), new Vector2(0f, -1f), new Vector2(0.6f, 0f), new Vector2(0.6f, -1f), new Vector2(0.6f, -1f), new Vector2(0f, -1f) };
                    points[0x56] = new Vector2[] { new Vector2(0.3f, -1f), new Vector2(0f, 0f), new Vector2(0.3f, -1f), new Vector2(0.6f, 0f) };
                    points[0x57] = new Vector2[] { new Vector2(0f, 0f), new Vector2(0f, -1f), new Vector2(0f, -1f), new Vector2(0.3f, -0.5f), new Vector2(0.6f, -1f), new Vector2(0.3f, -0.5f), new Vector2(0.6f, 0f), new Vector2(0.6f, -1f) };
                    points[0x58] = new Vector2[] { new Vector2(0.6f, -1f), new Vector2(0f, 0f), new Vector2(0.6f, 0f), new Vector2(0f, -1f) };
                    points[0x59] = new Vector2[] { new Vector2(0f, 0f), new Vector2(0.3f, -0.5f), new Vector2(0.6f, 0f), new Vector2(0.3f, -0.5f), new Vector2(0.3f, -1f), new Vector2(0.3f, -0.5f) };
                    points[90] = new Vector2[] { new Vector2(0.6f, 0f), new Vector2(0f, 0f), new Vector2(0.6f, 0f), new Vector2(0f, -1f), new Vector2(0.6f, -1f), new Vector2(0f, -1f) };
                    points[0x5b] = new Vector2[] { new Vector2(0.4f, 0f), new Vector2(0.1f, 0f), new Vector2(0.1f, -1f), new Vector2(0.4f, -1f), new Vector2(0.1f, -1f), new Vector2(0.1f, 0f) };
                    points[0x5c] = new Vector2[] { new Vector2(0.6f, -1f), new Vector2(0f, 0f) };
                    points[0x5d] = new Vector2[] { new Vector2(0.2f, 0f), new Vector2(0.5f, 0f), new Vector2(0.2f, -1f), new Vector2(0.5f, -1f), new Vector2(0.5f, 0f), new Vector2(0.5f, -1f) };
                    points[0x5e] = new Vector2[] { new Vector2(0f, -0.5f), new Vector2(0.3f, 0f), new Vector2(0.6f, -0.5f), new Vector2(0.3f, 0f) };
                    points[0x5f] = new Vector2[] { new Vector2(0f, -1f), new Vector2(0.8f, -1f) };
                    points[0x60] = new Vector2[] { new Vector2(0.5f, -0.3f), new Vector2(0.3f, 0f) };
                    points[0x61] = new Vector2[] { new Vector2(0.6f, -0.5f), new Vector2(0f, -0.5f), new Vector2(0f, -0.75f), new Vector2(0f, -1f), new Vector2(0.6f, -0.5f), new Vector2(0.6f, -1f), new Vector2(0f, -0.75f), new Vector2(0.6f, -0.75f), new Vector2(0.6f, -1f), new Vector2(0f, -1f) };
                    points[0x62] = new Vector2[] { new Vector2(0f, 0f), new Vector2(0f, -1f), new Vector2(0f, -0.5f), new Vector2(0.6f, -0.5f), new Vector2(0f, -1f), new Vector2(0.6f, -1f), new Vector2(0.6f, -1f), new Vector2(0.6f, -0.5f) };
                    points[0x63] = new Vector2[] { new Vector2(0.6f, -0.5f), new Vector2(0f, -0.5f), new Vector2(0f, -1f), new Vector2(0f, -0.5f), new Vector2(0.6f, -1f), new Vector2(0f, -1f) };
                    points[100] = new Vector2[] { new Vector2(0.6f, -0.5f), new Vector2(0f, -0.5f), new Vector2(0f, -1f), new Vector2(0f, -0.5f), new Vector2(0.6f, -1f), new Vector2(0f, -1f), new Vector2(0.6f, -1f), new Vector2(0.6f, 0f) };
                    points[0x65] = new Vector2[] { new Vector2(0.6f, -0.5f), new Vector2(0f, -0.5f), new Vector2(0f, -1f), new Vector2(0f, -0.5f), new Vector2(0.6f, -0.5f), new Vector2(0.6f, -0.75f), new Vector2(0f, -0.75f), new Vector2(0.6f, -0.75f), new Vector2(0.6f, -1f), new Vector2(0f, -1f) };
                    points[0x66] = new Vector2[] { new Vector2(0.15f, -1f), new Vector2(0.15f, -0.25f), new Vector2(0.45f, 0f), new Vector2(0.3f, 0f), new Vector2(0.15f, -0.25f), new Vector2(0.3f, 0f), new Vector2(0.45f, -0.5f), new Vector2(0.15f, -0.5f) };
                    points[0x67] = new Vector2[] { new Vector2(0f, -0.5f), new Vector2(0.6f, -0.5f), new Vector2(0f, -1.25f), new Vector2(0.6f, -1.25f), new Vector2(0.6f, -1.25f), new Vector2(0.6f, -0.5f), new Vector2(0f, -1f), new Vector2(0f, -0.5f), new Vector2(0f, -1f), new Vector2(0.6f, -1f) };
                    points[0x68] = new Vector2[] { new Vector2(0f, 0f), new Vector2(0f, -1f), new Vector2(0f, -0.5f), new Vector2(0.6f, -0.5f), new Vector2(0.6f, -1f), new Vector2(0.6f, -0.5f) };
                    points[0x69] = new Vector2[] { new Vector2(0.3f, -1f), new Vector2(0.3f, -0.5f), new Vector2(0.3f, -0.25f), new Vector2(0.3f, -0.15f) };
                    points[0x6a] = new Vector2[] { new Vector2(0.3f, -0.25f), new Vector2(0.3f, -0.15f), new Vector2(0.3f, -1.25f), new Vector2(0.3f, -0.5f), new Vector2(0f, -1.25f), new Vector2(0.3f, -1.25f) };
                    points[0x6b] = new Vector2[] { new Vector2(0f, -1f), new Vector2(0f, 0f), new Vector2(0f, -0.75f), new Vector2(0.3f, -0.5f), new Vector2(0f, -0.75f), new Vector2(0.6f, -1f) };
                    points[0x6c] = new Vector2[] { new Vector2(0.3f, -1f), new Vector2(0.3f, 0f) };
                    points[0x6d] = new Vector2[] { new Vector2(0.45f, -0.5f), new Vector2(0f, -0.5f), new Vector2(0.6f, -0.75f), new Vector2(0.45f, -0.5f), new Vector2(0.6f, -0.75f), new Vector2(0.6f, -1f), new Vector2(0f, -1f), new Vector2(0f, -0.5f), new Vector2(0.3f, -1f), new Vector2(0.3f, -0.5f) };
                    points[110] = new Vector2[] { new Vector2(0.45f, -0.5f), new Vector2(0f, -0.5f), new Vector2(0.6f, -0.75f), new Vector2(0.45f, -0.5f), new Vector2(0.6f, -0.75f), new Vector2(0.6f, -1f), new Vector2(0f, -1f), new Vector2(0f, -0.5f) };
                    points[0x6f] = new Vector2[] { new Vector2(0f, -1f), new Vector2(0f, -0.5f), new Vector2(0.6f, -0.5f), new Vector2(0f, -0.5f), new Vector2(0.6f, -1f), new Vector2(0f, -1f), new Vector2(0.6f, -1f), new Vector2(0.6f, -0.5f) };
                    points[0x70] = new Vector2[] { new Vector2(0.6f, -0.5f), new Vector2(0f, -0.5f), new Vector2(0.6f, -1f), new Vector2(0f, -1f), new Vector2(0.6f, -1f), new Vector2(0.6f, -0.5f), new Vector2(0f, -1.25f), new Vector2(0f, -0.5f) };
                    points[0x71] = new Vector2[] { new Vector2(0.6f, -0.5f), new Vector2(0f, -0.5f), new Vector2(0.6f, -1f), new Vector2(0f, -1f), new Vector2(0f, -1f), new Vector2(0f, -0.5f), new Vector2(0.6f, -1.25f), new Vector2(0.6f, -0.5f) };
                    points[0x72] = new Vector2[] { new Vector2(0f, -1f), new Vector2(0f, -0.5f), new Vector2(0.6f, -0.5f), new Vector2(0f, -0.5f), new Vector2(0.6f, -0.75f), new Vector2(0.6f, -0.5f) };
                    points[0x73] = new Vector2[] { new Vector2(0.6f, -0.5f), new Vector2(0f, -0.5f), new Vector2(0f, -0.75f), new Vector2(0f, -0.5f), new Vector2(0.6f, -0.75f), new Vector2(0f, -0.75f), new Vector2(0.6f, -0.75f), new Vector2(0.6f, -1f), new Vector2(0.6f, -1f), new Vector2(0f, -1f) };
                    points[0x74] = new Vector2[] { new Vector2(0.3f, -1f), new Vector2(0.3f, -0.25f), new Vector2(0.45f, -0.5f), new Vector2(0.15f, -0.5f), new Vector2(0.3f, -1f), new Vector2(0.45f, -1f) };
                    points[0x75] = new Vector2[] { new Vector2(0f, -1f), new Vector2(0f, -0.5f), new Vector2(0.6f, -1f), new Vector2(0.6f, -0.5f), new Vector2(0.6f, -1f), new Vector2(0f, -1f) };
                    points[0x76] = new Vector2[] { new Vector2(0.3f, -1f), new Vector2(0f, -0.5f), new Vector2(0.3f, -1f), new Vector2(0.6f, -0.5f) };
                    points[0x77] = new Vector2[] { new Vector2(0.15f, -1f), new Vector2(0f, -0.5f), new Vector2(0.3f, -0.75f), new Vector2(0.15f, -1f), new Vector2(0.3f, -0.75f), new Vector2(0.45f, -1f), new Vector2(0.45f, -1f), new Vector2(0.6f, -0.5f) };
                    points[120] = new Vector2[] { new Vector2(0.6f, -1f), new Vector2(0f, -0.5f), new Vector2(0f, -1f), new Vector2(0.6f, -0.5f) };
                    points[0x79] = new Vector2[] { new Vector2(0f, -1.25f), new Vector2(0.6f, -0.5f), new Vector2(0.3f, -0.875f), new Vector2(0f, -0.5f) };
                    points[0x7a] = new Vector2[] { new Vector2(0.6f, -0.5f), new Vector2(0f, -0.5f), new Vector2(0f, -1f), new Vector2(0.6f, -0.5f), new Vector2(0.6f, -1f), new Vector2(0f, -1f) };
                }
                return points;
            }
        }
    }
}

