using Zenject;

public class BattleInputInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<IBattleInputProvider>() // IInputProvider�ɑ΂���
            .To<BattleInputProvider>() // PlayerInputProvider�̃C���X�^���X�𒍓�����
            .AsCached(); // ���p�ł���C���X�^���X�����łɂ���΂���Ŏ��s
    }
}