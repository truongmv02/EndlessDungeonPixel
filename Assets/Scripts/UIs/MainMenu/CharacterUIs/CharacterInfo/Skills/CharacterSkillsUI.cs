using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSkillsUI : MonoBehaviour
{

    [SerializeField] SkillUI selectedSkill;
    [SerializeField] ListView skillList;
    [SerializeField] Transform skillListContent;
    [SerializeField] ListViewItem skillItemPrefab;

    [SerializeField] ScrollRect scrollView;
    [SerializeField] Scrollbar scrollbar;

    private void Awake()
    {
        /*skillList.OnChangeSelected += OnSelectedSkillChange;
        for (int i = 1; i < 6; i++)
        {
            ListViewItem skillItem = Instantiate(skillItemPrefab, skillListContent);
            SkillUI skillUI = skillItem.GetComponent<SkillUI>();
            skillUI.skillName.text = "Skill " + i;
            skillUI.subInfo.text = i + "nd skill";
            skillUI.description.text = "Skill description " + i;

            skillItem.onClick.AddListener(() =>
            {
                var rectTransform = skillItem.GetComponent<RectTransform>();
                var height = rectTransform.sizeDelta.y == 80f ? 230f : 80f;
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height);

                Vector2 targetContentPosition = (Vector2)rectTransform.localPosition;
            });
        }*/
    }

    void OnSelectedSkillChange(ListViewItem oldItem, ListViewItem newItem)
    {
        RectTransform rectTransform;
        if (oldItem != null)
        {
            rectTransform = oldItem.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 80);
        }
        SkillUI skillUI = newItem.GetComponent<SkillUI>();
        selectedSkill.skillName = skillUI.skillName;
        selectedSkill.skillImage.sprite = skillUI.skillImage.sprite;
    }

    private void OnDestroy()
    {
    }


}
