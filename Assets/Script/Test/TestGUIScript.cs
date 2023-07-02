using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<TKey> keys = new List<TKey>();

    [SerializeField]
    private List<TValue> values = new List<TValue>();

    // Unity callback before serialization
    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();

        foreach (KeyValuePair<TKey, TValue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    // Unity callback after deserialization
    public void OnAfterDeserialize()
    {
        Clear();

        if (keys.Count != values.Count)
        {
            throw new Exception($"Error deserializing dictionary. The number of keys ({keys.Count}) does not match the number of values ({values.Count}).");
        }

        for (int i = 0; i < keys.Count; i++)
        {
            if (!ContainsKey(keys[i]))
            {
                Add(keys[i], values[i]);
            }
        }
    }
}


[CustomPropertyDrawer(typeof(RequiredAttribute))]
public class RequiredDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        
        // Check if the property is null or empty
        if (string.IsNullOrEmpty(property.stringValue))
        {
            EditorGUI.HelpBox(position, "This field is required!", MessageType.Error);
            EditorGUI.PropertyField(position, property, label);
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }
}
public class RequiredAttribute : PropertyAttribute { }