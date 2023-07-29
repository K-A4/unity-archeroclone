using UnityEngine;
using Zenject;

public class PlayerView : MonoBehaviour, IHittable
{
    private Player player;
    private Levels levels;
    private MoneyCollector moneyCollector;

    [Inject]
    public void Construct(Player player,
        Levels levels,
        MoneyCollector moneyCollector)
    {
        this.player = player;
        this.levels = levels;
        this.moneyCollector = moneyCollector;
    }

    public void TakeDamage(float damage)
    {
        player.TakeDamage(damage);
    }

    private void OnGUI()
    {
        GUI.color = Color.black;
        var skin = GUI.skin;
        skin.label.fontSize = Screen.height / 50;
        GUI.Label(new Rect(new Vector2(100, 100), new Vector2(Screen.height / 5, Screen.height / 10)),
            $"PLayer HP: {player.Health} \nCurrent Level: {levels.CurrentIndex} \nMoneys: {moneyCollector.Money}");
    }
}
