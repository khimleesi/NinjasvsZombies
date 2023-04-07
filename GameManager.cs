using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject            _player     = null;
    private GameCamera                             _camera     = null;
   
    void Start()
    {
        _camera = GetComponent<GameCamera>();
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        _camera.SetTarget((Instantiate(_player, Vector3.zero, Quaternion.identity) as GameObject).transform);
    }
}
