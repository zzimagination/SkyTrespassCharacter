using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Entity a = new Entity();
        Entity b = new Entity();
        Vector3 dirMid = a.position - b.position;
        float angle= Vector3.Dot(dirMid, b.direction);
        if (angle > 0)
            Debug.Log("前方");
        else
            Debug.Log("后方");
       
    }
}

[AAA]
class Entity
{
    public Vector3 position { get; }
    public Vector3 direction { get; }
}

class AAA : System.Attribute
{
    public AAA()
    {

    }
}