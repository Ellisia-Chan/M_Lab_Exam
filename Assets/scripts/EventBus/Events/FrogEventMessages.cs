using System;

public class FrogEventInteracted { }
public class FrogEventCountChange {
    public int count;

    public FrogEventCountChange(int amount) {
        count = amount;
    }
}