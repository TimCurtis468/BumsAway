using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nathan : Target
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        this.TargetInit(animator);
    }

    // Update is called once per frame
    void Update()
    {
        MoveTarget();
    }


}
