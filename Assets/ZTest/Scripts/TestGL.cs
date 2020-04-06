using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGL : MonoBehaviour
{

    public Color lineColor;
    List<Mesh> meshs;
    List<Vector3> lines;

    // Start is called before the first frame update
    void Start()
    {
        GetMeshData();
        GenerateLines();

       
    }

   
    // Update is called once per frame
    void Update()
    {

    }

    private void OnRenderObject()
    {
        Shader shader = Shader.Find("Unlit/Color");
        Material mat = new Material(shader);
        mat.SetColor("_Color", lineColor);
        mat.SetPass(0);
    
        GL.PushMatrix();
        GL.MultMatrix(transform.localToWorldMatrix);
        GL.Begin(GL.LINES);
        for (int i = 0; i < lines.Count/3; i++)
        {
            GL.Vertex(lines[i * 3]);
            GL.Vertex(lines[i * 3+1]);
            GL.Vertex(lines[i * 3+1]);
            GL.Vertex(lines[i * 3+2]);
            GL.Vertex(lines[i * 3+2]);
            GL.Vertex(lines[i * 3]);
        }
        GL.End();
        GL.PopMatrix();
    }



    void GenerateLines()
    {
        if (lines != null)
            lines.Clear();
        else
            lines = new List<Vector3>();
        foreach (var mesh in meshs)
        {
            Vector3[] vertices = mesh.vertices;
            int[] triangles = mesh.triangles;
            for (int i = 0; i < triangles.Length/3; i++)
            {
                lines.Add(vertices[triangles[i*3]]);
                lines.Add(vertices[triangles[i*3+1]]);
                lines.Add(vertices[triangles[i*3+2]]);
            }


        }
    }

    void GetMeshData()
    {
        if (meshs == null)
            meshs = new List<Mesh>();
        else
            meshs.Clear();

        var meshFilers= GetComponentsInChildren<MeshFilter>();
        for (int i = 0; i < meshFilers.Length; i++)
        {
            meshs.Add(meshFilers[i].sharedMesh);
        }
    }
}
