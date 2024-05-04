using System;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private readonly int Walk = Animator.StringToHash(nameof(Walk));

    [SerializeField] private Animator _animator;

    private void Awake()
    {
        if (_animator == null)
            throw new ArgumentNullException(nameof(Animator) + " " + nameof(PlayerAnimator));
    }

    public void PlayWalk()
    {
        _animator.Play(Walk);
    }
}
