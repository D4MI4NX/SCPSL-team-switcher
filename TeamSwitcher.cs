using Exiled.API.Features;

namespace TeamSwitcher
{
    public class TeamSwitcher : Plugin<Config.Config>
    {
        public override string Name => "Team Switcher";
    
        public override string Prefix => "team_switcher";
        
        public override string Author => "D4MI4NX";
        
        public override Version Version => new(1, 1, 0);
        
        public override Version RequiredExiledVersion => new(9, 6, 1);


        public GenHandler Handler { get; private set; }

        public override void OnEnabled()
        {
            Handler = new GenHandler(this);
            Handler.Start();

            base.OnEnabled();
        }
        
        public override void OnDisabled()
        {
            Handler?.Stop();

            base.OnDisabled();
        }
    }
}
