using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortTool<T>
{
    private struct SortItem<T>
    {
        public SortItem(int id, T obj)
        {
            this.sortId = id;
            this.obj = obj;
        }
        public readonly int sortId;
        public readonly T obj;
    }

    private List<SortItem<T>> _itemList = new List<SortItem<T>>();

    public void AddItem(int sortId, T obj)
    {
        SortItem<T> sortItem = new SortItem<T>(sortId, obj);
        _itemList.Add(sortItem);
    }

    public T[] Sort(bool isDesc = false)
    {
        SortItem<T>[] arr = _itemList.ToArray();
        SortItem<T> temp;
        for (int i = 0; i < arr.Length - 1; i++)
        {
            for (int j = 0; j < arr.Length - 1 - i; j++)
            {
                if (arr[j].sortId > arr[j + 1].sortId)
                {
                    temp = arr[j + 1];
                    arr[j + 1] = arr[j];
                    arr[j] = temp;
                }
            }
        }

        T[] result = new T[arr.Length];
        for (int i = 0; i < result.Length; i++)
        {
            if (isDesc)
            {
                result[result.Length - i - 1] = arr[i].obj;
            }
            else
            {
                result[i] = arr[i].obj;
            }
        }
        return result;
    }

    private int Compare(SortItem<T> a, SortItem<T> b)
    {
        if (a.sortId > b.sortId)
        {
            return 1;
        }
        return -1;
    }
}
