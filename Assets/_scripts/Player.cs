using System;
using UnityEngine;

public enum PlayerState
{
    Walk,
    Idle,
    Jump
}

public enum Direction
{
    Forward,
    Backward
}

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);

    [SerializeField] private Mover _mover;
    [SerializeField] private Rotator _rotator;
    [SerializeField] private Jumper _jumper;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private PlayerAnimator _playerAnimator;

    private Direction _direction = Direction.Forward;
    private PlayerState _state = PlayerState.Idle;

    public event Action<PlayerState> StateChanged;

    private void Awake()
    {
        if (_mover == null)
            throw new ArgumentNullException(nameof(Mover) + " " + nameof(name));

        if (_rotator == null)
            throw new ArgumentNullException(nameof(Rotator) + " " + nameof(name));

        if (_jumper == null)
            throw new ArgumentNullException(nameof(Jumper) + " " + nameof(name));

        if (_groundDetector == null)
            throw new ArgumentNullException(nameof(GroundDetector) + " " + nameof(name));

        if (_playerAnimator == null)
            throw new ArgumentNullException(nameof(PlayerAnimator) + " " + nameof(name));
    }

    private void Update()
    {
        float direction = Input.GetAxisRaw(Horizontal);
        ChangeDirection(direction);

        if (direction != 0)
            Move();

        if (_groundDetector.IsGrounded)
        {
            if (direction == 0)
                _state = PlayerState.Idle;
            else
                _state = PlayerState.Walk;

            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
        }
        else
        {
            _state = PlayerState.Jump;
        }

        StateChanged?.Invoke(_state);
    }

    private void Jump()
    {
        _jumper.Jump();
    }

    private void Move()
    {
        _mover.Move(transform.right);
    }

    private void ChangeDirection(float direction)
    {
        if (direction < 0 && _direction != Direction.Backward)
        {
            _direction = Direction.Backward;
            _rotator.FlipY();
        }
        else if (direction > 0 && _direction != Direction.Forward)
        {
            _direction = Direction.Forward;
            _rotator.FlipY();
        }
    }
}