using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    public Slider speedSlider, sizeSlider, numItemSlider;
    public Dropdown sortMenu, modeMenu;
    public Button Shuffle, Reset;
    public static int speedMax, sizeMax;
	void Start () {
        speedMax = 3;
        speedSlider.maxValue = (int)Mathf.Pow(10, speedMax) - 1;

        sizeMax = 5;
        sizeSlider.maxValue = sizeMax;
        sizeSlider.minValue = 0.1f;
        sortMenu.options = new List<UnityEngine.UI.Dropdown.OptionData>(new UnityEngine.UI.Dropdown.OptionData[Main.sorts.Length]);
       
        for (int i = 0; i < Main.sorts.Length; i++)
        {
            sortMenu.options[i] = new UnityEngine.UI.Dropdown.OptionData(Main.sorts[i]);
        }
        sortMenu.GetComponentInChildren<Text>().text = Main.sorts[0];


        // Hard coded
        modeMenu.options = new List<UnityEngine.UI.Dropdown.OptionData>(new UnityEngine.UI.Dropdown.OptionData[2]);
        modeMenu.options[0] = new UnityEngine.UI.Dropdown.OptionData("Ball");
        modeMenu.options[1] = new UnityEngine.UI.Dropdown.OptionData("Block");
        // Add 3D option later

        numItemSlider.minValue = 1;
        numItemSlider.maxValue = 100;


    }
    public void updateSpeed()
    {
        // Later make logarithmic scale or equal intervals of slider
        // instead of linear scale (mod is weird outside of base 10)
        int c = (int)(Mathf.Pow(10, speedMax) - speedSlider.value);
        Main.instance.sortSpeed = Mathf.CeilToInt(c);
    }
    public void updateItemSize()
    {
        Item.c = sizeSlider.value;
    }
    public void reset()
    {
        Main.instance.init(sortMenu.options[sortMenu.value].text, modeMenu.options[modeMenu.value].text, (int) numItemSlider.value);
        print((int)numItemSlider.value);
    }
    public void shuffle()
    {
        Main.instance.shuffle();
    }
}
