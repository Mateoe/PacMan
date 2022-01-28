using UnityEngine;

public class Pellet : MonoBehaviour
{
    public int points = 10;
    PacManController PacMan;

    [SerializeField]
    private GameObject _me;

    [SerializeField]
    private PelletGenerator _pelletGenerator;

    void Start()
    {
        _me = this.gameObject;
        _pelletGenerator = GameObject.Find("Generator").GetComponent<PelletGenerator>();

        if((transform.position.x >= 2.5) && (transform.position.x <= 15.5))
        {
            if ((transform.position.z >= 6.5) && (transform.position.z <= 16.5))
            {

                Destroy(_me);
            }
        }

        if ((transform.position.z == 11.5) && ( transform.position.x <=  0.5 || transform.position.x >=  17.5 ))
        {

            Destroy(_me);
        }
    }

    public void Eat()
    {
        PacManController.AcumPoints(points);
        Destroy(_me);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.name == "PacMan")
        {
            Eat();
        }
        else if(collision.gameObject.tag == "Wall")
        {
            Destroy(_me);
        }
    }

}