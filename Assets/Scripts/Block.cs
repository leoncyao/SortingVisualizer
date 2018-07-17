using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
    public static List<Block> blocks = new List<Block>();
    public static List<int> positions;
    public int startValue;
    public float blockSize;
    public Color color;
    public virtual void Start()
    {
        startValue = blocks.Count;

        float c = 0.5f;
        blockSize = c * Main.instance.screenSize / Main.instance.numBlocks;
        transform.localScale = new Vector3(blockSize / 2f * startValue + 0.1f, c , c);

        setColor(startValue);

        blocks.Add(this);
    }
    // Sets Color
    public void setColor(int val)
    {
        Material material = new Material(Shader.Find("Standard"));
        float color = val / (float)Main.instance.numBlocks;
        material.color = new Color(color, color, color);
        GetComponent<Renderer>().material = material;
    }
    public virtual void Update()
    {
        transform.position = Main.instance.startLoc + 2 * blockSize * (Main.instance.numBlocks / 2.0f - positions.IndexOf(startValue)) * Vector2.up;
        //GetComponent<Renderer>().material.color = new Color(0, 0, Main.instance.screenSize / Main.instance.numBlocks * transform.position.x);
    }
}
