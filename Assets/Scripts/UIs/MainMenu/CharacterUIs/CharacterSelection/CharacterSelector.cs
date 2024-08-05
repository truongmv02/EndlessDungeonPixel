using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    public Image CharacterImage { protected set; get; }
    public CharacterInfo Info { set; get; }
    protected void Awake()
    {
        CharacterImage = transform.Find("CharacterSprite").GetComponent<Image>();
    }

    public void SetCharacterSprite(Sprite sprite)
    {
        CharacterImage.sprite = sprite;
        CharacterImage.SetNativeSize();
    }

    public void UnSelect()
    {
        var color = CharacterImage.color;
        CharacterImage.color = new Color(color.r, color.g, color.b, 0.6f);
        CharacterImage.transform.localScale = Vector3.one;
    }

    public void Select()
    {
        var color = CharacterImage.color;
        CharacterImage.color = new Color(color.r, color.g, color.b, 1f);
        CharacterImage.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

}
