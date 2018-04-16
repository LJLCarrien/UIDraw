using UnityEngine;
using System.Collections;
using Vectrosity;
using DG.Tweening;

public class Test : MonoBehaviour
{

    public Material lineMaterial;
    private int maxPoints = 1000;
    private float lineWidth = 4.0f;
    private int minPixelMove = 10; // Must move at least this many pixels per sample for a new segment to be recorded


    private Vector2[] linePoints;
    private VectorLine line;
    private int lineIndex = 0;
    private Vector2 previousPosition;
    private int sqrMinPixelMove;
    private bool canDraw = false;
    private int oldWidth;

    private void Start()
    {

        linePoints = new Vector2[maxPoints];
        line = new VectorLine("DrawnLine", linePoints, lineMaterial, lineWidth, LineType.Continuous);

        sqrMinPixelMove = minPixelMove * minPixelMove;
        oldWidth = Screen.width;


        if (mTexture != null)
            mTexture.onRender += SetMaterialValue;
    }

    private void Update()
    {
        Vector3 previousPosition = Vector3.zero;
#if UNITY_ANDROID||UNITY_IPHONE
        Vector3 touchPos = Input.touches[0].position;
        //按下
        if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Began)
        {
            Cancle();
            previousPosition = linePoints[0] = touchPos;
        }
        //滑动
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && canDraw)
        {
            previousPosition = linePoints[++lineIndex] = touchPos;
        }
        //松开
        //else if (Input.touches[0].phase == TouchPhase.Ended)
        //{

        //}

#else

        Vector3 mousePos = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            //DebugLabel3("+++1");
            Cancle();
            previousPosition = linePoints[0] = mousePos;
        }
        else if (Input.GetMouseButton(0) && (mousePos - previousPosition).sqrMagnitude > sqrMinPixelMove && canDraw)
        {
            previousPosition = linePoints[++lineIndex] = mousePos;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Catch();
        }
#endif

        line.minDrawIndex = lineIndex - 1;
        line.maxDrawIndex = lineIndex;
        if (lineIndex >= maxPoints - 1) canDraw = false;
        line.Draw();


        if (oldWidth != Screen.width)
        {
            oldWidth = Screen.width;
            VectorLine.SetCamera();
        }
    }
    [ContextMenu("Cancle")]
    public void Cancle()
    {
        line.ZeroPoints();
        line.minDrawIndex = 0;
        line.Draw();
        lineIndex = 0;
        canDraw = true;
        Catch();
        cam = VectorLine.GetCamera();
        if (cam.targetTexture != null)
        {
            cam.targetTexture = null;
        }
        if (rT != null)
        {
            RenderTexture.ReleaseTemporary(rT);
        }
        //if (mTexture != null && mTexture.material != null)
        //{
        //    mTexture.material.SetFloat("_ReverseRange", 1);

        //}
    }

    public Camera cam;
    //public Renderer render;
    public UITexture mTexture;
    Texture2D tex;
    int NeedWidth = 1280;
    int NeedHeight = 720;

    //[Range(-1, 1)]
    //public float _Reverse;

    [Range(0, 1000)]
    public float mSizeX;

    [Range(0, 1000)]
    public float mSizeY;

    private RenderTexture rT;
    [ContextMenu("Catch")]
    public void Catch()
    {
        cam = VectorLine.GetCamera();
        if (cam == null) return;
        if (mTexture != null)
        {
            NeedWidth = mTexture.width;
            NeedHeight = mTexture.height;
        }
        rT = RenderTexture.GetTemporary(NeedWidth, NeedHeight, 0, RenderTextureFormat.ARGB32);
        RenderTexture.active = rT;
        cam.targetTexture = rT;
        cam.Render();

        tex = new Texture2D(NeedWidth, NeedHeight, TextureFormat.ARGB32, false);
        tex.ReadPixels(new Rect(0, 0, NeedWidth, NeedHeight), 0, 0);
        tex.Apply();
    }

    private void SetMaterialValue(Material mat)
    {
        if (tex != null)
        {
            mat.SetTexture("_SubTex", tex);
            Vector4 cr = new Vector4(0, 0, mSizeX, mSizeY);
            Vector2 sharpness = new Vector2(1000.0f, 1000.0f);

            mat.SetVector("_ClipRange0", new Vector4(-cr.x / cr.z, -cr.y / cr.w, 1f / cr.z, 1f / cr.w));
            mat.SetVector("_ClipArgs0", new Vector4(sharpness.x, sharpness.y, Mathf.Sin(0), Mathf.Cos(0)));

            //mat.SetFloat("_ReverseRange", _Reverse);
        }
    }
    //public UIPanel mPanel;
    //Tweener dt;

    //[ContextMenu("aaa")]
    //public void PlayForwardAni()
    //{
    //    int fromInt = (int)mPanel.baseClipRegion.z;
    //    int w = (int)mPanel.baseClipRegion.w;
    //    DOTween.To(() => fromInt, z => mPanel.baseClipRegion = new Vector4(0, 0, z, w), 1280, 3);

    //}
    //[ContextMenu("bbb")]
    //public void PlayBackdAni()
    //{
    //    int fromInt = (int)mPanel.baseClipRegion.z;
    //    int w = (int)mPanel.baseClipRegion.w;

    //    DOTween.To(() => fromInt, z => mPanel.baseClipRegion = new Vector4(0, 0, z, w), 10, 3);

    //}
}
