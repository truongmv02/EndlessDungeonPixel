using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;

public class TestDAta : MonoBehaviour
{

    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;
    void Start()
    {
        text1.text = Application.streamingAssetsPath;
        var filePath = Path.Combine(Application.dataPath + "!/assets/", "Bow.json");
        filePath = "jar:file://" + filePath;
        text2.text = filePath;

        if (File.Exists(Application.persistentDataPath + "/Test.txt"))
        {
            text3.text = File.ReadAllText(Application.persistentDataPath + "/Test.txt");
        }
        else
        {
            text3.text = Application.persistentDataPath;
            File.WriteAllText(Application.persistentDataPath + "/Test.txt", "123456");
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
