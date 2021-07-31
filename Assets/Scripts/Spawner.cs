using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;    
    
    private Wave _CurrenWave;
    private int _CurrenWaveNumber = 0;
    private float _timeAfterLastSpawn;
    private int _spawned;
  
    public event UnityAction AllEnemySpawned;
    public event UnityAction<int, int> EnemyCountChenged;

    private void Start()
    {
        SetWave(_CurrenWaveNumber);        
    }

    private void Update()
    {
        if (_CurrenWave == null)
            return;

        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _CurrenWave.Delay)
        {
            InstantiateEnemy();
            _spawned++;
            _timeAfterLastSpawn = 0;
            EnemyCountChenged?.Invoke(_spawned, _CurrenWave.Count);
        }

        if (_CurrenWave.Count <= _spawned)
        {
            if (_waves.Count > _CurrenWaveNumber + 1)
                AllEnemySpawned?.Invoke();

            _CurrenWave = null;
        }
    }
    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_CurrenWave.Template, _spawnPoint.position, _spawnPoint.rotation, _spawnPoint).GetComponent<Enemy>();
        enemy.Init(_player);
        enemy.Dying += OnEnemyDying;

    }
    public void NextWave()
    {
        SetWave(++_CurrenWaveNumber);
        _spawned = 0;
    }
    private void SetWave(int index)
    {
        _CurrenWave = _waves[index];
        EnemyCountChenged?.Invoke(0, 1);
    }
    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnemyDying;
        _player.AddMoney(enemy.Reward);
    }    
}

[System.Serializable]
public class Wave
{
    public GameObject Template;
    public float Delay;
    public int Count;   
}

