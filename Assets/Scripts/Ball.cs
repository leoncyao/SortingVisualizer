using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Item {

    public Vector2 startingPos, endingPos;
    public float distX, distY, dist;
    //public static List<float> values = new List<float>();
    float initialX, initialY;

    public override void Start () {
        // call parent start
        base.Start();
        calcLoc();
        size = 0.5f;
    }
    public void calcLoc() {
        int k = Mathf.CeilToInt(Mathf.Sqrt(Main.instance.numItems));
        float initialX = size * (startValue % (k) - k/2.0f);
        float initialY = size * (Mathf.Floor(startValue / k) - k/2.0f);
        Vector2 initial = new Vector2(initialX, initialY);
        endingPos = Main.instance.startLoc + initial;
        //endingPos = Main.instance.startLoc - /* 1 / 2.0f * */ initial;
        transform.position = endingPos;
    }
    public void randomizeLoc(int a,int b)
    {
        System.Random random = new System.Random(a + b);

        //print("Check 1 " + c);
        //print("Check 2 " + Main.instance.numBlocks);
        //print("Check 3 " + (float) c / Main.instance.numBlocks);

        distX = Mathf.Pow(-1, random.Next(0, 2)) * ((float) a / Main.instance.numItems * Mathf.Min(Mathf.Abs(Main.instance.screenSize) - endingPos.x, endingPos.x));
        distY = Mathf.Pow(-1, random.Next(0, 2)) * ((float) b / Main.instance.numItems * Mathf.Min(Mathf.Abs(Main.instance.screenSize) - endingPos.y, endingPos.y));
        startingPos = endingPos + new Vector2(distX, distY);

        dist = (endingPos - startingPos).magnitude;
        //values.Add(dist);

    }
    public static void randomizeAll ()
    {
        for (int i = 0; i < positions.Count; i++)
        {
            Ball temp = (Ball) items[i];
            temp.randomizeLoc(positions[i], positions[positions.Count-i-1]);
            temp.startValue = positions[i];
            temp.setColor(positions[i]);
        }
    }
    public override void Update() {
        base.Update();
        calcLoc();
        //print(size);
        transform.position = startingPos + ((positions.IndexOf(startValue) + 1) / (float)(startValue + 1)) * (endingPos - startingPos);
    }
}
