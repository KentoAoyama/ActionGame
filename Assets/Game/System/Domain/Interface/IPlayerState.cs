/// <summary>
/// Player�̏�Ԃ����Ƃ̏������`����N���X�Ɍp��������C���^�[�t�F�[�X
/// </summary>
public interface IPlayerState
{
    /// <summary>
    /// ����State�ɕύX���ꂽ�ۂɎ��s����
    /// </summary>
    void Enter() { }

    /// <summary>
    /// ����State��Update����
    /// </summary>
    void Update() { }

    /// <summary>
    /// ����State����ʂ�State�ɕύX���ꂽ�ۂɎ��s����
    /// </summary>
    void Exit() { }
}