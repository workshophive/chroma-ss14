using Content.Shared._Chroma.Damage.Components;
using Content.Shared.Damage.Components;
using Content.Shared.Damage.Systems;
using Content.Shared.StatusEffectNew;
using Content.Shared.FixedPoint;
using Content.Shared.Movement.Systems;

namespace Content.Shared._Chroma.Damage.Systems;

public sealed partial class WillToLiveSystem : EntitySystem
{
    [Dependency] private MovementSpeedModifierSystem _movementSpeedModifierSystem = default!;
    [Dependency] private DamageableSystem _damage = default!;
    [Dependency] private SharedStaminaSystem _stamina = default!;

    public override void Initialize()
    {
        base.Initialize();
    }

    private void OnRefreshMovespeed(EntityUid uid, WillToLiveComponent component, RefreshMovementSpeedModifiersEvent args)
    {
        if (!TryComp<DamageableComponent>(uid, out var damage))
            return;

        var totalDamage = _damage.GetTotalDamage((uid, damage));

        if (totalDamage == FixedPoint2.Zero)
            return;
    }
}