using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    
    private bool gameEnded = false;

    void Update()
    {
        if (gameEnded) {  return; }
        if (PlayerStats.lives <= 0)
        {
            EndGame();
           
        }

    }

    void EndGame()
    {
        gameEnded = true;
        Debug.Log("Game Over!");
    }
}
