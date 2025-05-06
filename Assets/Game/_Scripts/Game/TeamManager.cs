using System.Collections.Generic;
using System.Linq.Expressions;
using RTS.Assets.Game._Scripts.Game.Enums;

namespace RTS.Assets.Game._Scripts.Game
{
    public class TeamManager
    {
        public Dictionary<Team, Player> teamList = new();
        private int bluePlayersCount = 0;
        private int redPlayersCount = 0;


        public void AddPlayer(Player player)
        {
            bluePlayersCount = 0;
            redPlayersCount = 0;
            foreach (var team in teamList.Keys)
            {
                if (team.Equals(Team.Blue))
                    bluePlayersCount++;

                if (team.Equals(Team.Red))
                    redPlayersCount++;
            }

            if(bluePlayersCount == redPlayersCount)
            {
                var redOrBlue = new System.Random().Next(0, 2) != 0;

                if (redOrBlue)
                    AssignTeam(Team.Red, player);
                else
                    AssignTeam(Team.Blue, player);
            }
            else
            {
                if (bluePlayersCount > redPlayersCount)
                    AssignTeam(Team.Red, player);
                else if (redPlayersCount > bluePlayersCount)
                    AssignTeam(Team.Blue, player);
            }
        }

        private void AssignTeam(Team team, Player player)
        {
            teamList.Add(team, player);
            player.PlayerTeam = team;
        }
    }
}
