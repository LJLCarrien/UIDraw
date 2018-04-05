using System;
using UnityEngine;
using Vectrosity;

[AddComponentMenu("Vectrosity/VisibilityControl")]
public class VisibilityControl : MonoBehaviour
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
        VectorManager.DrawArrayLine2(this.m_objectNumber.i);
    }

    private void OnDestroy()
    {
        if (!this.m_destroyed)
        {
            this.m_destroyed = true;
            VectorManager.VisibilityRemove(this.m_objectNumber.i);
            VectorLine.Destroy(ref this.m_vectorLine);
        }
    }

    public void Setup(VectorLine line, bool makeBounds)
    {
        if (makeBounds)
        {
            VectorManager.SetupBoundsMesh(base.gameObject, line);
        }
        VectorManager.VisibilitySetup(base.transform, line, out this.m_objectNumber);
        this.m_vectorLine = line;
    }

    public RefInt objectNumber { get { return this.m_objectNumber; } }
}

