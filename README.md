# Penguin Sidescroller — Game Development II (Unity)

A 2D sidescroller made in Unity during my Game Development II class at Seamedu.
A penguin runs, jumps, shoots and collects coins while fighting enemies.

## What this project shows
| Pattern / concept | Where |
|---|---|
| **State machine** (Idle, Walk, Jump, Attack) | `Assets/SimpleStateMachine/` |
| **Singleton** (game manager, score kept across scenes) | `GameManagerSingleton.cs`, `ScoreManager.cs` |
| **Factory** (spawns Dragon / Zombie / Monster by type) | `Factory.cs`, `EnemyType.cs` |
| **Object pooling** (bullets and coins, Unity `ObjectPool`) | `BulletManager.cs`, `CoinManagerPool.cs` |
| **Interfaces** | `IEnemy`, `IState`, `IColorable` |
| **Events / delegates** (player death, state change) | `Player.cs`, `DelegatesPractice.cs` |
| **Saving** (high score with PlayerPrefs) | `ScoreManager.cs` |

## Built with
Unity 6.5, C#, TextMeshPro

## Controls
- Move: A / D
- Jump: Space
- Shoot: Left mouse button

## Screenshots
(coming soon)

## Credits
Built during my Game Development II class at Seamedu.
Some code comes from my faculty's class demos; the rest I wrote and extended myself.
