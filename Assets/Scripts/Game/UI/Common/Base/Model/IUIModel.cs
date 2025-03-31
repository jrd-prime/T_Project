using Zenject;

namespace Game.UI.Common.Base.Model
{
    public interface IUIModel : IInitializable
    {

        // public void SetPreviousState();
    }

    public interface IMenuModel : IUIModel
    {
    }


    public interface IGameoverModel : IUIModel
    {
    }


    public interface IWinModel : IUIModel
    {
    }
}
