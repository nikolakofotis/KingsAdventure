using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Pooling : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    
   


    public Dictionary<string, Queue<GameObject>> cratesPooling;
    public List<Pool> pools;
    private GameObject objectToSpawn;
    
    void Start()
    {
        cratesPooling = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            cratesPooling.Add(pool.tag, objectPool);
        }
    }



    public GameObject Spawn(string tag, Vector3 position,Quaternion rotation)
    {
        objectToSpawn = cratesPooling[tag].Dequeue();


        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;


        //cratesPooling[tag].Enqueue(objectToSpawn);
        return objectToSpawn;

    }

    public void EnQ(string tag, GameObject obj,float EnQAfter)
    {
        if (EnQAfter > 0f)
        {
            StartCoroutine(EnQWait(EnQAfter,obj,tag));
        }
        else 
        {


            obj.SetActive(false);
            cratesPooling[tag].Enqueue(obj);
        }
    }

    private IEnumerator EnQWait(float s,GameObject objec,string tag)
    {
        yield return new WaitForSeconds(s);
        objec.SetActive(false);
        cratesPooling[tag].Enqueue(objec);
    }


    




}
