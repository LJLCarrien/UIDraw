using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using Vectrosity;
using Object = UnityEngine.Object;

[AddComponentMenu("Vectrosity/LineManager")]
public class LineManager : MonoBehaviour
{
    private bool destroyed = false;
    private static int lineCount = 0;
    private static List<VectorLine> lines;
    private static List<Transform> transforms;

    public void AddLine(VectorLine vectorLine, Transform thisTransform, float time)
    {
        if (time > 0f)
        {
            base.StartCoroutine(this.DisableLine(vectorLine, time, false));
        }
        for (int i = 0; i < lineCount; i++)
        {
            if (vectorLine == lines[i])
            {
                return;
            }
        }
        lines.Add(vectorLine);
        transforms.Add(thisTransform);
        if (++lineCount == 1)
        {
            base.enabled = (true);
        }
    }

    private void Awake()
    {
        this.Initialize();
        Object.DontDestroyOnLoad(this);
    }

    private void CheckDistance()
    {
        VectorManager.CheckDistance();
    }

    public void DisableIfUnused()
    {
        if ((!this.destroyed && (lineCount == 0)) && ((VectorManager.arrayCount == 0) && (VectorManager.arrayCount2 == 0)))
        {
            base.enabled = (false);
        }
    }

    public void DisableLine(VectorLine vectorLine, float time)
    {
        base.StartCoroutine(this.DisableLine(vectorLine, time, false));
    }

    private IEnumerator DisableLine(VectorLine vectorLine, float time, bool remove)
    {
        yield return new WaitForSeconds(time);
        if (!remove)
        {
            RemoveLine(vectorLine);
            VectorLine.Destroy(ref vectorLine);
        }
        else RemoveLine(vectorLine);
        vectorLine = null;
    }

    public void EnableIfUsed()
    {
        if ((VectorManager.arrayCount == 1) || (VectorManager.arrayCount2 == 1))
        {
            base.enabled = (true);
        }
    }

    private void Initialize()
    {
        lines = new List<VectorLine>();
        transforms = new List<Transform>();
        lineCount = 0;
        base.enabled = (false);
    }

    private void LateUpdate()
    {
        if (VectorLine.camTransformExists)
        {
            for (int i = 0; i < lineCount; i++)
            {
                if (lines[i].vectorObject != null)
                {
                    lines[i].Draw3D();
                }
                else
                {
                    this.RemoveLine(i--);
                }
            }
            if (VectorLine.CameraHasMoved())
            {
                VectorManager.DrawArrayLines();
            }
            VectorLine.UpdateCameraInfo();
            VectorManager.DrawArrayLines2();
        }
    }

    private void OnDestroy()
    {
        this.destroyed = true;
    }

    private void OnLevelWasLoaded()
    {
        this.Initialize();
    }

    private void RemoveLine(int i)
    {
        lines.RemoveAt(i);
        transforms.RemoveAt(i);
        lineCount--;
        this.DisableIfUnused();
    }

    public void RemoveLine(VectorLine vectorLine)
    {
        for (int i = 0; i < lineCount; i++)
        {
            if (vectorLine == lines[i])
            {
                this.RemoveLine(i);
                break;
            }
        }
    }

    public void StartCheckDistance()
    {
        base.InvokeRepeating("CheckDistance", 0.01f, VectorManager.distanceCheckFrequency);
    }
}

//[CompilerGenerated]
//private sealed class <DisableLine>c__Iterator0 : IEnumerator, IEnumerator<object>, IDisposable
//    {
//        internal object $current;
//        internal int $PC;
//        internal bool <$>remove;
//        internal float <$>time;
//        internal VectorLine<$> vectorLine;
//internal bool remove;
//internal float time;
//internal VectorLine vectorLine;

//[DebuggerHidden]
//public void Dispose()
//{
//    this.$PC = -1;
//}

//public bool MoveNext()
//{
//    uint num = (uint)this.$PC;
//    this.$PC = -1;
//    switch (num)
//    {
//        case 0:
//            this.$current = new WaitForSeconds(this.time);
//            this.$PC = 1;
//            return true;

//        case 1:
//            if (!this.remove)
//            {
//                this.<> f__this.RemoveLine(this.vectorLine);
//                VectorLine.Destroy(ref this.vectorLine);
//                break;
//            }
//            this.<> f__this.RemoveLine(this.vectorLine);
//            break;

//        default:
//            goto Label_0089;
//    }
//    this.vectorLine = null;
//    this.$PC = -1;
//Label_0089:
//    return false;
//}

//[DebuggerHidden]
//public void Reset()
//{
//    throw new NotSupportedException();
//}

//object IEnumerator<object>.Current{return//    this.$current;

//        object IEnumerator.Current{return//            this.$current;
//    }
//}

