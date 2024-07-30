using UnityEngine;
using System;

public static class TransformExtensions
{
    public static int DestroyChildren(this Transform trans)
    {
        int _numChildren = trans.childCount;
        for (int i = 0; i < _numChildren; i++)
            UnityEngine.Object.Destroy(trans.GetChild(i).gameObject);

        return _numChildren;
    }

    public static int DestroyChildren(this Transform trans, Predicate<Transform> _predicate)
    {
        int _numChildren = trans.childCount;
        for (int i = 0; i < _numChildren; i++)
        {
            Transform _child = trans.GetChild(i);
            if (_predicate.Invoke(_child))
                UnityEngine.Object.Destroy(_child.gameObject);
        }

        return _numChildren;
    }
}