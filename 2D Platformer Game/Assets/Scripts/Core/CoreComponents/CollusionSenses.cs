using UnityEngine;

public class CollusionSenses : CoreComponent
{
    [SerializeField] Transform _groundCheck;
    [SerializeField] Transform _wallCheck;
    [SerializeField] Transform _ledgeCheck;
    [SerializeField] Transform _ceilingCheck;

    [SerializeField] float _groundCheckRadius;
    [SerializeField] float _wallCheckDistance;
    [SerializeField] LayerMask _groundLayer;

    public Transform groundCheck { get => _groundCheck; }
    public Transform wallCheck { get => _wallCheck; }
    public Transform ledgeCheck { get => _ledgeCheck; }
    public Transform ceilingCheck { get => _ceilingCheck;  }

    public float groundCheckRadius { get => _groundCheckRadius;  }
    public float wallCheckDistance { get => _wallCheckDistance; }
    public LayerMask groundLayer { get => _groundLayer; }

    public bool ground { get { return Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer); } }
    public bool wallFront { get { return Physics2D.Raycast(_wallCheck.position, Vector2.right * core.movement.facingDirection, _wallCheckDistance, _groundLayer); } }
    public bool wallBack { get { return Physics2D.Raycast(_wallCheck.position, Vector2.right * -core.movement.facingDirection, _wallCheckDistance, _groundLayer); } }
    public bool ledge { get { return Physics2D.Raycast(_ledgeCheck.position, Vector2.right * core.movement.facingDirection, _wallCheckDistance, _groundLayer); } }
    public bool ceiling { get { return Physics2D.OverlapCircle(_ceilingCheck.position, _groundCheckRadius, _groundLayer); } }

}
