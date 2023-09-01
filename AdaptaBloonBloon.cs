using BTD_Mod_Helper.Api.Bloons;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Enums;
using Il2CppAssets.Scripts.Models.Bloons;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Unity.Display;

namespace AdaptaBloon;

public class AdaptaBloonBloon : ModBloon
{
    public override string Icon => "BloonIcon";

    public override void ModifyBaseBloonModel(BloonModel bloonModel)
    {
        bloonModel.RemoveBehaviors<SpawnChildrenModel>();
        bloonModel.childBloonModels.Clear();
        bloonModel.ApplyDisplay<PrimaryDisplay>();
    }

    public override bool Camo => false;
    public override bool Fortified => false;
    public override bool Regrow => false;

    public override string BaseBloon => BloonType.Green;
}

public class PrimaryDisplay : ModBloonDisplay<AdaptaBloonBloon>
{
    public override string BaseDisplay => "9d3c0064c3ace7448bf8fefa4a97a70f";
    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        Set2DTexture(node, "PrimaryBloon");
    }
}

public class MilitaryDisplay : ModBloonDisplay<AdaptaBloonBloon>
{
    public override string BaseDisplay => "9d3c0064c3ace7448bf8fefa4a97a70f";
    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        Set2DTexture(node, "MilitaryBloon");
    }
}

public class MagicDisplay : ModBloonDisplay<AdaptaBloonBloon>
{
    public override string BaseDisplay => "9d3c0064c3ace7448bf8fefa4a97a70f";
    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        Set2DTexture(node, "MagicBloon");
    }
}

public class SupportDisplay : ModBloonDisplay<AdaptaBloonBloon>
{
    public override string BaseDisplay => "9d3c0064c3ace7448bf8fefa4a97a70f";
    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        Set2DTexture(node, "SupportBloon");
    }
}