namespace Vectrosity
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using Object = UnityEngine.Object;

    [Serializable]
    public class VectorLine
    {
        private static LineManager _lineManager;
        private static int _vectorLayer = 0x1f;
        private static int _vectorLayer3D = 0;
        private static byte[] byteBlock;
        private static Camera cam;
        private static Camera cam3D;
        private static Transform camTransform;
        private static Dictionary<string, CapInfo> capDictionary;
        public float capLength;
        private const float cutoff = 0.15f;
        private static float defaultCapLength;
        private static Joins defaultJoins;
        private static Color defaultLineColor;
        private static int defaultLineDepth;
        private static Material defaultLineMaterial;
        private static LineType defaultLineType;
        private static float defaultLineWidth;
        private static Material defaultMaterial;
        private static bool defaultsSet = false;
        private static int endianDiff1;
        private static int endianDiff2;
        private static bool error = false;
        private static string[] functionNames = new string[] { "VectorLine.SetColors: Length of color", "VectorLine.SetWidths: Length of line widths", "MakeCurve", "MakeSpline", "MakeEllipse" };
        private static bool lineManagerCreated = false;
        private bool m_1pixelLine;
        private bool m_active;
        private EndCap m_capType;
        private bool m_collider;
        private bool m_continuous;
        private bool m_continuousTexture;
        private int m_depth;
        private float[] m_distances;
        private int m_drawEnd;
        private int m_drawStart;
        private string m_endCap;
        private bool m_is2D;
        private bool m_isAutoDrawing;
        private bool m_isPoints;
        private Joins m_joins;
        private int m_layer;
        private Color32[] m_lineColors;
        private Vector2[] m_lineUVs;
        private Vector3[] m_lineVertices;
        private float[] m_lineWidths;
        private Material m_material;
        private Matrix4x4 m_matrix;
        private int m_maxDrawIndex;
        private float m_maxWeldDistance;
        private Mesh m_mesh;
        private MeshFilter m_meshFilter;
        private static bool m_meshRenderMethodSet = false;
        private int m_minDrawIndex;
        private string m_name;
        private bool m_normalsCalculated;
        private PhysicsMaterial2D m_physicsMaterial;
        private int m_pointsLength;
        private static int m_screenHeight = 0;
        private Vector3[] m_screenPoints;
        private static int m_screenWidth = 0;
        private bool m_smoothColor;
        private bool m_smoothWidth;
        private bool m_tangentsCalculated;
        private float m_textureOffset;
        private float m_textureScale;
        private int m_triangleCount;
        private bool m_trigger;
        private bool m_useMatrix;
        private static bool m_useMeshLines = false;
        private static bool m_useMeshPoints = false;
        private bool m_useNormals;
        private bool m_useTangents;
        private bool m_useTextureScale;
        private Transform m_useTransform;
        private GameObject m_vectorObject;
        private int m_vertexCount;
        private bool m_viewportDraw;
        private static Vector3 oldPosition;
        private static Vector3 oldRotation;
        public Vector2[] points2;
        public Vector3[] points3;
        private static bool useOrthoCam;
        private static float zDist;

        public VectorLine(string lineName, Vector2[] linePoints, Material lineMaterial, float width)
        {
            this.m_active = true;
            this.capLength = 0f;
            this.m_depth = 0;
            this.m_smoothWidth = false;
            this.m_smoothColor = false;
            this.m_layer = -1;
            this.m_isAutoDrawing = false;
            this.m_minDrawIndex = 0;
            this.m_maxDrawIndex = 0;
            this.m_drawStart = 0;
            this.m_drawEnd = 0;
            this.m_useNormals = false;
            this.m_useTangents = false;
            this.m_normalsCalculated = false;
            this.m_tangentsCalculated = false;
            this.m_capType = EndCap.None;
            this.m_continuousTexture = false;
            this.m_1pixelLine = false;
            this.m_useTextureScale = false;
            this.m_useMatrix = false;
            this.m_collider = false;
            this.m_trigger = false;
            this.points2 = linePoints;
            Color[] colors = this.SetColor(Color.white, LineType.Discrete, linePoints.Length, false);
            this.SetupMesh(ref lineName, lineMaterial, colors, ref width, LineType.Discrete, Joins.None, true, false);
        }

        public VectorLine(string lineName, Vector3[] linePoints, Material lineMaterial, float width)
        {
            this.m_active = true;
            this.capLength = 0f;
            this.m_depth = 0;
            this.m_smoothWidth = false;
            this.m_smoothColor = false;
            this.m_layer = -1;
            this.m_isAutoDrawing = false;
            this.m_minDrawIndex = 0;
            this.m_maxDrawIndex = 0;
            this.m_drawStart = 0;
            this.m_drawEnd = 0;
            this.m_useNormals = false;
            this.m_useTangents = false;
            this.m_normalsCalculated = false;
            this.m_tangentsCalculated = false;
            this.m_capType = EndCap.None;
            this.m_continuousTexture = false;
            this.m_1pixelLine = false;
            this.m_useTextureScale = false;
            this.m_useMatrix = false;
            this.m_collider = false;
            this.m_trigger = false;
            this.points3 = linePoints;
            Color[] colors = this.SetColor(Color.white, LineType.Discrete, linePoints.Length, false);
            this.SetupMesh(ref lineName, lineMaterial, colors, ref width, LineType.Discrete, Joins.None, false, false);
        }

        protected VectorLine(bool usePoints, string lineName, Vector2[] linePoints, Material lineMaterial, float width)
        {
            this.m_active = true;
            this.capLength = 0f;
            this.m_depth = 0;
            this.m_smoothWidth = false;
            this.m_smoothColor = false;
            this.m_layer = -1;
            this.m_isAutoDrawing = false;
            this.m_minDrawIndex = 0;
            this.m_maxDrawIndex = 0;
            this.m_drawStart = 0;
            this.m_drawEnd = 0;
            this.m_useNormals = false;
            this.m_useTangents = false;
            this.m_normalsCalculated = false;
            this.m_tangentsCalculated = false;
            this.m_capType = EndCap.None;
            this.m_continuousTexture = false;
            this.m_1pixelLine = false;
            this.m_useTextureScale = false;
            this.m_useMatrix = false;
            this.m_collider = false;
            this.m_trigger = false;
            this.points2 = linePoints;
            Color[] colors = this.SetColor(Color.white, LineType.Continuous, linePoints.Length, true);
            this.SetupMesh(ref lineName, lineMaterial, colors, ref width, LineType.Continuous, Joins.None, true, true);
        }

        protected VectorLine(bool usePoints, string lineName, Vector3[] linePoints, Material lineMaterial, float width)
        {
            this.m_active = true;
            this.capLength = 0f;
            this.m_depth = 0;
            this.m_smoothWidth = false;
            this.m_smoothColor = false;
            this.m_layer = -1;
            this.m_isAutoDrawing = false;
            this.m_minDrawIndex = 0;
            this.m_maxDrawIndex = 0;
            this.m_drawStart = 0;
            this.m_drawEnd = 0;
            this.m_useNormals = false;
            this.m_useTangents = false;
            this.m_normalsCalculated = false;
            this.m_tangentsCalculated = false;
            this.m_capType = EndCap.None;
            this.m_continuousTexture = false;
            this.m_1pixelLine = false;
            this.m_useTextureScale = false;
            this.m_useMatrix = false;
            this.m_collider = false;
            this.m_trigger = false;
            this.points3 = linePoints;
            Color[] colors = this.SetColor(Color.white, LineType.Continuous, linePoints.Length, true);
            this.SetupMesh(ref lineName, lineMaterial, colors, ref width, LineType.Continuous, Joins.None, false, true);
        }

        public VectorLine(string lineName, Vector2[] linePoints, Color color, Material lineMaterial, float width)
        {
            this.m_active = true;
            this.capLength = 0f;
            this.m_depth = 0;
            this.m_smoothWidth = false;
            this.m_smoothColor = false;
            this.m_layer = -1;
            this.m_isAutoDrawing = false;
            this.m_minDrawIndex = 0;
            this.m_maxDrawIndex = 0;
            this.m_drawStart = 0;
            this.m_drawEnd = 0;
            this.m_useNormals = false;
            this.m_useTangents = false;
            this.m_normalsCalculated = false;
            this.m_tangentsCalculated = false;
            this.m_capType = EndCap.None;
            this.m_continuousTexture = false;
            this.m_1pixelLine = false;
            this.m_useTextureScale = false;
            this.m_useMatrix = false;
            this.m_collider = false;
            this.m_trigger = false;
            this.points2 = linePoints;
            Color[] colors = this.SetColor(color, LineType.Discrete, linePoints.Length, false);
            this.SetupMesh(ref lineName, lineMaterial, colors, ref width, LineType.Discrete, Joins.None, true, false);
        }

        public VectorLine(string lineName, Vector2[] linePoints, Material lineMaterial, float width, LineType lineType)
        {
            this.m_active = true;
            this.capLength = 0f;
            this.m_depth = 0;
            this.m_smoothWidth = false;
            this.m_smoothColor = false;
            this.m_layer = -1;
            this.m_isAutoDrawing = false;
            this.m_minDrawIndex = 0;
            this.m_maxDrawIndex = 0;
            this.m_drawStart = 0;
            this.m_drawEnd = 0;
            this.m_useNormals = false;
            this.m_useTangents = false;
            this.m_normalsCalculated = false;
            this.m_tangentsCalculated = false;
            this.m_capType = EndCap.None;
            this.m_continuousTexture = false;
            this.m_1pixelLine = false;
            this.m_useTextureScale = false;
            this.m_useMatrix = false;
            this.m_collider = false;
            this.m_trigger = false;
            this.points2 = linePoints;
            Color[] colors = this.SetColor(Color.white, lineType, linePoints.Length, false);
            this.SetupMesh(ref lineName, lineMaterial, colors, ref width, lineType, Joins.None, true, false);
        }

        public VectorLine(string lineName, Vector2[] linePoints, Color[] colors, Material lineMaterial, float width)
        {
            this.m_active = true;
            this.capLength = 0f;
            this.m_depth = 0;
            this.m_smoothWidth = false;
            this.m_smoothColor = false;
            this.m_layer = -1;
            this.m_isAutoDrawing = false;
            this.m_minDrawIndex = 0;
            this.m_maxDrawIndex = 0;
            this.m_drawStart = 0;
            this.m_drawEnd = 0;
            this.m_useNormals = false;
            this.m_useTangents = false;
            this.m_normalsCalculated = false;
            this.m_tangentsCalculated = false;
            this.m_capType = EndCap.None;
            this.m_continuousTexture = false;
            this.m_1pixelLine = false;
            this.m_useTextureScale = false;
            this.m_useMatrix = false;
            this.m_collider = false;
            this.m_trigger = false;
            this.points2 = linePoints;
            this.SetupMesh(ref lineName, lineMaterial, colors, ref width, LineType.Discrete, Joins.None, true, false);
        }

        public VectorLine(string lineName, Vector3[] linePoints, Color color, Material lineMaterial, float width)
        {
            this.m_active = true;
            this.capLength = 0f;
            this.m_depth = 0;
            this.m_smoothWidth = false;
            this.m_smoothColor = false;
            this.m_layer = -1;
            this.m_isAutoDrawing = false;
            this.m_minDrawIndex = 0;
            this.m_maxDrawIndex = 0;
            this.m_drawStart = 0;
            this.m_drawEnd = 0;
            this.m_useNormals = false;
            this.m_useTangents = false;
            this.m_normalsCalculated = false;
            this.m_tangentsCalculated = false;
            this.m_capType = EndCap.None;
            this.m_continuousTexture = false;
            this.m_1pixelLine = false;
            this.m_useTextureScale = false;
            this.m_useMatrix = false;
            this.m_collider = false;
            this.m_trigger = false;
            this.points3 = linePoints;
            Color[] colors = this.SetColor(color, LineType.Discrete, linePoints.Length, false);
            this.SetupMesh(ref lineName, lineMaterial, colors, ref width, LineType.Discrete, Joins.None, false, false);
        }

        public VectorLine(string lineName, Vector3[] linePoints, Material lineMaterial, float width, LineType lineType)
        {
            this.m_active = true;
            this.capLength = 0f;
            this.m_depth = 0;
            this.m_smoothWidth = false;
            this.m_smoothColor = false;
            this.m_layer = -1;
            this.m_isAutoDrawing = false;
            this.m_minDrawIndex = 0;
            this.m_maxDrawIndex = 0;
            this.m_drawStart = 0;
            this.m_drawEnd = 0;
            this.m_useNormals = false;
            this.m_useTangents = false;
            this.m_normalsCalculated = false;
            this.m_tangentsCalculated = false;
            this.m_capType = EndCap.None;
            this.m_continuousTexture = false;
            this.m_1pixelLine = false;
            this.m_useTextureScale = false;
            this.m_useMatrix = false;
            this.m_collider = false;
            this.m_trigger = false;
            this.points3 = linePoints;
            Color[] colors = this.SetColor(Color.white, lineType, linePoints.Length, false);
            this.SetupMesh(ref lineName, lineMaterial, colors, ref width, lineType, Joins.None, false, false);
        }

        public VectorLine(string lineName, Vector3[] linePoints, Color[] colors, Material lineMaterial, float width)
        {
            this.m_active = true;
            this.capLength = 0f;
            this.m_depth = 0;
            this.m_smoothWidth = false;
            this.m_smoothColor = false;
            this.m_layer = -1;
            this.m_isAutoDrawing = false;
            this.m_minDrawIndex = 0;
            this.m_maxDrawIndex = 0;
            this.m_drawStart = 0;
            this.m_drawEnd = 0;
            this.m_useNormals = false;
            this.m_useTangents = false;
            this.m_normalsCalculated = false;
            this.m_tangentsCalculated = false;
            this.m_capType = EndCap.None;
            this.m_continuousTexture = false;
            this.m_1pixelLine = false;
            this.m_useTextureScale = false;
            this.m_useMatrix = false;
            this.m_collider = false;
            this.m_trigger = false;
            this.points3 = linePoints;
            this.SetupMesh(ref lineName, lineMaterial, colors, ref width, LineType.Discrete, Joins.None, false, false);
        }

        protected VectorLine(bool usePoints, string lineName, Vector2[] linePoints, Color color, Material lineMaterial, float width)
        {
            this.m_active = true;
            this.capLength = 0f;
            this.m_depth = 0;
            this.m_smoothWidth = false;
            this.m_smoothColor = false;
            this.m_layer = -1;
            this.m_isAutoDrawing = false;
            this.m_minDrawIndex = 0;
            this.m_maxDrawIndex = 0;
            this.m_drawStart = 0;
            this.m_drawEnd = 0;
            this.m_useNormals = false;
            this.m_useTangents = false;
            this.m_normalsCalculated = false;
            this.m_tangentsCalculated = false;
            this.m_capType = EndCap.None;
            this.m_continuousTexture = false;
            this.m_1pixelLine = false;
            this.m_useTextureScale = false;
            this.m_useMatrix = false;
            this.m_collider = false;
            this.m_trigger = false;
            this.points2 = linePoints;
            Color[] colors = this.SetColor(color, LineType.Continuous, linePoints.Length, true);
            this.SetupMesh(ref lineName, lineMaterial, colors, ref width, LineType.Continuous, Joins.None, true, true);
        }

        protected VectorLine(bool usePoints, string lineName, Vector2[] linePoints, Color[] colors, Material lineMaterial, float width)
        {
            this.m_active = true;
            this.capLength = 0f;
            this.m_depth = 0;
            this.m_smoothWidth = false;
            this.m_smoothColor = false;
            this.m_layer = -1;
            this.m_isAutoDrawing = false;
            this.m_minDrawIndex = 0;
            this.m_maxDrawIndex = 0;
            this.m_drawStart = 0;
            this.m_drawEnd = 0;
            this.m_useNormals = false;
            this.m_useTangents = false;
            this.m_normalsCalculated = false;
            this.m_tangentsCalculated = false;
            this.m_capType = EndCap.None;
            this.m_continuousTexture = false;
            this.m_1pixelLine = false;
            this.m_useTextureScale = false;
            this.m_useMatrix = false;
            this.m_collider = false;
            this.m_trigger = false;
            this.points2 = linePoints;
            this.SetupMesh(ref lineName, lineMaterial, colors, ref width, LineType.Continuous, Joins.None, true, true);
        }

        protected VectorLine(bool usePoints, string lineName, Vector3[] linePoints, Color[] colors, Material lineMaterial, float width)
        {
            this.m_active = true;
            this.capLength = 0f;
            this.m_depth = 0;
            this.m_smoothWidth = false;
            this.m_smoothColor = false;
            this.m_layer = -1;
            this.m_isAutoDrawing = false;
            this.m_minDrawIndex = 0;
            this.m_maxDrawIndex = 0;
            this.m_drawStart = 0;
            this.m_drawEnd = 0;
            this.m_useNormals = false;
            this.m_useTangents = false;
            this.m_normalsCalculated = false;
            this.m_tangentsCalculated = false;
            this.m_capType = EndCap.None;
            this.m_continuousTexture = false;
            this.m_1pixelLine = false;
            this.m_useTextureScale = false;
            this.m_useMatrix = false;
            this.m_collider = false;
            this.m_trigger = false;
            this.points3 = linePoints;
            this.SetupMesh(ref lineName, lineMaterial, colors, ref width, LineType.Continuous, Joins.None, false, true);
        }

        protected VectorLine(bool usePoints, string lineName, Vector3[] linePoints, Color color, Material lineMaterial, float width)
        {
            this.m_active = true;
            this.capLength = 0f;
            this.m_depth = 0;
            this.m_smoothWidth = false;
            this.m_smoothColor = false;
            this.m_layer = -1;
            this.m_isAutoDrawing = false;
            this.m_minDrawIndex = 0;
            this.m_maxDrawIndex = 0;
            this.m_drawStart = 0;
            this.m_drawEnd = 0;
            this.m_useNormals = false;
            this.m_useTangents = false;
            this.m_normalsCalculated = false;
            this.m_tangentsCalculated = false;
            this.m_capType = EndCap.None;
            this.m_continuousTexture = false;
            this.m_1pixelLine = false;
            this.m_useTextureScale = false;
            this.m_useMatrix = false;
            this.m_collider = false;
            this.m_trigger = false;
            this.points3 = linePoints;
            Color[] colors = this.SetColor(color, LineType.Continuous, linePoints.Length, true);
            this.SetupMesh(ref lineName, lineMaterial, colors, ref width, LineType.Continuous, Joins.None, false, true);
        }

        public VectorLine(string lineName, Vector2[] linePoints, Color color, Material lineMaterial, float width, LineType lineType)
        {
            this.m_active = true;
            this.capLength = 0f;
            this.m_depth = 0;
            this.m_smoothWidth = false;
            this.m_smoothColor = false;
            this.m_layer = -1;
            this.m_isAutoDrawing = false;
            this.m_minDrawIndex = 0;
            this.m_maxDrawIndex = 0;
            this.m_drawStart = 0;
            this.m_drawEnd = 0;
            this.m_useNormals = false;
            this.m_useTangents = false;
            this.m_normalsCalculated = false;
            this.m_tangentsCalculated = false;
            this.m_capType = EndCap.None;
            this.m_continuousTexture = false;
            this.m_1pixelLine = false;
            this.m_useTextureScale = false;
            this.m_useMatrix = false;
            this.m_collider = false;
            this.m_trigger = false;
            this.points2 = linePoints;
            Color[] colors = this.SetColor(color, lineType, linePoints.Length, false);
            this.SetupMesh(ref lineName, lineMaterial, colors, ref width, lineType, Joins.None, true, false);
        }

        public VectorLine(string lineName, Vector2[] linePoints, Material lineMaterial, float width, LineType lineType, Joins joins)
        {
            this.m_active = true;
            this.capLength = 0f;
            this.m_depth = 0;
            this.m_smoothWidth = false;
            this.m_smoothColor = false;
            this.m_layer = -1;
            this.m_isAutoDrawing = false;
            this.m_minDrawIndex = 0;
            this.m_maxDrawIndex = 0;
            this.m_drawStart = 0;
            this.m_drawEnd = 0;
            this.m_useNormals = false;
            this.m_useTangents = false;
            this.m_normalsCalculated = false;
            this.m_tangentsCalculated = false;
            this.m_capType = EndCap.None;
            this.m_continuousTexture = false;
            this.m_1pixelLine = false;
            this.m_useTextureScale = false;
            this.m_useMatrix = false;
            this.m_collider = false;
            this.m_trigger = false;
            this.points2 = linePoints;
            Color[] colors = this.SetColor(Color.white, lineType, linePoints.Length, false);
            this.SetupMesh(ref lineName, lineMaterial, colors, ref width, lineType, joins, true, false);
        }

        public VectorLine(string lineName, Vector2[] linePoints, Color[] colors, Material lineMaterial, float width, LineType lineType)
        {
            this.m_active = true;
            this.capLength = 0f;
            this.m_depth = 0;
            this.m_smoothWidth = false;
            this.m_smoothColor = false;
            this.m_layer = -1;
            this.m_isAutoDrawing = false;
            this.m_minDrawIndex = 0;
            this.m_maxDrawIndex = 0;
            this.m_drawStart = 0;
            this.m_drawEnd = 0;
            this.m_useNormals = false;
            this.m_useTangents = false;
            this.m_normalsCalculated = false;
            this.m_tangentsCalculated = false;
            this.m_capType = EndCap.None;
            this.m_continuousTexture = false;
            this.m_1pixelLine = false;
            this.m_useTextureScale = false;
            this.m_useMatrix = false;
            this.m_collider = false;
            this.m_trigger = false;
            this.points2 = linePoints;
            this.SetupMesh(ref lineName, lineMaterial, colors, ref width, lineType, Joins.None, true, false);
        }

        public VectorLine(string lineName, Vector3[] linePoints, Color color, Material lineMaterial, float width, LineType lineType)
        {
            this.m_active = true;
            this.capLength = 0f;
            this.m_depth = 0;
            this.m_smoothWidth = false;
            this.m_smoothColor = false;
            this.m_layer = -1;
            this.m_isAutoDrawing = false;
            this.m_minDrawIndex = 0;
            this.m_maxDrawIndex = 0;
            this.m_drawStart = 0;
            this.m_drawEnd = 0;
            this.m_useNormals = false;
            this.m_useTangents = false;
            this.m_normalsCalculated = false;
            this.m_tangentsCalculated = false;
            this.m_capType = EndCap.None;
            this.m_continuousTexture = false;
            this.m_1pixelLine = false;
            this.m_useTextureScale = false;
            this.m_useMatrix = false;
            this.m_collider = false;
            this.m_trigger = false;
            this.points3 = linePoints;
            Color[] colors = this.SetColor(color, lineType, linePoints.Length, false);
            this.SetupMesh(ref lineName, lineMaterial, colors, ref width, lineType, Joins.None, false, false);
        }

        public VectorLine(string lineName, Vector3[] linePoints, Material lineMaterial, float width, LineType lineType, Joins joins)
        {
            this.m_active = true;
            this.capLength = 0f;
            this.m_depth = 0;
            this.m_smoothWidth = false;
            this.m_smoothColor = false;
            this.m_layer = -1;
            this.m_isAutoDrawing = false;
            this.m_minDrawIndex = 0;
            this.m_maxDrawIndex = 0;
            this.m_drawStart = 0;
            this.m_drawEnd = 0;
            this.m_useNormals = false;
            this.m_useTangents = false;
            this.m_normalsCalculated = false;
            this.m_tangentsCalculated = false;
            this.m_capType = EndCap.None;
            this.m_continuousTexture = false;
            this.m_1pixelLine = false;
            this.m_useTextureScale = false;
            this.m_useMatrix = false;
            this.m_collider = false;
            this.m_trigger = false;
            this.points3 = linePoints;
            Color[] colors = this.SetColor(Color.white, lineType, linePoints.Length, false);
            this.SetupMesh(ref lineName, lineMaterial, colors, ref width, lineType, joins, false, false);
        }

        public VectorLine(string lineName, Vector3[] linePoints, Color[] colors, Material lineMaterial, float width, LineType lineType)
        {
            this.m_active = true;
            this.capLength = 0f;
            this.m_depth = 0;
            this.m_smoothWidth = false;
            this.m_smoothColor = false;
            this.m_layer = -1;
            this.m_isAutoDrawing = false;
            this.m_minDrawIndex = 0;
            this.m_maxDrawIndex = 0;
            this.m_drawStart = 0;
            this.m_drawEnd = 0;
            this.m_useNormals = false;
            this.m_useTangents = false;
            this.m_normalsCalculated = false;
            this.m_tangentsCalculated = false;
            this.m_capType = EndCap.None;
            this.m_continuousTexture = false;
            this.m_1pixelLine = false;
            this.m_useTextureScale = false;
            this.m_useMatrix = false;
            this.m_collider = false;
            this.m_trigger = false;
            this.points3 = linePoints;
            this.SetupMesh(ref lineName, lineMaterial, colors, ref width, lineType, Joins.None, false, false);
        }

        public VectorLine(string lineName, Vector2[] linePoints, Color color, Material lineMaterial, float width, LineType lineType, Joins joins)
        {
            this.m_active = true;
            this.capLength = 0f;
            this.m_depth = 0;
            this.m_smoothWidth = false;
            this.m_smoothColor = false;
            this.m_layer = -1;
            this.m_isAutoDrawing = false;
            this.m_minDrawIndex = 0;
            this.m_maxDrawIndex = 0;
            this.m_drawStart = 0;
            this.m_drawEnd = 0;
            this.m_useNormals = false;
            this.m_useTangents = false;
            this.m_normalsCalculated = false;
            this.m_tangentsCalculated = false;
            this.m_capType = EndCap.None;
            this.m_continuousTexture = false;
            this.m_1pixelLine = false;
            this.m_useTextureScale = false;
            this.m_useMatrix = false;
            this.m_collider = false;
            this.m_trigger = false;
            this.points2 = linePoints;
            Color[] colors = this.SetColor(color, lineType, linePoints.Length, false);
            this.SetupMesh(ref lineName, lineMaterial, colors, ref width, lineType, joins, true, false);
        }

        public VectorLine(string lineName, Vector2[] linePoints, Color[] colors, Material lineMaterial, float width, LineType lineType, Joins joins)
        {
            this.m_active = true;
            this.capLength = 0f;
            this.m_depth = 0;
            this.m_smoothWidth = false;
            this.m_smoothColor = false;
            this.m_layer = -1;
            this.m_isAutoDrawing = false;
            this.m_minDrawIndex = 0;
            this.m_maxDrawIndex = 0;
            this.m_drawStart = 0;
            this.m_drawEnd = 0;
            this.m_useNormals = false;
            this.m_useTangents = false;
            this.m_normalsCalculated = false;
            this.m_tangentsCalculated = false;
            this.m_capType = EndCap.None;
            this.m_continuousTexture = false;
            this.m_1pixelLine = false;
            this.m_useTextureScale = false;
            this.m_useMatrix = false;
            this.m_collider = false;
            this.m_trigger = false;
            this.points2 = linePoints;
            this.SetupMesh(ref lineName, lineMaterial, colors, ref width, lineType, joins, true, false);
        }

        public VectorLine(string lineName, Vector3[] linePoints, Color color, Material lineMaterial, float width, LineType lineType, Joins joins)
        {
            this.m_active = true;
            this.capLength = 0f;
            this.m_depth = 0;
            this.m_smoothWidth = false;
            this.m_smoothColor = false;
            this.m_layer = -1;
            this.m_isAutoDrawing = false;
            this.m_minDrawIndex = 0;
            this.m_maxDrawIndex = 0;
            this.m_drawStart = 0;
            this.m_drawEnd = 0;
            this.m_useNormals = false;
            this.m_useTangents = false;
            this.m_normalsCalculated = false;
            this.m_tangentsCalculated = false;
            this.m_capType = EndCap.None;
            this.m_continuousTexture = false;
            this.m_1pixelLine = false;
            this.m_useTextureScale = false;
            this.m_useMatrix = false;
            this.m_collider = false;
            this.m_trigger = false;
            this.points3 = linePoints;
            Color[] colors = this.SetColor(color, lineType, linePoints.Length, false);
            this.SetupMesh(ref lineName, lineMaterial, colors, ref width, lineType, joins, false, false);
        }

        public VectorLine(string lineName, Vector3[] linePoints, Color[] colors, Material lineMaterial, float width, LineType lineType, Joins joins)
        {
            this.m_active = true;
            this.capLength = 0f;
            this.m_depth = 0;
            this.m_smoothWidth = false;
            this.m_smoothColor = false;
            this.m_layer = -1;
            this.m_isAutoDrawing = false;
            this.m_minDrawIndex = 0;
            this.m_maxDrawIndex = 0;
            this.m_drawStart = 0;
            this.m_drawEnd = 0;
            this.m_useNormals = false;
            this.m_useTangents = false;
            this.m_normalsCalculated = false;
            this.m_tangentsCalculated = false;
            this.m_capType = EndCap.None;
            this.m_continuousTexture = false;
            this.m_1pixelLine = false;
            this.m_useTextureScale = false;
            this.m_useMatrix = false;
            this.m_collider = false;
            this.m_trigger = false;
            this.points3 = linePoints;
            this.SetupMesh(ref lineName, lineMaterial, colors, ref width, lineType, joins, false, false);
        }

        private void AddColliderIfNeeded()
        {
            if (this.vectorObject.GetComponent<Collider2D>() == null)
            {
                this.vectorObject.AddComponent(!this.m_continuous ? typeof(PolygonCollider2D) : typeof(EdgeCollider2D));
                this.vectorObject.GetComponent<Collider2D>().isTrigger = (this.m_trigger);
            }
        }

        private void AddEndCap()
        {
            if (!this.m_1pixelLine)
            {
                int newSize = this.m_vertexCount + 8;
                if (newSize > 0xfffe)
                {
                    LogError("VectorLine: exceeded maximum vertex count of 65534 for \"" + this.m_name + "\"...use fewer points");
                }
                else
                {
                    Array.Resize<Vector3>(ref this.m_lineVertices, newSize);
                    Array.Resize<Vector2>(ref this.m_lineUVs, newSize);
                    Array.Resize<Color32>(ref this.m_lineColors, newSize);
                    EndCap capType = capDictionary[this.m_endCap].capType;
                    int[] numArray = new int[12];
                    int index = 0;
                    for (int i = newSize - 8; i < newSize; i += 4)
                    {
                        numArray[index] = i + 2;
                        numArray[index + 1] = i + 1;
                        numArray[index + 2] = i;
                        numArray[index + 3] = i + 2;
                        numArray[index + 4] = i + 3;
                        numArray[index + 5] = i + 1;
                        index += 6;
                    }
                    for (int j = newSize - 8; j < (newSize - 4); j++)
                    {
                        this.m_lineColors[j] = this.m_lineColors[0];
                        this.m_lineColors[j + 4] = this.m_lineColors[newSize - 12];
                    }
                    this.m_lineUVs[newSize - 8] = new Vector2(0f, 0.25f);
                    this.m_lineUVs[newSize - 7] = new Vector2(0f, 0f);
                    this.m_lineUVs[newSize - 6] = new Vector2(1f, 0.25f);
                    this.m_lineUVs[newSize - 5] = new Vector2(1f, 0f);
                    if (capType == EndCap.Mirror)
                    {
                        this.m_lineUVs[newSize - 4] = new Vector2(1f, 0.25f);
                        this.m_lineUVs[newSize - 3] = new Vector2(1f, 0f);
                        this.m_lineUVs[newSize - 2] = new Vector2(0f, 0.25f);
                        this.m_lineUVs[newSize - 1] = new Vector2(0f, 0f);
                    }
                    else
                    {
                        this.m_lineUVs[newSize - 4] = new Vector2(0f, 1f);
                        this.m_lineUVs[newSize - 3] = new Vector2(0f, 0.75f);
                        this.m_lineUVs[newSize - 2] = new Vector2(1f, 1f);
                        this.m_lineUVs[newSize - 1] = new Vector2(1f, 0.75f);
                    }
                    this.m_mesh.vertices = (this.m_lineVertices);
                    this.m_mesh.uv = (this.m_lineUVs);
                    this.m_mesh.colors32 = (this.m_lineColors);
                    this.m_mesh.subMeshCount = (2);
                    this.m_mesh.SetTriangles(numArray, 1);
                    Material[] materialArray = new Material[] { this.m_material, capDictionary[this.m_endCap].material };
                    this.m_vectorObject.GetComponent<Renderer>().sharedMaterials = (materialArray);
                }
            }
        }

        public void AddNormals()
        {
            this.m_useNormals = true;
            this.m_normalsCalculated = false;
        }

        public void AddTangents()
        {
            this.m_useTangents = true;
            this.m_tangentsCalculated = false;
        }

        private bool Approximately(float a, float b) { return ((Mathf.Round(a * 100f) / 100f) == (Mathf.Round(b * 100f) / 100f)); }

        private bool Approximately2(Vector2 p1, Vector2 p2) { return (this.Approximately(p1.x, p2.x) && this.Approximately(p1.y, p2.y)); }

        private bool Approximately3(Vector3 p1, Vector3 p2) { return ((this.Approximately(p1.x, p2.x) && this.Approximately(p1.y, p2.y)) && this.Approximately(p1.z, p2.z)); }

        private void BuildMesh(Color[] colors)
        {
            if (this.m_1pixelLine)
            {
                this.m_vertexCount = (this.m_continuous && !this.m_isPoints) ? ((this.m_pointsLength - 1) * 2) : this.m_pointsLength;
            }
            else if (this.m_isPoints)
            {
                this.m_vertexCount = this.m_pointsLength * 4;
            }
            else
            {
                this.m_vertexCount = !this.m_continuous ? (this.m_pointsLength * 2) : ((this.m_pointsLength - 1) * 4);
            }
            if (this.m_vertexCount > 0xfffe)
            {
                LogError("VectorLine: exceeded maximum vertex count of 65534 for \"" + this.name + "\"...use fewer points (maximum is approximately 16000 points for continuous lines and points, and approximately 32000 points for discrete lines)");
            }
            else
            {
                this.m_lineVertices = new Vector3[this.m_vertexCount];
                this.m_lineUVs = new Vector2[this.m_vertexCount];
                this.m_lineColors = new Color32[this.m_vertexCount];
                int index = 0;
                int length = 0;
                if (this.m_1pixelLine)
                {
                    length = colors.Length;
                    if (this.m_isPoints)
                    {
                        for (int i = 0; i < length; i++)
                        {
                            this.m_lineColors[i] = colors[i];
                        }
                    }
                    else
                    {
                        for (int j = 0; j < length; j++)
                        {
                            this.m_lineColors[index] = colors[j];
                            this.m_lineColors[index + 1] = colors[j];
                            index += 2;
                        }
                    }
                }
                else
                {
                    if (!this.m_isPoints)
                    {
                        length = !this.m_continuous ? (this.m_pointsLength / 2) : (this.m_pointsLength - 1);
                    }
                    else
                    {
                        length = this.m_pointsLength;
                    }
                    for (int k = 0; k < length; k++)
                    {
                        this.m_lineUVs[index] = new Vector2(0f, 1f);
                        this.m_lineUVs[index + 1] = new Vector2(0f, 0f);
                        this.m_lineUVs[index + 2] = new Vector2(1f, 1f);
                        this.m_lineUVs[index + 3] = new Vector2(1f, 0f);
                        index += 4;
                    }
                    index = 0;
                    for (int m = 0; m < length; m++)
                    {
                        this.m_lineColors[index] = colors[m];
                        this.m_lineColors[index + 1] = colors[m];
                        this.m_lineColors[index + 2] = colors[m];
                        this.m_lineColors[index + 3] = colors[m];
                        index += 4;
                    }
                }
                this.m_mesh.vertices = (this.m_lineVertices);
                this.m_mesh.uv = (this.m_lineUVs);
                this.m_mesh.colors32 = (this.m_lineColors);
                this.SetupTriangles();
                if (!this.m_is2D)
                {
                    this.m_screenPoints = new Vector3[this.m_lineVertices.Length];
                }
                if (this.m_useNormals)
                {
                    this.m_normalsCalculated = false;
                }
                if (this.m_useTangents)
                {
                    this.m_tangentsCalculated = false;
                }
                if (this.m_capType != EndCap.None)
                {
                    this.AddEndCap();
                }
                this.drawStart = 0;
                this.drawEnd = this.m_pointsLength - 1;
                this.minDrawIndex = 0;
                this.maxDrawIndex = this.m_pointsLength - 1;
            }
        }

        public static Vector2[] BytesToVector2Array(byte[] lineBytes)
        {
            if ((lineBytes.Length % 8) != 0)
            {
                LogError("VectorLine.BytesToVector2Array: Incorrect input byte length...must be a multiple of 8");
                return null;
            }
            SetupByteBlock();
            Vector2[] vectorArray = new Vector2[lineBytes.Length / 8];
            int num = 0;
            for (int i = 0; i < lineBytes.Length; i += 8)
            {
                vectorArray[num++] = new Vector2(ConvertToFloat(lineBytes, i), ConvertToFloat(lineBytes, i + 4));
            }
            return vectorArray;
        }

        public static Vector3[] BytesToVector3Array(byte[] lineBytes)
        {
            if ((lineBytes.Length % 12) != 0)
            {
                LogError("VectorLine.BytesToVector3Array: Incorrect input byte length...must be a multiple of 12");
                return null;
            }
            SetupByteBlock();
            Vector3[] vectorArray = new Vector3[lineBytes.Length / 12];
            int num = 0;
            for (int i = 0; i < lineBytes.Length; i += 12)
            {
                vectorArray[num++] = new Vector3(ConvertToFloat(lineBytes, i), ConvertToFloat(lineBytes, i + 4), ConvertToFloat(lineBytes, i + 8));
            }
            return vectorArray;
        }

        private void CalculateTangents()
        {
            if (!this.m_useNormals)
            {
                this.m_useNormals = true;
                this.m_mesh.RecalculateNormals();
            }
            Vector3[] vectorArray = new Vector3[this.m_lineVertices.Length];
            Vector3[] vectorArray2 = new Vector3[this.m_lineVertices.Length];
            Vector4[] vectorArray3 = new Vector4[this.m_lineVertices.Length];
            int[] numArray = this.m_mesh.triangles;
            Vector2[] vectorArray4 = this.m_mesh.uv;
            Vector3[] vectorArray5 = this.m_mesh.normals;
            int length = numArray.Length;
            int num2 = this.m_lineVertices.Length;
            for (int i = 0; i < length; i += 3)
            {
                int index = numArray[i];
                int num5 = numArray[i + 1];
                int num6 = numArray[i + 2];
                Vector3 vector = this.m_lineVertices[index];
                Vector3 vector2 = this.m_lineVertices[num5];
                Vector3 vector3 = this.m_lineVertices[num6];
                Vector2 vector4 = vectorArray4[index];
                Vector2 vector5 = vectorArray4[num5];
                Vector2 vector6 = vectorArray4[num6];
                float num7 = vector2.x - vector.x;
                float num8 = vector3.x - vector.x;
                float num9 = vector2.y - vector.y;
                float num10 = vector3.y - vector.y;
                float num11 = vector2.z - vector.z;
                float num12 = vector3.z - vector.z;
                float num13 = vector5.x - vector4.x;
                float num14 = vector6.x - vector4.x;
                float num15 = vector5.y - vector4.y;
                float num16 = vector6.y - vector4.y;
                float num17 = 1f / ((num13 * num16) - (num14 * num15));
                Vector3 vector7 = new Vector3(((num16 * num7) - (num15 * num8)) * num17, ((num16 * num9) - (num15 * num10)) * num17, ((num16 * num11) - (num15 * num12)) * num17);
                Vector3 vector8 = new Vector3(((num13 * num8) - (num14 * num7)) * num17, ((num13 * num10) - (num14 * num9)) * num17, ((num13 * num12) - (num14 * num11)) * num17);
                vectorArray[index] += vector7;
                vectorArray[num5] += vector7;
                vectorArray[num6] += vector7;
                vectorArray2[index] += vector8;
                vectorArray2[num5] += vector8;
                vectorArray2[num6] += vector8;
            }
            for (int j = 0; j < num2; j++)
            {
                Vector3 vector9 = vectorArray5[j];
                Vector3 vector10 = vectorArray[j];
                vectorArray3[j] = (vector10 - ((Vector3)(vector9 * Vector3.Dot(vector9, vector10)))).normalized;
                vectorArray3[j].w = (Vector3.Dot(Vector3.Cross(vector9, vector10), vectorArray2[j]) >= 0f) ? 1f : -1f;
            }
            this.m_mesh.tangents = (vectorArray3);
        }

        public static bool CameraHasMoved() { return ((oldPosition != camTransform.position) || (oldRotation != camTransform.eulerAngles)); }

        private bool CheckArrayLength(FunctionName functionName, int segments, int index)
        {
            if (segments < 1)
            {
                LogError("VectorLine." + functionNames[(int)functionName] + " needs at least 1 segment");
                return false;
            }
            if (this.m_isPoints)
            {
                if ((index + segments) <= this.m_pointsLength)
                {
                    return true;
                }
                if (index == 0)
                {
                    LogError("VectorLine." + functionNames[(int)functionName] + ": The number of segments cannot exceed the number of points in the array for \"" + this.name + "\"");
                    return false;
                }
                LogError(string.Concat(new object[] { "VectorLine: Calling ", functionNames[(int)functionName], " with an index of ", index, " would exceed the length of the Vector array for \"", this.name, "\"" }));
                return false;
            }
            if (this.m_continuous)
            {
                if ((index + (segments + 1)) > this.m_pointsLength)
                {
                    if (index == 0)
                    {
                        LogError("VectorLine." + functionNames[(int)functionName] + ": The length of the array for continuous lines needs to be at least the number of segments plus one for \"" + this.name + "\"");
                        return false;
                    }
                    LogError(string.Concat(new object[] { "VectorLine: Calling ", functionNames[(int)functionName], " with an index of ", index, " would exceed the length of the Vector array for \"", this.name, "\"" }));
                    return false;
                }
            }
            else if ((index + (segments * 2)) > this.m_pointsLength)
            {
                if (index == 0)
                {
                    LogError("VectorLine." + functionNames[(int)functionName] + ": The length of the array for discrete lines needs to be at least twice the number of segments for \"" + this.name + "\"");
                    return false;
                }
                LogError(string.Concat(new object[] { "VectorLine: Calling ", functionNames[(int)functionName], " with an index of ", index, " would exceed the length of the Vector array for \"", this.name, "\"" }));
                return false;
            }
            return true;
        }

        private bool CheckLine(bool draw3D)
        {
            if (this.m_mesh == null)
            {
                LogError("VectorLine \"" + this.m_name + "\" seems to have been destroyed. If you have used ObjectSetup, the way to remove the VectorLine is to destroy the GameObject passed into ObjectSetup.");
                return false;
            }
            if (!this.m_1pixelLine)
            {
                if (this.m_joins != Joins.Fill)
                {
                    if (this.m_triangleCount != (this.m_vertexCount + (this.m_vertexCount / 2)))
                    {
                        this.SetupTriangles();
                    }
                }
                else
                {
                    if (this.m_is2D)
                    {
                        if (((this.points2[0] != this.points2[this.m_pointsLength - 1]) && (this.m_triangleCount != ((this.m_vertexCount * 3) - 6))) || ((this.points2[0] == this.points2[this.m_pointsLength - 1]) && (this.m_triangleCount != (this.m_vertexCount * 3))))
                        {
                            this.RedoFillLine();
                        }
                    }
                    else if (((this.points3[0] != this.points3[this.m_pointsLength - 1]) && (this.m_triangleCount != ((this.m_vertexCount * 3) - 6))) || ((this.points3[0] == this.points3[this.m_pointsLength - 1]) && (this.m_triangleCount != (this.m_vertexCount * 3))))
                    {
                        this.RedoFillLine();
                    }
                    if (this.m_drawStart > 0)
                    {
                        this.m_lineVertices[(this.m_drawStart * 4) - 1] = this.m_lineVertices[this.m_drawStart * 4];
                        this.m_lineVertices[(this.m_drawStart * 4) - 2] = this.m_lineVertices[this.m_drawStart * 4];
                    }
                    if (((this.m_minDrawIndex > 0) && (this.m_lineVertices[(this.m_minDrawIndex * 4) - 1] == Vector3.zero)) && (this.m_lineVertices[(this.m_minDrawIndex * 4) - 2] == Vector3.zero))
                    {
                        this.m_lineVertices[(this.m_minDrawIndex * 4) - 1] = this.m_lineVertices[this.m_minDrawIndex * 4];
                        this.m_lineVertices[(this.m_minDrawIndex * 4) - 2] = this.m_lineVertices[this.m_minDrawIndex * 4];
                    }
                }
                if (this.m_capType != EndCap.None)
                {
                    if (this.m_capType <= EndCap.Mirror)
                    {
                        int index = this.m_drawStart * 4;
                        int num2 = (this.m_lineWidths.Length <= 1) ? 0 : this.m_drawStart;
                        if (!this.m_continuous)
                        {
                            num2 /= 2;
                            index /= 2;
                        }
                        if (!draw3D)
                        {
                            Vector3 vector2 = this.m_lineVertices[index] - this.m_lineVertices[index + 2];
                            Vector3 vector = (Vector3)(((vector2.normalized * this.m_lineWidths[num2]) * 2f) * capDictionary[this.m_endCap].ratio1);
                            Vector3 vector3 = (Vector3)(vector * capDictionary[this.m_endCap].offset);
                            this.m_lineVertices[this.m_vertexCount] = (this.m_lineVertices[index] + vector) + vector3;
                            this.m_lineVertices[this.m_vertexCount + 1] = (this.m_lineVertices[index + 1] + vector) + vector3;
                            this.m_lineVertices[index] += vector3;
                            this.m_lineVertices[index + 1] += vector3;
                        }
                        else
                        {
                            Vector3 vector5 = this.m_screenPoints[index] - this.m_screenPoints[index + 2];
                            Vector3 vector4 = (Vector3)(((vector5.normalized * this.m_lineWidths[num2]) * 2f) * capDictionary[this.m_endCap].ratio1);
                            Vector3 vector6 = (Vector3)(vector4 * capDictionary[this.m_endCap].offset);
                            this.m_lineVertices[this.m_vertexCount] = cam3D.ScreenToWorldPoint((this.m_screenPoints[index] + vector4) + vector6);
                            this.m_lineVertices[this.m_vertexCount + 1] = cam3D.ScreenToWorldPoint((this.m_screenPoints[index + 1] + vector4) + vector6);
                            this.m_lineVertices[index] = cam3D.ScreenToWorldPoint(this.m_screenPoints[index] + vector6);
                            this.m_lineVertices[index + 1] = cam3D.ScreenToWorldPoint(this.m_screenPoints[index + 1] + vector6);
                        }
                        this.m_lineVertices[this.m_vertexCount + 2] = this.m_lineVertices[index];
                        this.m_lineVertices[this.m_vertexCount + 3] = this.m_lineVertices[index + 1];
                    }
                    if (this.m_capType >= EndCap.Both)
                    {
                        int drawEnd = this.m_drawEnd;
                        if (this.m_continuous)
                        {
                            if (this.m_drawEnd == this.m_pointsLength)
                            {
                                drawEnd--;
                            }
                        }
                        else if (drawEnd < this.m_pointsLength)
                        {
                            drawEnd++;
                        }
                        int num4 = drawEnd * 4;
                        int num5 = (this.m_lineWidths.Length <= 1) ? 0 : (drawEnd - 1);
                        if (num5 < 0)
                        {
                            num5 = 0;
                        }
                        if (!this.m_continuous)
                        {
                            num5 /= 2;
                            num4 /= 2;
                        }
                        if (num4 < 4)
                        {
                            num4 = 4;
                        }
                        if (!draw3D)
                        {
                            Vector3 vector8 = this.m_lineVertices[num4 - 1] - this.m_lineVertices[num4 - 3];
                            Vector3 vector7 = (Vector3)(((vector8.normalized * this.m_lineWidths[num5]) * 2f) * capDictionary[this.m_endCap].ratio2);
                            Vector3 vector9 = (Vector3)(vector7 * capDictionary[this.m_endCap].offset);
                            this.m_lineVertices[this.m_vertexCount + 6] = (this.m_lineVertices[num4 - 2] + vector7) + vector9;
                            this.m_lineVertices[this.m_vertexCount + 7] = (this.m_lineVertices[num4 - 1] + vector7) + vector9;
                            this.m_lineVertices[num4 - 2] += vector9;
                            this.m_lineVertices[num4 - 1] += vector9;
                        }
                        else
                        {
                            Vector3 vector11 = this.m_screenPoints[num4 - 1] - this.m_screenPoints[num4 - 3];
                            Vector3 vector10 = (Vector3)(((vector11.normalized * this.m_lineWidths[num5]) * 2f) * capDictionary[this.m_endCap].ratio2);
                            Vector3 vector12 = (Vector3)(vector10 * capDictionary[this.m_endCap].offset);
                            this.m_lineVertices[this.m_vertexCount + 6] = cam3D.ScreenToWorldPoint((this.m_screenPoints[num4 - 2] + vector10) + vector12);
                            this.m_lineVertices[this.m_vertexCount + 7] = cam3D.ScreenToWorldPoint((this.m_screenPoints[num4 - 1] + vector10) + vector12);
                            this.m_lineVertices[num4 - 2] = cam3D.ScreenToWorldPoint(this.m_screenPoints[num4 - 2] + vector12);
                            this.m_lineVertices[num4 - 1] = cam3D.ScreenToWorldPoint(this.m_screenPoints[num4 - 1] + vector12);
                        }
                        this.m_lineVertices[this.m_vertexCount + 4] = this.m_lineVertices[num4 - 2];
                        this.m_lineVertices[this.m_vertexCount + 5] = this.m_lineVertices[num4 - 1];
                    }
                    if ((this.m_drawStart > 0) || (this.m_drawEnd < this.m_pointsLength))
                    {
                        this.SetEndCapColors();
                        this.m_mesh.colors32 = (this.m_lineColors);
                    }
                }
                if (this.m_continuousTexture)
                {
                    int num6 = 0;
                    float num7 = 0f;
                    this.SetDistances();
                    int num8 = this.m_distances.Length - 1;
                    float num9 = this.m_distances[num8];
                    for (int i = 0; i < num8; i++)
                    {
                        this.m_lineUVs[num6].x = num7;
                        this.m_lineUVs[num6 + 1].x = num7;
                        num7 = 1f / (num9 / this.m_distances[i + 1]);
                        this.m_lineUVs[num6 + 2].x = num7;
                        this.m_lineUVs[num6 + 3].x = num7;
                        num6 += 4;
                    }
                    this.m_mesh.uv = (this.m_lineUVs);
                }
            }
            return true;
        }

        private void CheckNormals()
        {
            if (this.m_useNormals && !this.m_normalsCalculated)
            {
                this.m_mesh.RecalculateNormals();
                this.m_normalsCalculated = true;
            }
            if (this.m_useTangents && !this.m_tangentsCalculated)
            {
                this.CalculateTangents();
                this.m_tangentsCalculated = true;
            }
        }

        private static void CheckPairPoints(Dictionary<Vector3Pair, bool> pairs, Vector3 p1, Vector3 p2, List<Vector3> linePoints)
        {
            Vector3Pair key = new Vector3Pair(p1, p2);
            Vector3Pair pair2 = new Vector3Pair(p2, p1);
            if (!pairs.ContainsKey(key) && !pairs.ContainsKey(pair2))
            {
                pairs[key] = true;
                pairs[pair2] = true;
                linePoints.Add(p1);
                linePoints.Add(p2);
            }
        }

        private static float ConvertToFloat(byte[] bytes, int i)
        {
            byteBlock[endianDiff1] = bytes[i];
            byteBlock[1 + endianDiff2] = bytes[i + 1];
            byteBlock[2 - endianDiff2] = bytes[i + 2];
            byteBlock[3 - endianDiff1] = bytes[i + 3];
            return BitConverter.ToSingle(byteBlock, 0);
        }

        public static void Destroy(ref VectorLine line)
        {
            DestroyLine(ref line);
        }

        public static void Destroy(List<VectorLine> lines)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                VectorLine line = lines[i];
                DestroyLine(ref line);
            }
        }

        public static void Destroy(VectorLine[] lines)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                DestroyLine(ref lines[i]);
            }
        }

        public static void Destroy(ref VectorPoints line)
        {
            DestroyPoints(ref line);
        }

        public static void Destroy(List<VectorPoints> lines)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                VectorPoints line = lines[i];
                DestroyPoints(ref line);
            }
        }

        public static void Destroy(VectorPoints[] lines)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                DestroyPoints(ref lines[i]);
            }
        }

        public static void Destroy(ref VectorLine line, GameObject go)
        {
            Destroy(ref line);
            if (go != null)
            {
                Object.Destroy(go);
            }
        }

        public static void Destroy(ref VectorPoints line, GameObject go)
        {
            Destroy(ref line);
            if (go != null)
            {
                Object.Destroy(go);
            }
        }

        private static void DestroyLine(ref VectorLine line)
        {
            if (line != null)
            {
                Object.Destroy(line.m_mesh);
                Object.Destroy(line.m_meshFilter);
                Object.Destroy(line.m_vectorObject);
                if (line.isAutoDrawing)
                {
                    line.StopDrawing3DAuto();
                }
                line = null;
            }
        }

        private static void DestroyPoints(ref VectorPoints line)
        {
            if (line != null)
            {
                Object.Destroy(line.m_mesh);
                Object.Destroy(line.m_meshFilter);
                Object.Destroy(line.m_vectorObject);
                if (line.isAutoDrawing)
                {
                    line.StopDrawing3DAuto();
                }
                line = null;
            }
        }

        public void Draw()
        {
            if (!error && this.m_active)
            {
                if (cam == null)
                {
                    SetCamera();
                    if (cam == null)
                    {
                        LogError("VectorLine.Draw: You must call SetCamera before calling Draw for \"" + this.name + "\"");
                        return;
                    }
                }
                if (this.m_isPoints)
                {
                    this.DrawPoints();
                }
                else if ((this.smoothWidth && (this.m_lineWidths.Length == 1)) && (this.pointsLength > 2))
                {
                    LogError("VectorLine.Draw called with smooth line widths for \"" + this.name + "\", but VectorLine.SetWidths has not been used");
                }
                else
                {
                    Matrix4x4 matrixx;
                    int num;
                    bool useTransformMatrix = this.UseMatrix(out matrixx);
                    zDist = !useOrthoCam ? ((screenHeight / 2) + ((100f - this.m_depth) * 0.0001f)) : ((float)(0x65 - this.m_depth));
                    int end = 0;
                    this.SetupDrawStartEnd(out num, out end);
                    if (this.m_is2D)
                    {
                        this.Line2D(num, end, matrixx, useTransformMatrix);
                    }
                    else if (this.m_continuous)
                    {
                        this.Line3DContinuous(num, end, matrixx, useTransformMatrix);
                    }
                    else
                    {
                        this.Line3DDiscrete(num, end, matrixx, useTransformMatrix);
                    }
                    if (this.CheckLine(false))
                    {
                        this.m_mesh.vertices = (this.m_lineVertices);
                        this.CheckNormals();
                        if ((this.m_mesh.bounds.center.x != (screenWidth / 2)) || (this.m_mesh.bounds.center.y != (screenHeight / 2)))
                        {
                            this.SetLineMeshBounds();
                        }
                        if (this.m_useTextureScale)
                        {
                            this.SetTextureScale();
                        }
                        if (this.m_collider)
                        {
                            this.SetCollider(true);
                        }
                    }
                }
            }
        }

        public void Draw3D()
        {
            if (!error && this.m_active)
            {
                if (cam3D == null)
                {
                    SetCamera3D();
                    if (cam3D == null)
                    {
                        LogError("VectorLine.Draw3D: You must call SetCamera or SetCamera3D before calling Draw3D for \"" + this.name + "\"");
                        return;
                    }
                }
                if (this.m_is2D)
                {
                    LogError("VectorLine.Draw3D can only be used with a Vector3 array, which \"" + this.name + "\" doesn't have");
                }
                else if (this.m_isPoints)
                {
                    this.DrawPoints3D();
                }
                else if ((this.smoothWidth && (this.m_lineWidths.Length == 1)) && (this.pointsLength > 2))
                {
                    LogError("VectorLine.Draw3D called with smooth line widths for \"" + this.name + "\", but VectorLine.SetWidths has not been used");
                }
                else
                {
                    int num;
                    int num2;
                    Matrix4x4 matrixx;
                    if (this.layer == -1)
                    {
                        this.m_vectorObject.layer = (_vectorLayer3D);
                        this.layer = _vectorLayer3D;
                    }
                    int index = 0;
                    this.SetupDrawStartEnd(out num, out num2);
                    bool flag = this.UseMatrix(out matrixx);
                    if (this.m_1pixelLine)
                    {
                        if (this.m_continuous)
                        {
                            int num6 = num * 2;
                            if (flag)
                            {
                                for (int i = num; i < num2; i++)
                                {
                                    this.m_lineVertices[num6] = matrixx.MultiplyPoint3x4(this.points3[i]);
                                    this.m_lineVertices[num6 + 1] = matrixx.MultiplyPoint3x4(this.points3[i + 1]);
                                    num6 += 2;
                                }
                            }
                            else
                            {
                                for (int j = num; j < num2; j++)
                                {
                                    this.m_lineVertices[num6] = this.points3[j];
                                    this.m_lineVertices[num6 + 1] = this.points3[j + 1];
                                    num6 += 2;
                                }
                            }
                        }
                        else if (flag)
                        {
                            for (int k = num; k <= num2; k++)
                            {
                                this.m_lineVertices[k] = matrixx.MultiplyPoint3x4(this.points3[k]);
                            }
                        }
                        else
                        {
                            for (int m = num; m <= num2; m++)
                            {
                                this.m_lineVertices[m] = this.points3[m];
                            }
                        }
                        if (this.CheckLine(true))
                        {
                            this.m_mesh.vertices = (this.m_lineVertices);
                            this.m_mesh.RecalculateBounds();
                        }
                    }
                    else
                    {
                        int num3;
                        int num4;
                        int num11 = 0;
                        if (this.m_lineWidths.Length > 1)
                        {
                            index = num;
                            num11 = 1;
                        }
                        if (this.m_continuous)
                        {
                            num3 = num * 4;
                            num4 = 1;
                        }
                        else
                        {
                            num3 = num * 2;
                            index /= 2;
                            num4 = 2;
                        }
                        Vector3 vector5 = Vector3.zero;
                        Vector3 vector6 = Vector3.zero;
                        for (int n = num; n < num2; n += num4)
                        {
                            Vector3 vector;
                            Vector3 vector2;
                            if (flag)
                            {
                                vector = cam3D.WorldToScreenPoint(matrixx.MultiplyPoint3x4(this.points3[n]));
                                vector2 = cam3D.WorldToScreenPoint(matrixx.MultiplyPoint3x4(this.points3[n + 1]));
                            }
                            else
                            {
                                vector = cam3D.WorldToScreenPoint(this.points3[n]);
                                vector2 = cam3D.WorldToScreenPoint(this.points3[n + 1]);
                            }
                            vector5.x = vector2.y;
                            vector5.y = vector.x;
                            vector6.x = vector.y;
                            vector6.y = vector2.x;
                            Vector3 vector3 = (vector5 - vector6).normalized;
                            Vector3 vector4 = (Vector3)(vector3 * this.m_lineWidths[index]);
                            this.m_screenPoints[num3] = vector - vector4;
                            this.m_screenPoints[num3 + 1] = vector + vector4;
                            this.m_lineVertices[num3] = cam3D.ScreenToWorldPoint(this.m_screenPoints[num3]);
                            this.m_lineVertices[num3 + 1] = cam3D.ScreenToWorldPoint(this.m_screenPoints[num3 + 1]);
                            if (this.smoothWidth && (n < (num2 - num4)))
                            {
                                vector4 = (Vector3)(vector3 * this.m_lineWidths[index + 1]);
                            }
                            this.m_screenPoints[num3 + 2] = vector2 - vector4;
                            this.m_screenPoints[num3 + 3] = vector2 + vector4;
                            this.m_lineVertices[num3 + 2] = cam3D.ScreenToWorldPoint(this.m_screenPoints[num3 + 2]);
                            this.m_lineVertices[num3 + 3] = cam3D.ScreenToWorldPoint(this.m_screenPoints[num3 + 3]);
                            num3 += 4;
                            index += num11;
                        }
                        if (this.m_joins == Joins.Weld)
                        {
                            if (this.m_continuous)
                            {
                                this.WeldJoins3D((num * 4) + 4, num2 * 4, (this.Approximately3(this.points3[0], this.points3[this.m_pointsLength - 1]) && (this.m_minDrawIndex == 0)) && ((this.m_maxDrawIndex == (this.points3.Length - 1)) || (this.m_maxDrawIndex == 0)));
                            }
                            else
                            {
                                this.WeldJoinsDiscrete3D(num + 1, num2, (this.Approximately3(this.points3[0], this.points3[this.m_pointsLength - 1]) && (this.m_minDrawIndex == 0)) && ((this.m_maxDrawIndex == (this.points3.Length - 1)) || (this.m_maxDrawIndex == 0)));
                            }
                        }
                        if (this.CheckLine(true))
                        {
                            this.m_mesh.vertices = (this.m_lineVertices);
                            this.m_mesh.RecalculateBounds();
                            this.CheckNormals();
                            if (this.m_useTextureScale)
                            {
                                this.SetTextureScale();
                            }
                            if (this.m_collider)
                            {
                                this.SetCollider(false);
                            }
                        }
                    }
                }
            }
        }

        public void Draw3DAuto()
        {
            this.Draw3DAuto(0f);
        }

        public void Draw3DAuto(float time)
        {
            if (this.m_1pixelLine)
            {
                Debug.LogWarning("VectorLine: When using a 1 pixel line and useMeshLines=true (or 1 pixel points and useMeshPoints=true), Draw3DAuto is unnecessary. Use Draw3D instead for optimal performance.");
            }
            if (time < 0f)
            {
                time = 0f;
            }
            lineManager.AddLine(this, this.m_useTransform, time);
            this.m_isAutoDrawing = true;
            this.Draw3D();
        }

        private void DrawPoints()
        {
            Matrix4x4 matrixx;
            int num;
            int num2;
            bool flag = this.UseMatrix(out matrixx);
            zDist = !useOrthoCam ? ((screenHeight / 2) + ((100f - this.m_depth) * 0.0001f)) : ((float)(0x65 - this.m_depth));
            this.SetupDrawStartEnd(out num, out num2);
            Vector2 vector = new Vector2((float)Screen.width, (float)Screen.height);
            if (this.m_1pixelLine)
            {
                if (!this.m_is2D)
                {
                    for (int i = num; i <= num2; i++)
                    {
                        this.m_lineVertices[i] = !flag ? cam3D.WorldToScreenPoint(this.points3[i]) : cam3D.WorldToScreenPoint(matrixx.MultiplyPoint3x4(this.points3[i]));
                        if (this.m_lineVertices[i].z < 0.15f)
                        {
                            this.m_lineVertices[i] = Vector3.zero;
                        }
                        else
                        {
                            this.m_lineVertices[i].z = zDist;
                        }
                    }
                }
                else
                {
                    for (int j = num; j <= num2; j++)
                    {
                        this.m_lineVertices[j] = !flag ? (Vector3)this.points2[j] : matrixx.MultiplyPoint3x4(this.points2[j]);
                        if (this.useViewportCoords)
                        {
                            this.m_lineVertices[j].x *= vector.x;
                            this.m_lineVertices[j].y *= vector.y;
                        }
                        this.m_lineVertices[j].z = zDist;
                    }
                }
                this.m_mesh.vertices = (this.m_lineVertices);
                if ((this.m_mesh.bounds.center.x != (screenWidth / 2)) || (this.m_mesh.bounds.center.y != (screenHeight / 2)))
                {
                    this.SetLineMeshBounds();
                }
            }
            else
            {
                Vector3 vector4;
                int idx = num * 4;
                int widthIdxAdd = (this.m_lineWidths.Length <= 1) ? 0 : 1;
                int widthIdx = num;
                Vector3 vector5 = new Vector3(this.m_lineWidths[0], this.m_lineWidths[0], 0f);
                Vector3 vector6 = new Vector3(-this.m_lineWidths[0], this.m_lineWidths[0], 0f);
                if (!this.m_is2D)
                {
                    for (int k = num; k <= num2; k++)
                    {
                        vector4 = !flag ? cam3D.WorldToScreenPoint(this.points3[k]) : cam3D.WorldToScreenPoint(matrixx.MultiplyPoint3x4(this.points3[k]));
                        if (vector4.z < 0.15f)
                        {
                            this.Skip(ref idx, ref widthIdx, ref vector4, ref widthIdxAdd);
                        }
                        else
                        {
                            vector4.z = zDist;
                            if (widthIdxAdd != 0)
                            {
                                vector5.x = vector5.y = vector6.y = this.m_lineWidths[widthIdx];
                                vector6.x = -this.m_lineWidths[widthIdx];
                                widthIdx++;
                            }
                            this.m_lineVertices[idx] = vector4 + vector6;
                            this.m_lineVertices[idx + 1] = vector4 - vector5;
                            this.m_lineVertices[idx + 2] = vector4 + vector5;
                            this.m_lineVertices[idx + 3] = vector4 - vector6;
                            idx += 4;
                        }
                    }
                }
                else
                {
                    for (int m = num; m <= num2; m++)
                    {
                        vector4 = !flag ? (Vector3)this.points2[m] : matrixx.MultiplyPoint3x4(this.points2[m]);
                        if (this.useViewportCoords)
                        {
                            vector4.x *= vector.x;
                            vector4.y *= vector.y;
                        }
                        vector4.z = zDist;
                        if (widthIdxAdd != 0)
                        {
                            vector5.x = vector5.y = vector6.y = this.m_lineWidths[widthIdx];
                            vector6.x = -this.m_lineWidths[widthIdx];
                            widthIdx++;
                        }
                        this.m_lineVertices[idx] = vector4 + vector6;
                        this.m_lineVertices[idx + 1] = vector4 - vector5;
                        this.m_lineVertices[idx + 2] = vector4 + vector5;
                        this.m_lineVertices[idx + 3] = vector4 - vector6;
                        idx += 4;
                    }
                }
                this.m_mesh.vertices = (this.m_lineVertices);
                if ((this.m_mesh.bounds.center.x != (screenWidth / 2)) || (this.m_mesh.bounds.center.y != (screenHeight / 2)))
                {
                    this.SetLineMeshBounds();
                }
            }
        }

        private void DrawPoints3D()
        {
            Matrix4x4 matrixx;
            int num;
            int num2;
            if (this.layer == -1)
            {
                this.m_vectorObject.layer = (_vectorLayer3D);
                this.layer = _vectorLayer3D;
            }
            bool flag = this.UseMatrix(out matrixx);
            int widthIdx = 0;
            this.SetupDrawStartEnd(out num, out num2);
            if (this.m_1pixelLine)
            {
                if (flag)
                {
                    for (int i = num; i <= num2; i++)
                    {
                        this.m_lineVertices[i] = matrixx.MultiplyPoint3x4(this.points3[i]);
                    }
                }
                else
                {
                    for (int j = num; j <= num2; j++)
                    {
                        this.m_lineVertices[j] = this.points3[j];
                    }
                }
                this.m_mesh.vertices = (this.m_lineVertices);
                this.m_mesh.RecalculateBounds();
            }
            else
            {
                int idx = this.m_minDrawIndex * 4;
                int widthIdxAdd = 0;
                if (this.m_lineWidths.Length > 1)
                {
                    widthIdx = num;
                    widthIdxAdd = 1;
                }
                Vector3 vector2 = Vector3.zero;
                Vector3 vector3 = Vector3.zero;
                for (int k = num; k <= num2; k++)
                {
                    Vector3 pos = !flag ? cam3D.WorldToScreenPoint(this.points3[k]) : cam3D.WorldToScreenPoint(matrixx.MultiplyPoint3x4(this.points3[k]));
                    if (pos.z < 0.15f)
                    {
                        pos = Vector3.zero;
                        this.Skip(ref idx, ref widthIdx, ref pos, ref widthIdxAdd);
                    }
                    else
                    {
                        vector2.x = vector2.y = vector3.y = this.m_lineWidths[widthIdx];
                        vector3.x = -this.m_lineWidths[widthIdx];
                        this.m_lineVertices[idx] = cam3D.ScreenToWorldPoint(pos + vector3);
                        this.m_lineVertices[idx + 1] = cam3D.ScreenToWorldPoint(pos - vector2);
                        this.m_lineVertices[idx + 2] = cam3D.ScreenToWorldPoint(pos + vector2);
                        this.m_lineVertices[idx + 3] = cam3D.ScreenToWorldPoint(pos - vector3);
                        idx += 4;
                        widthIdx += widthIdxAdd;
                    }
                }
                this.m_mesh.vertices = (this.m_lineVertices);
                this.m_mesh.RecalculateBounds();
                this.CheckNormals();
            }
        }

        private static float EvalCubicPoly(ref Vector4 p, float t) { return (((p.x + (p.y * t)) + (p.z * (t * t))) + (p.w * ((t * t) * t))); }

        private static Vector2 GetBezierPoint(ref Vector2 anchor1, ref Vector2 control1, ref Vector2 anchor2, ref Vector2 control2, float t)
        {
            float num = 3f * (control1.x - anchor1.x);
            float num2 = (3f * (control2.x - control1.x)) - num;
            float num3 = ((anchor2.x - anchor1.x) - num) - num2;
            float num4 = 3f * (control1.y - anchor1.y);
            float num5 = (3f * (control2.y - control1.y)) - num4;
            float num6 = ((anchor2.y - anchor1.y) - num4) - num5;
            return new Vector2((((num3 * ((t * t) * t)) + (num2 * (t * t))) + (num * t)) + anchor1.x, (((num6 * ((t * t) * t)) + (num5 * (t * t))) + (num4 * t)) + anchor1.y);
        }

        private static Vector3 GetBezierPoint3D(ref Vector3 anchor1, ref Vector3 control1, ref Vector3 anchor2, ref Vector3 control2, float t)
        {
            float num = 3f * (control1.x - anchor1.x);
            float num2 = (3f * (control2.x - control1.x)) - num;
            float num3 = ((anchor2.x - anchor1.x) - num) - num2;
            float num4 = 3f * (control1.y - anchor1.y);
            float num5 = (3f * (control2.y - control1.y)) - num4;
            float num6 = ((anchor2.y - anchor1.y) - num4) - num5;
            float num7 = 3f * (control1.z - anchor1.z);
            float num8 = (3f * (control2.z - control1.z)) - num7;
            float num9 = ((anchor2.z - anchor1.z) - num7) - num8;
            return new Vector3((((num3 * ((t * t) * t)) + (num2 * (t * t))) + (num * t)) + anchor1.x, (((num6 * ((t * t) * t)) + (num5 * (t * t))) + (num4 * t)) + anchor1.y, (((num9 * ((t * t) * t)) + (num8 * (t * t))) + (num7 * t)) + anchor1.z);
        }

        public static Camera GetCamera()
        {
            if (cam == null)
            {
                LogError("The vector cam has not been set up");
                return null;
            }
            return cam;
        }

        public Color GetColor(int index)
        {
            int num;
            if (this.m_1pixelLine)
            {
                num = !this.m_isPoints ? 2 : 1;
            }
            else
            {
                num = 4;
            }
            index *= num;
            if ((index >= 0) && (index < this.m_lineColors.Length))
            {
                return this.m_lineColors[index];
            }
            LogError("VectorLine.GetColor: index out of range");
            return Color.clear;
        }

        public float GetLength()
        {
            if ((this.m_distances == null) || (this.m_distances.Length != (!this.m_continuous ? ((this.pointsLength / 2) + 1) : this.pointsLength)))
            {
                this.SetDistances();
            }
            return this.m_distances[this.m_distances.Length - 1];
        }

        public Vector2 GetPoint(float distance)
        {
            int num;
            return this.GetPoint(distance, out num);
        }

        public Vector2 GetPoint(float distance, out int index)
        {
            Vector2 vector;
            if (!this.m_is2D)
            {
                LogError("VectorLine.GetPoint only works with Vector2 points");
                index = 0;
                return Vector2.zero;
            }
            if (this.points2.Length < 2)
            {
                LogError("VectorLine.GetPoint needs at least 2 points in the points2 array");
                index = 0;
                return Vector2.zero;
            }
            this.SetDistanceIndex(out index, distance);
            if (this.m_continuous)
            {
                vector = Vector2.Lerp(this.points2[index - 1], this.points2[index], Mathf.InverseLerp(this.m_distances[index - 1], this.m_distances[index], distance));
            }
            else
            {
                vector = Vector2.Lerp(this.points2[(index - 1) * 2], this.points2[((index - 1) * 2) + 1], Mathf.InverseLerp(this.m_distances[index - 1], this.m_distances[index], distance));
            }
            if (this.m_useTransform != null)
            {
                vector += new Vector2(this.m_useTransform.position.x, this.m_useTransform.position.y);
            }
            index--;
            return vector;
        }

        public Vector2 GetPoint01(float distance)
        {
            int num;
            return this.GetPoint(Mathf.Lerp(0f, this.GetLength(), distance), out num);
        }

        public Vector2 GetPoint01(float distance, out int index) { return this.GetPoint(Mathf.Lerp(0f, this.GetLength(), distance), out index); }

        public Vector3 GetPoint3D(float distance)
        {
            int num;
            return this.GetPoint3D(distance, out num);
        }

        public Vector3 GetPoint3D(float distance, out int index)
        {
            Vector3 vector;
            if (this.m_is2D)
            {
                LogError("VectorLine.GetPoint3D only works with Vector3 points");
                index = 0;
                return Vector3.zero;
            }
            if (this.points3.Length < 2)
            {
                LogError("VectorLine.GetPoint3D needs at least 2 points in the points3 array");
                index = 0;
                return Vector3.zero;
            }
            this.SetDistanceIndex(out index, distance);
            if (this.m_continuous)
            {
                vector = Vector3.Lerp(this.points3[index - 1], this.points3[index], Mathf.InverseLerp(this.m_distances[index - 1], this.m_distances[index], distance));
            }
            else
            {
                vector = Vector3.Lerp(this.points3[(index - 1) * 2], this.points3[((index - 1) * 2) + 1], Mathf.InverseLerp(this.m_distances[index - 1], this.m_distances[index], distance));
            }
            if (this.m_useTransform != null)
            {
                vector += this.m_useTransform.position;
            }
            index--;
            return vector;
        }

        public Vector3 GetPoint3D01(float distance)
        {
            int num;
            return this.GetPoint3D(Mathf.Lerp(0f, this.GetLength(), distance), out num);
        }

        public Vector3 GetPoint3D01(float distance, out int index) { return this.GetPoint3D(Mathf.Lerp(0f, this.GetLength(), distance), out index); }

        public int GetSegmentNumber()
        {
            if (this.m_continuous)
            {
                return (this.pointsLength - 1);
            }
            return (this.pointsLength / 2);
        }

        private static Vector2 GetSplinePoint(ref Vector2 p0, ref Vector2 p1, ref Vector2 p2, ref Vector2 p3, float t)
        {
            Vector4 p = Vector4.zero;
            Vector4 vector2 = Vector4.zero;
            float num = Mathf.Pow(VectorDistanceSquared(ref p0, ref p1), 0.25f);
            float num2 = Mathf.Pow(VectorDistanceSquared(ref p1, ref p2), 0.25f);
            float num3 = Mathf.Pow(VectorDistanceSquared(ref p2, ref p3), 0.25f);
            if (num2 < 0.0001f)
            {
                num2 = 1f;
            }
            if (num < 0.0001f)
            {
                num = num2;
            }
            if (num3 < 0.0001f)
            {
                num3 = num2;
            }
            InitNonuniformCatmullRom(p0.x, p1.x, p2.x, p3.x, num, num2, num3, ref p);
            InitNonuniformCatmullRom(p0.y, p1.y, p2.y, p3.y, num, num2, num3, ref vector2);
            return new Vector2(EvalCubicPoly(ref p, t), EvalCubicPoly(ref vector2, t));
        }

        private static Vector3 GetSplinePoint3D(ref Vector3 p0, ref Vector3 p1, ref Vector3 p2, ref Vector3 p3, float t)
        {
            Vector4 p = Vector4.zero;
            Vector4 vector2 = Vector4.zero;
            Vector4 vector3 = Vector4.zero;
            float num = Mathf.Pow(VectorDistanceSquared(ref p0, ref p1), 0.25f);
            float num2 = Mathf.Pow(VectorDistanceSquared(ref p1, ref p2), 0.25f);
            float num3 = Mathf.Pow(VectorDistanceSquared(ref p2, ref p3), 0.25f);
            if (num2 < 0.0001f)
            {
                num2 = 1f;
            }
            if (num < 0.0001f)
            {
                num = num2;
            }
            if (num3 < 0.0001f)
            {
                num3 = num2;
            }
            InitNonuniformCatmullRom(p0.x, p1.x, p2.x, p3.x, num, num2, num3, ref p);
            InitNonuniformCatmullRom(p0.y, p1.y, p2.y, p3.y, num, num2, num3, ref vector2);
            InitNonuniformCatmullRom(p0.z, p1.z, p2.z, p3.z, num, num2, num3, ref vector3);
            return new Vector3(EvalCubicPoly(ref p, t), EvalCubicPoly(ref vector2, t), EvalCubicPoly(ref vector3, t));
        }

        public float GetWidth(int index)
        {
            int num = this.MaxSegmentIndex();
            if ((index < 0) || (index >= num))
            {
                LogError("VectorLine.GetWidth: index out of range...must be >= 0 and < " + num);
                return 0f;
            }
            if (this.m_lineWidths.Length == 1)
            {
                return (this.m_lineWidths[0] * 2f);
            }
            return (this.m_lineWidths[index] * 2f);
        }

        private static void InitNonuniformCatmullRom(float x0, float x1, float x2, float x3, float dt0, float dt1, float dt2, ref Vector4 p)
        {
            float num = ((((x1 - x0) / dt0) - ((x2 - x0) / (dt0 + dt1))) + ((x2 - x1) / dt1)) * dt1;
            float num2 = ((((x2 - x1) / dt1) - ((x3 - x1) / (dt1 + dt2))) + ((x3 - x2) / dt2)) * dt1;
            p.x = x1;
            p.y = num;
            p.z = (((-3f * x1) + (3f * x2)) - (2f * num)) - num2;
            p.w = (((2f * x1) - (2f * x2)) + num) + num2;
        }

        private void Line2D(int start, int end, Matrix4x4 thisMatrix, bool useTransformMatrix)
        {
            Vector3 vector;
            Vector3 vector2;
            Vector3 vector3 = Vector3.zero;
            Vector3 vector4 = Vector3.zero;
            Vector2 vector5 = new Vector2((float)Screen.width, (float)Screen.height);
            if (this.m_1pixelLine)
            {
                if (this.m_continuous)
                {
                    int index = start * 2;
                    for (int i = start; i < end; i++)
                    {
                        if (useTransformMatrix)
                        {
                            vector = thisMatrix.MultiplyPoint3x4(this.points2[i]);
                            vector2 = thisMatrix.MultiplyPoint3x4(this.points2[i + 1]);
                        }
                        else
                        {
                            vector = this.points2[i];
                            vector2 = this.points2[i + 1];
                        }
                        if (this.useViewportCoords)
                        {
                            vector.x *= vector5.x;
                            vector.y *= vector5.y;
                            vector2.x *= vector5.x;
                            vector2.y *= vector5.y;
                        }
                        vector.z = zDist;
                        vector2.z = zDist;
                        this.m_lineVertices[index] = vector;
                        this.m_lineVertices[index + 1] = vector2;
                        index += 2;
                    }
                }
                else
                {
                    for (int j = start; j <= end; j++)
                    {
                        if (useTransformMatrix)
                        {
                            vector = thisMatrix.MultiplyPoint3x4(this.points2[j]);
                        }
                        else
                        {
                            vector = this.points2[j];
                        }
                        if (this.useViewportCoords)
                        {
                            vector.x *= vector5.x;
                            vector.y *= vector5.y;
                        }
                        vector.z = zDist;
                        this.m_lineVertices[j] = vector;
                    }
                }
            }
            else
            {
                int num4;
                int num5;
                int widthIdx = 0;
                int widthIdxAdd = 0;
                if (this.m_lineWidths.Length > 1)
                {
                    widthIdx = start;
                    widthIdxAdd = 1;
                }
                if (this.m_continuous)
                {
                    num5 = start * 4;
                    num4 = 1;
                }
                else
                {
                    num5 = start * 2;
                    num4 = 2;
                    widthIdx /= 2;
                }
                if (this.capLength == 0f)
                {
                    Vector3 vector6 = new Vector3(0f, 0f, 0f);
                    for (int k = start; k < end; k += num4)
                    {
                        if (useTransformMatrix)
                        {
                            vector = thisMatrix.MultiplyPoint3x4(this.points2[k]);
                            vector2 = thisMatrix.MultiplyPoint3x4(this.points2[k + 1]);
                        }
                        else
                        {
                            vector = this.points2[k];
                            vector2 = this.points2[k + 1];
                        }
                        if (this.useViewportCoords)
                        {
                            vector.x *= vector5.x;
                            vector.y *= vector5.y;
                            vector2.x *= vector5.x;
                            vector2.y *= vector5.y;
                        }
                        vector.z = zDist;
                        if ((vector.x == vector2.x) && (vector.y == vector2.y))
                        {
                            this.Skip(ref num5, ref widthIdx, ref vector, ref widthIdxAdd);
                        }
                        else
                        {
                            vector2.z = zDist;
                            vector3.x = vector2.y;
                            vector3.y = vector.x;
                            vector4.x = vector.y;
                            vector4.y = vector2.x;
                            vector6 = vector3 - vector4;
                            float num9 = 1f / Mathf.Sqrt((vector6.x * vector6.x) + (vector6.y * vector6.y));
                            vector6 = (Vector3)(vector6 * (num9 * this.m_lineWidths[widthIdx]));
                            this.m_lineVertices[num5] = vector - vector6;
                            this.m_lineVertices[num5 + 1] = vector + vector6;
                            if (this.smoothWidth && (k < (end - num4)))
                            {
                                vector6 = vector3 - vector4;
                                vector6 = (Vector3)(vector6 * (num9 * this.m_lineWidths[widthIdx + 1]));
                            }
                            this.m_lineVertices[num5 + 2] = vector2 - vector6;
                            this.m_lineVertices[num5 + 3] = vector2 + vector6;
                            num5 += 4;
                            widthIdx += widthIdxAdd;
                        }
                    }
                    if (this.m_joins == Joins.Weld)
                    {
                        if (this.m_continuous)
                        {
                            this.WeldJoins((start * 4) + ((start != 0) ? 0 : 4), end * 4, (this.Approximately2(this.points2[0], this.points2[this.points2.Length - 1]) && (this.m_minDrawIndex == 0)) && ((this.m_maxDrawIndex == (this.points2.Length - 1)) || (this.m_maxDrawIndex == 0)));
                        }
                        else
                        {
                            this.WeldJoinsDiscrete(start + 1, end, (this.Approximately2(this.points2[0], this.points2[this.points2.Length - 1]) && (this.m_minDrawIndex == 0)) && ((this.m_maxDrawIndex == (this.points2.Length - 1)) || (this.m_maxDrawIndex == 0)));
                        }
                    }
                }
                else
                {
                    Vector3 vector7 = new Vector3(0f, 0f, 0f);
                    for (int m = this.m_minDrawIndex; m < end; m += num4)
                    {
                        if (useTransformMatrix)
                        {
                            vector = thisMatrix.MultiplyPoint3x4(this.points2[m]);
                            vector2 = thisMatrix.MultiplyPoint3x4(this.points2[m + 1]);
                        }
                        else
                        {
                            vector = this.points2[m];
                            vector2 = this.points2[m + 1];
                        }
                        if (this.useViewportCoords)
                        {
                            vector.x *= vector5.x;
                            vector.y *= vector5.y;
                            vector2.x *= vector5.x;
                            vector2.y *= vector5.y;
                        }
                        vector.z = zDist;
                        if ((vector.x == vector2.x) && (vector.y == vector2.y))
                        {
                            this.Skip(ref num5, ref widthIdx, ref vector, ref widthIdxAdd);
                        }
                        else
                        {
                            vector2.z = zDist;
                            vector7 = vector2 - vector;
                            vector7 = (Vector3)(vector7 * (1f / Mathf.Sqrt((vector7.x * vector7.x) + (vector7.y * vector7.y))));
                            vector -= (Vector3)(vector7 * this.capLength);
                            vector2 += (Vector3)(vector7 * this.capLength);
                            vector3.x = vector7.y;
                            vector3.y = -vector7.x;
                            vector7 = (Vector3)(vector3 * this.m_lineWidths[widthIdx]);
                            this.m_lineVertices[num5] = vector - vector7;
                            this.m_lineVertices[num5 + 1] = vector + vector7;
                            if (this.smoothWidth && (m < (end - num4)))
                            {
                                vector7 = (Vector3)(vector3 * this.m_lineWidths[widthIdx + 1]);
                            }
                            this.m_lineVertices[num5 + 2] = vector2 - vector7;
                            this.m_lineVertices[num5 + 3] = vector2 + vector7;
                            num5 += 4;
                            widthIdx += widthIdxAdd;
                        }
                    }
                }
            }
        }

        private void Line3DContinuous(int start, int end, Matrix4x4 thisMatrix, bool useTransformMatrix)
        {
            if (cam3D == null)
            {
                LogError("The 3D camera no longer exists...if you have changed scenes, ensure that SetCamera3D is called in order to set it up.");
            }
            else if (this.m_1pixelLine)
            {
                Vector3 vector2 = !useTransformMatrix ? cam3D.WorldToScreenPoint(this.points3[start]) : cam3D.WorldToScreenPoint(thisMatrix.MultiplyPoint3x4(this.points3[start]));
                vector2.z = (vector2.z >= 0.15f) ? zDist : -zDist;
                int index = start * 2;
                for (int i = start; i < end; i++)
                {
                    Vector3 vector = vector2;
                    vector2 = !useTransformMatrix ? cam3D.WorldToScreenPoint(this.points3[i + 1]) : cam3D.WorldToScreenPoint(thisMatrix.MultiplyPoint3x4(this.points3[i + 1]));
                    vector2.z = (vector2.z >= 0.15f) ? zDist : -zDist;
                    this.m_lineVertices[index] = vector;
                    this.m_lineVertices[index + 1] = vector2;
                    index += 2;
                }
            }
            else
            {
                Vector3 vector5 = !useTransformMatrix ? cam3D.WorldToScreenPoint(this.points3[start]) : cam3D.WorldToScreenPoint(thisMatrix.MultiplyPoint3x4(this.points3[start]));
                vector5.z = (vector5.z >= 0.15f) ? zDist : -zDist;
                float num3 = 0f;
                int widthIdx = 0;
                int widthIdxAdd = 0;
                if (this.m_lineWidths.Length > 1)
                {
                    widthIdx = start;
                    widthIdxAdd = 1;
                }
                int idx = start * 4;
                Vector3 vector6 = Vector3.zero;
                Vector3 vector7 = Vector3.zero;
                for (int j = start; j < end; j++)
                {
                    Vector3 pos = vector5;
                    vector5 = !useTransformMatrix ? cam3D.WorldToScreenPoint(this.points3[j + 1]) : cam3D.WorldToScreenPoint(thisMatrix.MultiplyPoint3x4(this.points3[j + 1]));
                    if ((pos.x == vector5.x) && (pos.y == vector5.y))
                    {
                        this.Skip(ref idx, ref widthIdx, ref pos, ref widthIdxAdd);
                    }
                    else
                    {
                        vector5.z = (vector5.z >= 0.15f) ? zDist : -zDist;
                        vector6.x = vector5.y;
                        vector6.y = pos.x;
                        vector7.x = pos.y;
                        vector7.y = vector5.x;
                        Vector3 vector4 = vector6 - vector7;
                        num3 = 1f / Mathf.Sqrt((vector4.x * vector4.x) + (vector4.y * vector4.y));
                        vector4 = (Vector3)(vector4 * (num3 * this.m_lineWidths[widthIdx]));
                        this.m_lineVertices[idx] = pos - vector4;
                        this.m_lineVertices[idx + 1] = pos + vector4;
                        if (this.smoothWidth && (j < (end - 1)))
                        {
                            vector4 = vector6 - vector7;
                            vector4 = (Vector3)(vector4 * (num3 * this.m_lineWidths[widthIdx + 1]));
                        }
                        this.m_lineVertices[idx + 2] = vector5 - vector4;
                        this.m_lineVertices[idx + 3] = vector5 + vector4;
                        idx += 4;
                        widthIdx += widthIdxAdd;
                    }
                }
                if (this.m_joins == Joins.Weld)
                {
                    this.WeldJoins((start * 4) + 4, end * 4, (this.Approximately3(this.points3[0], this.points3[this.points3.Length - 1]) && (this.m_minDrawIndex == 0)) && ((this.m_maxDrawIndex == (this.points3.Length - 1)) || (this.m_maxDrawIndex == 0)));
                }
            }
        }

        private void Line3DDiscrete(int start, int end, Matrix4x4 thisMatrix, bool useTransformMatrix)
        {
            if (cam3D == null)
            {
                LogError("The 3D camera no longer exists...if you have changed scenes, ensure that SetCamera3D is called in order to set it up.");
            }
            else if (this.m_1pixelLine)
            {
                for (int i = start; i <= end; i++)
                {
                    Vector3 vector = !useTransformMatrix ? cam3D.WorldToScreenPoint(this.points3[i]) : cam3D.WorldToScreenPoint(thisMatrix.MultiplyPoint3x4(this.points3[i]));
                    vector.z = (vector.z >= 0.15f) ? zDist : -zDist;
                    this.m_lineVertices[i] = vector;
                }
            }
            else
            {
                Vector3 vector5 = Vector3.zero;
                Vector3 vector6 = Vector3.zero;
                float num2 = 0f;
                int widthIdx = 0;
                int widthIdxAdd = 0;
                if (this.m_lineWidths.Length > 1)
                {
                    widthIdx = start;
                    widthIdxAdd = 1;
                }
                int idx = start * 2;
                for (int j = start; j < end; j += 2)
                {
                    Vector3 vector2;
                    Vector3 vector3;
                    if (useTransformMatrix)
                    {
                        vector2 = cam3D.WorldToScreenPoint(thisMatrix.MultiplyPoint3x4(this.points3[j]));
                        vector3 = cam3D.WorldToScreenPoint(thisMatrix.MultiplyPoint3x4(this.points3[j + 1]));
                    }
                    else
                    {
                        vector2 = cam3D.WorldToScreenPoint(this.points3[j]);
                        vector3 = cam3D.WorldToScreenPoint(this.points3[j + 1]);
                    }
                    vector2.z = (vector2.z >= 0.15f) ? zDist : -zDist;
                    if ((vector2.x == vector3.x) && (vector2.y == vector3.y))
                    {
                        this.Skip(ref idx, ref widthIdx, ref vector2, ref widthIdxAdd);
                    }
                    else
                    {
                        vector3.z = (vector3.z >= 0.15f) ? zDist : -zDist;
                        vector5.x = vector3.y;
                        vector5.y = vector2.x;
                        vector6.x = vector2.y;
                        vector6.y = vector3.x;
                        Vector3 vector4 = vector5 - vector6;
                        num2 = 1f / Mathf.Sqrt((vector4.x * vector4.x) + (vector4.y * vector4.y));
                        vector4 = (Vector3)(vector4 * (num2 * this.m_lineWidths[widthIdx]));
                        this.m_lineVertices[idx] = vector2 - vector4;
                        this.m_lineVertices[idx + 1] = vector2 + vector4;
                        if (this.smoothWidth && (j < (end - 2)))
                        {
                            vector4 = vector5 - vector6;
                            vector4 = (Vector3)(vector4 * (num2 * this.m_lineWidths[widthIdx + 1]));
                        }
                        this.m_lineVertices[idx + 2] = vector3 - vector4;
                        this.m_lineVertices[idx + 3] = vector3 + vector4;
                        idx += 4;
                        widthIdx += widthIdxAdd;
                    }
                }
                if (this.m_joins == Joins.Weld)
                {
                    this.WeldJoinsDiscrete(start + 1, end, (this.Approximately3(this.points3[0], this.points3[this.points3.Length - 1]) && (this.m_minDrawIndex == 0)) && ((this.m_maxDrawIndex == (this.points3.Length - 1)) || (this.m_maxDrawIndex == 0)));
                }
            }
        }

        public static void LineManagerCheckDistance()
        {
            lineManager.StartCheckDistance();
        }

        public static void LineManagerDisable()
        {
            lineManager.DisableIfUnused();
        }

        public static void LineManagerEnable()
        {
            lineManager.EnableIfUsed();
        }

        private static void LogError(string errorString)
        {
            Debug.LogError(errorString);
            error = true;
        }

        public void MakeCircle(Vector3 origin, float radius)
        {
            this.MakeEllipse(origin, Vector3.forward, radius, radius, this.GetSegmentNumber(), 0f, 0);
        }

        public void MakeCircle(Vector3 origin, float radius, int segments)
        {
            this.MakeEllipse(origin, Vector3.forward, radius, radius, segments, 0f, 0);
        }

        public void MakeCircle(Vector3 origin, Vector3 upVector, float radius)
        {
            this.MakeEllipse(origin, upVector, radius, radius, this.GetSegmentNumber(), 0f, 0);
        }

        public void MakeCircle(Vector3 origin, float radius, int segments, int index)
        {
            this.MakeEllipse(origin, Vector3.forward, radius, radius, segments, 0f, index);
        }

        public void MakeCircle(Vector3 origin, float radius, int segments, float pointRotation)
        {
            this.MakeEllipse(origin, Vector3.forward, radius, radius, segments, pointRotation, 0);
        }

        public void MakeCircle(Vector3 origin, Vector3 upVector, float radius, int segments)
        {
            this.MakeEllipse(origin, upVector, radius, radius, segments, 0f, 0);
        }

        public void MakeCircle(Vector3 origin, float radius, int segments, float pointRotation, int index)
        {
            this.MakeEllipse(origin, Vector3.forward, radius, radius, segments, pointRotation, index);
        }

        public void MakeCircle(Vector3 origin, Vector3 upVector, float radius, int segments, int index)
        {
            this.MakeEllipse(origin, upVector, radius, radius, segments, 0f, index);
        }

        public void MakeCircle(Vector3 origin, Vector3 upVector, float radius, int segments, float pointRotation)
        {
            this.MakeEllipse(origin, upVector, radius, radius, segments, pointRotation, 0);
        }

        public void MakeCircle(Vector3 origin, Vector3 upVector, float radius, int segments, float pointRotation, int index)
        {
            this.MakeEllipse(origin, upVector, radius, radius, segments, pointRotation, index);
        }

        public void MakeCube(Vector3 position, float xSize, float ySize, float zSize)
        {
            this.MakeCube(position, xSize, ySize, zSize, 0);
        }

        public void MakeCube(Vector3 position, float xSize, float ySize, float zSize, int index)
        {
            if (this.m_continuous)
            {
                LogError("VectorLine.MakeCube only works with a discrete line");
            }
            else if (this.m_is2D)
            {
                LogError("VectorLine.MakeCube can only be used with a Vector3 array, which \"" + this.name + "\" doesn't have");
            }
            else if ((index + 0x18) > this.points3.Length)
            {
                if (index == 0)
                {
                    LogError("VectorLine.MakeCube: The length of the Vector3 array needs to be at least 24 for \"" + this.name + "\"");
                }
                else
                {
                    LogError(string.Concat(new object[] { "Calling VectorLine.MakeCube with an index of ", index, " would exceed the length of the Vector3 array for \"", this.name, "\"" }));
                }
            }
            else
            {
                xSize /= 2f;
                ySize /= 2f;
                zSize /= 2f;
                this.points3[index] = position + new Vector3(-xSize, ySize, -zSize);
                this.points3[index + 1] = position + new Vector3(xSize, ySize, -zSize);
                this.points3[index + 2] = position + new Vector3(xSize, ySize, -zSize);
                this.points3[index + 3] = position + new Vector3(xSize, ySize, zSize);
                this.points3[index + 4] = position + new Vector3(xSize, ySize, zSize);
                this.points3[index + 5] = position + new Vector3(-xSize, ySize, zSize);
                this.points3[index + 6] = position + new Vector3(-xSize, ySize, zSize);
                this.points3[index + 7] = position + new Vector3(-xSize, ySize, -zSize);
                this.points3[index + 8] = position + new Vector3(-xSize, -ySize, -zSize);
                this.points3[index + 9] = position + new Vector3(-xSize, ySize, -zSize);
                this.points3[index + 10] = position + new Vector3(xSize, -ySize, -zSize);
                this.points3[index + 11] = position + new Vector3(xSize, ySize, -zSize);
                this.points3[index + 12] = position + new Vector3(-xSize, -ySize, zSize);
                this.points3[index + 13] = position + new Vector3(-xSize, ySize, zSize);
                this.points3[index + 14] = position + new Vector3(xSize, -ySize, zSize);
                this.points3[index + 15] = position + new Vector3(xSize, ySize, zSize);
                this.points3[index + 0x10] = position + new Vector3(-xSize, -ySize, -zSize);
                this.points3[index + 0x11] = position + new Vector3(xSize, -ySize, -zSize);
                this.points3[index + 0x12] = position + new Vector3(xSize, -ySize, -zSize);
                this.points3[index + 0x13] = position + new Vector3(xSize, -ySize, zSize);
                this.points3[index + 20] = position + new Vector3(xSize, -ySize, zSize);
                this.points3[index + 0x15] = position + new Vector3(-xSize, -ySize, zSize);
                this.points3[index + 0x16] = position + new Vector3(-xSize, -ySize, zSize);
                this.points3[index + 0x17] = position + new Vector3(-xSize, -ySize, -zSize);
            }
        }

        public void MakeCurve(Vector2[] curvePoints)
        {
            this.MakeCurve(curvePoints, this.GetSegmentNumber(), 0);
        }

        public void MakeCurve(Vector3[] curvePoints)
        {
            this.MakeCurve(curvePoints, this.GetSegmentNumber(), 0);
        }

        public void MakeCurve(Vector2[] curvePoints, int segments)
        {
            this.MakeCurve(curvePoints, segments, 0);
        }

        public void MakeCurve(Vector3[] curvePoints, int segments)
        {
            this.MakeCurve(curvePoints, segments, 0);
        }

        public void MakeCurve(Vector2[] curvePoints, int segments, int index)
        {
            if (curvePoints.Length != 4)
            {
                LogError("VectorLine.MakeCurve needs exactly 4 points in the curve points array");
            }
            else
            {
                this.MakeCurve(curvePoints[0], curvePoints[1], curvePoints[2], curvePoints[3], segments, index);
            }
        }

        public void MakeCurve(Vector3[] curvePoints, int segments, int index)
        {
            if (curvePoints.Length != 4)
            {
                LogError("VectorLine.MakeCurve needs exactly 4 points in the curve points array");
            }
            else
            {
                this.MakeCurve(curvePoints[0], curvePoints[1], curvePoints[2], curvePoints[3], segments, index);
            }
        }

        public void MakeCurve(Vector3 anchor1, Vector3 control1, Vector3 anchor2, Vector3 control2)
        {
            this.MakeCurve(anchor1, control1, anchor2, control2, this.GetSegmentNumber(), 0);
        }

        public void MakeCurve(Vector3 anchor1, Vector3 control1, Vector3 anchor2, Vector3 control2, int segments)
        {
            this.MakeCurve(anchor1, control1, anchor2, control2, segments, 0);
        }

        public void MakeCurve(Vector3 anchor1, Vector3 control1, Vector3 anchor2, Vector3 control2, int segments, int index)
        {
            if (this.CheckArrayLength(FunctionName.MakeCurve, segments, index))
            {
                if (this.m_continuous)
                {
                    int num = !this.m_isPoints ? (segments + 1) : segments;
                    if (this.m_is2D)
                    {
                        Vector2 vector = anchor1;
                        Vector2 vector2 = anchor2;
                        Vector2 vector3 = control1;
                        Vector2 vector4 = control2;
                        for (int i = 0; i < num; i++)
                        {
                            this.points2[index + i] = GetBezierPoint(ref vector, ref vector3, ref vector2, ref vector4, ((float)i) / ((float)segments));
                        }
                    }
                    else
                    {
                        for (int j = 0; j < num; j++)
                        {
                            this.points3[index + j] = GetBezierPoint3D(ref anchor1, ref control1, ref anchor2, ref control2, ((float)j) / ((float)segments));
                        }
                    }
                }
                else
                {
                    int num4 = 0;
                    if (this.m_is2D)
                    {
                        Vector2 vector5 = anchor1;
                        Vector2 vector6 = anchor2;
                        Vector2 vector7 = control1;
                        Vector2 vector8 = control2;
                        for (int k = 0; k < segments; k++)
                        {
                            this.points2[index + num4++] = GetBezierPoint(ref vector5, ref vector7, ref vector6, ref vector8, ((float)k) / ((float)segments));
                            this.points2[index + num4++] = GetBezierPoint(ref vector5, ref vector7, ref vector6, ref vector8, ((float)(k + 1)) / ((float)segments));
                        }
                    }
                    else
                    {
                        for (int m = 0; m < segments; m++)
                        {
                            this.points3[index + num4++] = GetBezierPoint3D(ref anchor1, ref control1, ref anchor2, ref control2, ((float)m) / ((float)segments));
                            this.points3[index + num4++] = GetBezierPoint3D(ref anchor1, ref control1, ref anchor2, ref control2, ((float)(m + 1)) / ((float)segments));
                        }
                    }
                }
            }
        }

        public void MakeEllipse(Vector3 origin, float xRadius, float yRadius)
        {
            this.MakeEllipse(origin, Vector3.forward, xRadius, yRadius, this.GetSegmentNumber(), 0f, 0);
        }

        public void MakeEllipse(Vector3 origin, float xRadius, float yRadius, int segments)
        {
            this.MakeEllipse(origin, Vector3.forward, xRadius, yRadius, segments, 0f, 0);
        }

        public void MakeEllipse(Vector3 origin, Vector3 upVector, float xRadius, float yRadius)
        {
            this.MakeEllipse(origin, upVector, xRadius, yRadius, this.GetSegmentNumber(), 0f, 0);
        }

        public void MakeEllipse(Vector3 origin, float xRadius, float yRadius, int segments, int index)
        {
            this.MakeEllipse(origin, Vector3.forward, xRadius, yRadius, segments, 0f, index);
        }

        public void MakeEllipse(Vector3 origin, float xRadius, float yRadius, int segments, float pointRotation)
        {
            this.MakeEllipse(origin, Vector3.forward, xRadius, yRadius, segments, pointRotation, 0);
        }

        public void MakeEllipse(Vector3 origin, Vector3 upVector, float xRadius, float yRadius, int segments)
        {
            this.MakeEllipse(origin, upVector, xRadius, yRadius, segments, 0f, 0);
        }

        public void MakeEllipse(Vector3 origin, Vector3 upVector, float xRadius, float yRadius, int segments, int index)
        {
            this.MakeEllipse(origin, upVector, xRadius, yRadius, segments, 0f, index);
        }

        public void MakeEllipse(Vector3 origin, Vector3 upVector, float xRadius, float yRadius, int segments, float pointRotation)
        {
            this.MakeEllipse(origin, upVector, xRadius, yRadius, segments, pointRotation, 0);
        }

        public void MakeEllipse(Vector3 origin, Vector3 upVector, float xRadius, float yRadius, int segments, float pointRotation, int index)
        {
            if (segments < 3)
            {
                LogError("VectorLine.MakeEllipse needs at least 3 segments");
            }
            else if (this.CheckArrayLength(FunctionName.MakeEllipse, segments, index))
            {
                float num = (360f / ((float)segments)) * 0.01745329f;
                float num2 = -pointRotation * 0.01745329f;
                if (this.m_continuous)
                {
                    int num3 = 0;
                    if (this.m_is2D)
                    {
                        Vector2 vector = origin;
                        num3 = 0;
                        while (num3 < segments)
                        {
                            this.points2[index + num3] = vector + new Vector2(0.5f + (Mathf.Cos(num2) * xRadius), 0.5f + (Mathf.Sin(num2) * yRadius));
                            num2 += num;
                            num3++;
                        }
                        if (!this.m_isPoints)
                        {
                            this.points2[index + num3] = this.points2[index + (num3 - segments)];
                        }
                    }
                    else
                    {
                        Matrix4x4 matrixx = Matrix4x4.TRS(Vector3.zero, Quaternion.LookRotation(-upVector, upVector), Vector3.one);
                        num3 = 0;
                        while (num3 < segments)
                        {
                            this.points3[index + num3] = origin + matrixx.MultiplyPoint3x4(new Vector3(Mathf.Cos(num2) * xRadius, Mathf.Sin(num2) * yRadius, 0f));
                            num2 += num;
                            num3++;
                        }
                        if (!this.m_isPoints)
                        {
                            this.points3[index + num3] = this.points3[index + (num3 - segments)];
                        }
                    }
                }
                else if (this.m_is2D)
                {
                    Vector2 vector2 = origin;
                    for (int i = 0; i < (segments * 2); i++)
                    {
                        this.points2[index + i] = vector2 + new Vector2(0.5f + (Mathf.Cos(num2) * xRadius), 0.5f + (Mathf.Sin(num2) * yRadius));
                        num2 += num;
                        i++;
                        this.points2[index + i] = vector2 + new Vector2(0.5f + (Mathf.Cos(num2) * xRadius), 0.5f + (Mathf.Sin(num2) * yRadius));
                    }
                }
                else
                {
                    Matrix4x4 matrixx2 = Matrix4x4.TRS(Vector3.zero, Quaternion.LookRotation(-upVector, upVector), Vector3.one);
                    for (int j = 0; j < (segments * 2); j++)
                    {
                        this.points3[index + j] = origin + matrixx2.MultiplyPoint3x4(new Vector3(Mathf.Cos(num2) * xRadius, Mathf.Sin(num2) * yRadius, 0f));
                        num2 += num;
                        j++;
                        this.points3[index + j] = origin + matrixx2.MultiplyPoint3x4(new Vector3(Mathf.Cos(num2) * xRadius, Mathf.Sin(num2) * yRadius, 0f));
                    }
                }
            }
        }

        public static VectorLine MakeLine(string name, Vector2[] points)
        {
            if (!defaultsSet)
            {
                PrintMakeLineError();
                return null;
            }
            return new VectorLine(name, points, defaultLineColor, defaultLineMaterial, defaultLineWidth, defaultLineType, defaultJoins)
            {
                capLength = defaultCapLength,
                depth = defaultLineDepth
            };
        }

        public static VectorLine MakeLine(string name, Vector3[] points)
        {
            if (!defaultsSet)
            {
                PrintMakeLineError();
                return null;
            }
            return new VectorLine(name, points, defaultLineColor, defaultLineMaterial, defaultLineWidth, defaultLineType, defaultJoins)
            {
                capLength = defaultCapLength,
                depth = defaultLineDepth
            };
        }

        public static VectorLine MakeLine(string name, Vector2[] points, Color[] colors)
        {
            if (!defaultsSet)
            {
                PrintMakeLineError();
                return null;
            }
            return new VectorLine(name, points, colors, defaultLineMaterial, defaultLineWidth, defaultLineType, defaultJoins)
            {
                capLength = defaultCapLength,
                depth = defaultLineDepth
            };
        }

        public static VectorLine MakeLine(string name, Vector2[] points, Color color)
        {
            if (!defaultsSet)
            {
                PrintMakeLineError();
                return null;
            }
            return new VectorLine(name, points, color, defaultLineMaterial, defaultLineWidth, defaultLineType, defaultJoins)
            {
                capLength = defaultCapLength,
                depth = defaultLineDepth
            };
        }

        public static VectorLine MakeLine(string name, Vector3[] points, Color[] colors)
        {
            if (!defaultsSet)
            {
                PrintMakeLineError();
                return null;
            }
            return new VectorLine(name, points, colors, defaultLineMaterial, defaultLineWidth, defaultLineType, defaultJoins)
            {
                capLength = defaultCapLength,
                depth = defaultLineDepth
            };
        }

        public static VectorLine MakeLine(string name, Vector3[] points, Color color)
        {
            if (!defaultsSet)
            {
                PrintMakeLineError();
                return null;
            }
            return new VectorLine(name, points, color, defaultLineMaterial, defaultLineWidth, defaultLineType, defaultJoins)
            {
                capLength = defaultCapLength,
                depth = defaultLineDepth
            };
        }

        public void MakeRect(Rect rect)
        {
            this.MakeRect(new Vector2(rect.x, rect.y), new Vector2(rect.x + rect.width, rect.y - rect.height), 0);
        }

        public void MakeRect(Rect rect, int index)
        {
            this.MakeRect(new Vector2(rect.x, rect.y), new Vector2(rect.x + rect.width, rect.y - rect.height), index);
        }

        public void MakeRect(Vector3 topLeft, Vector3 bottomRight)
        {
            this.MakeRect(topLeft, bottomRight, 0);
        }

        public void MakeRect(Vector3 topLeft, Vector3 bottomRight, int index)
        {
            if (this.m_continuous)
            {
                if ((index + 5) > this.pointsLength)
                {
                    if (index == 0)
                    {
                        LogError("VectorLine.MakeRect: The length of the array for continuous lines needs to be at least 5 for \"" + this.name + "\"");
                    }
                    else
                    {
                        LogError(string.Concat(new object[] { "Calling VectorLine.MakeRect with an index of ", index, " would exceed the length of the Vector2 array for \"", this.name, "\"" }));
                    }
                }
                else if (this.m_is2D)
                {
                    this.points2[index] = new Vector2(topLeft.x, topLeft.y);
                    this.points2[index + 1] = new Vector2(bottomRight.x, topLeft.y);
                    this.points2[index + 2] = new Vector2(bottomRight.x, bottomRight.y);
                    this.points2[index + 3] = new Vector2(topLeft.x, bottomRight.y);
                    this.points2[index + 4] = new Vector2(topLeft.x, topLeft.y);
                }
                else
                {
                    this.points3[index] = new Vector3(topLeft.x, topLeft.y, topLeft.z);
                    this.points3[index + 1] = new Vector3(bottomRight.x, topLeft.y, topLeft.z);
                    this.points3[index + 2] = new Vector3(bottomRight.x, bottomRight.y, bottomRight.z);
                    this.points3[index + 3] = new Vector3(topLeft.x, bottomRight.y, bottomRight.z);
                    this.points3[index + 4] = new Vector3(topLeft.x, topLeft.y, topLeft.z);
                }
            }
            else if ((index + 8) > this.pointsLength)
            {
                if (index == 0)
                {
                    LogError("VectorLine.MakeRect: The length of the array for discrete lines needs to be at least 8 for \"" + this.name + "\"");
                }
                else
                {
                    LogError(string.Concat(new object[] { "Calling VectorLine.MakeRect with an index of ", index, " would exceed the length of the Vector2 array for \"", this.name, "\"" }));
                }
            }
            else if (this.m_is2D)
            {
                this.points2[index] = new Vector2(topLeft.x, topLeft.y);
                this.points2[index + 1] = new Vector2(bottomRight.x, topLeft.y);
                this.points2[index + 2] = new Vector2(bottomRight.x, topLeft.y);
                this.points2[index + 3] = new Vector2(bottomRight.x, bottomRight.y);
                this.points2[index + 4] = new Vector2(bottomRight.x, bottomRight.y);
                this.points2[index + 5] = new Vector2(topLeft.x, bottomRight.y);
                this.points2[index + 6] = new Vector2(topLeft.x, bottomRight.y);
                this.points2[index + 7] = new Vector2(topLeft.x, topLeft.y);
            }
            else
            {
                this.points3[index] = new Vector3(topLeft.x, topLeft.y, topLeft.z);
                this.points3[index + 1] = new Vector3(bottomRight.x, topLeft.y, topLeft.z);
                this.points3[index + 2] = new Vector3(bottomRight.x, topLeft.y, topLeft.z);
                this.points3[index + 3] = new Vector3(bottomRight.x, bottomRight.y, bottomRight.z);
                this.points3[index + 4] = new Vector3(bottomRight.x, bottomRight.y, bottomRight.z);
                this.points3[index + 5] = new Vector3(topLeft.x, bottomRight.y, bottomRight.z);
                this.points3[index + 6] = new Vector3(topLeft.x, bottomRight.y, bottomRight.z);
                this.points3[index + 7] = new Vector3(topLeft.x, topLeft.y, topLeft.z);
            }
        }

        public void MakeSpline(Vector2[] splinePoints)
        {
            this.MakeSpline(splinePoints, null, this.GetSegmentNumber(), 0, false);
        }

        public void MakeSpline(Vector3[] splinePoints)
        {
            this.MakeSpline(null, splinePoints, this.GetSegmentNumber(), 0, false);
        }

        public void MakeSpline(Vector2[] splinePoints, bool loop)
        {
            this.MakeSpline(splinePoints, null, this.GetSegmentNumber(), 0, loop);
        }

        public void MakeSpline(Vector2[] splinePoints, int segments)
        {
            this.MakeSpline(splinePoints, null, segments, 0, false);
        }

        public void MakeSpline(Vector3[] splinePoints, bool loop)
        {
            this.MakeSpline(null, splinePoints, this.GetSegmentNumber(), 0, loop);
        }

        public void MakeSpline(Vector3[] splinePoints, int segments)
        {
            this.MakeSpline(null, splinePoints, segments, 0, false);
        }

        public void MakeSpline(Vector2[] splinePoints, int segments, bool loop)
        {
            this.MakeSpline(splinePoints, null, segments, 0, loop);
        }

        public void MakeSpline(Vector2[] splinePoints, int segments, int index)
        {
            this.MakeSpline(splinePoints, null, segments, index, false);
        }

        public void MakeSpline(Vector3[] splinePoints, int segments, bool loop)
        {
            this.MakeSpline(null, splinePoints, segments, 0, loop);
        }

        public void MakeSpline(Vector3[] splinePoints, int segments, int index)
        {
            this.MakeSpline(null, splinePoints, segments, index, false);
        }

        public void MakeSpline(Vector2[] splinePoints, int segments, int index, bool loop)
        {
            this.MakeSpline(splinePoints, null, segments, index, loop);
        }

        public void MakeSpline(Vector3[] splinePoints, int segments, int index, bool loop)
        {
            this.MakeSpline(null, splinePoints, segments, index, loop);
        }

        private void MakeSpline(Vector2[] splinePoints2, Vector3[] splinePoints3, int segments, int index, bool loop)
        {
            int num = (splinePoints2 == null) ? splinePoints3.Length : splinePoints2.Length;
            if (num < 2)
            {
                LogError("VectorLine.MakeSpline needs at least 2 spline points");
            }
            else if ((splinePoints2 != null) && !this.m_is2D)
            {
                LogError("VectorLine.MakeSpline was called with a Vector2 spline points array, but the line uses Vector3 points");
            }
            else if ((splinePoints3 != null) && this.m_is2D)
            {
                LogError("VectorLine.MakeSpline was called with a Vector3 spline points array, but the line uses Vector2 points");
            }
            else if (this.CheckArrayLength(FunctionName.MakeSpline, segments, index))
            {
                int num2 = index;
                int num3 = !loop ? (num - 1) : num;
                float num4 = (1f / ((float)segments)) * num3;
                float num6 = 0f;
                int num8 = 0;
                int num9 = 0;
                int num10 = 0;
                int num7 = 0;
                while (num7 < num3)
                {
                    float num5;
                    num8 = num7 - 1;
                    num9 = num7 + 1;
                    num10 = num7 + 2;
                    if (num8 < 0)
                    {
                        num8 = !loop ? 0 : (num3 - 1);
                    }
                    if (loop && (num9 > (num3 - 1)))
                    {
                        num9 -= num3;
                    }
                    if (num10 > (num3 - 1))
                    {
                        num10 = !loop ? num3 : (num10 - num3);
                    }
                    if (this.m_continuous)
                    {
                        if (this.m_is2D)
                        {
                            num5 = num6;
                            while (num5 <= 1f)
                            {
                                this.points2[num2++] = GetSplinePoint(ref splinePoints2[num8], ref splinePoints2[num7], ref splinePoints2[num9], ref splinePoints2[num10], num5);
                                num5 += num4;
                            }
                        }
                        else
                        {
                            num5 = num6;
                            while (num5 <= 1f)
                            {
                                this.points3[num2++] = GetSplinePoint3D(ref splinePoints3[num8], ref splinePoints3[num7], ref splinePoints3[num9], ref splinePoints3[num10], num5);
                                num5 += num4;
                            }
                        }
                    }
                    else if (this.m_is2D)
                    {
                        num5 = num6;
                        while (num5 <= 1f)
                        {
                            this.points2[num2++] = GetSplinePoint(ref splinePoints2[num8], ref splinePoints2[num7], ref splinePoints2[num9], ref splinePoints2[num10], num5);
                            if ((num2 > (index + 1)) && (num2 < (index + (segments * 2))))
                            {
                                this.points2[num2++] = this.points2[num2 - 2];
                            }
                            num5 += num4;
                        }
                    }
                    else
                    {
                        num5 = num6;
                        while (num5 <= 1f)
                        {
                            this.points3[num2++] = GetSplinePoint3D(ref splinePoints3[num8], ref splinePoints3[num7], ref splinePoints3[num9], ref splinePoints3[num10], num5);
                            if ((num2 > (index + 1)) && (num2 < (index + (segments * 2))))
                            {
                                this.points3[num2++] = this.points3[num2 - 2];
                            }
                            num5 += num4;
                        }
                    }
                    num6 = num5 - 1f;
                    num7++;
                }
                if ((this.m_continuous && (num2 < (index + (segments + 1)))) || (!this.m_continuous && (num2 < (index + (segments * 2)))))
                {
                    if (this.m_is2D)
                    {
                        this.points2[num2] = GetSplinePoint(ref splinePoints2[num8], ref splinePoints2[num7 - 1], ref splinePoints2[num9], ref splinePoints2[num10], 1f);
                    }
                    else
                    {
                        this.points3[num2] = GetSplinePoint3D(ref splinePoints3[num8], ref splinePoints3[num7 - 1], ref splinePoints3[num9], ref splinePoints3[num10], 1f);
                    }
                }
            }
        }

        public void MakeText(string text, Vector3 startPos, float size)
        {
            this.MakeText(text, startPos, size, 1f, 1.5f, true);
        }

        public void MakeText(string text, Vector3 startPos, float size, bool uppercaseOnly)
        {
            this.MakeText(text, startPos, size, 1f, 1.5f, uppercaseOnly);
        }

        public void MakeText(string text, Vector3 startPos, float size, float charSpacing, float lineSpacing)
        {
            this.MakeText(text, startPos, size, charSpacing, lineSpacing, true);
        }

        public void MakeText(string text, Vector3 startPos, float size, float charSpacing, float lineSpacing, bool uppercaseOnly)
        {
            if (this.m_continuous)
            {
                LogError("VectorLine.MakeText only works with a discrete line");
            }
            else
            {
                int newSize = 0;
                for (int i = 0; i < text.Length; i++)
                {
                    int index = Convert.ToInt32(text[i]);
                    if ((index < 0) || (index > 0x100))
                    {
                        LogError("VectorLine.MakeText: Character '" + text[i] + "' is not valid");
                        return;
                    }
                    if ((uppercaseOnly && (index >= 0x61)) && (index <= 0x7a))
                    {
                        index -= 0x20;
                    }
                    if (VectorChar.data[index] != null)
                    {
                        newSize += VectorChar.data[index].Length;
                    }
                }
                if (newSize > this.pointsLength)
                {
                    this.Resize(newSize);
                }
                else if (newSize < this.pointsLength)
                {
                    this.ZeroPoints(newSize);
                }
                float num4 = 0f;
                float num5 = 0f;
                int num6 = 0;
                Vector2 vector = new Vector2(size, size);
                for (int j = 0; j < text.Length; j++)
                {
                    int num8 = Convert.ToInt32(text[j]);
                    switch (num8)
                    {
                        case 10:
                            num5 -= lineSpacing;
                            num4 = 0f;
                            break;

                        case 0x20:
                            num4 += charSpacing;
                            break;

                        default:
                            {
                                if ((uppercaseOnly && (num8 >= 0x61)) && (num8 <= 0x7a))
                                {
                                    num8 -= 0x20;
                                }
                                int length = 0;
                                if (VectorChar.data[num8] != null)
                                {
                                    length = VectorChar.data[num8].Length;
                                }
                                else
                                {
                                    num4 += charSpacing;
                                    break;
                                }
                                if (this.m_is2D)
                                {
                                    for (int k = 0; k < length; k++)
                                    {
                                        this.points2[num6++] = Vector2.Scale(VectorChar.data[num8][k] + new Vector2(num4, num5), vector) + (Vector2)startPos;
                                    }
                                }
                                else
                                {
                                    for (int m = 0; m < length; m++)
                                    {
                                        this.points3[num6++] = Vector3.Scale((Vector3)VectorChar.data[num8][m] + new Vector3(num4, num5, 0f), vector) + startPos;
                                    }
                                }
                                num4 += charSpacing;
                                break;
                            }
                    }
                }
            }
        }

        public void MakeWireframe(Mesh mesh)
        {
            if (this.m_continuous)
            {
                LogError("VectorLine.MakeWireframe only works with a discrete line");
            }
            else if (this.m_is2D)
            {
                LogError("VectorLine.MakeWireframe can only be used with a Vector3 array, which \"" + this.name + "\" doesn't have");
            }
            else if (mesh == null)
            {
                LogError("VectorLine.MakeWireframe can't use a null mesh");
            }
            else
            {
                int[] numArray = mesh.triangles;
                Vector3[] vectorArray = mesh.vertices;
                Dictionary<Vector3Pair, bool> pairs = new Dictionary<Vector3Pair, bool>();
                List<Vector3> linePoints = new List<Vector3>();
                for (int i = 0; i < numArray.Length; i += 3)
                {
                    CheckPairPoints(pairs, vectorArray[numArray[i]], vectorArray[numArray[i + 1]], linePoints);
                    CheckPairPoints(pairs, vectorArray[numArray[i + 1]], vectorArray[numArray[i + 2]], linePoints);
                    CheckPairPoints(pairs, vectorArray[numArray[i + 2]], vectorArray[numArray[i]], linePoints);
                }
                if (linePoints.Count > this.points3.Length)
                {
                    Array.Resize<Vector3>(ref this.points3, linePoints.Count);
                    this.Resize(linePoints.Count);
                }
                else if (linePoints.Count < this.points3.Length)
                {
                    this.ZeroPoints(linePoints.Count);
                }
                Array.Copy(linePoints.ToArray(), this.points3, linePoints.Count);
            }
        }

        private int MaxSegmentIndex() { return (!this.m_isPoints ? (!this.m_continuous ? (this.pointsLength / 2) : (this.pointsLength - 1)) : this.pointsLength); }

        private static void PrintMakeLineError()
        {
            LogError("VectorLine.MakeLine: Must call SetLineParameters before using MakeLine with these parameters");
        }

        private void RebuildMesh()
        {
            if (!this.m_continuous && ((this.m_pointsLength % 2) != 0))
            {
                LogError("VectorLine.Resize: Must have an even points array length for \"" + this.name + "\" when using LineType.Discrete");
            }
            else
            {
                this.m_mesh.Clear();
                Color[] colors = this.SetColor(this.m_lineColors[0], !this.m_continuous ? LineType.Discrete : LineType.Continuous, this.m_pointsLength, this.m_isPoints);
                if (this.m_lineWidths.Length > 1)
                {
                    float lineWidth = this.lineWidth;
                    this.m_lineWidths = new float[this.m_pointsLength];
                    this.lineWidth = lineWidth;
                }
                this.BuildMesh(colors);
            }
        }

        private void RedoFillLine()
        {
            if (this.m_capType != EndCap.None)
            {
                this.RemoveEndCapVertices();
            }
            this.SetupTriangles();
            if (this.m_capType != EndCap.None)
            {
                this.AddEndCap();
            }
        }

        private void RedoLine(bool use1Pixel)
        {
            int num;
            int num2;
            int num3;
            this.m_1pixelLine = use1Pixel;
            if (this.m_isPoints)
            {
                num = 0;
                num2 = 1;
                num3 = this.m_vertexCount;
            }
            else if (use1Pixel)
            {
                num = 2;
                num2 = 4;
                num3 = this.m_vertexCount / 4;
            }
            else
            {
                num = 1;
                num2 = 2;
                num3 = this.m_vertexCount / 2;
            }
            Color[] colors = new Color[num3];
            int vertexCount = this.m_vertexCount;
            int num5 = 0;
            for (int i = num; i < vertexCount; i += num2)
            {
                colors[num5++] = this.m_lineColors[i];
            }
            this.m_mesh.Clear();
            this.BuildMesh(colors);
        }

        public static void RemoveEndCap(string name)
        {
            if (!capDictionary.ContainsKey(name))
            {
                LogError("VectorLine: RemoveEndCap: \"" + name + "\" has not been set up");
            }
            else
            {
                Object.Destroy(capDictionary[name].texture);
                Object.Destroy(capDictionary[name].material);
                capDictionary.Remove(name);
            }
        }

        private void RemoveEndCapVertices()
        {
            Array.Resize<Vector3>(ref this.m_lineVertices, this.m_vertexCount);
            Array.Resize<Vector2>(ref this.m_lineUVs, this.m_vertexCount);
            Array.Resize<Color32>(ref this.m_lineColors, this.m_vertexCount);
            this.m_mesh.subMeshCount = (1);
            Material[] materialArray = new Material[] { this.m_vectorObject.GetComponent<Renderer>().materials[0] };
            this.m_vectorObject.GetComponent<Renderer>().materials = (materialArray);
        }

        public void ResetTextureScale()
        {
            if (!this.m_1pixelLine)
            {
                int length = this.m_lineUVs.Length;
                for (int i = 0; i < length; i += 4)
                {
                    this.m_lineUVs[i].x = 0f;
                    this.m_lineUVs[i + 1].x = 0f;
                    this.m_lineUVs[i + 2].x = 1f;
                    this.m_lineUVs[i + 3].x = 1f;
                }
                this.m_mesh.uv = (this.m_lineUVs);
            }
        }

        public void Resize(Vector2[] linePoints)
        {
            if (!this.m_is2D)
            {
                LogError("Must supply a Vector3 array instead of a Vector2 array for \"" + this.name + "\"");
            }
            else
            {
                this.points2 = linePoints;
                this.m_pointsLength = linePoints.Length;
                this.RebuildMesh();
            }
        }

        public void Resize(Vector3[] linePoints)
        {
            if (this.m_is2D)
            {
                LogError("Must supply a Vector2 array instead of a Vector3 array for \"" + this.name + "\"");
            }
            else
            {
                this.points3 = linePoints;
                this.m_pointsLength = linePoints.Length;
                this.RebuildMesh();
            }
        }

        public void Resize(int newSize)
        {
            if (this.m_is2D)
            {
                this.points2 = new Vector2[newSize];
            }
            else
            {
                this.points3 = new Vector3[newSize];
            }
            this.m_pointsLength = newSize;
            this.RebuildMesh();
        }

        public bool Selected(Vector2 p)
        {
            int num;
            return this.Selected(p, 0, out num);
        }

        public bool Selected(Vector2 p, out int index) { return this.Selected(p, 0, out index); }

        public bool Selected(Vector2 p, int extraDistance, out int index)
        {
            Vector2 vector3;
            int num = (this.m_lineWidths.Length != 1) ? 1 : 0;
            int num2 = !this.m_continuous ? ((this.m_drawStart / 2) - num) : (this.m_drawStart - num);
            if (this.m_lineWidths.Length == 1)
            {
                num = 0;
                num2 = 0;
            }
            else
            {
                num = 1;
            }
            int drawEnd = this.m_drawEnd;
            bool flag = this.m_useTransform != null;
            Matrix4x4 matrixx = !flag ? Matrix4x4.identity : this.m_useTransform.localToWorldMatrix;
            Vector2 vector = new Vector2((float)Screen.width, (float)Screen.height);
            if (this.m_isPoints)
            {
                Vector2 vector2;
                if (drawEnd == this.pointsLength)
                {
                    drawEnd--;
                }
                if (this.m_is2D)
                {
                    for (int k = this.m_drawStart; k <= drawEnd; k++)
                    {
                        num2 += num;
                        float num5 = this.m_lineWidths[num2] + extraDistance;
                        vector2 = !flag ? (Vector3)this.points2[k] : matrixx.MultiplyPoint3x4(this.points2[k]);
                        if (this.useViewportCoords)
                        {
                            vector2.x *= vector.x;
                            vector2.y *= vector.y;
                        }
                        if (((p.x >= (vector2.x - num5)) && (p.x <= (vector2.x + num5))) && ((p.y >= (vector2.y - num5)) && (p.y <= (vector2.y + num5))))
                        {
                            index = k;
                            return true;
                        }
                    }
                    index = -1;
                    return false;
                }
                for (int j = this.m_drawStart; j <= drawEnd; j++)
                {
                    num2 += num;
                    float num7 = this.m_lineWidths[num2] + extraDistance;
                    vector2 = !flag ? cam3D.WorldToScreenPoint(this.points3[j]) : cam3D.WorldToScreenPoint(matrixx.MultiplyPoint3x4(this.points3[j]));
                    if (((p.x >= (vector2.x - num7)) && (p.x <= (vector2.x + num7))) && ((p.y >= (vector2.y - num7)) && (p.y <= (vector2.y + num7))))
                    {
                        index = j;
                        return true;
                    }
                }
                index = -1;
                return false;
            }
            float num8 = 0f;
            int num9 = !this.m_continuous ? 2 : 1;
            Vector2 vector4 = Vector2.zero;
            if (this.m_continuous && (this.m_drawEnd == this.pointsLength))
            {
                drawEnd--;
            }
            if (this.m_is2D)
            {
                for (int m = this.m_drawStart; m < drawEnd; m += num9)
                {
                    num2 += num;
                    if ((this.points2[m].x != this.points2[m + 1].x) || (this.points2[m].y != this.points2[m + 1].y))
                    {
                        if (flag)
                        {
                            vector3 = matrixx.MultiplyPoint3x4(this.points2[m]);
                            vector4 = matrixx.MultiplyPoint3x4(this.points2[m + 1]);
                        }
                        else
                        {
                            vector3 = this.points2[m];
                            vector4 = this.points2[m + 1];
                        }
                        if (this.useViewportCoords)
                        {
                            vector3.x *= vector.x;
                            vector3.y *= vector.y;
                            vector4.x *= vector.x;
                            vector4.y *= vector.y;
                        }
                        Vector2 vector5 = vector4 - vector3;
                        num8 = Vector2.Dot(p - vector3, vector4 - vector3) / vector5.sqrMagnitude;
                        if ((num8 >= 0f) && (num8 <= 1f))
                        {
                            Vector2 vector6 = p - (vector3 + ((Vector2)(num8 * (vector4 - vector3))));
                            if (vector6.sqrMagnitude <= ((this.m_lineWidths[num2] + extraDistance) * (this.m_lineWidths[num2] + extraDistance)))
                            {
                                index = !this.m_continuous ? (m / 2) : m;
                                return true;
                            }
                        }
                    }
                }
                index = -1;
                return false;
            }
            Vector3 vector8 = Vector3.zero;
            for (int i = this.m_drawStart; i < drawEnd; i += num9)
            {
                num2 += num;
                if (((this.points3[i].x != this.points3[i + 1].x) || (this.points3[i].y != this.points3[i + 1].y)) || (this.points3[i].z != this.points3[i + 1].z))
                {
                    Vector3 vector7;
                    if (flag)
                    {
                        vector7 = cam3D.WorldToScreenPoint(matrixx.MultiplyPoint3x4(this.points3[i]));
                        vector8 = cam3D.WorldToScreenPoint(matrixx.MultiplyPoint3x4(this.points3[i + 1]));
                    }
                    else
                    {
                        vector7 = cam3D.WorldToScreenPoint(this.points3[i]);
                        vector8 = cam3D.WorldToScreenPoint(this.points3[i + 1]);
                    }
                    if ((vector7.z >= 0.15f) && (vector8.z >= 0.15f))
                    {
                        vector3.x = (int)vector7.x;
                        vector4.x = (int)vector8.x;
                        vector3.y = (int)vector7.y;
                        vector4.y = (int)vector8.y;
                        if ((vector3.x != vector4.x) || (vector3.y != vector4.y))
                        {
                            Vector2 vector9 = vector4 - vector3;
                            num8 = Vector2.Dot(p - vector3, vector4 - vector3) / vector9.sqrMagnitude;
                            if ((num8 >= 0f) && (num8 <= 1f))
                            {
                                Vector2 vector10 = p - (vector3 + ((Vector2)(num8 * (vector4 - vector3))));
                                if (vector10.sqrMagnitude <= ((this.m_lineWidths[num2] + extraDistance) * (this.m_lineWidths[num2] + extraDistance)))
                                {
                                    index = !this.m_continuous ? (i / 2) : i;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            index = -1;
            return false;
        }

        public static Camera SetCamera() { return SetCamera(CameraClearFlags.Depth, false); }

        public static Camera SetCamera(bool useOrtho) { return SetCamera(CameraClearFlags.Depth, useOrtho); }

        public static Camera SetCamera(Camera thisCamera) { return SetCamera(thisCamera, CameraClearFlags.Depth, false); }

        public static Camera SetCamera(CameraClearFlags clearFlags) { return SetCamera(clearFlags, false); }

        public static Camera SetCamera(Camera thisCamera, bool useOrtho) { return SetCamera(thisCamera, CameraClearFlags.Depth, useOrtho); }

        public static Camera SetCamera(Camera thisCamera, CameraClearFlags clearFlags) { return SetCamera(thisCamera, clearFlags, false); }

        public static Camera SetCamera(CameraClearFlags clearFlags, bool useOrtho)
        {
            if (Camera.main == null)
            {
                LogError("VectorLine.SetCamera: no camera tagged \"Main Camera\" found. Please call SetCamera with a specific camera instead.");
                return null;
            }
            return SetCamera(Camera.main, clearFlags, useOrtho);
        }

        public static Camera SetCamera(Camera thisCamera, CameraClearFlags clearFlags, bool useOrtho)
        {
            if (cam == null)
            {
                Type[] typeArray1 = new Type[] { typeof(Camera) };
                cam = new GameObject("VectorCam", typeArray1).GetComponent<Camera>();
                Object.DontDestroyOnLoad(cam);
            }
            cam.depth = (thisCamera.depth + 1f);
            cam.clearFlags = (clearFlags);
            cam.orthographic = (useOrtho);
            useOrthoCam = useOrtho;
            if (useOrtho)
            {
                cam.orthographicSize = ((float)(screenHeight / 2));
                cam.farClipPlane = (101.1f);
                cam.nearClipPlane = (0.9f);
            }
            else
            {
                cam.fieldOfView = (90f);
                cam.farClipPlane = ((screenHeight / 2) + 0.0101f);
                cam.nearClipPlane = ((screenHeight / 2) - 0.0001f);
            }
            cam.transform.position = (new Vector3((screenWidth / 2) - 0.5f, (screenHeight / 2) - 0.5f, 0f));
            cam.transform.eulerAngles = (Vector3.zero);
            cam.cullingMask = (((int)1) << _vectorLayer);
            cam.backgroundColor = (thisCamera.backgroundColor);
            cam.useOcclusionCulling = (false);
            cam.hdr = (thisCamera.hdr);
            thisCamera.cullingMask = (thisCamera.cullingMask & ~(((int)1) << _vectorLayer));
            camTransform = thisCamera.transform;
            cam3D = thisCamera;
            oldPosition = camTransform.position + Vector3.one;
            oldRotation = camTransform.eulerAngles + Vector3.one;
            return cam;
        }

        public static void SetCamera3D()
        {
            if (Camera.main == null)
            {
                LogError("VectorLine.SetCamera3D: no camera tagged \"Main Camera\" found. Please call SetCamera3D with a specific camera instead.");
            }
            else
            {
                SetCamera3D(Camera.main);
            }
        }

        public static void SetCamera3D(Camera thisCamera)
        {
            camTransform = thisCamera.transform;
            cam3D = thisCamera;
            oldPosition = camTransform.position + Vector3.one;
            oldRotation = camTransform.eulerAngles + Vector3.one;
        }

        public static Camera SetCameraRenderTexture(RenderTexture renderTexture) { return SetCameraRenderTexture(renderTexture, Color.black, CameraClearFlags.Color, false); }

        public static Camera SetCameraRenderTexture(RenderTexture renderTexture, bool useOrtho) { return SetCameraRenderTexture(renderTexture, Color.black, CameraClearFlags.Color, useOrtho); }

        public static Camera SetCameraRenderTexture(RenderTexture renderTexture, CameraClearFlags clearFlags) { return SetCameraRenderTexture(renderTexture, Color.black, clearFlags, false); }

        public static Camera SetCameraRenderTexture(RenderTexture renderTexture, CameraClearFlags clearFlags, bool useOrtho) { return SetCameraRenderTexture(renderTexture, Color.black, clearFlags, useOrtho); }

        public static Camera SetCameraRenderTexture(RenderTexture renderTexture, Color color, bool useOrtho) { return SetCameraRenderTexture(renderTexture, color, CameraClearFlags.Color, useOrtho); }

        public static Camera SetCameraRenderTexture(RenderTexture renderTexture, Color color, CameraClearFlags clearFlags, bool useOrtho)
        {
            Camera camera;
            if (renderTexture == null)
            {
                m_screenWidth = 0;
                m_screenHeight = 0;
                camera = SetCamera(useOrtho);
                camera.aspect = (((float)screenWidth) / ((float)screenHeight));
                camera.targetTexture = (null);
                return camera;
            }
            int num = renderTexture.width;
            int num2 = renderTexture.height;
            m_screenWidth = num;
            m_screenHeight = num2;
            camera = SetCamera(clearFlags, useOrtho);
            camera.aspect = (((float)num) / ((float)num2));
            camera.backgroundColor = (color);
            camera.targetTexture = (renderTexture);
            return camera;
        }

        private void SetCollider(bool convertToWorldSpace)
        {
            if (cam3D.transform.rotation != Quaternion.identity)
            {
                Debug.LogWarning("The line collider will not be correct if the camera is rotated");
            }
            Vector3 vector = new Vector3(0f, 0f, -cam3D.transform.position.z);
            int num = (this.drawStart <= this.minDrawIndex) ? this.minDrawIndex : this.drawStart;
            int num2 = (this.drawEnd >= this.maxDrawIndex) ? this.maxDrawIndex : this.drawEnd;
            if (this.m_continuous)
            {
                EdgeCollider2D component = this.m_vectorObject.GetComponent(typeof(EdgeCollider2D)) as EdgeCollider2D;
                Vector2[] vectorArray = new Vector2[((num2 - num) * 4) + 1];
                int index = 0;
                int num4 = vectorArray.Length - 2;
                if (convertToWorldSpace)
                {
                    for (int i = num * 4; i < (num2 * 4); i += 4)
                    {
                        vector.x = this.m_lineVertices[i].x;
                        vector.y = this.m_lineVertices[i].y;
                        vectorArray[index] = cam3D.ScreenToWorldPoint(vector);
                        vector.x = this.m_lineVertices[i + 2].x;
                        vector.y = this.m_lineVertices[i + 2].y;
                        vectorArray[index + 1] = cam3D.ScreenToWorldPoint(vector);
                        vector.x = this.m_lineVertices[i + 1].x;
                        vector.y = this.m_lineVertices[i + 1].y;
                        vectorArray[num4] = cam3D.ScreenToWorldPoint(vector);
                        vector.x = this.m_lineVertices[i + 3].x;
                        vector.y = this.m_lineVertices[i + 3].y;
                        vectorArray[num4 - 1] = cam3D.ScreenToWorldPoint(vector);
                        index += 2;
                        num4 -= 2;
                    }
                }
                else
                {
                    for (int j = num * 4; j < (num2 * 4); j += 4)
                    {
                        vectorArray[index].x = this.m_lineVertices[j].x;
                        vectorArray[index].y = this.m_lineVertices[j].y;
                        vectorArray[index + 1].x = this.m_lineVertices[j + 2].x;
                        vectorArray[index + 1].y = this.m_lineVertices[j + 2].y;
                        vectorArray[num4].x = this.m_lineVertices[j + 1].x;
                        vectorArray[num4].y = this.m_lineVertices[j + 1].y;
                        vectorArray[num4 - 1].x = this.m_lineVertices[j + 3].x;
                        vectorArray[num4 - 1].y = this.m_lineVertices[j + 3].y;
                        index += 2;
                        num4 -= 2;
                    }
                }
                vectorArray[vectorArray.Length - 1] = vectorArray[0];
                component.points = (vectorArray);
            }
            else
            {
                PolygonCollider2D colliderd2 = this.m_vectorObject.GetComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;
                Vector2[] vectorArray2 = new Vector2[4];
                colliderd2.pathCount = (((num2 - num) + 1) / 2);
                int num7 = ((num2 + 1) / 2) * 4;
                int num8 = 0;
                if (convertToWorldSpace)
                {
                    for (int k = (num / 2) * 4; k < num7; k += 4)
                    {
                        vector.x = this.m_lineVertices[k].x;
                        vector.y = this.m_lineVertices[k].y;
                        vectorArray2[0] = cam3D.ScreenToWorldPoint(vector);
                        vector.x = this.m_lineVertices[k + 1].x;
                        vector.y = this.m_lineVertices[k + 1].y;
                        vectorArray2[1] = cam3D.ScreenToWorldPoint(vector);
                        vector.x = this.m_lineVertices[k + 3].x;
                        vector.y = this.m_lineVertices[k + 3].y;
                        vectorArray2[2] = cam3D.ScreenToWorldPoint(vector);
                        vector.x = this.m_lineVertices[k + 2].x;
                        vector.y = this.m_lineVertices[k + 2].y;
                        vectorArray2[3] = cam3D.ScreenToWorldPoint(vector);
                        colliderd2.SetPath(num8++, vectorArray2);
                    }
                }
                else
                {
                    for (int m = (num / 2) * 4; m < num7; m += 4)
                    {
                        vectorArray2[0].x = this.m_lineVertices[m].x;
                        vectorArray2[0].y = this.m_lineVertices[m].y;
                        vectorArray2[1].x = this.m_lineVertices[m + 1].x;
                        vectorArray2[1].y = this.m_lineVertices[m + 1].y;
                        vectorArray2[2].x = this.m_lineVertices[m + 3].x;
                        vectorArray2[2].y = this.m_lineVertices[m + 3].y;
                        vectorArray2[3].x = this.m_lineVertices[m + 2].x;
                        vectorArray2[3].y = this.m_lineVertices[m + 2].y;
                        colliderd2.SetPath(num8++, vectorArray2);
                    }
                }
            }
        }

        public void SetColor(Color color)
        {
            this.SetColor(color, 0, this.pointsLength);
        }

        public void SetColor(Color color, int index)
        {
            this.SetColor(color, index, index);
        }

        public void SetColor(Color color, int startIndex, int endIndex)
        {
            int pointsLength;
            int num2;
            if (this.m_isPoints)
            {
                pointsLength = this.pointsLength;
            }
            else
            {
                pointsLength = !this.m_continuous ? (this.pointsLength / 2) : (this.pointsLength - 1);
            }
            if (this.m_1pixelLine)
            {
                num2 = !this.m_isPoints ? 2 : 1;
            }
            else
            {
                num2 = 4;
            }
            int num3 = Mathf.Clamp((startIndex * num2) + (!this.smoothColor ? 0 : (num2 / 2)), 0, pointsLength * num2);
            int num4 = Mathf.Clamp(((endIndex + 1) * num2) + (!this.smoothColor ? 0 : (num2 / 2)), 0, pointsLength * num2);
            for (int i = num3; i < num4; i++)
            {
                this.m_lineColors[i] = color;
            }
            if ((this.m_capType != EndCap.None) && ((startIndex <= 0) || (endIndex >= (pointsLength - 1))))
            {
                this.SetEndCapColors();
            }
            this.m_mesh.colors32 = (this.m_lineColors);
        }

        private Color[] SetColor(Color color, LineType lineType, int size, bool usePoints)
        {
            if (size == 0)
            {
                LogError("VectorLine: Must use a points array with more than 0 entries");
                return null;
            }
            if (!usePoints)
            {
                size = (lineType != LineType.Continuous) ? (size / 2) : (size - 1);
            }
            Color[] colorArray = new Color[size];
            for (int i = 0; i < size; i++)
            {
                colorArray[i] = color;
            }
            return colorArray;
        }

        public void SetColors(Color[] lineColors)
        {
            if (lineColors == null)
            {
                LogError("VectorLine.SetColors: lineColors array must not be null");
            }
            else
            {
                if (!this.m_isPoints)
                {
                    if (this.WrongArrayLength(lineColors.Length, FunctionName.SetColors))
                    {
                        return;
                    }
                }
                else if (lineColors.Length != this.pointsLength)
                {
                    LogError("VectorLine.SetColors: Length of lineColors array in \"" + this.name + "\" must be same length as points array");
                    return;
                }
                int start = 0;
                int length = lineColors.Length;
                this.SetStartAndEnd(ref start, ref length);
                int index = start * 4;
                if (this.m_isPoints)
                {
                    length++;
                }
                if (!this.smoothColor)
                {
                    if (this.m_1pixelLine)
                    {
                        if (this.m_isPoints)
                        {
                            for (int i = start; i < length; i++)
                            {
                                this.m_lineColors[i] = lineColors[i];
                            }
                        }
                        else
                        {
                            index = start * 2;
                            for (int j = start; j < length; j++)
                            {
                                this.m_lineColors[index] = lineColors[j];
                                this.m_lineColors[index + 1] = lineColors[j];
                                index += 2;
                            }
                        }
                    }
                    else
                    {
                        for (int k = start; k < length; k++)
                        {
                            this.m_lineColors[index] = lineColors[k];
                            this.m_lineColors[index + 1] = lineColors[k];
                            this.m_lineColors[index + 2] = lineColors[k];
                            this.m_lineColors[index + 3] = lineColors[k];
                            index += 4;
                        }
                    }
                }
                else if (this.m_1pixelLine)
                {
                    index = start * 2;
                    this.m_lineColors[index] = lineColors[start];
                    this.m_lineColors[index + 1] = lineColors[start];
                    index += 2;
                    for (int m = start + 1; m < length; m++)
                    {
                        this.m_lineColors[index] = lineColors[m - 1];
                        this.m_lineColors[index + 1] = lineColors[m];
                        index += 2;
                    }
                }
                else
                {
                    this.m_lineColors[index] = lineColors[start];
                    this.m_lineColors[index + 1] = lineColors[start];
                    this.m_lineColors[index + 2] = lineColors[start];
                    this.m_lineColors[index + 3] = lineColors[start];
                    index += 4;
                    for (int n = start + 1; n < length; n++)
                    {
                        this.m_lineColors[index] = lineColors[n - 1];
                        this.m_lineColors[index + 1] = lineColors[n - 1];
                        this.m_lineColors[index + 2] = lineColors[n];
                        this.m_lineColors[index + 3] = lineColors[n];
                        index += 4;
                    }
                }
                if (this.m_capType != EndCap.None)
                {
                    this.SetEndCapColors();
                }
                this.m_mesh.colors32 = (this.m_lineColors);
            }
        }

        public static void SetDepth(Transform thisTransform, int depth)
        {
            depth = Mathf.Clamp(depth, 0, 100);
            thisTransform.position = (new Vector3(thisTransform.position.x, thisTransform.position.y, !useOrthoCam ? ((screenHeight / 2) + ((100f - depth) * 0.0001f)) : ((float)(0x65 - depth))));
        }

        private void SetDistanceIndex(out int i, float distance)
        {
            if (this.m_distances == null)
            {
                this.SetDistances();
            }
            i = (this.m_minDrawIndex <= this.m_drawStart) ? (this.m_drawStart + 1) : (this.m_minDrawIndex + 1);
            if (!this.m_continuous)
            {
                i = (i + 1) / 2;
            }
            if (i >= this.m_distances.Length)
            {
                i = this.m_distances.Length - 1;
            }
            int num = (this.m_maxDrawIndex >= this.m_drawEnd) ? this.m_drawEnd : this.m_maxDrawIndex;
            if (!this.m_continuous)
            {
                num = (num + 1) / 2;
            }
            while ((distance > this.m_distances[i]) && (i < num))
            {
                i++;
            }
        }

        public void SetDistances()
        {
            if ((this.m_distances == null) || (this.m_distances.Length != (!this.m_continuous ? ((this.m_pointsLength / 2) + 1) : this.m_pointsLength)))
            {
                this.m_distances = new float[!this.m_continuous ? ((this.m_pointsLength / 2) + 1) : this.m_pointsLength];
            }
            double num = 0.0;
            int num2 = this.pointsLength - 1;
            if (this.points3 != null)
            {
                if (this.m_continuous)
                {
                    for (int i = 0; i < num2; i++)
                    {
                        Vector3 vector = this.points3[i] - this.points3[i + 1];
                        num += Math.Sqrt((double)(((vector.x * vector.x) + (vector.y * vector.y)) + (vector.z * vector.z)));
                        this.m_distances[i + 1] = (float)num;
                    }
                }
                else
                {
                    int num4 = 1;
                    for (int j = 0; j < num2; j += 2)
                    {
                        Vector3 vector2 = this.points3[j] - this.points3[j + 1];
                        num += Math.Sqrt((double)(((vector2.x * vector2.x) + (vector2.y * vector2.y)) + (vector2.z * vector2.z)));
                        this.m_distances[num4++] = (float)num;
                    }
                }
            }
            else if (this.m_continuous)
            {
                for (int k = 0; k < num2; k++)
                {
                    Vector2 vector3 = this.points2[k] - this.points2[k + 1];
                    num += Math.Sqrt((double)((vector3.x * vector3.x) + (vector3.y * vector3.y)));
                    this.m_distances[k + 1] = (float)num;
                }
            }
            else
            {
                int num7 = 1;
                for (int m = 0; m < num2; m += 2)
                {
                    Vector2 vector4 = this.points2[m] - this.points2[m + 1];
                    num += Math.Sqrt((double)((vector4.x * vector4.x) + (vector4.y * vector4.y)));
                    this.m_distances[num7++] = (float)num;
                }
            }
        }

        public static void SetEndCap(string name, EndCap capType)
        {
            SetEndCap(name, capType, null, 0f, null);
        }

        public static void SetEndCap(string name, EndCap capType, Material material, params Texture2D[] textures)
        {
            SetEndCap(name, capType, material, 0f, textures);
        }

        public static void SetEndCap(string name, EndCap capType, Material material, float offset, params Texture2D[] textures)
        {
            if (capDictionary == null)
            {
                capDictionary = new Dictionary<string, CapInfo>();
            }
            if ((name == null) || (name == ""))
            {
                LogError("VectorLine: must supply a name for SetEndCap");
            }
            else if (capDictionary.ContainsKey(name) && (capType != EndCap.None))
            {
                LogError("VectorLine: end cap \"" + name + "\" has already been set up");
            }
            else
            {
                if (capType == EndCap.Both)
                {
                    if (textures.Length < 2)
                    {
                        LogError("VectorLine: must supply two textures when using SetEndCap with EndCap.Both");
                        return;
                    }
                    if ((textures[0].width != textures[1].width) || (textures[0].height != textures[1].height))
                    {
                        LogError("VectorLine: when using SetEndCap with EndCap.Both, both textures must have the same width and height");
                        return;
                    }
                }
                if ((((capType == EndCap.Front) || (capType == EndCap.Back)) || (capType == EndCap.Mirror)) && (textures.Length < 1))
                {
                    LogError("VectorLine: must supply a texture when using SetEndCap with EndCap.Front, EndCap.Back, or EndCap.Mirror");
                }
                else if (capType == EndCap.None)
                {
                    if (capDictionary.ContainsKey(name))
                    {
                        RemoveEndCap(name);
                    }
                }
                else if (material == null)
                {
                    LogError("VectorLine: must supply a material when using SetEndCap with any EndCap type except EndCap.None");
                }
                else if (!material.HasProperty("_MainTex"))
                {
                    LogError("VectorLine: the material supplied when using SetEndCap must contain a shader that has a \"_MainTex\" property");
                }
                else
                {
                    int num = textures[0].width;
                    int num2 = textures[0].height;
                    float num3 = 0f;
                    float num4 = 0f;
                    Color[] pixels = null;
                    Color[] colorArray2 = null;
                    if (capType == EndCap.Front)
                    {
                        pixels = textures[0].GetPixels();
                        colorArray2 = new Color[num * num2];
                        num3 = ((float)textures[0].width) / ((float)textures[0].height);
                    }
                    else if (capType == EndCap.Back)
                    {
                        pixels = new Color[num * num2];
                        colorArray2 = textures[0].GetPixels();
                        num4 = ((float)textures[0].width) / ((float)textures[0].height);
                    }
                    else if (capType == EndCap.Both)
                    {
                        pixels = textures[0].GetPixels();
                        colorArray2 = textures[1].GetPixels();
                        num3 = ((float)textures[0].width) / ((float)textures[0].height);
                        num4 = ((float)textures[1].width) / ((float)textures[1].height);
                    }
                    else if (capType == EndCap.Mirror)
                    {
                        pixels = textures[0].GetPixels();
                        colorArray2 = new Color[num * num2];
                        num3 = ((float)textures[0].width) / ((float)textures[0].height);
                        num4 = num3;
                    }
                    Texture2D texture = new Texture2D(num, num2 * 4, TextureFormat.ARGB32, false);
                    texture.wrapMode = TextureWrapMode.Clamp;
                    texture.filterMode = (textures[0].filterMode);
                    texture.SetPixels(0, 0, num, num2, pixels);
                    texture.SetPixels(0, num2 * 3, num, num2, colorArray2);
                    texture.SetPixels(0, num2, num, num2 * 2, new Color[num * (num2 * 2)]);
                    texture.Apply(false, true);
                    Material material2 = Object.Instantiate(material);
                    material2.name = (material.name + " EndCap");
                    material2.mainTexture = (texture);
                    capDictionary.Add(name, new CapInfo(capType, material2, texture, num3, num4, offset));
                }
            }
        }

        private void SetEndCapColors()
        {
            if (!this.m_1pixelLine)
            {
                if (this.m_capType <= EndCap.Mirror)
                {
                    int num = !this.m_continuous ? (this.m_drawStart * 2) : (this.m_drawStart * 4);
                    for (int i = 0; i < 4; i++)
                    {
                        this.m_lineColors[i + this.m_vertexCount] = this.m_lineColors[i + num];
                    }
                }
                if (this.m_capType >= EndCap.Both)
                {
                    int drawEnd = this.m_drawEnd;
                    if (this.m_continuous)
                    {
                        if (this.m_drawEnd == this.pointsLength)
                        {
                            drawEnd--;
                        }
                    }
                    else if (drawEnd < this.pointsLength)
                    {
                        drawEnd++;
                    }
                    int num4 = (drawEnd * (!this.m_continuous ? 2 : 4)) - 8;
                    if (num4 < -4)
                    {
                        num4 = -4;
                    }
                    for (int j = 4; j < 8; j++)
                    {
                        this.m_lineColors[j + this.m_vertexCount] = this.m_lineColors[j + num4];
                    }
                }
            }
        }

        private void SetIntersectionPoint(int p1, int p2, int p3, int p4)
        {
            Vector3 vector = this.m_lineVertices[p1];
            Vector3 vector2 = this.m_lineVertices[p2];
            Vector3 vector3 = this.m_lineVertices[p3];
            Vector3 vector4 = this.m_lineVertices[p4];
            float num = ((vector4.y - vector3.y) * (vector2.x - vector.x)) - ((vector4.x - vector3.x) * (vector2.y - vector.y));
            if (num != 0f)
            {
                float num2 = (((vector4.x - vector3.x) * (vector.y - vector3.y)) - ((vector4.y - vector3.y) * (vector.x - vector3.x))) / num;
                Vector3 vector5 = new Vector3(vector.x + (num2 * (vector2.x - vector.x)), vector.y + (num2 * (vector2.y - vector.y)), vector.z);
                Vector3 vector6 = vector5 - vector2;
                if (vector6.sqrMagnitude <= this.m_maxWeldDistance)
                {
                    this.m_lineVertices[p2] = vector5;
                    this.m_lineVertices[p3] = vector5;
                }
            }
        }

        private void SetIntersectionPoint3D(int p1, int p2, int p3, int p4)
        {
            Vector3 vector = this.m_screenPoints[p1];
            Vector3 vector2 = this.m_screenPoints[p2];
            Vector3 vector3 = this.m_screenPoints[p3];
            Vector3 vector4 = this.m_screenPoints[p4];
            float num = ((vector4.y - vector3.y) * (vector2.x - vector.x)) - ((vector4.x - vector3.x) * (vector2.y - vector.y));
            if (num != 0f)
            {
                float num2 = (((vector4.x - vector3.x) * (vector.y - vector3.y)) - ((vector4.y - vector3.y) * (vector.x - vector3.x))) / num;
                Vector3 vector5 = new Vector3(vector.x + (num2 * (vector2.x - vector.x)), vector.y + (num2 * (vector2.y - vector.y)), vector.z);
                Vector3 vector6 = vector5 - vector2;
                if (vector6.sqrMagnitude <= this.m_maxWeldDistance)
                {
                    this.m_lineVertices[p2] = cam3D.ScreenToWorldPoint(vector5);
                    this.m_lineVertices[p3] = this.m_lineVertices[p2];
                }
            }
        }

        public static VectorLine SetLine(Color color, params Vector2[] points) { return SetLine(color, 0f, points); }

        public static VectorLine SetLine(Color color, params Vector3[] points) { return SetLine(color, 0f, points); }

        public static VectorLine SetLine(Color color, float time, params Vector2[] points)
        {
            if (points.Length < 2)
            {
                LogError("VectorLine.SetLine needs at least two points");
                return null;
            }
            VectorLine vectorLine = new VectorLine("Line", points, color, null, 1f, LineType.Continuous, Joins.None);
            if (time > 0f)
            {
                lineManager.DisableLine(vectorLine, time);
            }
            vectorLine.Draw();
            return vectorLine;
        }

        public static VectorLine SetLine(Color color, float time, params Vector3[] points)
        {
            if (points.Length < 2)
            {
                LogError("VectorLine.SetLine needs at least two points");
                return null;
            }
            VectorLine vectorLine = new VectorLine("SetLine", points, color, null, 1f, LineType.Continuous, Joins.None);
            if (time > 0f)
            {
                lineManager.DisableLine(vectorLine, time);
            }
            vectorLine.Draw();
            return vectorLine;
        }

        public static VectorLine SetLine3D(Color color, params Vector3[] points) { return SetLine3D(color, 0f, points); }

        public static VectorLine SetLine3D(Color color, float time, params Vector3[] points)
        {
            if (points.Length < 2)
            {
                LogError("VectorLine.SetLine3D needs at least two points");
                return null;
            }
            VectorLine line = new VectorLine("SetLine3D", points, color, null, 1f, LineType.Continuous, Joins.None);
            line.Draw3DAuto(time);
            return line;
        }

        private void SetLineMeshBounds()
        {
            Bounds bounds = new Bounds();
            if (!useOrthoCam)
            {
                bounds.center = (new Vector3((float)(screenWidth / 2), (float)(screenHeight / 2), (float)(screenHeight / 2)));
                bounds.extents = (new Vector3((float)(screenWidth * 100), (float)(screenHeight * 100), 0.1f));
            }
            else
            {
                bounds.center = (new Vector3((float)(screenWidth / 2), (float)(screenHeight / 2), 50.5f));
                bounds.extents = (new Vector3((float)(screenWidth * 100), (float)(screenHeight * 100), 51f));
            }
            this.m_mesh.bounds = (bounds);
        }

        public static void SetLineParameters(Color color, Material material, float width, float capLength, int depth, LineType lineType, Joins joins)
        {
            defaultLineColor = color;
            defaultLineMaterial = material;
            defaultLineWidth = width;
            defaultLineDepth = depth;
            defaultCapLength = capLength;
            defaultLineType = lineType;
            defaultJoins = joins;
            defaultsSet = true;
        }

        public static VectorLine SetRay(Color color, Vector3 origin, Vector3 direction) { return SetRay(color, 0f, origin, direction); }

        public static VectorLine SetRay(Color color, float time, Vector3 origin, Vector3 direction)
        {
            Vector3[] linePoints = new Vector3[] { origin, new Ray(origin, direction).GetPoint(direction.magnitude) };
            VectorLine vectorLine = new VectorLine("SetRay", linePoints, color, null, 1f, LineType.Continuous, Joins.None);
            if (time > 0f)
            {
                lineManager.DisableLine(vectorLine, time);
            }
            vectorLine.Draw();
            return vectorLine;
        }

        public static VectorLine SetRay3D(Color color, Vector3 origin, Vector3 direction) { return SetRay3D(color, 0f, origin, direction); }

        public static VectorLine SetRay3D(Color color, float time, Vector3 origin, Vector3 direction)
        {
            Vector3[] linePoints = new Vector3[] { origin, new Ray(origin, direction).GetPoint(direction.magnitude) };
            VectorLine line = new VectorLine("SetRay3D", linePoints, color, null, 1f, LineType.Continuous, Joins.None);
            line.Draw3DAuto(time);
            return line;
        }

        private void SetStartAndEnd(ref int start, ref int end)
        {
            start = (this.m_minDrawIndex != 0) ? (!this.m_continuous ? (this.m_minDrawIndex / 2) : this.m_minDrawIndex) : 0;
            if (this.m_maxDrawIndex > 0)
            {
                if (this.m_continuous)
                {
                    end = this.m_maxDrawIndex;
                }
                else
                {
                    end = this.m_maxDrawIndex / 2;
                    if ((this.m_maxDrawIndex % 2) != 0)
                    {
                        end++;
                    }
                }
            }
        }

        private void SetTextureScale()
        {
            if (!this.m_1pixelLine)
            {
                int num = !this.m_continuous ? this.pointsLength : (this.pointsLength - 1);
                int num2 = !this.m_continuous ? 2 : 1;
                int index = 0;
                int num4 = 0;
                int num5 = (this.m_lineWidths.Length != 1) ? 1 : 0;
                float num6 = 1f / this.m_textureScale;
                bool flag = this.m_useTransform != null;
                Matrix4x4 matrixx = !flag ? Matrix4x4.identity : this.m_useTransform.localToWorldMatrix;
                Vector2 vector = Vector2.zero;
                Vector2 vector2 = Vector2.zero;
                float textureOffset = this.m_textureOffset;
                if (this.m_is2D)
                {
                    if (!this.m_viewportDraw)
                    {
                        for (int i = 0; i < num; i += num2)
                        {
                            if (flag)
                            {
                                vector = matrixx.MultiplyPoint3x4(this.points2[i]);
                                vector2 = matrixx.MultiplyPoint3x4(this.points2[i + 1]);
                            }
                            else
                            {
                                vector = this.points2[i];
                                vector2 = this.points2[i + 1];
                            }
                            Vector2 vector3 = vector - vector2;
                            float num9 = num6 / ((this.m_lineWidths[num4] * 2f) / vector3.magnitude);
                            this.m_lineUVs[index].x = textureOffset;
                            this.m_lineUVs[index + 1].x = textureOffset;
                            this.m_lineUVs[index + 2].x = num9 + textureOffset;
                            this.m_lineUVs[index + 3].x = num9 + textureOffset;
                            index += 4;
                            textureOffset = (textureOffset + num9) % 1f;
                            num4 += num5;
                        }
                    }
                    else
                    {
                        for (int j = 0; j < num; j += num2)
                        {
                            if (flag)
                            {
                                vector = matrixx.MultiplyPoint3x4(new Vector2(this.points2[j].x * Screen.width, this.points2[j].y * Screen.height));
                                vector2 = matrixx.MultiplyPoint3x4(new Vector2(this.points2[j + 1].x * Screen.width, this.points2[j + 1].y * Screen.height));
                            }
                            else
                            {
                                vector = new Vector2(this.points2[j].x * Screen.width, this.points2[j].y * Screen.height);
                                vector2 = new Vector2(this.points2[j + 1].x * Screen.width, this.points2[j + 1].y * Screen.height);
                            }
                            Vector2 vector4 = vector - vector2;
                            float num11 = num6 / ((this.m_lineWidths[num4] * 2f) / vector4.magnitude);
                            this.m_lineUVs[index].x = textureOffset;
                            this.m_lineUVs[index + 1].x = textureOffset;
                            this.m_lineUVs[index + 2].x = num11 + textureOffset;
                            this.m_lineUVs[index + 3].x = num11 + textureOffset;
                            index += 4;
                            textureOffset = (textureOffset + num11) % 1f;
                            num4 += num5;
                        }
                    }
                }
                else
                {
                    for (int k = 0; k < num; k += num2)
                    {
                        if (flag)
                        {
                            vector = cam3D.WorldToScreenPoint(matrixx.MultiplyPoint3x4(this.points3[k]));
                            vector2 = cam3D.WorldToScreenPoint(matrixx.MultiplyPoint3x4(this.points3[k + 1]));
                        }
                        else
                        {
                            vector = cam3D.WorldToScreenPoint(this.points3[k]);
                            vector2 = cam3D.WorldToScreenPoint(this.points3[k + 1]);
                        }
                        Vector2 vector5 = vector - vector2;
                        float num13 = num6 / ((this.m_lineWidths[num4] * 2f) / vector5.magnitude);
                        this.m_lineUVs[index].x = textureOffset;
                        this.m_lineUVs[index + 1].x = textureOffset;
                        this.m_lineUVs[index + 2].x = num13 + textureOffset;
                        this.m_lineUVs[index + 3].x = num13 + textureOffset;
                        index += 4;
                        textureOffset = (textureOffset + num13) % 1f;
                        num4 += num5;
                    }
                }
                this.m_mesh.uv = (this.m_lineUVs);
            }
        }

        private static void SetupByteBlock()
        {
            if (byteBlock == null)
            {
                byteBlock = new byte[4];
            }
            if (BitConverter.IsLittleEndian)
            {
                endianDiff1 = 0;
                endianDiff2 = 0;
            }
            else
            {
                endianDiff1 = 3;
                endianDiff2 = 1;
            }
        }

        private void SetupDrawStartEnd(out int start, out int end)
        {
            start = this.m_minDrawIndex;
            end = this.m_maxDrawIndex;
            if (this.m_drawStart > 0)
            {
                start = this.m_drawStart;
                this.ZeroVertices(0, start);
            }
            if (this.m_drawEnd < (this.m_pointsLength - 1))
            {
                end = this.m_drawEnd;
                this.ZeroVertices(end, this.m_pointsLength);
            }
        }

        protected void SetupMesh(ref string lineName, Material useMaterial, Color[] colors, ref float width, LineType lineType, Joins joins, bool use2Dlines, bool usePoints)
        {
            this.m_continuous = lineType == LineType.Continuous;
            this.m_is2D = use2Dlines;
            if ((joins == Joins.Fill) && !this.m_continuous)
            {
                LogError("VectorLine: Must use LineType.Continuous if using Joins.Fill for \"" + lineName + "\"");
            }
            else if ((this.m_is2D && (this.points2 == null)) || (!this.m_is2D && (this.points3 == null)))
            {
                LogError("VectorLine: the points array is null for \"" + lineName + "\"");
            }
            else if (colors == null)
            {
                LogError("Vectorline: the colors array is null for \"" + lineName + "\"");
            }
            else
            {
                this.m_pointsLength = !this.m_is2D ? this.points3.Length : this.points2.Length;
                if (!usePoints && (this.m_pointsLength < 2))
                {
                    LogError("The points array must contain at least two points");
                }
                else if (!this.m_continuous && ((this.m_pointsLength % 2) != 0))
                {
                    LogError("VectorLine: Must have an even points array length for \"" + lineName + "\" when using LineType.Discrete");
                }
                else
                {
                    this.m_maxWeldDistance = (width * 2f) * (width * 2f);
                    this.m_drawEnd = this.m_pointsLength;
                    this.m_lineWidths = new float[] { width * 0.5f };
                    this.m_isPoints = usePoints;
                    this.m_joins = joins;
                    bool flag = true;
                    int pointsLength = 0;
                    if ((width == 1f) && ((this.m_isPoints && m_useMeshPoints) || (!this.m_isPoints && m_useMeshLines)))
                    {
                        this.m_1pixelLine = true;
                    }
                    if (!usePoints)
                    {
                        if (this.m_continuous)
                        {
                            if (colors.Length != (this.m_pointsLength - 1))
                            {
                                Debug.LogWarning("VectorLine: Length of color array for \"" + lineName + "\" must be length of points array minus one");
                                flag = false;
                                pointsLength = this.m_pointsLength - 1;
                            }
                        }
                        else if (colors.Length != (this.m_pointsLength / 2))
                        {
                            Debug.LogWarning("VectorLine: Length of color array for \"" + lineName + "\" must be exactly half the length of points array");
                            flag = false;
                            pointsLength = this.m_pointsLength / 2;
                        }
                    }
                    else if (colors.Length != this.m_pointsLength)
                    {
                        Debug.LogWarning("VectorLine: Length of color array for \"" + lineName + "\" must be the same length as the points array");
                        flag = false;
                        pointsLength = this.m_pointsLength;
                    }
                    if (!flag)
                    {
                        colors = new Color[pointsLength];
                        for (int i = 0; i < pointsLength; i++)
                        {
                            colors[i] = Color.white;
                        }
                    }
                    if (useMaterial == null)
                    {
                        if (defaultMaterial == null)
                        {
                            defaultMaterial = new Material("Shader \"Vertex Colors/Alpha\" {Category{Tags {\"Queue\"=\"Transparent\" \"IgnoreProjector\"=\"True\" \"RenderType\"=\"Transparent\"}SubShader {Cull Off ZWrite On Blend SrcAlpha OneMinusSrcAlpha Pass {BindChannels {Bind \"Color\", color Bind \"Vertex\", vertex}}}}}");
                        }
                        this.m_material = defaultMaterial;
                    }
                    else
                    {
                        this.m_material = useMaterial;
                    }
                    Type[] typeArray1 = new Type[] { typeof(MeshRenderer) };
                    this.m_vectorObject = new GameObject("Vector " + lineName, typeArray1);
                    this.m_vectorObject.layer = (vectorLayer);
                    this.m_vectorObject.GetComponent<Renderer>().material = (this.m_material);
                    this.m_mesh = new Mesh();
                    this.m_mesh.name = (lineName);
                    this.m_meshFilter = this.m_vectorObject.AddComponent<MeshFilter>();
                    this.m_meshFilter.mesh = (this.m_mesh);
                    this.name = lineName;
                    m_meshRenderMethodSet = true;
                    this.BuildMesh(colors);
                }
            }
        }

        private void SetupTriangles()
        {
            bool flag = false;
            if (this.m_1pixelLine)
            {
                if (this.m_continuous)
                {
                    this.m_triangleCount = !this.m_isPoints ? ((this.m_pointsLength - 1) * 2) : this.m_pointsLength;
                }
                else
                {
                    this.m_triangleCount = this.m_pointsLength;
                }
            }
            else if (this.m_continuous)
            {
                this.m_triangleCount = !this.m_isPoints ? (this.m_triangleCount = (this.m_pointsLength - 1) * 6) : (this.m_triangleCount = this.m_pointsLength * 6);
                if (this.m_joins == Joins.Fill)
                {
                    this.m_triangleCount += (this.m_pointsLength - 2) * 6;
                    if ((this.m_is2D && (this.points2[0] == this.points2[this.points2.Length - 1])) || (!this.m_is2D && (this.points3[0] == this.points3[this.points3.Length - 1])))
                    {
                        this.m_triangleCount += 6;
                        flag = true;
                    }
                }
            }
            else
            {
                this.m_triangleCount = (this.m_pointsLength / 2) * 6;
            }
            int[] numArray = new int[this.m_triangleCount];
            int pointsLength = 0;
            int index = 0;
            if (!this.m_isPoints)
            {
                pointsLength = !this.m_continuous ? (this.m_pointsLength * 2) : ((this.m_pointsLength - 1) * 4);
            }
            else
            {
                pointsLength = this.m_pointsLength * 4;
            }
            if (this.m_1pixelLine)
            {
                if (!this.m_isPoints)
                {
                    pointsLength = !this.m_continuous ? this.m_pointsLength : ((this.m_pointsLength - 1) * 2);
                }
                else
                {
                    pointsLength = this.m_pointsLength;
                }
                if (this.m_continuous)
                {
                    int num4 = 0;
                    if (!this.m_isPoints)
                    {
                        for (index = 0; index < pointsLength; index++)
                        {
                            numArray[num4] = index;
                            numArray[num4++] = index;
                        }
                    }
                    else
                    {
                        for (index = 0; index < pointsLength; index++)
                        {
                            numArray[index] = index;
                        }
                    }
                }
                else
                {
                    index = 0;
                    while (index < pointsLength)
                    {
                        numArray[index] = index;
                        index++;
                    }
                }
                this.m_mesh.SetIndices(numArray, !this.m_isPoints ? ((MeshTopology)3) : ((MeshTopology)5), 0);
            }
            else
            {
                int num5 = 0;
                for (index = 0; index < pointsLength; index += 4)
                {
                    numArray[num5] = index;
                    numArray[num5 + 1] = index + 2;
                    numArray[num5 + 2] = index + 1;
                    numArray[num5 + 3] = index + 2;
                    numArray[num5 + 4] = index + 3;
                    numArray[num5 + 5] = index + 1;
                    num5 += 6;
                }
                if (this.m_joins == Joins.Fill)
                {
                    pointsLength -= 2;
                    index = 2;
                    while (index < pointsLength)
                    {
                        numArray[num5] = index;
                        numArray[num5 + 1] = index + 2;
                        numArray[num5 + 2] = index + 3;
                        numArray[num5 + 3] = index + 1;
                        numArray[num5 + 4] = index + 2;
                        numArray[num5 + 5] = index + 3;
                        num5 += 6;
                        index += 4;
                    }
                    if (flag)
                    {
                        numArray[num5] = index;
                        numArray[num5 + 1] = 0;
                        numArray[num5 + 2] = index + 1;
                        numArray[num5 + 3] = index;
                        numArray[num5 + 4] = 1;
                        numArray[num5 + 5] = index + 1;
                    }
                }
                this.m_mesh.triangles = (numArray);
            }
        }

        public static void SetVectorCamDepth(int depth)
        {
            if (cam == null)
            {
                LogError("The vector cam has not been set up");
            }
            else
            {
                cam.depth = ((float)depth);
            }
        }

        public void SetWidth(float width)
        {
            this.SetWidth(width, 0, this.pointsLength, true);
        }

        public void SetWidth(float width, int index)
        {
            this.SetWidth(width, index, index, false);
        }

        public void SetWidth(float width, int startIndex, int endIndex)
        {
            this.SetWidth(width, startIndex, endIndex, false);
        }

        private void SetWidth(float width, int startIndex, int endIndex, bool setAll)
        {
            int num = this.MaxSegmentIndex();
            startIndex = Mathf.Clamp(startIndex, 0, num);
            endIndex = Mathf.Clamp(endIndex, 0, num);
            if (this.m_lineWidths.Length == 1)
            {
                if (setAll)
                {
                    this.m_lineWidths[0] = width * 0.5f;
                    return;
                }
                float num2 = this.m_lineWidths[0];
                this.m_lineWidths = new float[this.pointsLength];
                for (int j = 0; j < this.pointsLength; j++)
                {
                    this.m_lineWidths[j] = num2;
                }
            }
            for (int i = startIndex; i <= endIndex; i++)
            {
                this.m_lineWidths[i] = width * 0.5f;
            }
        }

        public void SetWidths(int[] lineWidths)
        {
            this.SetWidths(null, lineWidths, lineWidths.Length, false);
        }

        public void SetWidths(float[] lineWidths)
        {
            this.SetWidths(lineWidths, null, lineWidths.Length, true);
        }

        private void SetWidths(float[] lineWidthsFloat, int[] lineWidthsInt, int arrayLength, bool doFloat)
        {
            if ((doFloat && (lineWidthsFloat == null)) || (!doFloat && (lineWidthsInt == null)))
            {
                LogError("VectorLine.SetWidths: line widths array must not be null");
            }
            else
            {
                if (this.m_isPoints)
                {
                    if (arrayLength != this.pointsLength)
                    {
                        LogError("VectorLine.SetWidths: line widths array must be the same length as the points array for \"" + this.name + "\"");
                        return;
                    }
                }
                else if (this.WrongArrayLength(arrayLength, FunctionName.SetWidths))
                {
                    return;
                }
                if (this.m_1pixelLine)
                {
                    this.RedoLine(false);
                }
                this.m_lineWidths = new float[arrayLength];
                if (doFloat)
                {
                    for (int i = 0; i < arrayLength; i++)
                    {
                        this.m_lineWidths[i] = lineWidthsFloat[i] * 0.5f;
                    }
                }
                else
                {
                    for (int j = 0; j < arrayLength; j++)
                    {
                        this.m_lineWidths[j] = lineWidthsInt[j] * 0.5f;
                    }
                }
            }
        }

        private void Skip(ref int idx, ref int widthIdx, ref Vector3 pos, ref int widthIdxAdd)
        {
            this.m_lineVertices[idx] = pos;
            this.m_lineVertices[idx + 1] = pos;
            this.m_lineVertices[idx + 2] = pos;
            this.m_lineVertices[idx + 3] = pos;
            idx += 4;
            widthIdx += widthIdxAdd;
        }

        public void StopDrawing3DAuto()
        {
            lineManager.RemoveLine(this);
            this.m_isAutoDrawing = false;
        }

        public static void UpdateCameraInfo()
        {
            oldPosition = camTransform.position;
            oldRotation = camTransform.eulerAngles;
        }

        private bool UseMatrix(out Matrix4x4 thisMatrix)
        {
            if (this.m_useTransform != null)
            {
                thisMatrix = this.m_useTransform.localToWorldMatrix;
                return true;
            }
            if (this.m_useMatrix)
            {
                thisMatrix = this.m_matrix;
                return true;
            }
            thisMatrix = Matrix4x4.identity;
            return false;
        }

        private static float VectorDistanceSquared(ref Vector2 p, ref Vector2 q)
        {
            float num = q.x - p.x;
            float num2 = q.y - p.y;
            return ((num * num) + (num2 * num2));
        }

        private static float VectorDistanceSquared(ref Vector3 p, ref Vector3 q)
        {
            float num = q.x - p.x;
            float num2 = q.y - p.y;
            float num3 = q.z - p.z;
            return (((num * num) + (num2 * num2)) + (num3 * num3));
        }

        public static string Version() { return "Vectrosity version 3.1.2"; }

        private void WeldJoins(int start, int end, bool connectFirstAndLast)
        {
            if (connectFirstAndLast)
            {
                this.SetIntersectionPoint(this.m_vertexCount - 4, this.m_vertexCount - 2, 0, 2);
                this.SetIntersectionPoint(this.m_vertexCount - 3, this.m_vertexCount - 1, 1, 3);
            }
            for (int i = start; i < end; i += 4)
            {
                this.SetIntersectionPoint(i - 4, i - 2, i, i + 2);
                this.SetIntersectionPoint(i - 3, i - 1, i + 1, i + 3);
            }
        }

        private void WeldJoins3D(int start, int end, bool connectFirstAndLast)
        {
            if (connectFirstAndLast)
            {
                this.SetIntersectionPoint3D(this.m_vertexCount - 4, this.m_vertexCount - 2, 0, 2);
                this.SetIntersectionPoint3D(this.m_vertexCount - 3, this.m_vertexCount - 1, 1, 3);
            }
            for (int i = start; i < end; i += 4)
            {
                this.SetIntersectionPoint3D(i - 4, i - 2, i, i + 2);
                this.SetIntersectionPoint3D(i - 3, i - 1, i + 1, i + 3);
            }
        }

        private void WeldJoinsDiscrete(int start, int end, bool connectFirstAndLast)
        {
            if (connectFirstAndLast)
            {
                this.SetIntersectionPoint(this.m_vertexCount - 4, this.m_vertexCount - 2, 0, 2);
                this.SetIntersectionPoint(this.m_vertexCount - 3, this.m_vertexCount - 1, 1, 3);
            }
            int num = ((start + 1) / 2) * 4;
            if (this.m_is2D)
            {
                for (int i = start; i < end; i += 2)
                {
                    if (this.points2[i] == this.points2[i + 1])
                    {
                        this.SetIntersectionPoint(num - 4, num - 2, num, num + 2);
                        this.SetIntersectionPoint(num - 3, num - 1, num + 1, num + 3);
                    }
                    num += 4;
                }
            }
            else
            {
                for (int j = start; j < end; j += 2)
                {
                    if (this.points3[j] == this.points3[j + 1])
                    {
                        this.SetIntersectionPoint(num - 4, num - 2, num, num + 2);
                        this.SetIntersectionPoint(num - 3, num - 1, num + 1, num + 3);
                    }
                    num += 4;
                }
            }
        }

        private void WeldJoinsDiscrete3D(int start, int end, bool connectFirstAndLast)
        {
            if (connectFirstAndLast)
            {
                this.SetIntersectionPoint3D(this.m_vertexCount - 4, this.m_vertexCount - 2, 0, 2);
                this.SetIntersectionPoint3D(this.m_vertexCount - 3, this.m_vertexCount - 1, 1, 3);
            }
            int num = ((start + 1) / 2) * 4;
            for (int i = start; i < end; i += 2)
            {
                if (this.points3[i] == this.points3[i + 1])
                {
                    this.SetIntersectionPoint3D(num - 4, num - 2, num, num + 2);
                    this.SetIntersectionPoint3D(num - 3, num - 1, num + 1, num + 3);
                }
                num += 4;
            }
        }

        private bool WrongArrayLength(int arrayLength, FunctionName functionName)
        {
            if (this.m_continuous)
            {
                if (arrayLength != (this.m_pointsLength - 1))
                {
                    LogError(functionNames[(int)functionName] + " array for \"" + this.name + "\" must be length of points array minus one for a continuous line (one entry per line segment)");
                    return true;
                }
            }
            else if (arrayLength != (this.m_pointsLength / 2))
            {
                LogError(functionNames[(int)functionName] + " array in \"" + this.name + "\" must be exactly half the length of points array for a discrete line (one entry per line segment)");
                return true;
            }
            return false;
        }

        public void ZeroPoints()
        {
            this.ZeroPoints(0, this.m_pointsLength);
        }

        public void ZeroPoints(int startIndex)
        {
            this.ZeroPoints(startIndex, this.m_pointsLength);
        }

        public void ZeroPoints(int startIndex, int endIndex)
        {
            if (((endIndex < 0) || (endIndex > this.pointsLength)) || (((startIndex < 0) || (startIndex > this.pointsLength)) || (startIndex > endIndex)))
            {
                LogError(string.Concat(new object[] { "VectorLine: index out of range for \"", this.name, "\" when calling ZeroPoints. StartIndex: ", startIndex, ", EndIndex: ", endIndex, ", array length: ", this.m_pointsLength }));
            }
            else if (this.m_is2D)
            {
                Vector2 vector = Vector2.zero;
                for (int i = startIndex; i < endIndex; i++)
                {
                    this.points2[i] = vector;
                }
            }
            else
            {
                Vector3 vector2 = Vector3.zero;
                for (int j = startIndex; j < endIndex; j++)
                {
                    this.points3[j] = vector2;
                }
            }
        }

        private void ZeroVertices(int startIndex, int endIndex)
        {
            Vector3 vector = Vector3.zero;
            if (this.m_1pixelLine)
            {
                if (this.m_continuous)
                {
                    int index = startIndex * 2;
                    if ((endIndex * 2) > this.m_vertexCount)
                    {
                        endIndex--;
                    }
                    for (int i = startIndex; i < endIndex; i++)
                    {
                        this.m_lineVertices[index] = vector;
                        this.m_lineVertices[index + 1] = vector;
                        index += 2;
                    }
                }
                else
                {
                    for (int j = startIndex; j < endIndex; j++)
                    {
                        this.m_lineVertices[j] = vector;
                    }
                }
            }
            else if (this.m_continuous)
            {
                startIndex *= 4;
                endIndex *= 4;
                if (endIndex > this.m_vertexCount)
                {
                    endIndex -= 4;
                }
                for (int k = startIndex; k < endIndex; k += 4)
                {
                    this.m_lineVertices[k] = vector;
                    this.m_lineVertices[k + 1] = vector;
                    this.m_lineVertices[k + 2] = vector;
                    this.m_lineVertices[k + 3] = vector;
                }
            }
            else
            {
                startIndex *= 2;
                endIndex *= 2;
                for (int m = startIndex; m < endIndex; m += 2)
                {
                    this.m_lineVertices[m] = vector;
                    this.m_lineVertices[m + 1] = vector;
                }
            }
        }

        public bool active
        {
            get { return this.m_active; }
            set
            {
                this.m_active = value;
                if (this.m_vectorObject != null)
                {
                    this.m_vectorObject.GetComponent<Renderer>().enabled = (this.m_active);
                }
            }
        }

        public static bool camTransformExists { get { return (camTransform != null); } }

        public static Vector3 camTransformPosition { get { return camTransform.position; } }

        public bool collider
        {
            get { return this.m_collider; }
            set
            {
                this.m_collider = value;
                this.AddColliderIfNeeded();
                this.vectorObject.GetComponent<Collider2D>().enabled = (value);
            }
        }

        public Color color
        { get { return this.m_lineColors[0]; } }

        public bool continuous
        { get { return this.m_continuous; } }

        public bool continuousTexture
        {
            get { return this.m_continuousTexture; }
            set
            {
                this.m_continuousTexture = value;
                if (!value)
                {
                    this.ResetTextureScale();
                }
            }
        }

        public int depth
        {
            get { return this.m_depth; }
            set
            {
                this.m_depth = Mathf.Clamp(value, 0, 100);
            }
        }

        public int drawEnd
        {
            get { return this.m_drawEnd; }
            set
            {
                if (!this.m_continuous && ((value & 1) == 0))
                {
                    value++;
                }
                this.m_drawEnd = Mathf.Clamp(value, 0, this.pointsLength - 1);
            }
        }

        public int drawStart
        {
            get { return this.m_drawStart; }
            set
            {
                if (!this.m_continuous && ((value & 1) != 0))
                {
                    value++;
                }
                this.m_drawStart = Mathf.Clamp(value, 0, this.pointsLength - 1);
            }
        }

        public Transform drawTransform
        {
            get { return this.m_useTransform; }
            set
            {
                this.m_useTransform = value;
            }
        }

        public string endCap
        {
            get { return this.m_endCap; }
            set
            {
                if (this.m_isPoints)
                {
                    LogError("VectorPoints can't use end caps");
                }
                else if ((value == null) || (value == ""))
                {
                    this.m_endCap = null;
                    this.m_capType = EndCap.None;
                    this.RemoveEndCapVertices();
                }
                else if ((capDictionary == null) || !capDictionary.ContainsKey(value))
                {
                    LogError("End cap \"" + value + "\" is not set up");
                }
                else
                {
                    this.m_endCap = value;
                    this.m_capType = capDictionary[value].capType;
                    if (this.m_capType != EndCap.None)
                    {
                        this.AddEndCap();
                    }
                }
            }
        }

        public bool isAutoDrawing
        { get { return this.m_isAutoDrawing; } }

        public Joins joins
        {
            get { return this.m_joins; }
            set
            {
                if (!this.m_isPoints && (this.m_continuous || (value != Joins.Fill)))
                {
                    this.m_joins = value;
                }
            }
        }

        public int layer
        {
            get { return this.m_layer; }
            set
            {
                this.m_layer = value;
                if (this.m_layer < 0)
                {
                    this.m_layer = 0;
                }
                else if (this.m_layer > 0x1f)
                {
                    this.m_layer = 0x1f;
                }
                if (this.m_vectorObject != null)
                {
                    this.m_vectorObject.layer = (this.m_layer);
                }
            }
        }

        public static LineManager lineManager
        {
            get
            {
                if (!lineManagerCreated)
                {
                    lineManagerCreated = true;
                    GameObject obj2 = new GameObject("LineManager");
                    _lineManager = obj2.AddComponent(typeof(LineManager)) as LineManager;
                    _lineManager.enabled = (false);
                    Object.DontDestroyOnLoad(_lineManager);
                }
                return _lineManager;
            }
        }

        public float lineWidth
        {
            get { return (this.m_lineWidths[0] * 2f); }
            set
            {
                if (this.m_lineWidths.Length == 1)
                {
                    this.m_lineWidths[0] = value * 0.5f;
                }
                else
                {
                    float num = value * 0.5f;
                    for (int i = 0; i < this.m_lineWidths.Length; i++)
                    {
                        this.m_lineWidths[i] = num;
                    }
                }
                this.m_maxWeldDistance = (value * 2f) * (value * 2f);
                if (!this.m_1pixelLine && (value == 1f))
                {
                    this.RedoLine(true);
                }
                else if (this.m_1pixelLine && (value != 1f))
                {
                    this.RedoLine(false);
                }
            }
        }

        public Material material
        {
            get { return this.m_material; }
            set
            {
                this.m_material = value;
                if (this.m_vectorObject != null)
                {
                    this.m_vectorObject.GetComponent<Renderer>().material = (this.m_material);
                }
            }
        }

        public Matrix4x4 matrix
        {
            get { return this.m_matrix; }
            set
            {
                this.m_matrix = value;
                this.m_useMatrix = this.m_matrix != Matrix4x4.identity;
            }
        }

        public int maxDrawIndex
        {
            get { return this.m_maxDrawIndex; }
            set
            {
                if (!this.m_continuous && ((value & 1) == 0))
                {
                    value++;
                }
                this.m_maxDrawIndex = Mathf.Clamp(value, 0, this.pointsLength - 1);
            }
        }

        public float maxWeldDistance
        {
            get { return Mathf.Sqrt(this.m_maxWeldDistance); }
            set
            {
                this.m_maxWeldDistance = value * value;
            }
        }

        public Mesh mesh
        { get { return this.m_mesh; } }

        public int minDrawIndex
        {
            get { return this.m_minDrawIndex; }
            set
            {
                if (!this.m_continuous && ((value & 1) != 0))
                {
                    value++;
                }
                this.m_minDrawIndex = Mathf.Clamp(value, 0, this.pointsLength - 1);
            }
        }

        public string name
        {
            get { return this.m_name; }
            set
            {
                this.m_name = value;
                if (this.m_vectorObject != null)
                {
                    this.m_vectorObject.name = ("Vector " + value);
                }
                if (this.m_mesh != null)
                {
                    this.m_mesh.name = (value);
                }
            }
        }

        public PhysicsMaterial2D physicsMaterial
        {
            get { return this.m_physicsMaterial; }
            set
            {
                this.AddColliderIfNeeded();
                this.m_physicsMaterial = value;
                this.m_vectorObject.GetComponent<Collider2D>().sharedMaterial = (value);
                this.m_vectorObject.GetComponent<Collider2D>().enabled = (false);
                this.m_vectorObject.GetComponent<Collider2D>().enabled = (true);
            }
        }

        private int pointsLength
        {
            get
            {
                if ((!this.m_is2D || (this.m_pointsLength == this.points2.Length)) && (this.m_is2D || (this.m_pointsLength == this.points3.Length)))
                {
                    return this.m_pointsLength;
                }
                LogError("The points array for \"" + this.name + "\" must not be resized. Use Resize if you need to change the length of the points array");
                return 0;
            }
        }

        private static int screenHeight
        {
            get
            {
                if (m_screenHeight == 0)
                {
                    return Screen.height;
                }
                return m_screenHeight;
            }
        }

        private static int screenWidth
        {
            get
            {
                if (m_screenWidth == 0)
                {
                    return Screen.width;
                }
                return m_screenWidth;
            }
        }

        public bool smoothColor
        {
            get { return this.m_smoothColor; }
            set
            {
                this.m_smoothColor = !this.m_isPoints ? value : false;
            }
        }

        public bool smoothWidth
        {
            get { return this.m_smoothWidth; }
            set
            {
                this.m_smoothWidth = !this.m_isPoints ? value : false;
            }
        }

        public int sortingLayerID
        {
            get { return this.vectorObject.GetComponent<Renderer>().sortingLayerID; }
            set
            {
                this.vectorObject.GetComponent<Renderer>().sortingLayerID = (value);
            }
        }

        public int sortingOrder
        {
            get { return this.vectorObject.GetComponent<Renderer>().sortingOrder; }
            set
            {
                if ((value < -32768) || (value > 0x7fff))
                {
                    Debug.LogError("sortingOrder out of range");
                }
                else
                {
                    this.vectorObject.GetComponent<Renderer>().sortingOrder = (value);
                }
            }
        }

        public float textureOffset
        {
            get { return this.m_textureOffset; }
            set
            {
                this.m_textureOffset = value;
                this.SetTextureScale();
            }
        }

        public float textureScale
        {
            get { return this.m_textureScale; }
            set
            {
                this.m_textureScale = value;
                this.m_useTextureScale = this.m_textureScale != 0f;
            }
        }

        public bool trigger
        {
            get { return this.m_trigger; }
            set
            {
                this.m_trigger = value;
                if (this.vectorObject.GetComponent<Collider2D>() != null)
                {
                    this.vectorObject.GetComponent<Collider2D>().isTrigger = (value);
                }
            }
        }

        public static bool useMeshLines
        {
            get { return m_useMeshLines; }
            set
            {
                if (!m_meshRenderMethodSet)
                {
                    m_useMeshLines = value;
                }
                else
                {
                    Debug.LogWarning("useMeshLines not changed, since a VectorLine has already been created");
                }
            }
        }

        public static bool useMeshPoints
        {
            get { return m_useMeshPoints; }
            set
            {
                if (!m_meshRenderMethodSet)
                {
                    m_useMeshPoints = value;
                }
                else
                {
                    Debug.LogWarning("useMeshPoints not changed, since a VectorLine has already been created");
                }
            }
        }

        public bool useViewportCoords
        {
            get { return this.m_viewportDraw; }
            set
            {
                if (this.m_is2D)
                {
                    this.m_viewportDraw = value;
                }
                else
                {
                    Debug.LogWarning("Line must be 2D in order to use viewport coords");
                }
            }
        }

        public static int vectorLayer
        {
            get { return _vectorLayer; }
            set
            {
                _vectorLayer = value;
                if (_vectorLayer > 0x1f)
                {
                    _vectorLayer = 0x1f;
                }
                else if (_vectorLayer < 0)
                {
                    _vectorLayer = 0;
                }
            }
        }

        public static int vectorLayer3D
        {
            get { return _vectorLayer3D; }
            set
            {
                _vectorLayer3D = value;
                if (_vectorLayer > 0x1f)
                {
                    _vectorLayer3D = 0x1f;
                }
                else if (_vectorLayer < 0)
                {
                    _vectorLayer3D = 0;
                }
            }
        }

        public GameObject vectorObject
        {
            get
            {
                if (this.m_vectorObject != null)
                {
                    return this.m_vectorObject;
                }
                LogError("Vector object not set up");
                return null;
            }
        }

        private enum FunctionName
        {
            SetColors,
            SetWidths,
            MakeCurve,
            MakeSpline,
            MakeEllipse
        }
    }
}

