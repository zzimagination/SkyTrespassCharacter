using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacter : MonoBehaviour
{
    public Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Death()
    {
        _animator.Play("Death");
    }
}
