using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManger : MonoBehaviour
{
    public GameObject[] floorPrefabs;
    public void SpawnFloor(){
        int r=Random.Range(0,floorPrefabs.Length);
        GameObject floor = Instantiate(floorPrefabs[r],transform);
        floor.transform.position = new Vector3(Random.Range(-3.8f,3.8f),-6f,0); 
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
