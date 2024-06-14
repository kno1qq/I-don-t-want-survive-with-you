using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPonints;
    [SerializeField] private LivingEntity playerEnity;

    public Wave[]waves;
    private Wave _currantWave;
    private int _currantWaveIndex;

    private int _enemyRemainingLiveCount;

    private void Start()
    {
        if (spawnPonints.Length== 0) 
        {
            Debug.LogError("A");
            return;
        }
        StartCoroutine(routine:NextWaveCoroutine());
    }
    private IEnumerator NextWaveCoroutine()
    {
        _currantWaveIndex++;
        if(_currantWaveIndex-1<waves.Length)
        {
            _currantWave = waves[_currantWaveIndex - 1];
            for (int i = 0; i <_currantWave.count; i++)
            {
                int spawnIndex = Random.Range(0, spawnPonints.Length);
                monster love = Instantiate(_currantWave.love, spawnPonints[spawnIndex].position,Quaternion.identity);
                love.target=playerEnity.transform;
                yield return new WaitForSeconds(_currantWave.timeBetweenSpawn);
            }
        }
    }
}
