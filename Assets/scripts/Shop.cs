using UnityEngine;

public class Shop : MonoBehaviour
{
    public TowerBlueperint archerTower;
    public TowerBlueperint mageTower;
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

    public void SetLastClickedSpace(buildingSpaces space)
    {
        lastClickedSpace = space;
    }
   
}   