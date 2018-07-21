using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Item {

    public Vector3 startingPos, endingPos;
    public float distX, distY, distZ, dist;
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
        Vector3 initial = new Vector3(initialX, initialY, 0);
        endingPos = new Vector3(Main.instance.startLoc.x, Main.instance.startLoc.y) + initial;
        //endingPos = Main.instance.startLoc - /* 1 / 2.0f * */ initial;
    }
    public void randomizeLoc(int a,int b)
    {
        System.Random random = new System.Random(a + b);

        //print("Check 1 " + c);
        //print("Check 2 " + Main.instance.numBlocks);
        //print("Check 3 " + (float) c / Main.instance.numBlocks);

        distX = Mathf.Pow(-1, random.Next(0, 2)) * ((float) a / Main.instance.numItems * Mathf.Min(Mathf.Abs(Main.instance.screenSize) - endingPos.x, endingPos.x));
        distY = Mathf.Pow(-1, random.Next(0, 2)) * ((float) b / Main.instance.numItems * Mathf.Min(Mathf.Abs(Main.instance.screenSize) - endingPos.y, endingPos.y));
        startingPos = endingPos + new Vector3(distX, distY);

        dist = (endingPos - startingPos).magnitude;
        //values.Add(dist);

    }
    public void randomizeLoc(int a, int b, int c)
    {
        System.Random random = new System.Random(a + b + c);

        //print("Check 1 " + c);
        //print("Check 2 " + Main.instance.numBlocks);
        //print("Check 3 " + (float) c / Main.instance.numBlocks);
        print("c is " + c);
        print(Main.instance.screenSize);
        distX = Mathf.Pow(-1, random.Next(0, 2)) * ((float)a / Main.instance.numItems * Mathf.Min(Mathf.Abs(Main.instance.screenSize) - endingPos.x, endingPos.x));
        distY = Mathf.Pow(-1, random.Next(0, 2)) * ((float)b / Main.instance.numItems * Mathf.Min(Mathf.Abs(Main.instance.screenSize) - endingPos.y, endingPos.y));
        distZ = Mathf.Pow(-1, random.Next(0, 2)) * ((float)c / Main.instance.numItems)* Mathf.Abs(Main.instance.screenSize);
        print("DistZ is " + distZ);
        //*Mathf.Min(Mathf.Abs(Main.instance.screenSize) - endingPos.z, endingPos.z));
        startingPos = endingPos + new Vector3(distX, distY, distZ);

        dist = (endingPos - startingPos).magnitude;
        //values.Add(dist);

    }
    public static void randomizeAll ()
    {
        for (int i = 0; i < positions.Count; i++)
        {
            print("cechk 3 " + Main.instance.modeType);
            Ball temp = (Ball) items[i];
            if (Main.instance.modeType == "Ball")
            {
                temp.randomizeLoc(positions[i], positions[positions.Count - i - 1]);
                temp.startValue = positions[i];
                temp.setColor(positions[i]);
            }else if (Main.instance.modeType == "3D")
            {
                temp.randomizeLoc(positions[i], positions[positions.Count - i - 1], (int)((positions[i] + positions[positions.Count - i - 1]) / 2.0f));
                temp.startValue = positions[i];
                temp.setColor(positions[i]);
                print("check2");
            }
        }
    }
    public override void Update() {
        base.Update();
        calcLoc();
        //print(size);
        //print(startValue);
        //print(startingPos);
        if (positions.IndexOf(startValue) >= 0){
            transform.position = startingPos + ((positions.IndexOf(startValue) + 1) / (float)(startValue + 1)) * (endingPos - startingPos);
        }
        else
        {
            transform.position = endingPos;
        }
    }
}
