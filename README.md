# CSE4500_Project

## Input / Action
- WASD Movement for Player.
- Space / Click to shoot for some form of player.
- Mouse to aim.

## Obstacles
- Enemies: Generate regularly.
  - Pig: HP lower than half, change to Angry Pig.
  - Ghost: Invulnerable every 5 seconds.
  - Bee: Shoot to Player.
  - ....

## Rewards
- Survival Rewards: Every 30 seconds, player upgrade.
- Coin Rewards: Beat enemy to gian coin. Use coin to buy upgrade.
- The final score is the score of this game.

## Code
- Player:
  - PlayerController
  - PlayerShoot
  -
- Enemy:
  - Enemy: Base Class.
  - A* to track Player.
  - Different Enemy has different derived class.
- GameController:
  - Upgrade
  - Coin    
