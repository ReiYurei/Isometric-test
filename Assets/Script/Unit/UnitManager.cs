using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static SerializableDictionary<string, GameObject> UnitInScene = new SerializableDictionary<string, GameObject>();
}
