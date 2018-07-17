using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
    public int numBlocks;
    public GameObject itemPrefab;
    public Vector2 startLoc;
    public static Main instance;
    public List<Vector2> swaps;
    public int t;
    public int sortSpeed;

    public float screenSize;
    float centerX, centerY;
    Camera mainCamera;

    public void Start()
    {
        instance = this;
        numBlocks = 9;
        t = 0;
        //sortSpeed = 4;
        int sortSpeedScale = 1;
        sortSpeed = sortSpeedScale * 60 / numBlocks;

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

        swaps = new List<Vector2>();
        Block.positions = new List<int>();
        for (int i = 0; i < numBlocks; i++)
        {
            Block.positions.Add(i);
        }
        //Block.positions = new List<int>( new int[] {5, 4, 2, 1, 9, 0, 3, 6, 8 ,7});
        Block.positions = new List<int>(makeRandomVals(0, numBlocks));
        //listPrinter(Block.positions);
        for (int i = 0; i < numBlocks; i++)
        {
            Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
        }
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (swaps.Count == 0)
            {
                bubbleSort(Ball.values);
                List<int> copy = new List<int>();
                foreach (Ball ball in Ball.balls)
                {
                    copy.Add(Ball.values.IndexOf(ball.dist));
                }
                Block.positions = new List<int>(copy);
                bubbleSort(copy);         
            }
        }
        //If there are swaps to be done (positions isn't sorted)
        if (swaps.Count > 0)
        {
            if (t % (100/sortSpeed) == 0)
            {
                swap(Block.positions, (int)swaps[0].x, (int)swaps[0].y);
                swaps.RemoveAt(0);
                listPrinter(Block.positions);
            }
            t += 1;
        }
    }
    public void bubbleSort<T>(List<T> A) where T : System.IComparable<T>
    {
        for (int i = 0; i < A.Count-1; i++)
        {
            for (int j = 0; j < A.Count-1; j++)
            {
                if (A[j].CompareTo(A[j + 1]) > 0)
                {
                    swap(A, j, j + 1);
                    swaps.Add(new Vector2(j, j + 1));
                }
            }
        }
    }
    public void swap<T>(List<T> A, int a, int b) 
    {
        T temp = A[a];
        A[a] = A[b];
        A[b] = temp;
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
        int[] values = new int[numBlocks];
        for (int i = a; i < numBlocks; i++)
        {
            values[i] = i;
        }
        List<int> randomValues = new List<int>(values);

        // Next, swap the values randomly 
        System.Random random = new System.Random();
        for (int i = a; i < numBlocks; i++)
        {
            if (random.Next(0, 2) == 1)
            {
                int choice = random.Next(a, b);
                swap(randomValues, i, choice);
            }
        }
        return randomValues;
    }
}
