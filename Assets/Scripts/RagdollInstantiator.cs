using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollInstantiator : MonoBehaviour
{
    public GameObject ragdoll;
    List<GameObject> ragdollList = new List<GameObject>();
    public float spawnRate = 5f;
    [SerializeField]
    float nextTimeToSpawn = 0f;
    // Start is called before the first frame update
  

    // Update is called once per frame
    void Update()
    {
        SpawnRagdoll();
        MovePosition();
        if(ragdollList.Count >= 100)
        {
            foreach(GameObject ragdoll in ragdollList)
            {
                Destroy(ragdoll);
            }
            ragdollList.Clear();
        }
    }
    void MovePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);
        this.transform.position = mousePos;
    }
    void SpawnRagdoll()
    {
        if(nextTimeToSpawn <= 0f && Input.GetMouseButton(0))
        {
            //spawn here
            GameObject newRagdoll = Instantiate(ragdoll,transform.position,Quaternion.identity);
            ragdollList.Add(newRagdoll);
            
            
            
            ;
            //reset nextTimeToSpawn
            nextTimeToSpawn = 1 / spawnRate;
        }

        else
         nextTimeToSpawn -= Time.deltaTime;
    }
}
