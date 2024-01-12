using Zenject;
using Domain;

namespace Presentation
{
    public class BattleInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // BattleSceneで利用するInputProviderを注入する
            Container
                .Bind<IBattleInput>() // IInputProviderに対して
                .To<BattleInput>() // PlayerInputProviderのインスタンスを注入する
                .AsCached(); // 利用できるインスタンスがすでにあればそれで実行

            // IPlayerComponentに対してPlayerControllerのインスタンスを注入する
            Container
                .Bind<IPlayerComponent>() // IPlayerComponentに対して
                .To<PlayerController>() // PlayerControllerのインスタンスを注入する
                .AsCached(); // 利用できるインスタンスがすでにあればそれで実行
        }
    }
}