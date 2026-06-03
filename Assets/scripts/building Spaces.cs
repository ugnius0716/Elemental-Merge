using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class buildingSpaces : MonoBehaviour
{
    [Header("Visual")]
    public GameObject visual;
    public Transform spawnPoint;

    [Header("Hover")]
    public Color hoverColor = Color.red;

    [Header("Radial Menu")]
    public GameObject towerRadialMenuPrefab;

    private Color originalColor;
    private SpriteRenderer spriteRenderer;
    private buildManager buildManager;
    private GameObject radialMenuInstance;
    private static buildingSpaces activeMenuOwner;

    public Vector3 positionOffset;
    public GameObject tower;

    void Start()
    {
        if (visual == null)
            Debug.LogWarning($"Visual not assigned on {name}");

        if (spawnPoint == null)
            spawnPoint = visual != null ? visual.transform : transform;

        spriteRenderer = visual != null ? visual.GetComponent<SpriteRenderer>() : null;
        if (spriteRenderer != null) originalColor = spriteRenderer.color;

        buildManager = buildManager.instance;
        if (buildManager == null)
            Debug.LogWarning($"buildManager.instance is null in {name}. Make sure a buildManager exists and is enabled in the scene.");
    }

    void OnMouseEnter()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;
        if (tower != null) return;
        if (spriteRenderer != null) spriteRenderer.color = hoverColor;
    }

    void OnMouseExit()
    {
        if (tower != null) return;
        if (spriteRenderer != null) spriteRenderer.color = originalColor;
    }

    void OnMouseDown()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;
        if (tower != null) return;

        // Close any other open menu
        if (activeMenuOwner != null && activeMenuOwner != this)
            activeMenuOwner.CloseRadialMenu();

        // Toggle this menu
        if (radialMenuInstance != null)
        {
            CloseRadialMenu();
            return;
        }

        // Show radial menu
        var canvas = GameObject.Find("Canvas");
        if (canvas == null || towerRadialMenuPrefab == null) return;

        radialMenuInstance = Instantiate(towerRadialMenuPrefab, canvas.transform, false);

        // Position at campfire
        var rt = radialMenuInstance.GetComponent<RectTransform>();
        if (rt != null && Camera.main != null)
        {
            Vector3 worldPos = visual != null ? visual.transform.position : transform.position;
            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
            rt.position = screenPos;
        }

        var menuUI = radialMenuInstance.GetComponent<RadialMenuUI>();
        if (menuUI != null) menuUI.owner = this;

        // Tell Shop which space was clicked
        Shop shop = FindAnyObjectByType<Shop>();
        if (shop != null) shop.SetLastClickedSpace(this);

        activeMenuOwner = this;
    }

    public void BuildTowerWithBlueprint(TowerBlueperint blueprint)
    {
        if (tower != null)
        {
            Debug.Log($"Space {name} already has a tower.");
            return;
        }

        if (blueprint == null)
        {
            Debug.LogWarning($"BuildTowerWithBlueprint called with null blueprint on {name}.");
            return;
        }

        if (blueprint.prefab == null)
        {
            Debug.LogWarning($"Blueprint for cost {blueprint.cost} has no prefab assigned. Assign the prefab in the inspector.");
            return;
        }

        if (buildManager == null)
        {
            buildManager = buildManager.instance;
            if (buildManager == null)
            {
                Debug.LogError($"No buildManager found when trying to build on {name}.");
                return;
            }
        }

        GameObject inst = buildManager.BuildTowerOn(this, blueprint);

        if (inst != null)
        {
            tower = inst;
            if (visual != null) visual.SetActive(false);
        }

        CloseRadialMenu();
    }

    public void CloseRadialMenu()
    {
        if (radialMenuInstance != null)
        {
            Destroy(radialMenuInstance);
            radialMenuInstance = null;
        }
        if (activeMenuOwner == this) activeMenuOwner = null;
    }

    void OnDisable()
    {
        if (activeMenuOwner == this) activeMenuOwner = null;
    }

    void OnDestroy()
    {
        if (activeMenuOwner == this) activeMenuOwner = null;
    }

    public Vector3 GetBuildPosition()
    {
        return (spawnPoint != null ? spawnPoint.position : transform.position) + positionOffset;
    }
}