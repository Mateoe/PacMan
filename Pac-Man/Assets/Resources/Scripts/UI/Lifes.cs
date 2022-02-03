using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifes : MonoBehaviour
{
    [SerializeField]
    private GameObject _lifes;

    [SerializeField]
    private GameObject _lifesPrefab;

    private Vector3 _rectTransformPosition;

    [SerializeField]
    private int _lifesCount;

    [SerializeField]
    List <GameObject> _lifesList;

    void Start()
    {
        _lifes = GameObject.Find("Lifes");
        Instance();
        EventSystem.OnPacManDeath += Destroy;
    }

    private void Instance()
    {
        
        for(int i = 0;i < _lifesCount; i++)
        {
            _rectTransformPosition = new Vector3((i*50)+10,80,0);
            GameObject _prefab = Instantiate(_lifesPrefab,_rectTransformPosition,_lifes.transform.rotation);
            _prefab.transform.SetParent(_lifes.transform);

            _lifesList.Add(_prefab);
        }
    }

    private void Destroy()
    {    
        for(int i = 0;i < 1; i++)
        {
            if(_lifesList.Count >0)
            {
                Destroy(_lifesList[i]);
                _lifesList.Remove(_lifesList[i]);
                _lifesCount -=1;
            }
            else
            {
                EventSystem.GameOver();
            }
        }
    }

    private void OnDestroy()
    {
        EventSystem.OnPacManDeath -= Destroy;
    }
}
