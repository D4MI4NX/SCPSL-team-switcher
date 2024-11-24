using Exiled.API.Interfaces;

namespace TeamSwitcher.Config
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = false;
    }
}