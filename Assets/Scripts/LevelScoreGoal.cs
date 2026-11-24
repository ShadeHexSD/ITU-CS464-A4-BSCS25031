// Shared score target for the HUD and both legacy managers.
// The HUD prefab is reused by every blockout, so its serialized "10" cannot be right for
// levels that contain 6 or 7 coins. This counts the coins actually loaded in the active
// scene, including coins nested inside prefab instances, and returns their total value.
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelScoreGoal
{
    public static int TotalInActiveScene() => TotalInScene(SceneManager.GetActiveScene());

    public static int TotalInScene(Scene scene)
    {
        if (!scene.IsValid() || !scene.isLoaded) return 0;

        int total = 0;
        Coin[] coins = Object.FindObjectsByType<Coin>(
            FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (Coin coin in coins)
        {
            if (coin != null && coin.gameObject.scene == scene)
                total += coin.Value;
        }

        return total;
    }
}
