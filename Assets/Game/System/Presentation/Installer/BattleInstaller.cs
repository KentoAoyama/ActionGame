using Zenject;
using Domain;

namespace Presentation
{
    public class BattleInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // BattleScene�ŗ��p����InputProvider�𒍓�����
            Container
                .Bind<IBattleInput>() // IInputProvider�ɑ΂���
                .To<BattleInput>() // PlayerInputProvider�̃C���X�^���X�𒍓�����
                .AsCached(); // ���p�ł���C���X�^���X�����łɂ���΂���Ŏ��s

            // IPlayerComponent�ɑ΂���PlayerController�̃C���X�^���X�𒍓�����
            Container
                .Bind<IPlayerComponent>() // IPlayerComponent�ɑ΂���
                .To<PlayerController>() // PlayerController�̃C���X�^���X�𒍓�����
                .AsCached(); // ���p�ł���C���X�^���X�����łɂ���΂���Ŏ��s
        }
    }
}