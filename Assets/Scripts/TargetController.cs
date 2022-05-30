using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TargetController : MonoBehaviour
{

    TMP_Text targetPosition;
    int minRange = 100;
    int maxRange = 3901;
    float distance;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = GameObject.Find("TargetPosition").GetComponent<TMP_Text>();
        targetPosition.text = "Положення цілі:\nx = " + transform.position.x + "; z = " + transform.position.z + ";";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomPosition() 
    {
        int randX = RandomPoint();
        int randZ = RandomPoint();
        distance = VectorDistance(randX, randZ);

        Debug.Log(distance);

        while (distance > maxRange) 
        {   
            
            randZ = RandomPoint();
            distance = VectorDistance(randX, randZ);
            Debug.Log(distance);
        }

        transform.position = new Vector3(randX, transform.position.y, randZ);
        targetPosition.text = "Положення цілі:\nx = " + transform.position.x + "; z = " + transform.position.z + ";";

    }

    int RandomPoint() 
    {
        return Random.Range(minRange, maxRange) * (Random.Range(0, 2) * 2 - 1);
    }

    float VectorDistance(int randX, int randZ) 
    {
        return Mathf.Sqrt(randX * randX + randZ * randZ);
    }
}
