using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletGenerator : MonoBehaviour
{

    public GameObject Pellet;

    [SerializeField]
    private GameObject _generator;

    float initial_x = -3.5f;
    float initial_z = -3.5f;
    float final_x = 21.5f;
    float final_z = 24.5f;

    
    // Start is called before the first frame update
    void Start()
    {
        for (float zposition = initial_z; zposition <= final_z; zposition++){
            for (float xposition = initial_x; xposition <= final_x; xposition++)
            {
                GameObject _instance = Instantiate(Pellet, new Vector3(xposition, 0, zposition), Quaternion.identity);
                _instance.transform.SetParent(_generator.transform);

            }
        }
    }
}
