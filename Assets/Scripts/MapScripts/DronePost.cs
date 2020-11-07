using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DronePost : MonoBehaviour
{
    public ChromaticAberration chromaticAberration;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Rendering.VolumeProfile volumeProfile = GetComponent<UnityEngine.Rendering.Volume>()?.profile;
        if (!volumeProfile) throw new System.NullReferenceException(nameof(UnityEngine.Rendering.VolumeProfile));        
        if (!volumeProfile.TryGet(out chromaticAberration)) throw new System.NullReferenceException(nameof(chromaticAberration));
    }
}
