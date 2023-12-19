using System;
using System.Linq;
using UnityEngine;
public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int level = -1;
    [SerializeField] private int budget = -1;

    
    // Makes an array of 'weights' which define the probability and cost of spawning an enemy
    weight[] enemyWeights = new weight[(int) Enum.GetValues(typeof(ShipType)).Cast<ShipType>().Max()+1];
    int[] levelBudgets = new int[10];
    bool spawnsEnabled;
    int[] enemiesSelected = new int[(int) Enum.GetValues(typeof(ShipType)).Cast<ShipType>().Max()+1];

    GameRandom rand = new GameRandom();
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        /*------------------------------------------------
        ###ENEMY WEIGHTS##################################
        ------------------------------------------------*/

        enemyWeights[(int) ShipType.player] = new weight(0,0);
        enemyWeights[(int) ShipType.kami] =  new weight(50,.5);
        enemyWeights[(int) ShipType.grunt] =  new weight(100,.3);

        /*------------------------------------------------
        ###LEVEL WEIGHTS##################################
        ------------------------------------------------*/

        // Placeholder, will replace with enums in the future I think.
        levelBudgets[0] = 1000;

        /*------------------------------------------------
        ###INITIALIZATION##################################
        ------------------------------------------------*/

        spawnsEnabled = false;
        start(0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!spawnsEnabled)
            return;
        // Spawn things
    }

    // Sets the budget and the level. Selects enemies based on their weights until the level budget is exhusted.
    public void start(int l)
    {
        budget = levelBudgets[l];
        level = l;
    
        while (budget > 0)
        {
            for (int i = 0; i < enemyWeights.Length; ++i)
            {
                weight w = enemyWeights[i];
                if (rand.Test(w.p))
                {
                    enemiesSelected[i]++;
                    budget -= w.c;
                }
            }
        }
        spawnsEnabled = true;
        Debug.Log($"[{string.Join(",", enemiesSelected)}]");
    }
    public void stop()
    {
        for (int i = 0; i < enemiesSelected.Length; ++i) enemiesSelected[i] = 0;
        spawnsEnabled = false;
    }
}
