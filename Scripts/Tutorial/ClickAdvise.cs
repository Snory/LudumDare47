using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName = "NewAdvise", menuName = "Advise/ClickAdvise", order = 1)]
public class ClickAdvise : Advise
{
    public string ReactionTag;
    public bool HitMaskReaction;
    public LayerMask ReactionMask;
    private EventSystem _eventSystem;
    public override void OnStart()
    {
        _eventSystem = FindObjectOfType<EventSystem>();
    }

    public override void OnUpdate(Tutor tutor)
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (!HitMaskReaction)
            {
                tutor.HideWindow();
            }
                        
            if(ReactionMask == LayerMask.GetMask(Tags.UI_MASK))
            {
                bool ui = _eventSystem.IsPointerOverGameObject();

                if (ui)
                {
                    tutor.HideWindow();
                }
            }

            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(camRay.origin, camRay.direction, Mathf.Infinity, ReactionMask);

            if (hit) { 
                if(ReactionTag.Length > 0)
                {
                    if (hit.collider.gameObject.tag == ReactionTag)
                    {
                        tutor.HideWindow();
                    }
                } else
                {
                    tutor.HideWindow();
                }
            }

        }
    }
}
