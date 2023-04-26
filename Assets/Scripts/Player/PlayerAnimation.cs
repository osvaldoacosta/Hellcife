using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const string IS_RUNNING = "isRunning";

    public Animator anim;

    [SerializeField] private PlayerMovement pm;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetBool(IS_RUNNING, pm.isRunning);
        
        
    }

    void OnFootstep()
    {

    }
    

  
}
