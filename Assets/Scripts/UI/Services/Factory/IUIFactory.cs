using Assets.Scripts.Infrastructure.Services;

namespace Assets.Scripts.UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        void CreateShop();
        void CreateUIRoot();
    }
}