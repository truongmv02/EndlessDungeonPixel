using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestLayer : MonoBehaviour
{
    public LayerMask layer;
    private LayerMask layer1;

    void Start()
    {
        List<GameObject> list = new List<GameObject>();
        list = Spawn().ToList();
        Debug.Log(list.Count);
    }

    IEnumerable<GameObject> Spawn()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject gameObject = new GameObject();
            gameObject.name = i.ToString();
            yield return gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}