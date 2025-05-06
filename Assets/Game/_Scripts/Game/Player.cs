using RTS.Assets.Game._Scripts.Game.Enums;
using Unity.Netcode;

namespace RTS.Assets.Game._Scripts.Game
{
    public class Player : NetworkBehaviour
    {
        public Team PlayerTeam;
    }
}
