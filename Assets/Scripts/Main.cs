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

    public string modeType, sortType, operationType;

    public DateTime initialTime;
    public DateTime startTime;
    public TimeSpan sortTime;

    public int operationCounter;

    GUIStyle guiStyle;

    public static String[] sorts = new string[] { "BubbleSort", "InsertionSort1", "InsertionSort2", "MergeSort1" };
    public static String[] modes = new string[] { "Ball", "3D", "Block" };

    public List<GameObject> currentItems;

    public void Start()
    {

        instance = this;
        guiStyle = new GUIStyle();
        guiStyle.fontSize = 40;
        screenSize = 6f;

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
        
        init(sorts[1], modes[0], 50);
    }
    public void Update()
    {
        //If there are operations to be done (positions isn't sorted)
        if (SortingMethods.swaps.Count > 0 || SortingMethods.insertions.Count > 0 && !isSorted)
        {
            if ((DateTime.Now-initialTime).Ticks % (sortSpeed) == 0)
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
                operationCounter += 1;
            }
            sortTime = DateTime.Now - startTime;
        }
        checkFinished();
    }
    public void init(string sortType, string modeType, int numItems)
    {
        if (isSorted)
        {
            this.sortType = sortType;
            this.modeType = modeType;
            this.numItems = numItems;
            print("ModeType is " + modeType);

            if (modeType == "Ball" || modeType == "3D")
            {
                itemPrefab = (GameObject)Resources.Load("prefabs/Ball", typeof(GameObject));
            }
            else if (modeType == "Block")
            {
                itemPrefab = (GameObject)Resources.Load("prefabs/Block", typeof(GameObject));
            }
            
            // if modeType is not 3D, make camera orthographic
            mainCamera.orthographic = !(modeType == "3D");
          

            startTime = new DateTime();
            initialTime = DateTime.Now;

            isSorted = true;
            sortSpeed = UI.sizeMax;

            //// Testing Sorts
            //TestSort(sortType);

            foreach (GameObject temp in currentItems)
            {
                Destroy(temp);
            }
            Item.items = new List<Item>();
            currentItems = new List<GameObject>();
            for (int i = 0; i < numItems; i++)
            {
                GameObject temp = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                temp.GetComponent<Ball>().calcLoc();
                currentItems.Add(temp);
            }
        }
    }
    public void shuffle()
    {
        if (SortingMethods.swaps.Count == 0 && SortingMethods.insertions.Count == 0)
        { 
            List<int> copy = new List<int>();
            if (modeType == "Ball" || modeType == "3D")
            {
                Item.positions = new List<int>(makeRandomVals(0, numItems));
                Ball.randomizeAll();
                for (int i = 0; i < Item.positions.Count; i++)
                {
                    copy.Add(Item.positions[i]);
                }
            }
            else if (modeType == "Block")
            {
                copy = new List<int>(makeRandomVals(0, numItems));
                Block.positions = new List<int>(copy);
            }

            bool toPrint = false;

            if (toPrint)
            {
                print("starting array is ");
                listPrinter(copy);
            }

            //Get the method information using the method info class
            //MethodInfo mi = this.GetType().GetMethod(sortType);
            //mi.Invoke(this, new List<int>[] { copy });

            // instead i did it manually because i had issues with the built in method
            chooseSort(copy, sortType);

            if (toPrint)
            {
                print("After sort is ");
                listPrinter(copy);
            }

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
    // Displays stats
    public void OnGUI()
    {
        GUI.Label(new Rect(40, 20 , 100, 100), sortType + " time (in seconds)" , guiStyle);
        GUI.Label(new Rect(40, 20 + guiStyle.fontSize + 1, 100, 100), ((float)sortTime.TotalMilliseconds/1000.0f).ToString(),  guiStyle);
        GUI.Label(new Rect(40, 20 + 2 * guiStyle.fontSize + 1, 100, 100), "number of " + operationType, guiStyle);
        GUI.Label(new Rect(40, 20 + 3 * guiStyle.fontSize + 1, 100, 100), operationCounter.ToString(), guiStyle);
    }
    public static void listPrinter<T>(List<T> A)
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
    public List<T> chooseSort<T>(List<T> A, string sortName) where T : System.IComparable<T>
    {
        List<T> B = new List<T>();
        if (sortName == "BubbleSort")
        {
            SortingMethods.BubbleSort(A);
        }
        else if (sortName == "InsertionSort1")
        {
            SortingMethods.InsertionSort1(A);
        }
        else if (sortName == "InsertionSort2")
        {
            SortingMethods.InsertionSort2(A);
        }
        else if (sortName == "SelectionSort")
        {
            SortingMethods.SelectionSort(A);
        }
        else if (sortName == "MergeSort1")
        {
            B = SortingMethods.MergeSort1(A);
        }
        return B;
    }
    public void TestSort(string sortName)
    {
        List<int> temp = makeRandomVals(0, 100);
        listPrinter(temp);
        // if in place
        chooseSort(temp, sortName);
        listPrinter(temp);

        // if not
        listPrinter(chooseSort(temp, sortName));
    }
    public void miscellaneous()
    {
        //List<int> temp1 = new List<int>(new int[] { 4, 5, 6 });
        //SortingMethods.insert(temp1, 2, 1);
        //listPrinter(temp1);
        //print("first half slice is ");
        //listPrinter(SortingMethods.Slice(temp, 0, 5));
        //print("second half slice is ");
        //listPrinter(SortingMethods.Slice(temp, 5, 10));
        //print("merging 1, 2, 3 and 4 5 6");
        //listPrinter(SortingMethods.merge1(new List<int>(new int[] { 1, 2, 3 }), new List<int>(new int[] { 4, 5, 6 })));
        //print("merging 4 5 6 and 1, 2, 3");
        //listPrinter(SortingMethods.merge1(new List<int>(new int[] { 4, 5, 6 }), new List<int>(new int[] { 1, 2, 3 })));
        //print("merging 4 2 6 and 1, 5, 3");
        //listPrinter(SortingMethods.merge1(new List<int>(new int[] { 4, 5, 6 }), new List<int>(new int[] { 1, 2, 3 })));
        //print("merging 2 4 6 and 1, 5");
        //List<int> temp = new List<int>(new int[] { 2, 4, 6, 1, 5 });
        //SortingMethods.merge2(temp,0 , 3 ,3 , 5);
        //// Testing Partition
        //List<int> test = makeRandomVals(0, 10);
        //listPrinter(test);
        //print("check partition");
        //SortingMethods.partition(test, 0, 5, 10);
        //listPrinter(test);
    }


}
