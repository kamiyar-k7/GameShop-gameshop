using Domain.entities.GamePart.Game;

namespace Domain.entities.GamePart.Platform;

public class GameSelectedPlatform
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int PlatformId { get; set; }

    #region Rels
    public Domain.entities.GamePart.Game.Game Game { get; set; }
    public Platform Platform { get; set; }
    #endregion
}
