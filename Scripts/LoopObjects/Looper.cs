using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Looper : MonoBehaviour
{
    private LayerMask _groundMask, _loopMask;
    public List<Action> ListOfAvailableActions;
    private List<Action> _listOfActions;
    public CircleMenu Menu { get; set; }
    public Action SelectedAction { get; set; }

    [SerializeField]
    private Action _currentSelectedAction;
    public Action SelectedTriggerAction { get; set; }
    
    [SerializeField]
    private Action _currentTriggerAction { get; set; }
    public Action ActiveFromStartAction;
    public bool IsGrounded;

    [SerializeField]
    private Transform _downCollision;


    private void Start()
    {
        _groundMask = LayerMask.GetMask(Tags.GROUND_MASK);
        _loopMask = LayerMask.GetMask(Tags.LOOPABLE_MASK);
        PrepareActions();
        foreach(Action action in _listOfActions)
        {
            action.OnActionStart(this);
        }

        GameManager.Instance.GameManagerStateChanged += OnGameManagerStateChanged;
    }

    private void OnGameManagerStateChanged(GameManagerState before, GameManagerState after)
    {
        if(after != GameManagerState.SELECTING && Menu != null && before == GameManagerState.TUTORIAL)
        {
            GameManager.Instance.TryChangeManagerState(GameManagerState.SELECTING, "Looper: Menu with select");
        }
    }

    public virtual void PrepareActions()
    {
        _listOfActions = new List<Action>();
        foreach (Action action in ListOfAvailableActions)
        {
            Action newAction = UnityEngine.Object.Instantiate(action);
            if (ActiveFromStartAction)
            {

                if (action.name == ActiveFromStartAction.name)
                {
                    newAction.SetActiveFromStart();
                    _currentSelectedAction = newAction;
                }
            }
            _listOfActions.Add(newAction);
            newAction.ActionActiveChanged += GameManager.Instance.OnActiveActionChanged;
        }
    }

    
    // Update is called once per frame
    void FixedUpdate()
    {
        CheckGrounded();

        if ((SelectedAction != null && SelectedTriggerAction != null))
        {
            this.StopAllCoroutines();
            TransitToSelectedAction();
            _currentSelectedAction.OnActionUpdate(this, SelectedTriggerAction);
            _currentTriggerAction = SelectedTriggerAction;
            SelectedTriggerAction = null;
        }
    }

    private void CheckGrounded()
    {
        IsGrounded = Physics2D.Raycast(_downCollision.position, Vector2.down, 0.1f, _groundMask);
        if (!IsGrounded)
        {
            RaycastHit2D hitted = Physics2D.Raycast(_downCollision.position, Vector2.down, 0.1f, LayerMask.GetMask(Tags.LOOPABLE_MASK));
            if (hitted) { 
                if(hitted.collider.gameObject != this.gameObject)
                {
                    IsGrounded = true;
                }
            }

        }
    }

    public void DisplayMenu(Looper position, bool displayAll)
    {      
       Menu = CircleMenuSpawner.Instance.SpawnLooperMenu(position, displayAll);
       Menu.SelectecActionEvent += OnSelectedActionEvent;    
    }

    private void LateUpdate()
    {
        this.transform.parent.position = transform.position;
        this.transform.localPosition = Vector3.zero;
    }

    public void HideMenu()
    {
        if(Menu != null) { 
            Destroy(Menu.gameObject);
        }
    }


    private void OnSelectedActionEvent(Action action)
    {
        SelectedAction = action;
    }


    private void TransitToSelectedAction()
    {

        if (_currentSelectedAction )
        {
            if (_currentSelectedAction.IsActionActive()) { 
                _currentSelectedAction.OnActionExit();
            }
        }
        _currentSelectedAction = SelectedAction;
        _currentSelectedAction.OnActionEnter(this);
        SelectedAction = null;

    }

    public List<Action> GetListOfActions(){
        return _listOfActions;
    }

    public List<Action> GetListOfActiveActions()
    {
        return _listOfActions.Where(x => x.IsActionActive()).ToList();
    }








}
