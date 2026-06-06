using UnityEngine;

public class Shop : MonoBehaviour
{
    public TowerBlueperint archerTower;
    public TowerBlueperint mageTower;
    public TowerBlueperint steamTower;
    private buildingSpaces lastClickedSpace;

    public void SelectArcherTower()
    {
        if (lastClickedSpace != null)
            lastClickedSpace.BuildTowerWithBlueprint(archerTower);
    }

    public void SelectMageTower()
    {
        if (lastClickedSpace != null)
            lastClickedSpace.BuildTowerWithBlueprint(mageTower);
    }
    public void SelectSteamTower()
    {
        if (lastClickedSpace != null)
            lastClickedSpace.BuildTowerWithBlueprint(steamTower);
    }
    public void SetLastClickedSpace(buildingSpaces space)
    {
        lastClickedSpace = space;
    }
   
}   