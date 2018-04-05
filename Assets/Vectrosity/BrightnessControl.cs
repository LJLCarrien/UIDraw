using System;
using UnityEngine;
using Vectrosity;

[AddComponentMenu("Vectrosity/BrightnessControl")]
public class BrightnessControl : MonoBehaviour
{
    private bool m_destroyed = false;
    private RefInt m_objectNumber;
    private bool m_useLine = false;
    private VectorLine m_vectorLine;

    public void OnBecameInvisible()
    {
        if (this.m_useLine)
        {
            this.m_vectorLine.active = false;
        }
    }

    private void OnBecameVisible()
    {
        VectorManager.SetOldDistance(this.m_objectNumber.i, -1);
        VectorManager.SetDistanceColor(this.m_objectNumber.i);
        if (this.m_useLine)
        {
            this.m_vectorLine.active = true;
        }
    }

    private void OnDestroy()
    {
        if (!this.m_destroyed)
        {
            this.m_destroyed = true;
            VectorManager.DistanceRemove(this.m_objectNumber.i);
            if (this.m_useLine)
            {
                VectorLine.Destroy(ref this.m_vectorLine);
            }
        }
    }

    public void Setup(VectorLine line, bool m_useLine)
    {
        this.m_objectNumber = new RefInt(0);
        VectorManager.CheckDistanceSetup(base.transform, line, line.color, this.m_objectNumber);
        VectorManager.SetDistanceColor(this.m_objectNumber.i);
        if (m_useLine)
        {
            this.m_useLine = true;
            this.m_vectorLine = line;
        }
    }

    public void SetUseLine(bool useLine)
    {
        this.m_useLine = useLine;
    }

    public RefInt objectNumber { get { return this.m_objectNumber; } }
}

