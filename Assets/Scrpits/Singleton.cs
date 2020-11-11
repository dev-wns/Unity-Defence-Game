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
                    var singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<Type>();
                    singletonObject.name = typeof( Type ).ToString() + " (Singleton)";

                    DontDestroyOnLoad( singletonObject );
                }
            }
            return instance;
        }
    }
}
