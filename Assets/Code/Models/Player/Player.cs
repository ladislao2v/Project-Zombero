using UnityEngine;

public class Player : Unit
{
    private readonly float _rotateSpeed = 7f;
    private readonly float _speed = 12f; 

    public Player(int healthValue) : base(healthValue) { }

    public void GetMovementDirection(Vector3 direction) => Direction = direction;

    public override Quaternion Rotate(Transform presenter)
    {
        if (Vector3.Angle(presenter.forward, Direction) > 0)
            return Quaternion.LookRotation(Vector3.RotateTowards(presenter.forward, Direction, _rotateSpeed, 0));

        return presenter.rotation;
    }

    public override Vector3 Move(Vector3 position, float deltaTime)
    {
        if(Direction != Vector3.zero)
            return Direction * _speed * deltaTime;

        return position;
    }
}
