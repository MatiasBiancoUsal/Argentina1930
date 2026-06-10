using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private int totalCoins;
    private int collectedCoins;

    public bool IrADormir
    {
        get { return collectedCoins >= totalCoins; }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        totalCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
    }

    public void CollectCoin()
    {
        collectedCoins++;

        Debug.Log("Monedas: " + collectedCoins + "/" + totalCoins);

        if (IrADormir)
        {
            Debug.Log("¡A Dormir!");
        }
    }
}