using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YieldCache : MonoBehaviour
{
    public static readonly WaitForEndOfFrame wait_for_end_frame = new WaitForEndOfFrame();
    public static readonly WaitForFixedUpdate wait_for_fixed_update = new WaitForFixedUpdate();

    private static readonly Dictionary<float, WaitForSeconds> time_internal = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds WaitForSeconds( float _sec )
    {
        WaitForSeconds wfs;
        if ( !time_internal.TryGetValue( _sec, out wfs ) )
        {
            time_internal.Add( _sec, wfs = new WaitForSeconds( _sec ) );
        }
        return wfs;
    }

}
