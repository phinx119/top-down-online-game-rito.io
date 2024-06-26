using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpCollect : MonoBehaviour
{
    public GameObject player;
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerStats>().currExp += 1;
            gameObject.SetActive(false);
        }
    }
}
