/// <summary>
/// Playerの状態をごとの処理を定義するクラスに継承させるインターフェース
/// </summary>
public interface IPlayerState
{
    /// <summary>
    /// このStateに変更された際に実行する
    /// </summary>
    void Enter() { }

    /// <summary>
    /// このStateのUpdate処理
    /// </summary>
    void Update() { }

    /// <summary>
    /// このStateから別のStateに変更された際に実行する
    /// </summary>
    void Exit() { }
}