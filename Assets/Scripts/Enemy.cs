using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("à⁄ìÆë¨ìx")]
    private float _moveSpeed;
    [SerializeField, Header("çUåÇóÕ")]
    private int _attackPower;

    private Rigidbody2D _rigid;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>(); // Get Rigidbody2D component

    }

    // Update is called once per frame
    void Update()
    {
        _Move();
    }

    private void _Move()
    {
        _rigid.linearVelocity = new Vector2(Vector2.left.x * _moveSpeed, _rigid.linearVelocityY);
    }

    public void PlayerDamage(Player player)
    {
        player.Damage(_attackPower);
    }
}
