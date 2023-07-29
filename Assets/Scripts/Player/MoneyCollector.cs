using Zenject;

public class MoneyCollector
{
    public int Money => money;
    private int money;
    private SignalBus signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        this.signalBus = signalBus;
        signalBus.Subscribe<EnemyDied>(OnEnemyDied);
    }

    private void OnEnemyDied(EnemyDied enemyInfo)
    {
        money += enemyInfo.enemy.MoneyValue;
    }
}
