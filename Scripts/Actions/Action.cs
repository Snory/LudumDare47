using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ActionStateDelegate(bool active);

public abstract class Action : ScriptableObject
{
    //public ActionStateData CurrentState { get; set; }


    private bool _isActive;
    public Color Selected = Color.white, Highlighted = Color.white, Pressed = Color.gray, Normal = Color.black;
    public Sprite ActiveIcon;
    public Sprite InActiveIcon;
    public event ActionStateDelegate ActionActiveChanged;
    private Looper _looper;
    private bool _startRoutine = false;

    public virtual void OnActionEnter(Looper looper)
    {
        _looper = looper;
        SetActive(true);
    }
    public virtual void OnActionExit()
    {
        SetActive(false);
    }
    
    public void OnActionStart(Looper looper)
    {
             
        if (_startRoutine)
        {
            OnActionEnter(looper);
            looper.StartCoroutine(ActionRoutine(this));
        }
    }

    public void OnActionUpdate(Looper looper, Action triggerAction)
    {
        looper.StartCoroutine(ActionRoutine(triggerAction));
    }
    
    protected virtual IEnumerator ActionRoutine(Action triggerAction)
    {

        while (triggerAction.IsActionActive())
        {
            yield return null;
        }
       
    }

    public void SetActive(bool isActive)
    {
        _isActive = isActive;
        if(ActionActiveChanged != null)
        {

            ActionActiveChanged.Invoke(_isActive);
        }    
    }

    public bool IsActionActive()
    {
 
        return _isActive;
    }

    public void SetActiveFromStart()
    {
        _startRoutine = true;
    }

   

}
