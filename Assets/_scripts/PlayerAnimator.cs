using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private readonly int Idle = Animator.StringToHash(nameof(Idle));
    private readonly int Walk = Animator.StringToHash(nameof(Walk));
    private readonly int Jump = Animator.StringToHash(nameof(Jump));

    [SerializeField] private Animator _animator;
    [SerializeField] private Player _player;

    private void Awake()
    {
        if (_animator == null)
            throw new ArgumentNullException(nameof(Animator) + " " + nameof(PlayerAnimator));

        if (_player == null)
            throw new ArgumentNullException(nameof(Player) + " " + nameof(PlayerAnimator));
    }

    private void OnEnable()
    {
        _player.StateChanged += PlayAnimation;
    }

    private void OnDisable()
    {
        _player.StateChanged -= PlayAnimation;        
    }

    private void PlayAnimation(PlayerState state)
    {
        switch(state)
        {
            case PlayerState.Idle:
                _animator.Play(Idle);
                break;

            case PlayerState.Walk:
                _animator.Play(Walk);
                break;

            case PlayerState.Jump:
                _animator.Play(Jump);
                break;
        }
    }
}
