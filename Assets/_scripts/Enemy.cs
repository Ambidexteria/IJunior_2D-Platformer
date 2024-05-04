using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private Rotator _rotator;
    [SerializeField] private Transform _patrolPoint;
    [SerializeField] private float _patrolDistance = 5f;
    [SerializeField] private EnemyAnimator _enemyAnimator;

    private Vector2 _endPointsX;
    private Vector2 _movementDirection;

    private void Awake()
    {
        if (_mover == null)
            throw new ArgumentNullException(nameof(Mover) + " in " + nameof(name));

        if (_rotator == null)
            throw new ArgumentNullException(nameof(Rotator) + " in " + nameof(name));

        if (_enemyAnimator == null)
            throw new ArgumentNullException(nameof(EnemyAnimator) + " in " + nameof(name));
    }

    private void Start()
    {
        _enemyAnimator.PlayWalk();
        _endPointsX = GetEndPoints();
        _movementDirection = Vector2.left;
    }

    private void Update()
    {
        _mover.Move(_movementDirection);

        ChangeMovementDirection();
        ChangeLookDirection();
    }

    private void ChangeMovementDirection()
    {
        if (transform.position.x >= _endPointsX.y)
            _movementDirection = Vector2.left;
        else if (transform.position.x <= _endPointsX.x)
            _movementDirection = Vector2.right;
    }

    private void ChangeLookDirection()
    {
        if (_movementDirection == Vector2.left)
            _rotator.LookRight();
        else if (_movementDirection == Vector2.right)
            _rotator.LookLeft();
    }

    private Vector2 GetEndPoints()
    {
        float rightX = _patrolPoint.position.x + _patrolDistance;
        float leftX = _patrolPoint.position.x - _patrolDistance;

        return new Vector2(leftX, rightX);
    }
}
