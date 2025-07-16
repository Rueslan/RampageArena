using System.Threading.Tasks;
using Assets.Scripts.Data;

namespace Assets.Scripts.Infrastructure.Services.PersistentProgress
{
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }
}
