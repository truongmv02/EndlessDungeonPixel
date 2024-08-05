using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        string characterName = DynamicData.Instance.Data.characterSelect;
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("AnimatorController/UI/Character/" + characterName);
    }

}
