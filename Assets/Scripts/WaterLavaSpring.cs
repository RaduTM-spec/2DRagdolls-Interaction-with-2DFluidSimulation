using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WaterLavaSpring : MonoBehaviour
{
    public GameObject waterParticle, lavaParticle;
    public List<GameObject> particlesList = new List<GameObject>();

    [Range(1,1500)] public int maxParticles;
    static public int currentParticles = 0;
    // Update is called once per frame
    void Update()
    {
        Spawn(GetPosition());
        CheckMaxParticles();
    }
    void CheckMaxParticles()
    {
       
        while(currentParticles > maxParticles)
        { 
            Destroy(particlesList[0]);
            particlesList.RemoveAt(0);
            currentParticles--;
        }
    }
    Vector2 GetPosition()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return mousePos;
    }
    void Spawn(Vector2 position)
    {
        if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftAlt))
            particlesList.Add(Instantiate(lavaParticle, position, Quaternion.identity));
        else if (Input.GetMouseButton(1) && !Input.GetKey(KeyCode.LeftAlt))
            particlesList.Add(Instantiate(waterParticle, position, Quaternion.identity));
        else return;
        currentParticles++;
    }
    public List<GameObject> AccessList()
    {
        return particlesList;
    }
    static public ref int GetCurrentParticles()
    {
        return ref currentParticles;
    }
}
