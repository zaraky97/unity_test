using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("à⁄ìÆë¨ìx")]
    private float _moveSpeed;
    [SerializeField, Header("çUåÇóÕ")]
    private int _attackPower;
    private Vector2 _inputDirection;
    private float _direction = 1f;

    private float _floorMinX;
    private float _floorMaxX;

    private Rigidbody2D _rigid;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>(); // Get Rigidbody2D component
        GameObject floor = GameObject.Find("Floor");
        Debug.Log(floor.transform.lossyScale.x);
        _floorMinX =(floor.transform.lossyScale.x / 2) * -1 + 0.2f;
        _floorMaxX = floor.transform.lossyScale.x / 2 - 0.2f;

    }

    // Update is called once per frame
    void Update()
    {
        _Move();
        _FlipByHorizontalDirection();
    }

    private void _Move()
    {
        Debug.Log(_direction);
        _rigid.linearVelocity = new Vector2((_direction > 0 ? Vector2.left.x : Vector2.right.x) * _moveSpeed, _rigid.linearVelocityY);
        if (transform.position.x <= _floorMinX)
        {
            _direction = -1f;
        }
        else if (transform.position.x >= _floorMaxX)
        {
            _direction = 1f;
        }
    }


    public void PlayerDamage(Player player)
    {
        player.Damage(_attackPower);
    }

    private void _FlipByHorizontalDirection()
    {
        if (_direction > 0)
        {
            transform.localScale = new Vector2(
                Mathf.Abs(transform.localScale.x),
                transform.localScale.y
            );
        }
        else if (_direction < 0)
        {
            transform.localScale = new Vector2(
                -Mathf.Abs(transform.localScale.x),
                transform.localScale.y
            );
        }
    }
}
