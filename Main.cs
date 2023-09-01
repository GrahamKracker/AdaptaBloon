using AdaptaBloon;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Enums;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Bloons;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Simulation.Track;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;

[assembly: MelonInfo(typeof(AdaptaBloon.Main), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace AdaptaBloon;

[HarmonyPatch]
public class Main : BloonsTD6Mod
{
    public override void OnNewGameModel(GameModel result)
    {
        for (var roundNumber = 0; roundNumber < result.roundSet.rounds.Count; roundNumber++)
        {
            var round = result.roundSet.rounds[roundNumber];
            foreach (var group in round.groups)
            {
                if (group.bloon is BloonType.Green or BloonType.Yellow && roundNumber % 2 == 0)
                {
                    group.bloon = ModContent.BloonID<AdaptaBloonBloon>();
                }
            }
        }
    }
    
    public override void OnBloonEmitted(Spawner spawner, BloonModel bloonModel, int round, int index,
        float startingDist, ref Bloon bloon)
    {
        if (bloonModel.id == ModContent.BloonID<AdaptaBloonBloon>())
        {
            var newTowerSet = GetTowerSetWithMostValue();     
            if (!newTowerSet.HasValue)
            {
                return;
            }       
            var newBloon = bloonModel.Duplicate();
            bloon.TowerSetImmunity = newTowerSet.Value;
            switch (newTowerSet.Value)
            {
                case TowerSet.None:
                    break;
                case TowerSet.Primary:
                    newBloon.display = ModContent.CreatePrefabReference<PrimaryDisplay>();
                    break;
                case TowerSet.Military:
                    newBloon.display = ModContent.CreatePrefabReference<MilitaryDisplay>();
                    break;
                case TowerSet.Magic:
                    newBloon.display = ModContent.CreatePrefabReference<MagicDisplay>();
                    break;
                case TowerSet.Support:
                    newBloon.display = ModContent.CreatePrefabReference<SupportDisplay>();
                    break;
                case TowerSet.Hero:
                case TowerSet.Paragon:
                case TowerSet.Items:
                default:
                    break;
            }
            bloon.UpdateRootModel(newBloon);
        }
    }

    static TowerSet? GetTowerSetWithMostValue()
    {
        return InGame.instance.GetAllTowerToSim().GroupBy(t => t.tower.towerModel.towerSet)
            .OrderByDescending(g => g.Sum(t => t.worth)).FirstOrDefault(t => t.Key != TowerSet.Hero)?.Key;
    }
}