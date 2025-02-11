using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public GameManager gameManager; // I think, with this i'll be able to call the StopWave void.
    public int money = 0;
    public int PlayerHealth = 200;
    public TMPro.TextMeshProUGUI playerhealth;

    public void AddMoney(int amount)
    {
        money += amount;
        Debug.Log($"stonks, actual money:{money}");
    }

    public bool SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            return true;
        }
        Debug.Log($"not enough money, actual money:{money}");
        return false;
    }

    public void playerhit(int DamagePlayer)
    {
        PlayerHealth -= DamagePlayer;
        UpdateHealthText();
        if (PlayerHealth <= 0)
        {
            gameManager.stopWave();
            // I have to expand this a bit more, rn I just have some debug.log and end the game logic
            Debug.Log("THE PLAYER IS DEAD!"); // THE HEAVY IS DEAD (TF2 reference)
        }
        else
        {
            Debug.Log($"Player took {DamagePlayer} damage. Remaining PlayerHealth: {PlayerHealth}");
        }
    }
    private void UpdateHealthText()
    {
        if (playerhealth != null)
        {
            playerhealth.text = $"Skibidi Vida: {PlayerHealth}";
        }
    }
}
// PEDRO SANCHEZ HIJO DE PUTA (Spanish reference)