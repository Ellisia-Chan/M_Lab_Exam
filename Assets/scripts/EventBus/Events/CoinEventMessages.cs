using System.Collections;
using System.Collections.Generic;

public class CoinCollectedEvent {
    public int amount;

    public CoinCollectedEvent(int amount) {
        this.amount = amount;
    }
}

public class CoinSpendEvent {
    public int amount;

    public CoinSpendEvent(int amount) {
        this.amount = amount;
    }
}

public class CoinValueChangeEvent {
    public int amount;

    public CoinValueChangeEvent(int amount) {
        this.amount = amount;
    }
}
