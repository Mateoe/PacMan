using UnityEngine;

public class Pellet : MonoBehaviour
{
    public int points = 10;
    PacManController PacMan;


    public void Eat()
    {        
        gameObject.SetActive(false);
        PacManController.AcumPoints(points);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.name == "PacMan")
        {
            Eat();
        }
        else if(collision.gameObject.tag == "Wall"){
            gameObject.SetActive(false);
        }
    }

}