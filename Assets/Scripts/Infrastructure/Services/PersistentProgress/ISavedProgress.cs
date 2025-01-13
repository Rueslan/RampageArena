namespace Assets.Scripts.Infrastructure.Services.PersistentProgress
{
    public interface ISavedProgress : ISavedProgressReader
    {
        void UpdateProgress(PlayerProgress progress);        
    }
}