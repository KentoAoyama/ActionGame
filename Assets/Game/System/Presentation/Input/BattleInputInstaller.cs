using Zenject;

public class BattleInputInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<IBattleInputProvider>() // IInputProviderに対して
            .To<BattleInputProvider>() // PlayerInputProviderのインスタンスを注入する
            .AsCached(); // 利用できるインスタンスがすでにあればそれで実行
    }
}