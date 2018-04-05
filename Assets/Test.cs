using UnityEngine;
using System.Collections;
using Vectrosity;

public class Test : MonoBehaviour
{

    public Material lineMaterial;
    private int maxPoints = 1000;
    private float lineWidth = 7.0f;
    private int minPixelMove = 5; // Must move at least this many pixels per sample for a new segment to be recorded


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
#if UNITY_ANDROID
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
#elif UNITY_IPHONE
      Debgug.Log("UNITY_IPHONE");
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
    }

    public Camera cam;
    //public Renderer render;
    public UITexture mTexture;
    Texture2D tex;
    const int ScreenWidth = 1280;
    const int ScreenHeight = 720;
    [ContextMenu("Catch")]
    public void Catch()
    {
        Debug.Log(string.Format("宽：{0},高{1}", Screen.width, Screen.height));
        cam = VectorLine.GetCamera();
        if (cam == null) return;
        RenderTexture rT = new RenderTexture(ScreenWidth, ScreenHeight, 0);
        RenderTexture.active = rT;
        cam.targetTexture = rT;
        cam.Render();
        tex = new Texture2D(ScreenWidth, ScreenHeight, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, ScreenWidth, ScreenHeight), 0, 0);
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
        Debug.Log("sdfsdf");
        if ( tex != null)
        {
            mat.SetTexture("_SubTex", tex);
        }
    }
}
