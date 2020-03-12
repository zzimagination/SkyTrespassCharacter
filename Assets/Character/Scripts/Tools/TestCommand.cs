using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TestCommand : MonoBehaviour
{
    public Camera mainCamera;
    public MeshFilter meshFilter;
    CommandBuffer cb;

    public Material mat;
    public RenderTexture rt;
    // Start is called before the first frame update
    void Start()
    {
        cb = new CommandBuffer();
        cb.name = "OLLLLLLCommand";
        Material cm = new Material(mat);
        cm.hideFlags = HideFlags.DontSave;
        rt = new RenderTexture(Screen.width, Screen.height, 0);
        RenderTexture tex = RenderTexture.GetTemporary(rt.descriptor);
        cb.Blit(BuiltinRenderTextureType.CurrentActive, tex);
        cb.Blit(tex, rt, mat);
        //mainCamera.AddCommandBuffer(CameraEvent.BeforeImageEffects, cb);

        //Mesh screen = new Mesh();
        //List<Vector3> vertexs = new List<Vector3>();
        //vertexs.Add(new Vector3(0, 0, 0));
        //vertexs.Add(new Vector3(1, 0, 0));
        //vertexs.Add(new Vector3(1, 1, 0));
        //vertexs.Add(new Vector3(1, 1, 0));
        //vertexs.Add(new Vector3(0, 1, 0));
        //vertexs.Add(new Vector3(0, 0, 0));

        //List<Vector2> uvs = new List<Vector2>();
        //uvs.Add(new Vector2(0,0));
        //uvs.Add(new Vector2(1,0));
        //uvs.Add(new Vector2(1,1));
        //uvs.Add(new Vector2(1,1));
        //uvs.Add(new Vector2(0,1));
        //uvs.Add(new Vector2(0,0));

        //int[] indexes = new int[] { 0, 1, 2, 3, 4, 5 };


        ////List<int> index = new List<int>()
        ////{
        ////    0, 3, 2, 0, 1, 3
        ////};
        //screen.SetVertices(vertexs);
        //screen.SetUVs(0, uvs);
        ////  screen.SetTriangles(index, 0);
        //screen.SetIndices(indexes, MeshTopology.Triangles, 0);
        //meshFilter.mesh = screen;



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(rt, destination);
    }

 
    private void OnDestroy()
    {
        cb.Dispose();
    }
}
