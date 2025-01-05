using UnityEngine;
using UnityEngine.UI;

public class PlayerHP: MonoBehaviour
{
    [SerializeField, Header("HPÉAÉCÉRÉì")]
    private GameObject _playerIcon;

    private Player _player;
    private int _beforeHP;

    void Start()
    {
        _player = FindFirstObjectByType<Player>();
        _beforeHP = _player.GetHp();
        _CreateHP();
    }

    private void _CreateHP()
    {
        for (int i = 0; i < _player.GetHp(); i++)
        {
            GameObject _playerHPObj = Instantiate(_playerIcon);
            _playerHPObj.transform.parent = transform;
        }
    }

    void Update()
    {
       _ShowHPIcon();
    }

    private void _ShowHPIcon()
    {
        if (_beforeHP == _player.GetHp()) return;

        Image[] icons = transform.GetComponentsInChildren<Image>();

        for(int i = 0; i < icons.Length; i++)
        {
            icons[i].gameObject.SetActive(i < _player.GetHp());
        }
        _beforeHP = _player.GetHp();
    }
}
