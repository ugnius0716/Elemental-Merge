using UnityEngine;

public class RadialMenuUI : MonoBehaviour
{
    public buildingSpaces owner;

    public void BuildTower(int towerTypeIndex)
    {
        if (owner == null)
        {
            Debug.LogWarning("RadialMenuUI owner is null.");
            return;
        }

        Shop shop = FindAnyObjectByType<Shop>();
        if (shop == null)
        {
            Debug.LogWarning("Shop not found in scene. Make sure a GameObject has the Shop component and is enabled.");
            return;
        }

        TowerBlueperint blueprint = null;
        if (towerTypeIndex == 0) blueprint = shop.archerTower;
        else if (towerTypeIndex == 1) blueprint = shop.mageTower;

        if (blueprint == null)
        {
            Debug.LogWarning($"No blueprint found for towerTypeIndex {towerTypeIndex} in Shop.");
            return;
        }

        owner.BuildTowerWithBlueprint(blueprint);
    }
}