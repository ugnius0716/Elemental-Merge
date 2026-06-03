using UnityEngine;

public class buildManager : MonoBehaviour
{
    public static buildManager instance;

    public buildingSpaces buildingSpaces;
    private TowerBlueperint towerToBuild;
    public GameObject ArcherTowerPrefab;
    public GameObject MageTowerPrefab;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one buildManager in scene!");
            return;
        }
        instance = this;
    }

    public bool CanBuild => towerToBuild != null;

    public void SelectTowerToBuild(TowerBlueperint tower)
    {
        towerToBuild = tower;
    }

    public GameObject BuildTowerOn(buildingSpaces space, TowerBlueperint blueprint)
    {
        if (blueprint.prefab == null)
        {
            Debug.LogWarning("Blueprint prefab is null. Assign a prefab to the TowerBlueperint in the inspector.");
            return null;
        }

        if (PlayerStats.money < blueprint.cost)
        {
            Debug.Log($"Not enough money to build that tower! Cost: {blueprint.cost}, Have: {PlayerStats.money}");
            return null;
        }

        PlayerStats.money -= blueprint.cost;

        Vector3 spawnPos = space.GetBuildPosition();
        GameObject tower = Instantiate(blueprint.prefab);
        tower.transform.SetParent(null);

        Vector3 visualOffset = Vector3.zero;
        var sprite = tower.GetComponentInChildren<SpriteRenderer>(true);
        if (sprite != null)
            visualOffset = sprite.bounds.center - tower.transform.position;
        else
        {
            var renderer = tower.GetComponentInChildren<Renderer>(true);
            if (renderer != null)
                visualOffset = renderer.bounds.center - tower.transform.position;
        }

        tower.transform.position = spawnPos - visualOffset;
        tower.transform.rotation = Quaternion.identity;
        tower.transform.localScale = blueprint.prefab.transform.localScale;

        space.tower = tower;
        Debug.Log($"Built {tower.name} on {space.name}. Money: {PlayerStats.money}");

        return tower;
    }
}