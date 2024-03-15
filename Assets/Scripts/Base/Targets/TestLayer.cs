using System.Collections;
using UnityEngine;

public class TestLayer : MonoBehaviour
{
    public LayerMask layer;
    private LayerMask layer1;
    void Start()
    {
        layer1 = LayerMask.GetMask(new[] { "Player", "Enemy" });

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log(JsonUtility.ToJson(layer));
            Debug.Log(layer1.value);
        }
    }
}