using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    private float _moveSpeed;
    [SerializeField, Header("ジャンプ速度")]
    private float _jumpSpeed;
    [SerializeField, Header("体力")]
    private int _hp;

    private Vector2 _inputDirection;
    private Rigidbody2D _rigid;
    private Animator _anim;
    private bool _bJump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>(); // Get Rigidbody2D component
        _anim = GetComponent<Animator>(); // Get Animator component
        _bJump = false;

    }

    // Update is called once per frame
    void Update()
    {
        _Move();
        _FlipByHorizontalDirection();
    }

    private void _Move()
    {
        _rigid.linearVelocity = new Vector2(_inputDirection.x * _moveSpeed, _rigid.linearVelocityY);
        _anim.SetBool("Walk", _inputDirection.x != 0.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            _bJump = false;
            _anim.SetBool("Jump", _bJump);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            _HitEnemy(collision.gameObject);
        }
    }

    private void _HitEnemy(GameObject enemy)
    {
        float halfScaleY = transform.lossyScale.y / 2.0f;
        float enemyHalfScaleY = enemy.transform.lossyScale.y / 2.0f;
        if (transform.position.y - (halfScaleY - 0.1f) >= enemy.transform.position.y + (enemyHalfScaleY - 0.1f))
        {
            Destroy(enemy);
            _rigid.AddForce(Vector2.up * (_jumpSpeed / 2), ForceMode2D.Impulse);
        }
        else
        {
            enemy.GetComponent<Enemy>().PlayerDamage(this);
        }
    }

    private void _Dead()
    {
        if (_hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _inputDirection = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed || _bJump) return;
        _rigid.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
        _bJump = true;
        _anim.SetBool("Jump", _bJump);
    }

    public void Damage(int damage)
    {
        _hp = Mathf.Max(_hp - damage, 0);
        _Dead();
    }

    public int GetHp()
    {
        return _hp;
    }

    private void _FlipByHorizontalDirection()
    {
        if (_inputDirection.x > 0)
        {
            transform.localScale = new Vector2(
                Mathf.Abs(transform.localScale.x),
                transform.localScale.y
            );
        }
        else if (_inputDirection.x < 0)
        {
            transform.localScale = new Vector2(
                -Mathf.Abs(transform.localScale.x),
                transform.localScale.y
            );
        }
    }
}
