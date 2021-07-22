using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldData 
{
   
   
    public string[] diamond,dialogues;
    public bool newGame;


    public WorldData(PlayerMove player)
    {


        dialogues = player.dialogues.ToArray();
            diamond = player.diamond.ToArray();
        
    }
}
