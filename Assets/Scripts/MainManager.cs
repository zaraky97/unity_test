using UnityEngine;

public class MainManager : MonoBehaviour
{
    [SerializeField, Header("�Q�[���I�[�o�[")]
    private GameObject _gameOverUI;

    private GameObject _player;

    void Start()
    {
        _player = FindFirstObjectByType<Player>().gameObject;
    }

    void Update()
    {
        _ShowGameOverUI();
    }

    private void _ShowGameOverUI()
    {
        if (_player != null) return;

        _gameOverUI.SetActive(true);
    }
}
