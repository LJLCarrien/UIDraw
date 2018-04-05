using System;
using UnityEngine;
using Vectrosity;

[AddComponentMenu("Vectrosity/VisibilityControlAlways")]
public class VisibilityControlAlways : MonoBehaviour
{
    private bool m_destroyed = false;
    private RefInt m_objectNumber;
    private VectorLine m_vectorLine;

    private void OnDestroy()
    {
        if (!this.m_destroyed)
        {
            this.m_destroyed = true;
            VectorManager.VisibilityRemove(this.m_objectNumber.i);
            VectorLine.Destroy(ref this.m_vectorLine);
        }
    }

    public void Setup(VectorLine line)
    {
        VectorManager.VisibilitySetup(base.transform, line, out this.m_objectNumber);
        VectorManager.DrawArrayLine2(this.m_objectNumber.i);
        this.m_vectorLine = line;
    }

    public RefInt objectNumber { get { return this.m_objectNumber; } }
}

