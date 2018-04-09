using UnityEngine;
using System.Collections;
using Vectrosity;

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
        if (mTexture != null && mTexture.material != null)
        {
            mTexture.material.SetFloat("_ReverseRange", 1);

        }
    }

    public Camera cam;
    //public Renderer render;
    public UITexture mTexture;
    Texture2D tex;
    int NeedWidth = 1280;
    int NeedHeight = 720;

    [Range(-1, 1)]
    public float _Reverse;
    [ContextMenu("Catch")]
    public void Catch()
    {
        //Debug.Log(string.Format("宽：{0},高{1}", Screen.width, Screen.height));
        cam = VectorLine.GetCamera();
        if (cam == null) return;
        if (mTexture != null)
        {
            NeedWidth = mTexture.width;
            NeedHeight = mTexture.height;
        }
        RenderTexture rT = new RenderTexture(NeedWidth, NeedHeight, 0);
        rT.format = RenderTextureFormat.ARGB32;
        RenderTexture.active = rT;
        cam.targetTexture = rT;
        cam.Render();

        tex = new Texture2D(NeedWidth, NeedHeight, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, NeedWidth, NeedHeight), 0, 0);
        tex.Apply();




        //if (mTexture != null)
        //{
        //    mTexture.material.SetTexture("_SubTex", tex);
        //}

        //RenderTexture.active = null;
        //cam.targetTexture = null;

    }

    private void SetMaterialValue(Material mat)
    {
        if (tex != null)
        {
            mat.SetTexture("_SubTex", tex);
            mat.SetFloat("_ReverseRange", _Reverse);
        }
    }
}
