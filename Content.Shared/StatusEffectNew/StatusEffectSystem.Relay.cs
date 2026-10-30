using Content.Shared.Body.Events;
using Content.Shared.Atmos;
using Content.Shared.Chat;
using Content.Shared.Chemistry.Events;
using Content.Shared.Climbing.Events;
using Content.Shared.Cuffs;
using Content.Shared.Damage.Events;
using Content.Shared.Damage.Systems;
using Content.Shared.Examine;
using Content.Shared.Eye.Blinding.Systems;
using Content.Shared.Flash;
using Content.Shared.Hands;
using Content.Shared.Medical;
using Content.Shared.Mobs;
using Content.Shared.Mobs.Events;
using Content.Shared.Mobs.Systems;
using Content.Shared.Movement.Events;
using Content.Shared.Movement.Systems;
using Content.Shared.NightVision;
using Content.Shared.Overlays;
using Content.Shared.Rejuvenate;
using Content.Shared.Speech;
using Content.Shared.Speech.EntitySystems;
using Content.Shared.StatusEffectNew.Components;
using Content.Shared.Stunnable;
using Content.Shared.Throwing;
using Content.Shared.Weapons.Ranged.Events;
using Robust.Shared.Collections;
using Robust.Shared.Player;

namespace Content.Shared.StatusEffectNew;

public sealed partial class StatusEffectsSystem
{
    private void InitializeRelay()
    {
        SubscribeLocalEvent<StatusEffectContainerComponent, LocalPlayerAttachedEvent>(RelayStatusEffectEvent);
        SubscribeLocalEvent<StatusEffectContainerComponent, LocalPlayerDetachedEvent>(RelayStatusEffectEvent);
        // SubscribeLocalEvent<StatusEffectContainerComponent, RejuvenateEvent>(RelayStatusEffectEvent);

        SubscribeLocalEvent<StatusEffectContainerComponent, RefreshMovementSpeedModifiersEvent>(RelayStatusEffectEvent);
        SubscribeLocalEvent<StatusEffectContainerComponent, ModifySlowOnDamageSpeedEvent>(RefRelayStatusEffectEvent);
        SubscribeLocalEvent<StatusEffectContainerComponent, UpdateCanMoveEvent>(RelayStatusEffectEvent);

        // By Ref Events
        SubscribeLocalEvent<StatusEffectContainerComponent, MobStateChangedEvent>(RefRelayStatusEffectEvent);

        SubscribeLocalEvent<StatusEffectContainerComponent, RefreshFrictionModifiersEvent>(RefRelayStatusEffectEvent);
        SubscribeLocalEvent<StatusEffectContainerComponent, TileFrictionEvent>(RefRelayStatusEffectEvent);

        SubscribeLocalEvent<StatusEffectContainerComponent, StandUpAttemptEvent>(RefRelayStatusEffectEvent);
        SubscribeLocalEvent<StatusEffectContainerComponent, StunEndAttemptEvent>(RefRelayStatusEffectEvent);
        SubscribeLocalEvent<StatusEffectContainerComponent, RefreshStaminaCritThresholdEvent>(RefRelayStatusEffectEvent);
        SubscribeLocalEvent<StatusEffectContainerComponent, CanSeeAttemptEvent>(RelayStatusEffectEvent);
        SubscribeLocalEvent<StatusEffectContainerComponent, FlashAttemptEvent>(RefRelayStatusEffectEvent);
        SubscribeLocalEvent<StatusEffectContainerComponent, SelfBeforeClimbEvent>(RelayStatusEffectEvent);

        SubscribeLocalEvent<StatusEffectContainerComponent, BeforeForceSayEvent>(RelayStatusEffectEvent);
        SubscribeLocalEvent<StatusEffectContainerComponent, BeforeAlertSeverityCheckEvent>(RelayStatusEffectEvent);
        SubscribeLocalEvent<StatusEffectContainerComponent, SpeakAttemptEvent>(RelayStatusEffectEvent, after: [typeof(MobStateSystem)]);
        SubscribeLocalEvent<StatusEffectContainerComponent, ExaminedEvent>(RelayStatusEffectEvent);

        SubscribeLocalEvent<StatusEffectContainerComponent, EmoteActionEvent>(RelayStatusEffectEvent, before: new[] { typeof(VocalSystem) });
        SubscribeLocalEvent<StatusEffectContainerComponent, AccentGetEvent>(RefRelayStatusEffectEvent);
        SubscribeLocalEvent<StatusEffectContainerComponent, EmoteEvent>(RefRelayStatusEffectEvent, before: new[] { typeof(VocalSystem), typeof(MumbleAccentSystem) });

        SubscribeLocalEvent<StatusEffectContainerComponent, BleedModifierEvent>(RefRelayStatusEffectEvent);
        SubscribeLocalEvent<StatusEffectContainerComponent, DamageModifyEvent>(RelayStatusEffectEvent);

        SubscribeLocalEvent<StatusEffectContainerComponent, Content.Shared._Offbrand.Wounds.WoundGetDamageEvent>(RefRelayStatusEffectEvent); // Offbrand
        SubscribeLocalEvent<StatusEffectContainerComponent, Content.Shared._Offbrand.Wounds.GetWoundsWithSpaceEvent>(RefRelayStatusEffectEvent); // Offbrand
        SubscribeLocalEvent<StatusEffectContainerComponent, Content.Shared._Offbrand.Wounds.GetPainEvent>(RefRelayStatusEffectEvent); // Offbrand
        SubscribeLocalEvent<StatusEffectContainerComponent, Content.Shared._Offbrand.Wounds.HealWoundsEvent>(RefRelayStatusEffectEvent); // Offbrand
        SubscribeLocalEvent<StatusEffectContainerComponent, Content.Shared._Offbrand.Wounds.GetBleedLevelEvent>(RefRelayStatusEffectEvent); // Offbrand
        SubscribeLocalEvent<StatusEffectContainerComponent, Content.Shared._Offbrand.Wounds.PainSuppressionEvent>(RefRelayStatusEffectEvent); // Offbrand
        SubscribeLocalEvent<StatusEffectContainerComponent, Content.Shared._Offbrand.Organs.BeforeDealOrganOxygenDamage>(RefRelayStatusEffectEvent); // Offbrand
        SubscribeLocalEvent<StatusEffectContainerComponent, Content.Shared._Offbrand.Organs.BeforeDepleteOrganOxygen>(RefRelayStatusEffectEvent); // Offbrand
        SubscribeLocalEvent<StatusEffectContainerComponent, Content.Shared._Offbrand.Organs.BeforeHealOrganOxygenDamage>(RefRelayStatusEffectEvent); // Offbrand
        SubscribeLocalEvent<StatusEffectContainerComponent, Content.Shared._Offbrand.Wounds.ModifiedVascularToneEvent>(RefRelayStatusEffectEvent); // Offbrand
        SubscribeLocalEvent<StatusEffectContainerComponent, Content.Shared._Offbrand.Wounds.ModifiedLungFunctionEvent>(RefRelayStatusEffectEvent); // Offbrand
        SubscribeLocalEvent<StatusEffectContainerComponent, Content.Shared._Offbrand.Wounds.ModifiedMetabolicRateEvent>(RefRelayStatusEffectEvent); // Offbrand
        SubscribeLocalEvent<StatusEffectContainerComponent, Content.Shared._Offbrand.Wounds.ModifiedCardiacOutputEvent>(RefRelayStatusEffectEvent); // Offbrand
        SubscribeLocalEvent<StatusEffectContainerComponent, Content.Shared._Offbrand.Wounds.ModifiedRespiratoryRateEvent>(RefRelayStatusEffectEvent); // Offbrand
        SubscribeLocalEvent<StatusEffectContainerComponent, Content.Shared._Offbrand.Weapons.RelayedGetMeleeDamageEvent>(RefRelayStatusEffectEvent); // Offbrand
        SubscribeLocalEvent<StatusEffectContainerComponent, Content.Shared._Offbrand.Weapons.RelayedGetMeleeAttackRateEvent>(RefRelayStatusEffectEvent); // Offbrand
        SubscribeLocalEvent<StatusEffectContainerComponent, Content.Shared._Offbrand.Weapons.RelayedGunRefreshModifiersEvent>(RefRelayStatusEffectEvent); // Offbrand
        SubscribeLocalEvent<StatusEffectContainerComponent, Content.Shared.Damage.Systems.ModifySlowOnDamageSpeedEvent>(RefRelayStatusEffectEvent); // Offbrand
        SubscribeLocalEvent<StatusEffectContainerComponent, Content.Shared.Eye.Blinding.Systems.GetBlurEvent>(RelayStatusEffectEvent); // Offbrand
        SubscribeLocalEvent<StatusEffectContainerComponent, Content.Shared.IdentityManagement.Components.SeeIdentityAttemptEvent>(RelayStatusEffectEvent); // Offbrand
        SubscribeLocalEvent<StatusEffectContainerComponent, Content.Shared.Movement.Pulling.Events.PullStartedMessage>(RelayStatusEffectEvent); // Offbrand
        SubscribeLocalEvent<StatusEffectContainerComponent, Content.Shared.Movement.Pulling.Events.PullStoppedMessage>(RelayStatusEffectEvent); // Offbrand
        SubscribeLocalEvent<StatusEffectContainerComponent, Content.Shared.Weapons.Ranged.Events.SelfBeforeGunShotEvent>(RelayStatusEffectEvent); // Offbrand
        SubscribeLocalEvent<StatusEffectContainerComponent, Content.Shared.Chemistry.Events.SelfBeforeInjectEvent>(RelayStatusEffectEvent); // Offbrand
        SubscribeLocalEvent<StatusEffectContainerComponent, DamageChangedEvent>(RelayStatusEffectEvent); // Offbrand
        SubscribeLocalEvent<StatusEffectContainerComponent, Content.Shared.Examine.ExaminedEvent>(RelayStatusEffectEvent); // Offbrand
        SubscribeLocalEvent<StatusEffectContainerComponent, Content.Shared.Verbs.GetVerbsEvent<Content.Shared.Verbs.AlternativeVerb>>(RelayStatusEffectEvent); // Offbrand
        SubscribeLocalEvent<StatusEffectContainerComponent, Content.Shared.Hands.BeforeEquippingHandEvent>(RefRelayStatusEffectEvent); // Offbrand

        SubscribeLocalEvent<StatusEffectContainerComponent, RefreshPressureImmunityEvent>(RefRelayStatusEffectEvent);
        SubscribeLocalEvent<StatusEffectContainerComponent, SelfBeforeDefibrillatorZapsEvent>(RelayStatusEffectEvent);
        SubscribeLocalEvent<StatusEffectContainerComponent, SelfBeforeInjectEvent>(RelayStatusEffectEvent);
        SubscribeLocalEvent<StatusEffectContainerComponent, RefreshNightVisionEvent>(RefRelayStatusEffectEvent);

        SubscribeLocalEvent<StatusEffectContainerComponent, CatchAttemptEvent>(RefRelayStatusEffectEvent);
        SubscribeLocalEvent<StatusEffectContainerComponent, SelfBeforeGunShotEvent>(RelayStatusEffectEvent);
        SubscribeLocalEvent<StatusEffectContainerComponent, BeforeEquippingHandEvent>(RefRelayStatusEffectEvent);

        SubscribeLocalEvent<StatusEffectContainerComponent, TargetHandcuffedEvent>(RefRelayStatusEffectEvent);
        SubscribeLocalEvent<StatusEffectContainerComponent, BeforeTargetHandcuffedEvent>(RefRelayStatusEffectEvent);
    }

    private void RefRelayStatusEffectEvent<T>(EntityUid uid, StatusEffectContainerComponent component, ref T args) where T : struct
    {
        RelayEvent((uid, component), ref args);
    }

    private void RelayStatusEffectEvent<T>(EntityUid uid, StatusEffectContainerComponent component, T args) where T : class
    {
        RelayEvent((uid, component), args);
    }

    public void RelayEvent<T>(Entity<StatusEffectContainerComponent> statusEffect, ref T args) where T : struct
    {
        if(statusEffect.Comp.ActiveStatusEffects?.ContainedEntities is not {} originalCollection || originalCollection.Count == 0)
            return;

        // this copies the by-ref event if it is a struct
        var ev = new StatusEffectRelayedEvent<T>(args, statusEffect);

        // Prevent a collection modified enumeration error by copying the list in case a status adds another status
        var list = new ValueList<EntityUid>(originalCollection);
        foreach (var activeEffect in list)
        {
            RaiseLocalEvent(activeEffect, ref ev);
        }
        // and now we copy it back
        args = ev.Args;
    }

    public void RelayEvent<T>(Entity<StatusEffectContainerComponent> statusEffect, T args) where T : class
    {
        if (statusEffect.Comp.ActiveStatusEffects?.ContainedEntities is not { } originalCollection || originalCollection.Count == 0)
            return;

        // this copies the by-ref event if it is a struct
        var ev = new StatusEffectRelayedEvent<T>(args, statusEffect);

        // Prevent a collection modified enumeration error by copying the list in case a status adds another status
        var list = new ValueList<EntityUid>(originalCollection);
        foreach (var activeEffect in list)
        {
            RaiseLocalEvent(activeEffect, ref ev);
        }
    }
}

/// <summary>
/// Event wrapper for relayed events.
/// </summary>
[ByRefEvent]
public record struct StatusEffectRelayedEvent<TEvent>(TEvent Args, EntityUid AppliedTo);
