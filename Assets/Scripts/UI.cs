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
        modeMenu.options = new List<UnityEngine.UI.Dropdown.OptionData>(new UnityEngine.UI.Dropdown.OptionData[Main.modes.Length]);
        //print(modeMenu);
        //print(Main.modes);
        //print(new List<string>(Main.modes));
        for (int i = 0; i < Main.modes.Length; i++)
        {
            modeMenu.options[i] = new UnityEngine.UI.Dropdown.OptionData(Main.modes[i]);
        }
        print("check");
        //Main.instance.listPrinter(new List<string>(Main.modes));
        Main.listPrinter(new List<string>(Main.modes));
        modeMenu.GetComponentInChildren<Text>().text = Main.modes[0];
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
        string sort = sortMenu.options[sortMenu.value].text;
        string mode = modeMenu.options[modeMenu.value].text;
        int numItems = (int)numItemSlider.value;
        print(sort);
        print(mode);
        print(numItems);
        Main.instance.init(sort, mode, numItems);
    }
    public void shuffle()
    {
        Main.instance.shuffle();
    }
}
