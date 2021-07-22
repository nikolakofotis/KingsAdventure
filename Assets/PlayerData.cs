using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
   
    public float diamonds,lives;
    public int level, weapon, usableHearts,size,nextLevel,currentLevel;
    public int bulletCount;
    public float[] checkpoints;
    public float[] beginLevel;
    public int[] hasWeapons,hasBottles;
    public bool newGame,hasFinishedGame;
    public Dictionary<int, float[]> dic = new Dictionary<int, float[]>();
    public float[] diamond;
    public string[] dialogues;
    public int levelsCheck;
    public int hasMadeBottles;
    public string language;
   
    
   
    


    public PlayerData(PlayerMove player)
    {
        newGame = player.newGame;
        diamonds = player.diamonds;
        currentLevel = player.currentLevel;
        nextLevel = player.nextLevel;
        bulletCount = player.bulletCount;
        hasBottles = new int[4];
        hasFinishedGame = player.hasEndedTheGame;
        language = player.language;


       

        
        weapon = player.weapon;
        lives = player.lives;
        if (newGame == false)
        {
            hasMadeBottles = player.hasMadeBottles;
            levelsCheck = player.hasKeyForLevels;
            hasBottles = new int[4];
            hasBottles = player.hasBottles;
            checkpoints = new float[2];
            checkpoints[0] = player.checkpointPos[0];
            checkpoints[1] = player.checkpointPos[1];
            beginLevel = new float[2];
            beginLevel[0] = player.doorPos[0];
            beginLevel[1] = player.doorPos[1];
            hasWeapons = new int[8];
            hasWeapons = player.hasWeapons;
            usableHearts = player.usableHearts;
           
        }


        
      
       













    }
   
        
    

    
    
}
