using Assets.Scripts.Infrastructure.Services;

namespace Assets.Scripts.Infrastructure
{
    public interface IRandomService : IService
    {
        int Next(int Min, int Max);
    }
}