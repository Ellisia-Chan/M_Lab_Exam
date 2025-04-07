using System;

public class PlayerEventDamageHit {
    public int damage;

    public PlayerEventDamageHit(int damage) {
        this.damage = damage;
    }
}

public class PlayerEventDeath { }
public class PlayerEventRespawn { }

public class PlayerEventHealthChange {
    public int health;
    public PlayerEventHealthChange(int health) {
        this.health = health;
    }
}
