using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;



public class Main : MonoBehaviour {
    public int numItems;
    public GameObject itemPrefab;
    public Vector2 startLoc;
    public static Main instance;
    public int sortSpeed;
    public float screenSize;
    float centerX, centerY;
    Camera mainCamera;
    public bool isSorted;

    public string itemType, sortType, operationType;

    public DateTime t;
    public DateTime startTime;
    public TimeSpan sortTime;

    public int operationCounter;

    GUIStyle guiStyle;
    public void Start()
    {
        itemType = "Block";
        sortType = "MergeSort1";

        instance = this;
        numItems = 10;
        startTime = new DateTime();
        t = new DateTime();

        isSorted = true;

        //sortSpeed = 4;
        //int sortSpeedScale = 100;
        //sortSpeed =  Mathf.Max(1, sortSpeedScale * numItems / 60);
        sortSpeed = 1;
        screenSize = 6f;

        if (itemType == "Ball")
        {
            itemPrefab = (GameObject)Resources.Load("prefabs/Ball", typeof(GameObject));
        }
        else if (itemType == "Block")
        {
            itemPrefab = (GameObject)Resources.Load("prefabs/Block", typeof(GameObject));
        }

        centerX = screenSize / 2;
        centerY = screenSize / 2;
        startLoc = new Vector2(centerX, centerY);

        // Setting camera
        Vector3 cameraPos = new Vector3(centerX, centerX, -screenSize / 2);
        mainCamera = Camera.main;
        mainCamera.transform.position = cameraPos;
        mainCamera.orthographic = true;
        mainCamera.orthographicSize = screenSize / 2;
        mainCamera.backgroundColor = Color.blue;

        guiStyle = new GUIStyle();
        guiStyle.fontSize = 40;

        Item.positions = new List<int>();
        for (int i = 0; i < numItems; i++)
        {
            Item.positions.Add(i);
        }

        //listPrinter(Block.positions);
        for (int i = 0; i < numItems; i++)
        {
            Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
        }

        List<int> temp1 = new List<int>(makeRandomVals(0, 4));
        List<int> temp2 = new List<int>(makeRandomVals(0, 4));
        listPrinter(temp1);
        listPrinter(temp2);
        listPrinter<int>(SortingMethods.merge1(temp1, temp2));
        listPrinter(temp1);
        listPrinter(temp2);
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shuffle();
        }
        //If there are swaps to be done (positions isn't sorted)
        if (SortingMethods.swaps.Count > 0 || SortingMethods.insertions.Count > 0 && !isSorted)
        {               
            if (t.Ticks % (sortSpeed) == 0)
                {
                // This can be simplified by making temp list and setting it equal to swaps or insertions
                if (sortType == "BubbleSort" || sortType == "InsertionSort2" || sortType == "SelectionSort")
                {
                    SortingMethods.swap(Item.positions, (int)SortingMethods.swaps[0].x, (int)SortingMethods.swaps[0].y);
                    SortingMethods.swaps.RemoveAt(0);
                    operationType = "Swaps";
                } else if (sortType == "InsertionSort1")
                {
                    SortingMethods.insert(Item.positions, (int)SortingMethods.insertions[0].x, (int)SortingMethods.insertions[0].y);
                    SortingMethods.insertions.RemoveAt(0);
                    operationType = "Insertions";
                }
                else if (sortType == "MergeSort1")
                {
                    SortingMethods.swap(Item.positions, (int)SortingMethods.insertions[0].x, (int)SortingMethods.insertions[0].y);
                    SortingMethods.swaps.RemoveAt(0);
                    operationType = "swaps";
                }
                //listPrinter(Item.positions);
                operationCounter += 1;
            }

            sortTime = DateTime.Now - startTime;
        }
        checkFinished();
    }
    public void shuffle()
    {
        if (SortingMethods.swaps.Count == 0 && SortingMethods.insertions.Count == 0)
        {
            
            List<int> copy = new List<int>();
            if (itemType == "Ball")
            {
                Item.positions = new List<int>(makeRandomVals(0, numItems));
                Ball.randomizeAll();
                for (int i = 0; i < Item.positions.Count; i++)
                {
                    copy.Add(Item.positions[i]);
                }
                //bubbleSort(copy);
            }
            else if (itemType == "Block")
            {
                copy = new List<int>(makeRandomVals(0, numItems));
                Block.positions = new List<int>(copy);
            }
            print("starting array is ");
            listPrinter(copy);

            ////Get the method information using the method info class
            //MethodInfo mi = this.GetType().GetMethod(sortType);
            //mi.Invoke(this, new List<int>[] { copy });

            if (sortType == "BubbleSort")
            {
                SortingMethods.BubbleSort(copy);
            }
            else if (sortType == "InsertionSort1")
            {
                SortingMethods.InsertionSort1(copy);
            }
            else if (sortType == "InsertionSort2")
            {
                SortingMethods.InsertionSort2(copy);
            }
            else if (sortType == "SelectionSort")
            {
                SortingMethods.SelectionSort(copy);
            }
            else if (sortType == "MergeSort1")
            {
                SortingMethods.MergeSort1(copy);
            }

            print("After sort is ");
            listPrinter(copy);

            operationCounter = 0;
            startTime = DateTime.Now;
            isSorted = false;
        }
    }
    public void checkFinished()
    {
        if (SortingMethods.swaps.Count == 0 && SortingMethods.insertions.Count == 0 && !isSorted)
        {
            isSorted = true;
        }
    }
    public void OnGUI()
    {
        GUI.Label(new Rect(40, 20 , 100, 100), sortType + " time (in seconds)" , guiStyle);
        GUI.Label(new Rect(40, 20 + guiStyle.fontSize + 1, 100, 100), sortTime.Seconds.ToString(),  guiStyle);
        GUI.Label(new Rect(40, 20 + 2 * guiStyle.fontSize + 1, 100, 100), "number of " + operationType, guiStyle);
        GUI.Label(new Rect(40, 20 + 3 * guiStyle.fontSize + 1, 100, 100), operationCounter.ToString(), guiStyle);
    }
    public void listPrinter<T>(List<T> A)
    {
        string word = "";
        foreach (T temp in A)
        {
            word += temp + " ";
        }
        print(word);
    }
    // Creates a list of distinct random integers in the range [a, b)
    public List<int> makeRandomVals(int a, int b)
    {
        // First Generate a list a...b
        int[] values = new int[b - a];
        for (int i = a; i < b - a; i++)
        {
            values[i] = i;
        }
        List<int> randomValues = new List<int>(values);

        // Next, swap the values randomly 
        System.Random random = new System.Random();
        for (int i = a; i < b - a; i++)
        {
            if (random.Next(0, 2) == 1)
            {
                int choice = random.Next(a, b);
                SortingMethods.swap(randomValues, i, choice);
            }
        }
        return randomValues;
    }
    



}
