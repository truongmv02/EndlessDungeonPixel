using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController character;
    public InputHandler InputHandler { set; get; }
    MovementJoystick movementJoystick;

    private void Start()
    {
        character = GetComponent<CharacterController>();
        InputHandler = InputHandler.Instance;
    }

    private void Update()
    {
        if (InputHandler == null) return;

        if (InputHandler.SwitchWeaponInput)
        {
            character.Inventory.SwitchWeapon();
            InputHandler.SwitchWeaponInput = false;
        }

        var target = character.TargetDetector.GetTarget();
        Vector2 playerDirection = InputHandler.LastMoveInput;
        Vector2 weapondirection = new(playerDirection.x < 0 ? -1 : 1, 0);
        var weapon = character.Inventory.Weapon;

        if (target != null)
        {
            if (target.TryGetComponent<ItemPickupController>(out var item) && item.Type != ItemPickupType.AutoPickup)
            {
                if (InputHandler.ActionInput)
                {
                    item.SetTargetCollect(character);
                    InputHandler.ActionInput = false;
                }
            }
            else if (target.TryGetComponent<EnemyController>(out var enemy))
            {
                if (enemy != null)
                {
                    weapondirection = enemy.transform.position - weapon.Root.position;
                    playerDirection = enemy.transform.position - transform.position;
                }
            }
        }

        character.Movement.CheckIfShouldFlip(playerDirection.x);
        float radians = Mathf.Atan2(weapondirection.y, weapondirection.x);
        float degrees = radians * Mathf.Rad2Deg;
        float dir = playerDirection.x >= 0f ? 1f : -1f;
        float x = (playerDirection.x < 0f) ? 180 : 0;

        weapon.Root.localEulerAngles = new Vector3(x, x, degrees * dir);

        weapon.InputValue = InputHandler.ActionInput && character.Stats["CurrentEnergy"].Value >= character.Inventory.Weapon.Stats["Energy"].Value;
    }



}
