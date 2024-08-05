using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : SingletonMonoBehaviour<InputHandler>
{
    [SerializeField] InputActionAsset inputControl;

    #region INPUT ACTION

    InputAction movementAction;
    InputAction action;
    InputAction switchWeaponAction;
    InputAction skillAction;

    #endregion

    #region INPUT VARIABLE
    public Vector2 MoveInput { get; private set; }
    public Vector2 LastMoveInput { get; private set; }
    public bool ActionInput { get; set; }
    public bool SwitchWeaponInput { get; set; }
    public bool SkillInput { get; private set; }

    #endregion

    #region KEY

    string actionMapName = "Player";
    const string MovementKey = "Movement";
    const string ActionKey = "Action";
    const string SwitchWeaponKey = "SwitchWeapon";
    const string SkillKey = "Skill";

    #endregion

    protected override void Awake()
    {
        CreateInstance(false);
        var actionMap = inputControl.FindActionMap(actionMapName);

        movementAction = actionMap.FindAction(MovementKey);
        action = actionMap.FindAction(ActionKey);
        switchWeaponAction = actionMap.FindAction(SwitchWeaponKey);
        skillAction = actionMap.FindAction(SkillKey);
        RegisterInputActions();
    }




    void RegisterInputActions()
    {
        movementAction.performed += context =>
        {
            MoveInput = context.ReadValue<Vector2>();
            LastMoveInput = MoveInput;
        };

        movementAction.canceled += context => MoveInput = Vector2.zero;

        action.performed += context => ActionInput = true;
        action.canceled += context => ActionInput = false;

        switchWeaponAction.performed += context => SwitchWeaponInput = true;
        switchWeaponAction.canceled += context => SwitchWeaponInput = false;

        skillAction.performed += context => SkillInput = true;
        skillAction.canceled += context => SkillInput = false;

    }

    private void OnEnable()
    {
        movementAction.Enable();
        action.Enable();
        switchWeaponAction.Enable();
        skillAction.Enable();
    }

    private void OnDisable()
    {
        if (movementAction == null) return;
        movementAction.Disable();
        action.Disable();
        switchWeaponAction.Disable();
        skillAction.Disable();
    }

    private void OnDestroy()
    {
    }
}