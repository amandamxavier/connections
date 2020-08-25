using System;
using System.Collections.Generic;
using UnityEngine;

public class UIRegion : MonoBehaviour {

    public static Dictionary<string, List<UIRegion>> regionTypes = new Dictionary<string, List<UIRegion>>();
    private void Awake() {
        if (regionTypes.ContainsKey(type)) {
            regionTypes[type].Add(this);
        }
        else {
            regionTypes.Add(type, new List<UIRegion>() { this });
        }
    }
    private void OnDestroy() {
        if (regionTypes.ContainsKey(type)) {
            regionTypes[type].Remove(this);
            if (regionTypes[type].Count == 0) {
                regionTypes.Remove(type);
            }
        }
    }
    public static bool ContainsMousePos(string type) {
        if (regionTypes.ContainsKey(type)) {
            return regionTypes[type].Exists(i => i.ContainsMousePos());
        }
        else {
            return false;
        }
    }

    public string type;

    public bool ContainsMousePos() {
        return RectTransformUtility.RectangleContainsScreenPoint((RectTransform)transform, Input.mousePosition);
    }

}
