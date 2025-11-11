using System;
using System.Collections.Generic;
using UnityEngine;

namespace Study
{
    /// <summary>
    /// use two serializable list to represent dictionary
    /// </summary>
    /// <typeparam name="kType">key type in MyDictionary</typeparam>
    /// <typeparam name="vType">vale type in MyDictionary</typeparam>
    [Serializable]
    public class MyDictionary<kType,vType> : Dictionary<kType,vType>,ISerializationCallbackReceiver
    {
        [SerializeField] private List<kType> keys = new List<kType>();
        [SerializeField] private List<vType> values = new List<vType>();

        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();
            foreach (var pair in this)
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            this.Clear();
            if (keys.Count != values.Count)
            {
                Debug.Log("The count of key and value in dictionary is dis-matching.");
            }

            for (int i = 0; i < keys.Count; i++)
            {
                if (this.ContainsKey(keys[i]))
                {
                    Debug.LogWarning("Do not allow to add the same key in a dictionary");
                }
                else
                {
                    Add(keys[i],values[i]);
                }
            }
        }
    }
}