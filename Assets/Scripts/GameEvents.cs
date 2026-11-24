// Lecture 09, slide 67. Events with no particular sender. They are static, so they outlive every scene:
// whatever subscribes must unsubscribe in OnDisable.
using System;

public static class GameEvents
{
    public static event Action<int, int> PlayerDamaged;
    public static event Action<int> CoinCollected;

    // Only this class can invoke its events, so the rest of the game raises them through these.
    public static void RaisePlayerDamaged(int current, int max) => PlayerDamaged?.Invoke(current, max);
    public static void RaiseCoinCollected(int value) => CoinCollected?.Invoke(value);
}
