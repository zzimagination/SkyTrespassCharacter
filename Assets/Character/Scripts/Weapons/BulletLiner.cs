using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLiner : MonoBehaviour
{
    public BulletLinerPool pool;
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    public float lineWidth;
    public float fadeAway;
    public float uvOffset;

    Mesh lineMesh;
    List<Vector3> vertexList;
    List<Vector2> uvList;
    List<int> triangleList;
    int lineNumber;
    float timeAway;
    int uvNumber;

    bool isPlay;
    private void Start()
    {

    }
    private void Update()
    {
        if (isPlay)
        {
            UpdateUV();
        }
    }

    void UpdateUV()
    {
        if (timeAway > 0.02f)
        {
            timeAway = 0;
            for (int i = 0; i < uvList.Count; i++)
            {
                Vector2 t = uvList[i];
                t.x += uvOffset;
                uvList[i] = t;
            }
            lineMesh.SetUVs(0, uvList);
            uvNumber++;
            if (uvNumber > 2)
            {
                uvList[0] = (new Vector2(0, 0));
                uvList[1] = (new Vector2(0, 1));
                uvList[2] = (new Vector2(uvOffset, 1));
                uvList[3] = (new Vector2(uvOffset, 0));
                Stop();
            }
        }
        else
        {
            timeAway += Time.deltaTime;
        }
    }

    public void Play()
    {
        isPlay = true;
        timeAway = 0;
        uvNumber = 0;
        meshRenderer.enabled = true;
    }

    public void Stop()
    {
        isPlay = false;
        pool.InActiveLiner(this);
        meshRenderer.enabled = false;
    }

    public void SetLine(Vector3 start, Vector3 end)
    {
        if (meshFilter.mesh)
        {
            lineMesh = meshFilter.mesh;
        }
        else
        {
            lineMesh = new Mesh();
            meshFilter.mesh = lineMesh;
        }
        vertexList = new List<Vector3>();
        uvList = new List<Vector2>();
        triangleList = new List<int>();

        vertexList.Add(new Vector3(start.x - lineWidth / 2, start.y, start.z));
        vertexList.Add(new Vector3(end.x - lineWidth / 2, end.y, end.z));
        vertexList.Add(new Vector3(end.x + lineWidth / 2, end.y, end.z));
        vertexList.Add(new Vector3(start.x + lineWidth / 2, start.y, start.z));

        uvList.Add((new Vector2(0, 0)));
        uvList.Add((new Vector2(0, 1)));
        uvList.Add((new Vector2(uvOffset, 1)));
        uvList.Add((new Vector2(uvOffset, 0)));

        triangleList.Add(0);
        triangleList.Add(1);
        triangleList.Add(2);
        triangleList.Add(0);
        triangleList.Add(2);
        triangleList.Add(3);
        lineMesh.SetVertices(vertexList);
        lineMesh.SetUVs(0, uvList);
        lineMesh.SetTriangles(triangleList, 0);

        Play();
    }


}
