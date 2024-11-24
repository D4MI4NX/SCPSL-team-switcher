using System.ComponentModel;
using Exiled.API.Interfaces;

namespace TeamSwitcher.Config
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = false;

        [Description("Facility guards will become MTF when escaping")]
        public bool GuardsBecomeMtf { get; set; } = true;

        [Description("MTF can become Chaos when escaping cuffed")]
        public bool MtfBecomeChaos { get; set; } = true;

        [Description("Chaos can become MTF when escaping cuffed")]
        public bool ChaosBecomesMtf { get; set; } = true;
    }
}