using Content.Shared.FixedPoint;
using Content.Shared.Damage;
using Robust.Shared.GameStates;

namespace Content.Shared._Chroma.Damage.Components
{
    [RegisterComponent, NetworkedComponent]
    public sealed partial class WillToLiveComponent : Component
    {
        [DataField("speedModifierThresholds", required: true)]
        public Dictionary<FixedPoint2, float> SpeedModifierThresholds = default!;
    }
}