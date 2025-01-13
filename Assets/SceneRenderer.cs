using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Zenject;

public class SceneRenderer
{
    private Volume _volume;

    [Inject]
    public void Construct(Volume volume)
    {
        _volume = volume;
    }

    public void EnableVignette()
    {
        if (_volume.profile.TryGet(out Vignette vignette))
        {
            vignette.active = true;
        }
    }

    public void DisableVignette()
    {
        if (_volume.profile.TryGet(out Vignette vignette))
        {
            vignette.active = false;
        }
    }
}
