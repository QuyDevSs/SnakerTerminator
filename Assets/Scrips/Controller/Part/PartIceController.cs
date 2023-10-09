using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartIceController : PartController
{
    public override void Update()
    {
        if (isPause)
        {
            return;
        }
    }
}
