using UnityEngine;
using Zenject;

public class PausePanelInstaller : MonoInstaller
{
    [SerializeField] private PausePanel _pausePanelPrefab;
    [SerializeField] private Canvas _canvasController;

    public override void InstallBindings()
    {
        //BindPausePanel();
    }

    //private void BindPausePanel()
    //{
    //    var panelInstance =
    //                Container.InstantiatePrefab(_pausePanelPrefab, _canvasController.transform);


    //    Container.Bind<PausePanel>().
    //       FromInstance(panelInstance.GetComponent<PausePanel>()).
    //       AsSingle().
    //       NonLazy();
       
    //}
}
