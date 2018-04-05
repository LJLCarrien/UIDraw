namespace Vectrosity
{
    using System;
    using UnityEngine;

    public class CapInfo
    {
        public EndCap capType;
        public Material material;
        public float offset;
        public float ratio1;
        public float ratio2;
        public Texture2D texture;

        public CapInfo(EndCap capType, Material material, Texture2D texture, float ratio1, float ratio2, float offset)
        {
            this.capType = capType;
            this.material = material;
            this.texture = texture;
            this.ratio1 = ratio1;
            this.ratio2 = ratio2;
            this.offset = offset;
        }
    }
}

