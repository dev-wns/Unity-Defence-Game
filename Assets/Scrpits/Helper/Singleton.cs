using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<Type> : MonoBehaviour where Type : MonoBehaviour
{
    private static Type instance;

    public static Type Instance
    {
        get
        {
            if (instance == null )
            {
                instance = ( Type )FindObjectOfType( typeof( Type ) );

                if ( instance == null )
                {
                    var singleton_object = new GameObject();
                    instance = singleton_object.AddComponent<Type>();
                    singleton_object.name = typeof( Type ).ToString() + " (Singleton)";

                    DontDestroyOnLoad( singleton_object );
                }
            }
            return instance;
        }
    }
}
