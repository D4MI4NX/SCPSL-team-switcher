using Exiled.Events.EventArgs.Player;
using Players = Exiled.Events.Handlers.Player;

namespace TeamSwitcher
{
    public class GenHandler(TeamSwitcher pluginInstance)
    {
        private readonly TeamSwitcher ts = pluginInstance;

        public void Start()
        {
            Players.Escaping += OnEscaping;
        }

        public void Stop()
        {
            Players.Escaping -= OnEscaping;
        }

        public void OnEscaping(EscapingEventArgs ev)
        {
            if (ts.Config.GuardsBecomeMtf)
            {
                if (ev.Player.Role == PlayerRoles.RoleTypeId.FacilityGuard)
                {
                    ev.NewRole = PlayerRoles.RoleTypeId.NtfPrivate;
                    ev.IsAllowed = true;
                }
            }
        }
    }
}
