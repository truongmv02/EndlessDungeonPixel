using System.Collections;
using TMPro;
using UnityEngine;

public class TextPopup : MonoBehaviour
{
    private TextMeshPro textMesh;
    float moveSpeed = 3f;
    private float disappearTimer = 1.5f;
    private float timer;

    private Color textColor;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    public void Setup(string text)
    {
        textMesh.SetText(text);
        textColor = textMesh.color;
        textColor.a = 1;
        textMesh.color = textColor;
        timer = disappearTimer;
    }

    private void Update()
    {
        transform.position += new Vector3(0, moveSpeed) * Time.deltaTime;
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            textColor.a -= 3 * Time.deltaTime;
            textMesh.color = textColor;

            if (textColor.a < 0)
            {
                ObjectPool.Instance.DestroyObject(gameObject);
            }
        }
    }
}
