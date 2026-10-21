using Content.Server._CD.Traits;
using Content.Server.Silicons.Laws;
using Content.Server.StationEvents.Components;
using Content.Shared.GameTicking.Components;
using Content.Shared.Silicons.Laws.Components;
using Content.Shared.Station.Components;
// Used in CD's System
using Content.Server.Chat.Managers;
using Content.Shared.Chat;
using Robust.Shared.Player;

namespace Content.Server.StationEvents.Events;

/// <summary>
/// Handler for events that alter the laws of silicon entities (e.g. cyborgs, AI) on the affected station.
/// </summary>
/// <seealso cref="IonStormRuleComponent"/>
public sealed partial class IonStormRule : StationEventSystem<IonStormRuleComponent>
{
    [Dependency] private IonStormSystem _ionStorm = default!;
    [Dependency] private readonly IChatManager _chatManager = default!; // Used for CD's System

    protected override void Started(Entity<IonStormRuleComponent, GameRuleComponent> ent, ref GameRuleStartedEvent args)
    {
        base.Started(ent, ref args);

        if (!Station.TryGetRandomStation<StationEventEligibleComponent>(out var chosenStation))
            return;

        // CD Change - Go through everyone with the SynthComponent and inform them a storm is happening.
        var synthQuery = EntityQueryEnumerator<SynthComponent>();
        while (synthQuery.MoveNext(out var ent, out var synthComp))
        {
            if (RobustRandom.Prob(synthComp.AlertChance))
                continue;

            if (!TryComp<ActorComponent>(ent, out var actor))
                continue;

            var msg = Loc.GetString("station-event-ion-storm-synth");
            var wrappedMessage = Loc.GetString("chat-manager-server-wrap-message", ("message", msg));
            _chatManager.ChatMessageToOne(ChatChannel.Server, msg, wrappedMessage, default, false, actor.PlayerSession.Channel, colorOverride: Color.Yellow);
        }
        // End of CD change

        var query = EntityQueryEnumerator<SiliconLawBoundComponent, TransformComponent, IonStormTargetComponent>();
        while (query.MoveNext(out var borgUid, out var lawBound, out var xform, out var target))
        {
            // only affect law holders on the station
            if (CompOrNull<StationMemberComponent>(xform.GridUid)?.Station != chosenStation.Value.Owner)
                continue;

            _ionStorm.IonStormTarget((borgUid, lawBound, target));
        }
    }
}
