using UnityEngine;

public interface IInputProvider
{
    /// <summary>
    /// ˆÚ“®•ûŒü‚Ì“ü—Íˆ—
    /// </summary>
    /// <returns>ˆÚ“®‚Ì•ûŒü</returns>
    Vector2 GetMoveDir();

    /// <summary>
    /// UŒ‚‚Ì“ü—Íˆ—
    /// </summary>
    /// <returns>UŒ‚‚Ì“ü—Í”»’è</returns>
    bool GetFire();
}
