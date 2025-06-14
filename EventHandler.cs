using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Players = Exiled.Events.Handlers.Player;

namespace TeamSwitcher
{
    public class GenHandler(TeamSwitcher pluginInstance)
    {
        private readonly TeamSwitcher ts = pluginInstance;

        private readonly Dictionary<PlayerRoles.RoleTypeId, PlayerRoles.RoleTypeId> mtf2Chaos = new()
        {
            {PlayerRoles.RoleTypeId.FacilityGuard, PlayerRoles.RoleTypeId.ChaosConscript},
        };

        private readonly Dictionary<PlayerRoles.RoleTypeId, PlayerRoles.RoleTypeId> chaos2Mtf = new()
        {
            {PlayerRoles.RoleTypeId.ChaosConscript, PlayerRoles.RoleTypeId.NtfSpecialist},
            {PlayerRoles.RoleTypeId.ChaosRifleman, PlayerRoles.RoleTypeId.NtfPrivate},
            {PlayerRoles.RoleTypeId.ChaosMarauder, PlayerRoles.RoleTypeId.NtfSergeant},
            {PlayerRoles.RoleTypeId.ChaosRepressor, PlayerRoles.RoleTypeId.NtfCaptain},
        };

        public void Start()
        {
            // Reverse chaos2Mtf dictionary
            foreach (var entry in chaos2Mtf)
            {
                mtf2Chaos[entry.Value] = entry.Key;
            }

            Players.Escaping += OnEscaping;
        }

        public void Stop()
        {
            Players.Escaping -= OnEscaping;
        }

        public void OnEscaping(EscapingEventArgs ev)
        {
            if (
                ts.Config.GuardsBecomeMtf &&
                ev.Player.Role == PlayerRoles.RoleTypeId.FacilityGuard &&
                ev.Player.Cuffer == null
            )
            {
                ev.EscapeScenario = EscapeScenario.CustomEscape;
                ev.NewRole = PlayerRoles.RoleTypeId.NtfPrivate;
                ev.IsAllowed = true;
                Log.Debug(string.Format("{0} escaped as {1}", ev.Player.Nickname, ev.NewRole));
            }

            if (ev.Player.Cuffer == null)
            {
                return;
            }

            if (ev.Player.Role.Team == PlayerRoles.Team.FoundationForces && ts.Config.MtfBecomeChaos)
            {
                ev.EscapeScenario = EscapeScenario.CustomEscape;
                ev.NewRole = mtf2Chaos[ev.Player.Role];
                ev.IsAllowed = true;
                Log.Debug(string.Format("{0} escaped as {1}", ev.Player.Nickname, ev.NewRole));
            }

            if (ev.Player.Role.Team == PlayerRoles.Team.ChaosInsurgency && ts.Config.ChaosBecomesMtf)
            {
                ev.EscapeScenario = EscapeScenario.CustomEscape;
                ev.NewRole = chaos2Mtf[ev.Player.Role];
                ev.IsAllowed = true;
                Log.Debug(string.Format("{0} escaped as {1}", ev.Player.Nickname, ev.NewRole));
            }
        }
    }
}
