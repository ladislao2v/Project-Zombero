using UnityEngine;

public class Zombie : Unit
{
    public Transform Target { get; private set; }

    public Zombie(int healthValue) : base(healthValue)
    {
    }

    public override Vector3 Move(Vector3 position, float deltaTime)
    {
        if (Target)
        {
            return Target.position;
        }
        
        return position;
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }
}