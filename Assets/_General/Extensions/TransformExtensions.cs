using UnityEngine;

public static class TransformExtensions
{
    public static int DestroyChildren(this Transform trans)
    {
        int _numChildren = trans.childCount;
        for (int i = 0; i < _numChildren; i++)
            Object.Destroy(trans.GetChild(i).gameObject);

        return _numChildren;
    }
}