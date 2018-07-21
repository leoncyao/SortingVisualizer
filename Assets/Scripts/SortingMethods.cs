using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SortingMethods : MonoBehaviour {
    public static List<Vector2> swaps = new List<Vector2>(), insertions = new List<Vector2>();
    public static int numRuns = 0;
    public static void BubbleSort<T>(List<T> A) where T : System.IComparable<T>
    {
        // Does the Outside loop have to go all the way to A.Count - 1?
        for (int i = 0; i < A.Count - 1; i++)
        {
            // Does the inside loop have to go all the way to A.Count - 1?
            for (int j = 0; j < A.Count - 1; j++)
            {
                if (A[j].CompareTo(A[j + 1]) > 0)
                {
                    swap(A, j, j + 1);
                    swaps.Add(new Vector2(j, j + 1));
                }
            }
        }
    }
    // With insertions
    public static void InsertionSort1<T>(List<T> A) where T : System.IComparable<T>
    {
        for (int i = 0; i < A.Count; i++) {
            int j = i;
            while (j > 0 && A[i].CompareTo(A[j - 1]) < 0) {
                j -= 1;
            }
            insert(A, i, j);
            insertions.Add(new Vector2(i, j));
        }
    }
    // With Swaps
    public static void InsertionSort2<T>(List<T> A) where T : System.IComparable<T>
    {
        for (int i = 1; i < A.Count; i++)
        {
            int j = i;
            while (j > 0 && A[j].CompareTo(A[j - 1]) < 0)
            {
                swap(A, j, j - 1);
                swaps.Add(new Vector2(j, j - 1));
                j -= 1;
            }
        }
    }
    public static void SelectionSort<T>(List<T> A) where T : System.IComparable<T>
    {
        // Copied From Wikipedia
        /* a[0] to a[n-1] is the array to sort */
        int i, j;
        int n = A.Count;
        /* advance the position through the entire array */
        /*   (could do j < n-1 because single element is also min element) */
        for (j = 0; j < n - 1; j++)
        {
            /* find the min element in the unsorted a[j .. n-1] */

            /* assume the min is the first element */
            int iMin = j;
            /* test against elements after j to find the smallest */
            for (i = j + 1; i < n; i++)
            {
                /* if this element is less, then it is the new minimum */
                if (A[i].CompareTo(A[iMin]) < 0)
                {
                    /* found new minimum; remember its index */
                    iMin = i;
                }
            }
            if (iMin != j)
            {
                swap(A, j, iMin);
                swaps.Add(new Vector2(j, iMin));
            }
        }
    }
    // normal recursive
    public static List<T> MergeSort1<T>(List<T> A) where T : System.IComparable<T>
    {
        if (A.Count() == 1)
        {
            return A;
        }
        else
        {
            List<T> A1 = MergeSort1(Slice(A, 0, (int)(A.Count() / 2.0f)));
            List<T> A2 = MergeSort1(Slice(A, (int)(A.Count() / 2.0f), A.Count()));
            //Main.instance.listPrinter(A1);
            //Main.instance.listPrinter(A2);
            return merge1(A1.ToList(), A2.ToList());
        }
        //    if (len(A) == 1):
        //        return A
        //    else:
        //        A1 = mergeSort(A[0:floor(len(A) / 2)])
        //        A2 = mergeSort(A[floor(len(A) / 2):len(A)])
        //        return merge(A1, A2)
    }
    // Recursive merging
    public static List<T> merge1<T>(List<T> A, List<T> B) where T : System.IComparable<T>
    {
        List<T> C = new List<T>();
        int i = 0, j = 0;
        while (i < A.Count && j < B.Count)
        {
            if (A[i].CompareTo(B[j]) < 0)
            {
                C.Add(A[i]);
                i++;
            }
            else if (A[i].CompareTo(B[j]) >= 0)
            {
                C.Add(B[j]);
                j++;
            }
        }
        if (i > j)
        {
            for (; j < B.Count; j++)
            {
                C.Add(B[j]);
            }
        }
        else
        {
            for (; i < A.Count; i++)
            {
                C.Add(A[i]);
            }
        }
        return C;
    }
    // in place
    public static void MergeSort2<T>(List<T> A, int start, int end) where T : System.IComparable<T>
    {
        numRuns += 1;
        if (numRuns > 100)
        {
            return;
        }
        print("end is " + end + " start is " + start);
        if (end - start <= 1)
        {
            print("There should be one element");
            Main.listPrinter(Slice(A, start, end));
            //print("base Case");
        }
        else
        {
            int mid = (int)Mathf.CeilToInt((end - start) / 2.0f);
            MergeSort2(A, start, mid);
            MergeSort2(A, mid, end);

            //Main.instance.listPrinter(A1);
            //Main.instance.listPrinter(A2);
            merge2(A, start, mid, end);
        }
    }
    // In place merging
    public static void merge2<T>(List<T> A, int start, int mid, int end) where T : System.IComparable<T>
    {
        //print("before merge");
        //Main.instance.listPrinter(Slice(A, a, b));
        //Main.instance.listPrinter(Slice(A, c, d));
        int i = start, j = mid;
        while (i <= mid && j < end)
        {
            //print("i and j " + i + " " + j);
            //print("A[i] and A[j] are " + A[i] + " " + A[j]);
            if (A[i].CompareTo(A[j]) < 0)
            {
                i++;
            }
            else if (A[i].CompareTo(A[j]) >= 0)
            {
                insert(A, j, i);
                j++;
            }
        }
        //print("After merge");
        //Main.instance.listPrinter(Slice(A, a, d));
        if (i > j)
        {
            for (; j < A.Count; j++)
            {
                insert(A, j, i);
            }
        }
        else
        {
            //for (; i < A.Count; i++)
            //{
            //    C.Add(A[i]);
            //}
        }
    }
    public static List<T> QuickSort<T>(List<T> A) where T : System.IComparable<T>
    {
        if (A.Count <= 1)
        {
            return A;
        }
        else
        {
            T pivot = A[0];
            A.RemoveAt(0);

            List<T> B = new List<T>();
            List<T> C = new List<T>();
            foreach (T item in A)
            {
                if (item.CompareTo(pivot) < 0)
                {
                    B.Add(item);
                }
                else
                {
                    C.Add(item);
                }
            }
            B = QuickSort(B);
            B.Add(pivot);
            B.AddRange(QuickSort(C));
            return B;
        }
    }
    public static void partition<T>(List<T> A, int start, int mid, int end) where T : System.IComparable
    {
        int i = start;
        int j = end;
        int k = mid;
        T pivot = A[mid];
        print("pivot is " + pivot);
        while (i < k)
        {
            print("A[i] is" + A[i]);
            if (A[i].CompareTo(pivot) > 0) {
                T temp = A[i];
                A.RemoveAt(i);
                A.Insert(k, temp);
                k -= 1;
            }
            else
            {
                i++;
            }
            print("check1");
            Main.listPrinter(A);
        }
        while (j > k+i)
        { 
            if (A[i].CompareTo(pivot) < 0)
            {
                T temp = A[j];
                A.RemoveAt(j);
                A.Insert(k, temp);
                k += 1;
            }
            else
            {
                j--;

            }
            print("check2");
            Main.listPrinter(A);
        }
    }
    // swaps items in A at indexes a and b
    public static void swap<T>(List<T> A, int a, int b)
    {
        //print("A.Count " + A.Count + " a " + a + " b " + b);
        //Main.instance.listPrinter(A);
        T temp = A[a];
        A[a] = A[b];
        A[b] = temp;
    }
    // inserts item at index a into slot at b and removes the item 
    // that was at index a as we don't want duplicates
    public static void insert<T>(List<T> A, int a, int b)
    {
        //print("check " + A.Count + " " + a + " " + b);
        A.Insert(b, A[a]);
        //print(b);
        Main.listPrinter(A);
        if (a < b)
        {
            A.RemoveAt(a);
        }
        else
        {
            A.RemoveAt(a + 1);
        }
    }
    // Taken from stackoverflow ~Cristian Garcia
    public static List<T> Slice<T>(List<T> A, int from, int to)
    {
        IEnumerable<T> e = A.AsEnumerable<T>();
        IEnumerable<T> toReturn =  e.Take(to).Skip(from);
        return toReturn.ToList();
    }
}
