using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Item {

    public override void Start()
    {
        base.Start();
        transform.localScale = new Vector3(size / 2f * startValue + 0.1f, size, size);
    }

    public override void Update()
    {
        transform.position = Main.instance.startLoc + 2 * size * (Main.instance.numItems / 2.0f - positions.IndexOf(startValue)) * Vector2.up;
        //GetComponent<Renderer>().material.color = new Color(0, 0, Main.instance.screenSize / Main.instance.numBlocks * transform.position.x);
    }
}
