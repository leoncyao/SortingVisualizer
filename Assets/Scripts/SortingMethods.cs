using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SortingMethods: MonoBehaviour {
    public static List<Vector2> swaps = new List<Vector2>(), insertions = new List<Vector2>();
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
            while (j > 0 && A[i].CompareTo(A[j - 1]) < 0){ 
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
            List<T> A1 = MergeSort1<T>(Slice(A, 0, (int)(A.Count() / 2.0f)));
            List<T> A2 = MergeSort1<T>(Slice(A, (int)(A.Count() / 2.0f), A.Count()));
            Main.instance.listPrinter(A1);
            Main.instance.listPrinter(A2);
            return merge1(A1.ToList(), A2.ToList());
        }
    }
    // in place
    public static void MergeSort2<T>(List<T> A) where T : System.IComparable<T>
    {
        if (A.Count() > 1)
        {
            IEnumerable<T> A1 = MergeSort1(Slice<T>(A, 0, (int)(A.Count() / 2.0f)));
            IEnumerable<T> A2 = MergeSort1(Slice<T>(A, (int)(A.Count() / 2.0f), A.Count()));
            //A = merge2(A, 0, (int)(A.Count() / 2.0f), (int)(A.Count() / 2.0f), A.Count());
        }
    }
    public static void QuickSort<T>(List<T> A) where T : System.IComparable<T>
    {

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
        if (a < b)
        {
            A.RemoveAt(a);
        }
        else
        {
            A.RemoveAt(a + 1);
        }
    }
    // Recursive merging
    public static List<T> merge1<T>(List<T> A, List<T> B) where T : System.IComparable<T>
    {
        List<T> C = new List<T>();
        for (int n = 0; n < A.Count + B.Count; n++)
        {
            T temp = default(T);
            C.Add(temp);
        }
        int i = 0, j = 0, k = 0;
        while (i < A.Count && j < B.Count)
        {
            if (A[i].CompareTo(B[j]) < 0)
            {
                C[k] = A[i];
                i++;
                k++;
            }
            else if (A[i].CompareTo(B[j]) >= 0)
            {
                C[k] = B[j];
                j++;
                k++;
            }
        }
        if (i > j)
        {
            for (int m = k; m < A.Count; m++)
            {
                C[m] = A[i];
                i++;
            }
        }
        else
        {
            for (int m = k; m < B.Count; m++)
            {
                C[m] = B[j];
                j++;
            }
        }
        return C;
    }
    // In place merging
    public static IEnumerable<T> merge2<T>(List<T> A, int a, int b, int c, int d) where T : System.IComparable<T>
    {
        List<T> C = new List<T>();
        int i = 0, j = 0;
        while (i < b && j < d - b)
        {
            if (A[i].CompareTo(A[j]) < 0){
                // nothing to be done
                i++;
            }
            else
            {
            
            }
        }
        return C;
    }
    // Taken from stackoverflow ~Cristian Garcia
    public static List<T> Slice<T>(List<T> A, int from, int to)
    {
        IEnumerable<T> e = A.AsEnumerable<T>();
        IEnumerable<T> toReturn =  e.Take(to).Skip(from);
        return toReturn.ToList();
    }
}
