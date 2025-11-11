using System;
using System.Collections.Generic;
using UnityEngine;

namespace Study
{
    public class Player : MonoBehaviour
    {
        public int attack;
        public float defence;
        public GameObject enemy;
        public int[] items ={3,23,4,5};
        public List<string> weapons = new() { "sword", "shield", "rax" };
        public TestDataStructure info = new TestDataStructure(){detail = "heal the character",id = 29,price = 23} ;
        public MyDictionary<string, int> buff = new MyDictionary<string, int>() { { "Anger", 10 }, { "dash", 4 } };


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                foreach (var pair in buff)
                {
                    print($"key:{pair.Key} value:{pair.Value}");
                }
            }
        }
    }
}
