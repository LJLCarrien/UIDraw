using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using Vectrosity;

[AddComponentMenu("Vectrosity/VisibilityControlStatic")]
public class VisibilityControlStatic : MonoBehaviour
{
    private bool m_destroyed = false;
    private RefInt m_objectNumber;
    private VectorLine m_vectorLine;

    private void OnBecameInvisible()
    {
        this.m_vectorLine.active = false;
    }

    private void OnBecameVisible()
    {
        this.m_vectorLine.active = true;
        VectorManager.DrawArrayLine(this.m_objectNumber.i);
    }

    private void OnDestroy()
    {
        if (!this.m_destroyed)
        {
            this.m_destroyed = true;
            VectorManager.VisibilityStaticRemove(this.m_objectNumber.i);
            VectorLine.Destroy(ref this.m_vectorLine);
        }
    }

    public void Setup(VectorLine line, bool makeBounds)
    {
        if (makeBounds)
        {
            VectorManager.SetupBoundsMesh(base.gameObject, line);
        }
        Vector3[] vectorArray = new Vector3[line.points3.Length];
        Matrix4x4 matrixx = base.transform.localToWorldMatrix;
        for (int i = 0; i < vectorArray.Length; i++)
        {
            vectorArray[i] = matrixx.MultiplyPoint3x4(line.points3[i]);
        }
        line.points3 = vectorArray;
        this.m_vectorLine = line;
        VectorManager.VisibilityStaticSetup(line, out this.m_objectNumber);
        base.StartCoroutine(this.WaitCheck());
    }

    private IEnumerator WaitCheck()
    {
        VectorManager.DrawArrayLine(this.m_objectNumber.i);
        yield return null;
        if (!this.GetComponent<Renderer>().isVisible)
        {
            this.m_vectorLine.active = false;
        }
    }
}

