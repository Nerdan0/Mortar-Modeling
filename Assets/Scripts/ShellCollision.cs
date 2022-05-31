using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShellCollision : MonoBehaviour
{
    [SerializeField] GameObject craterPrefab;
    TMP_Text hitX;
    TMP_Text hitZ;

    // Start is called before the first frame update
    void Start()
    {
        hitX = GameObject.Find("HitX").GetComponent<TMP_Text>();
        hitZ = GameObject.Find("HitZ").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        hitX.text = "X:" + Mathf.Round(transform.position.x); 
        hitZ.text = "Z:" + Mathf.Round(transform.position.z);
        Instantiate(craterPrefab, new Vector3(transform.position.x, 0.05f, transform.position.z), Quaternion.identity);
        Destroy(gameObject);

        GameObject[] craters = GameObject.FindGameObjectsWithTag("Crater");
        if (craters.Length>5)
        {
            Destroy(craters[0]);
        }


    }
}
