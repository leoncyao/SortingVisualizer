using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Block {

    public static List<Ball> balls = new List<Ball>();
    public float ballSize;
    public Vector2 startingPos, endingPos;
    public float distX, distY, dist;
    float t = 0;
    public static List<float> values = new List<float>();
    public override void Start () {
        // call parent start
        base.Start();
        transform.localScale = new Vector3(blockSize, blockSize, blockSize);

        intialLoc();

        //randomizeLoc();

    }
    public void intialLoc() {
        float initialX = startValue % Mathf.Floor(Mathf.Sqrt(Main.instance.numBlocks));
        float initialY = Mathf.Floor(startValue / Mathf.Floor(Mathf.Sqrt(Main.instance.numBlocks)));
        Vector2 initial = new Vector2(initialX, initialY);
        endingPos = Main.instance.startLoc - 1 / 2.0f * initial;
        transform.position = endingPos;
    }
    public void randomizeLoc()
    {
        System.Random random = new System.Random(startValue);
        distX = ((float)random.NextDouble() * 2 - 1) * Mathf.Min(Mathf.Abs(Main.instance.screenSize) - endingPos.x, endingPos.x);
        distY = ((float)random.NextDouble() * 2 - 1) * Mathf.Min(Mathf.Abs(Main.instance.screenSize) - endingPos.y, endingPos.y);
        startingPos = endingPos + new Vector2(distX, distY);
        dist = (endingPos - startingPos).magnitude;
        values.Add(dist);
    }

    public override void Update() {
        //transform.position = startingPos + (positions.IndexOf(startValue)/Main.instance.numBlocks) * (endingPos - startingPos);
        //if (t < 1) { 
        //    t += 0.1f;
        //}
        //transform.position = Main.instance.startLoc + 2 * blockSize * (Main.instance.numBlocks / 2.0f - positions.IndexOf(startValue)) * Vector2.up;
    }
}
