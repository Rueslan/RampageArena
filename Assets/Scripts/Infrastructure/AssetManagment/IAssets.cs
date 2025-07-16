using System.Threading.Tasks;
using Assets.Scripts.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Scripts.Infrastructure.AssetManagment
{
    public interface IAssets : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
        Task<T> Load<T>(AssetReference assetReference) where T: class;

        void CleanUp();
        Task<T> Load<T>(string gameObject) where T: class;
        void Initialize();
        Task<GameObject> InstantiateAsyncFromLoaded(string prefabPath, Vector3 at);
    }
}