namespace Assets.Scripts.Infrastructure.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress PlayerProgress { get; set; }
    }
}