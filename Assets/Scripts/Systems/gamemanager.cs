using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour // Changed from class to MonoBehaviour
{
    [SerializeField] private List<WaveConfiguration> waveConfigurations = new List<WaveConfiguration>();
    [SerializeField] private Button startWaveButton; // Reference to the Start Wave button
    private bool isWaveActive = false;

    public void AddWave(WaveConfiguration waveConfig)
    {
        waveConfigurations.Add(waveConfig);
    }

    public void StartWave()
    {
        if (isWaveActive)
        {
            Debug.Log("A wave is already in progress!");
            return;
        }

        if (!waveConfigurations.Any())
        {
            Debug.Log("No wave configurations assigned!");
            return;
        }

        StartCoroutine(HandleWave());
    }

    private IEnumerator HandleWave()
    {
        isWaveActive = true;
        startWaveButton.interactable = false; // Disable the button

        foreach (var waveConfig in waveConfigurations)
        {
            foreach (var config in waveConfig.spawnPointConfigs)
            {
                Debug.Log($"Spawning {config.quantity} of {config.enemyPrefab.name} " +
                          $"at spawnpoint {config.spawnpointID} with interval {config.spawnInterval}");

                Spawnpoint spawnpoint = FindSpawnpointByID(config.spawnpointID);
                if (spawnpoint != null)
                {
                    yield return spawnpoint.SpawnEnemies(config.enemyPrefab, config.quantity, config.spawnInterval);
                }
                else
                {
                    Debug.LogWarning($"Spawnpoint with ID {config.spawnpointID} not found!");
                }
            }
        }

        isWaveActive = false;
        startWaveButton.interactable = true; // Enable the button
        Debug.Log("Wave finished!");
    }

    private Spawnpoint FindSpawnpointByID(string id)
    {
        foreach (Spawnpoint spawnpoint in UnityEngine.Object.FindObjectsOfType<Spawnpoint>())
        {
            if (spawnpoint.SpawnpointID == id)
            {
                return spawnpoint;
            }
        }
        return null;
    }
}