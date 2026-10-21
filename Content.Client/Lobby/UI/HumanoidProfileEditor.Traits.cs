using System.Linq;
using Content.Client.Lobby.UI.Roles;
using Content.Client.Stylesheets;
using Content.Shared.Traits;
using Robust.Shared.Prototypes;
using Robust.Shared.Enums;
using Robust.Client.UserInterface.Controls;
using Robust.Shared.Utility;
using Content.Shared._Chroma.Traits;

namespace Content.Client.Lobby.UI;

public sealed partial class HumanoidProfileEditor
{

    /// <summary>
    /// Refreshes traits selector
    /// </summary>
    public void OnTraitsSelectionChanged(HashSet<ProtoId<TraitPrototype>> traits)
    {
        if (Profile is null)
            return;
        
        TabContainer.SetTabTitle(3, Loc.GetString("humanoid-profile-editor-traits-tab"));

        // Remove all existing traits - iterate directly over readonly collection
        foreach (var existingTrait in Profile.TraitPreferences)
        {
            Profile = Profile.WithoutTraitPreference(existingTrait, _prototypeManager);
        }

        // Add newly selected traits
        foreach (var trait in traits)
        {
            Profile = Profile.WithTraitPreference(trait.Id, _prototypeManager);
        }

        SetDirty();
    }

    /// <summary>
    /// Updates the traits tab with the current profile's selected traits.
    /// </summary>
    private void UpdateTraitsSelection()
    {
        if (Profile is null)
        {
            Traits.SetSelectedTraits(new HashSet<ProtoId<TraitPrototype>>(), Profile);
            return;
        }
        
        // Convert profile's trait preferences (strings) to ProtoId<TraitPrototype>
        var selectedTraits = new HashSet<ProtoId<TraitPrototype>>(Profile.TraitPreferences.Count);
        foreach (var traitId in Profile.TraitPreferences)
        {
            // Validate that the trait still exists in prototypes
            if (_prototypeManager.HasIndex(traitId))
            {
                selectedTraits.Add(new ProtoId<TraitPrototype>(traitId));
            }
        }

        Traits.SetSelectedTraits(selectedTraits, Profile);
        Traits.UpdateRequirements(Profile);
        }

    }
