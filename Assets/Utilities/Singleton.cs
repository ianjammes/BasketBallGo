using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Abstract class : it's gonna be inherited by subclasses -> partially implemented
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    //T is a template type
    //T is a gateaway to access this class
    private static T instance;

    public static T Instance
    {
        get{
            if(instance == null){
                instance = FindObjectOfType<T>(); //We instanciate it, whatever Template classed we passed in
            }
            DontDestroyOnLoad(instance);
            return instance;
        }
    }

}
