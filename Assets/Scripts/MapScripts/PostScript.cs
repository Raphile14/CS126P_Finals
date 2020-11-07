using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PostScript : MonoBehaviour
{
    public MotionBlur motionBlur;
    public Vignette vignette;
    public FilmGrain filmGrain;
    public ColorAdjustments colorAdjustments;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Rendering.VolumeProfile volumeProfile = GetComponent<UnityEngine.Rendering.Volume>()?.profile;
        if (!volumeProfile) throw new System.NullReferenceException(nameof(UnityEngine.Rendering.VolumeProfile));

        if (!volumeProfile.TryGet(out motionBlur)) throw new System.NullReferenceException(nameof(motionBlur));
        if (!volumeProfile.TryGet(out vignette)) throw new System.NullReferenceException(nameof(vignette));
        if (!volumeProfile.TryGet(out filmGrain)) throw new System.NullReferenceException(nameof(filmGrain));
        if (!volumeProfile.TryGet(out colorAdjustments)) throw new System.NullReferenceException(nameof(colorAdjustments));
    }

}
