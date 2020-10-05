using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{

    private LayerMask _loopableMask, _uiMask;
    private List<Looper> _selectedLoops;

    [SerializeField]
    private EventSystem _eventSystem;
    // Start is called before the first frame update

    private void Awake()
    {
        _eventSystem = FindObjectOfType<EventSystem>();
    }


    void Start()
    {
        _loopableMask = LayerMask.GetMask(Tags.LOOPABLE_MASK);
        _uiMask = LayerMask.GetMask(Tags.UI_MASK);
        _selectedLoops = new List<Looper>();

    }

    // Update is called once per frame
    void Update()
    {
        OnMouseDown();
        TryPerformAction();
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D hitLoop = Physics2D.Raycast(camRay.origin, camRay.direction, Mathf.Infinity, _loopableMask);
            bool ui = _eventSystem.IsPointerOverGameObject();

            if (hitLoop && !ui)
            {
                AddToLoopArray(hitLoop.collider.gameObject.GetComponent<Looper>());
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if(GameManager.Instance.GetGameManagerState() != GameManagerState.TUTORIAL)
            EmptyLoopArray();
        }
    }

    private void AddToLoopArray(Looper loop)
    {

        if (_selectedLoops.Contains(loop)) return;

        if (_selectedLoops.Count == 2)
        {
            return;
        }

        if (_selectedLoops.Count == 0)
        {
            if (loop.GetListOfActions().Where(x => x.IsActionActive()).FirstOrDefault() == null)
            {
                return;
            } 
        }

        
        GameManager.Instance.TryChangeManagerState(GameManagerState.SELECTING, this.gameObject.name + ":AddingLoop");
        _selectedLoops.Add(loop);
        loop.DisplayMenu(loop, _selectedLoops.Count != 1);
        
    }

    private void EmptyLoopArray()
    {

        foreach(Looper loop in _selectedLoops.ToList())
        {
          loop.HideMenu();         
        }

        GameManager.Instance.TryChangeManagerState(GameManagerState.RUNNING, this.gameObject.name + ":HiddingLoop");
        _selectedLoops.Clear();
    }

    private void TryPerformAction()
    {
        if(_selectedLoops.Count == 2)
        {
            if (_selectedLoops[0].SelectedAction  && _selectedLoops[1].SelectedAction)
            {
                _selectedLoops[1].SelectedTriggerAction = _selectedLoops[0].SelectedAction;
                _selectedLoops[0].SelectedAction = null;
                EmptyLoopArray();
            }
        }
    }





    
}
