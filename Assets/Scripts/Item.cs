using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public static List<Item> items = new List<Item>();
    public static List<int> positions = new List<int>();
    public int startValue;
    public Color color;
    public float size;
    public static float c;
    public virtual void Start()
    {
        startValue = items.Count;
        c = 1;
        setColor(startValue);
        items.Add(this);
    }
    // Sets Color
    public void setColor(int val)
    {
        Material material = new Material(Shader.Find("Standard"));
        float color = val / (float)Main.instance.numItems;
        material.color = new Color(color, color, color);
        GetComponent<Renderer>().material = material;
    }
    public virtual void Update()
    {
        //print("c is " + c);
        size = c * Main.instance.screenSize / Main.instance.numItems;
        //print("size is " + size);
        transform.localScale = new Vector3(size, size, size);
        //print(size);
        //GetComponent<Renderer>().material.color = new Color(0, 0, Main.instance.screenSize / Main.instance.numBlocks * transform.position.x);
    }
}
