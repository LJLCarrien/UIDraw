using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Vectrosity;
using Object = UnityEngine.Object;

public class VectorManager
{
    public static int _arrayCount = 0;
    private static int _arrayCount2 = 0;
    private static int _arrayCount3 = 0;
    private static int brightnessLevels = 0x20;
    private static List<Color> colors;
    public static float distanceCheckFrequency = 0.2f;
    private static Color fogColor;
    public static float maxBrightnessDistance = 250f;
    private static Dictionary<string, Mesh> meshTable;
    public static float minBrightnessDistance = 500f;
    private static List<RefInt> objectNumbers;
    private static List<RefInt> objectNumbers2;
    private static List<RefInt> objectNumbers3;
    private static List<int> oldDistances;
    private static List<Transform> transforms3;
    public static bool useDraw3D = false;
    private static List<VectorLine> vectorLines;
    private static List<VectorLine> vectorLines2;
    private static List<VectorLine> vectorLines3;

    public static void CheckDistance()
    {
        for (int i = 0; i < _arrayCount3; i++)
        {
            SetDistanceColor(i);
        }
    }

    public static void CheckDistanceSetup(Transform thisTransform, VectorLine line, Color color, RefInt objectNum)
    {
        VectorLine.LineManagerEnable();
        if (vectorLines3 == null)
        {
            vectorLines3 = new List<VectorLine>();
            transforms3 = new List<Transform>();
            oldDistances = new List<int>();
            colors = new List<Color>();
            objectNumbers3 = new List<RefInt>();
            VectorLine.LineManagerCheckDistance();
        }
        transforms3.Add(thisTransform);
        vectorLines3.Add(line);
        oldDistances.Add(-1);
        colors.Add(color);
        objectNum.i = _arrayCount3++;
        objectNumbers3.Add(objectNum);
    }

    public static void DistanceRemove(int objectNumber)
    {
        if (objectNumber >= vectorLines3.Count)
        {
            Debug.LogError("VectorManager: object number exceeds array length in DistanceRemove");
        }
        else
        {
            for (int i = objectNumber + 1; i < _arrayCount3; i++)
            {
                RefInt local1 = objectNumbers3[i];
                local1.i--;
            }
            transforms3.RemoveAt(objectNumber);
            vectorLines3.RemoveAt(objectNumber);
            oldDistances.RemoveAt(objectNumber);
            colors.RemoveAt(objectNumber);
            objectNumbers3.RemoveAt(objectNumber);
            _arrayCount3--;
        }
    }

    public static void DrawArrayLine(int i)
    {
        if (useDraw3D)
        {
            vectorLines[i].Draw3D();
        }
        else
        {
            vectorLines[i].Draw();
        }
    }

    public static void DrawArrayLine2(int i)
    {
        if (useDraw3D)
        {
            vectorLines2[i].Draw3D();
        }
        else
        {
            vectorLines2[i].Draw();
        }
    }

    public static void DrawArrayLines()
    {
        if (useDraw3D)
        {
            for (int i = 0; i < _arrayCount; i++)
            {
                vectorLines[i].Draw3D();
            }
        }
        else
        {
            for (int j = 0; j < _arrayCount; j++)
            {
                vectorLines[j].Draw();
            }
        }
    }

    public static void DrawArrayLines2()
    {
        if (useDraw3D)
        {
            for (int i = 0; i < _arrayCount2; i++)
            {
                vectorLines2[i].Draw3D();
            }
        }
        else
        {
            for (int j = 0; j < _arrayCount2; j++)
            {
                vectorLines2[j].Draw();
            }
        }
    }

    public static Bounds GetBounds(VectorLine line)
    {
        if (line.points3 == null)
        {
            Debug.LogError("VectorManager: GetBounds can only be used with a Vector3 array");
            return new Bounds();
        }
        return GetBounds(line.points3);
    }

    public static Bounds GetBounds(Vector3[] points3)
    {
        Bounds bounds = new Bounds();
        Vector3 vector = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        Vector3 vector2 = new Vector3(float.MinValue, float.MinValue, float.MinValue);
        int length = points3.Length;
        for (int i = 0; i < length; i++)
        {
            if (points3[i].x < vector.x)
            {
                vector.x = points3[i].x;
            }
            else if (points3[i].x > vector2.x)
            {
                vector2.x = points3[i].x;
            }
            if (points3[i].y < vector.y)
            {
                vector.y = points3[i].y;
            }
            else if (points3[i].y > vector2.y)
            {
                vector2.y = points3[i].y;
            }
            if (points3[i].z < vector.z)
            {
                vector.z = points3[i].z;
            }
            else if (points3[i].z > vector2.z)
            {
                vector2.z = points3[i].z;
            }
        }
        bounds.min = (vector);
        bounds.max = (vector2);
        return bounds;
    }

    public static float GetBrightnessValue(Vector3 pos)
    {
        Vector3 vector = pos - VectorLine.camTransformPosition;
        return Mathf.InverseLerp(minBrightnessDistance, maxBrightnessDistance, vector.sqrMagnitude);
    }

    private static Mesh MakeBoundsMesh(Bounds bounds)
    {
        Mesh mesh = new Mesh();
        Vector3[] vectorArray1 = new Vector3[] { bounds.center + new Vector3(-bounds.extents.x, bounds.extents.y, bounds.extents.z), bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, bounds.extents.z), bounds.center + new Vector3(-bounds.extents.x, bounds.extents.y, -bounds.extents.z), bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, -bounds.extents.z), bounds.center + new Vector3(-bounds.extents.x, -bounds.extents.y, bounds.extents.z), bounds.center + new Vector3(bounds.extents.x, -bounds.extents.y, bounds.extents.z), bounds.center + new Vector3(-bounds.extents.x, -bounds.extents.y, -bounds.extents.z), bounds.center + new Vector3(bounds.extents.x, -bounds.extents.y, -bounds.extents.z) };
        mesh.vertices = (vectorArray1);
        return mesh;
    }

    public static void ObjectSetup(GameObject go, VectorLine line, Visibility visibility, Brightness brightness)
    {
        ObjectSetup(go, line, visibility, brightness, true);
    }

    public static void ObjectSetup(GameObject go, VectorLine line, Visibility visibility, Brightness brightness, bool makeBounds)
    {
        VisibilityControl component = go.GetComponent(typeof(VisibilityControl)) as VisibilityControl;
        VisibilityControlStatic @static = go.GetComponent(typeof(VisibilityControlStatic)) as VisibilityControlStatic;
        VisibilityControlAlways always = go.GetComponent(typeof(VisibilityControlAlways)) as VisibilityControlAlways;
        BrightnessControl control2 = go.GetComponent(typeof(BrightnessControl)) as BrightnessControl;
        if (visibility == Visibility.None)
        {
            if (component != null)
            {
                Object.Destroy(component);
            }
            if (@static != null)
            {
                Object.Destroy(@static);
            }
            if (always != null)
            {
                Object.Destroy(always);
            }
        }
        if (visibility == Visibility.Dynamic)
        {
            if (component == null)
            {
                component = go.AddComponent(typeof(VisibilityControl)) as VisibilityControl;
                component.Setup(line, makeBounds);
                if (control2 != null)
                {
                    control2.SetUseLine(false);
                }
            }
        }
        else if (visibility == Visibility.Static)
        {
            if (@static == null)
            {
                @static = go.AddComponent(typeof(VisibilityControlStatic)) as VisibilityControlStatic;
                @static.Setup(line, makeBounds);
                if (control2 != null)
                {
                    control2.SetUseLine(false);
                }
            }
        }
        else if ((visibility == Visibility.Always) && (always == null))
        {
            always = go.AddComponent(typeof(VisibilityControlAlways)) as VisibilityControlAlways;
            always.Setup(line);
            if (control2 != null)
            {
                control2.SetUseLine(false);
            }
        }
        if (brightness == Brightness.Fog)
        {
            if (control2 == null)
            {
                control2 = go.AddComponent(typeof(BrightnessControl)) as BrightnessControl;
                if (((component == null) && (@static == null)) && (always == null))
                {
                    control2.Setup(line, true);
                }
                else
                {
                    control2.Setup(line, false);
                }
            }
        }
        else if (control2 != null)
        {
            Object.Destroy(control2);
        }
    }

    public static void SetBrightnessParameters(float min, float max, int levels, float frequency, Color color)
    {
        minBrightnessDistance = min * min;
        maxBrightnessDistance = max * max;
        brightnessLevels = levels;
        distanceCheckFrequency = frequency;
        fogColor = color;
    }

    public static void SetDistanceColor(int i)
    {
        if (vectorLines3[i].active)
        {
            float brightnessValue = GetBrightnessValue(transforms3[i].position);
            int num2 = (int)(brightnessValue * brightnessLevels);
            if (num2 != oldDistances[i])
            {
                vectorLines3[i].SetColor(Color.Lerp(fogColor, colors[i], brightnessValue));
            }
            oldDistances[i] = num2;
        }
    }

    public static void SetOldDistance(int objectNumber, int val)
    {
        oldDistances[objectNumber] = val;
    }

    public static void SetupBoundsMesh(GameObject go, VectorLine line)
    {
        MeshFilter component = go.GetComponent<MeshFilter>();
        if (component == null)
        {
            component = go.AddComponent<MeshFilter>();
        }
        MeshRenderer renderer = go.GetComponent<MeshRenderer>();
        if (renderer == null)
        {
            renderer = go.AddComponent<MeshRenderer>();
        }
        renderer.enabled = (true);
        if (meshTable == null)
        {
            meshTable = new Dictionary<string, Mesh>();
        }
        if (!meshTable.ContainsKey(line.vectorObject.name))
        {
            meshTable.Add(line.vectorObject.name, MakeBoundsMesh(GetBounds(line)));
            meshTable[line.vectorObject.name].name = (line.vectorObject.name + " Bounds");
        }
        component.mesh = (meshTable[line.vectorObject.name]);
    }

    public static void VisibilityRemove(int objectNumber)
    {
        if (objectNumber >= vectorLines2.Count)
        {
            Debug.LogError("VectorManager: object number exceeds array length in VisibilityRemove");
        }
        else
        {
            for (int i = objectNumber + 1; i < _arrayCount2; i++)
            {
                RefInt local1 = objectNumbers2[i];
                local1.i--;
            }
            vectorLines2.RemoveAt(objectNumber);
            objectNumbers2.RemoveAt(objectNumber);
            _arrayCount2--;
            VectorLine.LineManagerDisable();
        }
    }

    public static void VisibilitySetup(Transform thisTransform, VectorLine line, out RefInt objectNum)
    {
        if (vectorLines2 == null)
        {
            vectorLines2 = new List<VectorLine>();
            objectNumbers2 = new List<RefInt>();
        }
        line.drawTransform = thisTransform;
        vectorLines2.Add(line);
        objectNum = new RefInt(_arrayCount2++);
        objectNumbers2.Add(objectNum);
        VectorLine.LineManagerEnable();
    }

    public static void VisibilityStaticRemove(int objectNumber)
    {
        if (objectNumber >= vectorLines.Count)
        {
            Debug.LogError("VectorManager: object number exceeds array length in VisibilityStaticRemove");
        }
        else
        {
            for (int i = objectNumber + 1; i < _arrayCount; i++)
            {
                RefInt local1 = objectNumbers[i];
                local1.i--;
            }
            vectorLines.RemoveAt(objectNumber);
            objectNumbers.RemoveAt(objectNumber);
            _arrayCount--;
            VectorLine.LineManagerDisable();
        }
    }

    public static void VisibilityStaticSetup(VectorLine line, out RefInt objectNum)
    {
        if (vectorLines == null)
        {
            vectorLines = new List<VectorLine>();
            objectNumbers = new List<RefInt>();
        }
        vectorLines.Add(line);
        objectNum = new RefInt(_arrayCount++);
        objectNumbers.Add(objectNum);
        VectorLine.LineManagerEnable();
    }

    public static int arrayCount { get { return _arrayCount; } }

    public static int arrayCount2
    { get { return _arrayCount2; } }
}

