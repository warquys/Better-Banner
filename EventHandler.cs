using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using Mirror;
using Respawning;
using Respawning.NamingRules;
using System;

namespace Better_Server_Banner
{
    public class EventHandler
    {
        #region Properties & Variables
        public SyncList<SyncUnit> UnitList => RespawnManager.Singleton.NamingManager.AllUnitNames;

        private readonly Config _config;

        #endregion

        #region Constructor & Destructor
        internal EventHandler(Config config)
        {
            _config = config;
            AttachEvent();
        }
        #endregion

        #region Methods
        public void AttachEvent()
        {
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStart;
        }

        public void DetachEvent()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStart;
        }

        //Synapse3 Code 
        public void AddUnit(string name, Team team, int index = int.MaxValue)
        {
            var id = (byte)((byte)team + 1);

            var firstEntry = -1;
            var lastEntry = 0;

            for (int i = 0; i < UnitList.Count; i++)
            {
                var syncUnit = UnitList[i];
                if (syncUnit.SpawnableTeam != id) continue;

                if (firstEntry == -1)
                    firstEntry = i;

                lastEntry = i;
            }

            if (firstEntry == -1)
                firstEntry = 0;

            var maxIndex = lastEntry - firstEntry + 1;
            if (index > maxIndex)
                index = maxIndex;

            var newSyncUnit = new SyncUnit()
            {
                SpawnableTeam = id,
                UnitName = name
            };

            if (index < UnitList.Count)
                UnitList.Insert(firstEntry + index, newSyncUnit);
            else
                UnitList.Add(newSyncUnit);
        }

        #endregion

        #region Events

        private void OnRoundStart()
        {
            var teams = Enum.GetValues(typeof(Team)).ToArray<Team>();
            var isDefaultSet = !String.IsNullOrWhiteSpace(_config.ByDefault);

            if (_config.TeamMessage is null || _config.TeamMessage.Count == 0)
            {
                if (!isDefaultSet) return;

                foreach (var team in teams)
                    AddUnit(_config.ByDefault, team);
            }
            else 
            {
                foreach (var team in teams)
                {
                    if (_config.TeamMessage.ContainsKey(team))
                        AddUnit(_config.TeamMessage[team], team);
                    else if (!isDefaultSet)
                        AddUnit(_config.ByDefault, team);
                }
            }
        }
        #endregion
    }
}
