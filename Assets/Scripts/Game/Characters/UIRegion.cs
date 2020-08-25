using System;
using System.Collections.Generic;
using UnityEngine;

public class UIRegion : MonoBehaviour {

    public static List<UIRegion> instanceList = new List<UIRegion>();
    private void Awake() {
        instanceList.Add(this);
    }
    private void OnDestroy() {
        instanceList.Remove(this);
    }
    public static bool AnyContainsMousePos() {
        return instanceList.Exists(i => i.ContainsMousePos());
    }

    public bool ContainsMousePos() {
        return RectTransformUtility.RectangleContainsScreenPoint((RectTransform)transform, Input.mousePosition);
    }

}
