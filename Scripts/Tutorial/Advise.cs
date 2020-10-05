using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public abstract class Advise : ScriptableObject
{
    public string AdviseText;


    public abstract void OnStart();
    public abstract void OnUpdate(Tutor tutor);


}
