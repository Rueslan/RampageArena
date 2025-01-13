using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Volume>().FromInstance(GetComponent<Volume>()).AsSingle();
        Container.Bind<SceneRenderer>().AsSingle();

    }

}
