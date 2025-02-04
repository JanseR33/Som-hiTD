using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro.Examples;
public class GameManager : MonoBehaviour
{
    [SerializeField] private List<WaveConfiguration> waveConfigurations = new List<WaveConfiguration>();
    [SerializeField] private Button startWaveButton; // The button that starts the wave (hopefully)
    private List<Spawnpoint> allSpawnpoints = new List<Spawnpoint>();
    private bool isWaveActive = false;
    
    private void Start()
    {
        // this is a debug.log that will tell me when the button is pressed
        startWaveButton.onClick.AddListener(() => Debug.Log("Button pressed!"));
        // First, find all spawnpoints
        allSpawnpoints = FindObjectsOfType<Spawnpoint>().ToList();
    }
    
    public void AddWave(WaveConfiguration waveConfig)
    {
        waveConfigurations.Add(waveConfig);
    }
    public void StartWave()
    {
        if (startWaveButton == null)
        {
            Debug.Log("No start wave button assigned!"); // With this, I expect to be able to realize when the button is not assigned
            return;
        }
        if (isWaveActive)
        {
            Debug.Log("A wave is already in progress!");
            return;
        }
        // this will send a warn message if there are no wave configurations
        if (!waveConfigurations.Any())
        {
            Debug.LogWarning("No wave configurations assigned!");
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
        Spawnpoint sp = allSpawnpoints.FirstOrDefault(sp => sp.SpawnpointID == id);
        if (sp != null)
        {
            Debug.Log("Spawnpoint found!");
        }
        else
        {
            Debug.LogWarning($"Spawnpoint with ID {id} not found!");
        }
        return sp;
    }
}