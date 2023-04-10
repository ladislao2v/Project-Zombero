using UnityEngine;

public class UserInputHandler
{
    private Input _input;
    private Player _player;
    private Joystick _joystick;

    public UserInputHandler(Player player, Joystick joystick)
    {
        _player = player;
        _input = new Input();
        _joystick = joystick;
    }

    public void OnEnable()
    {
        _input.Enable();
    }

    public void OnDisable()
    {
        _input.Disable();
    }

    public void Update()
    {
        Vector3 direction;
        var inputDirection = _input.Gameplay.Movement.ReadValue<Vector2>();

        if (_joystick.Direction == Vector2.zero)
            direction = new Vector3(inputDirection.x, 0, inputDirection.y);
        else
            direction = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);

        _player.GetMovementDirection(direction);
    }
}
