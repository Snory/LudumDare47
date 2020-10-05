using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutor : MonoBehaviour
{
    public Vector2 OffsetWindowFromTutor;
    private TutorialWindow _window;
    public List<Advise> ListOfAdvise;
    private Advise _currentAdvise;


    private void Start()
    {
       
        
        
        foreach(Advise a in ListOfAdvise)
        {
            a.OnStart();
        }

        if (GameManager.Instance.RestartedLevel)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.gameObject.tag == "Player")
        {
            DisplayWindow();
        }
    }

    private void DisplayWindow()
    {
        _currentAdvise = ListOfAdvise[0];
        _window = TutorialWindowSpawner.Instance.SpawnTutorialWindow(this);
        GameManager.Instance.TryChangeManagerState(GameManagerState.TUTORIAL,this.gameObject.name + ":Display");
    }


    public void HideWindow()
    {
        RemoveCurrentAdvise();
        Destroy(_window.gameObject);
        if (ListOfAdvise.Count > 0)
        {
            DisplayWindow();
        } else
        {
            GameManager.Instance.TryChangeManagerState(GameManagerState.RUNNING, this.gameObject.name + ":Hide");
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if(_currentAdvise) _currentAdvise.OnUpdate(this);
    }

    private void RemoveCurrentAdvise()
    {
        ListOfAdvise.Remove(_currentAdvise);
        _currentAdvise = null;
    }

    public Advise GetCurrentAdvise()
    {
        return _currentAdvise;
    }

 

}
