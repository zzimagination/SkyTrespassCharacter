using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TestCommand : MonoBehaviour
{
    CommandBuffer cb;

    public Material mat;
   
    public RenderTexture rt;
    public bool isGray;
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
        cb.Blit(tex, rt,mat);
        //RenderTexture.ReleaseTemporary(tex);
        mat.EnableKeyword("Gray");
        //mat.DisableKeyword("Gray");
        mat.DisableKeyword("Color");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            mat.EnableKeyword("Gray");
            mat.DisableKeyword("Color");
        }else if(Input.GetKeyDown(KeyCode.C))
        {
            mat.DisableKeyword("Gray");
            mat.EnableKeyword("Color");
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.ExecuteCommandBuffer(cb);
        Graphics.Blit(rt, destination);
    }

 
    private void OnDestroy()
    {
        cb.Dispose();
    }
}
