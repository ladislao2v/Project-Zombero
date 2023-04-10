using UnityEngine;

public class Cam
{
    private readonly float _returnSpeed = 2;
    private readonly float _height = 16;
    private readonly float _rearDistance = 6;

    private Vector3 _currentVector;
    private Vector3 _startPosition;

    public void GetStartPosition(Vector3 position)
    {
        _startPosition = position;
    }

    public Vector3 Follow(Transform camera, Transform player)
    {
        _currentVector = new Vector3(_startPosition.x, player.position.y + _height, player.position.z - _rearDistance);
        return Vector3.Lerp(camera.position, _currentVector, _returnSpeed * Time.deltaTime);
    }
}
