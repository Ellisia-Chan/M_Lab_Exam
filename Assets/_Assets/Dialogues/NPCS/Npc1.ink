VAR required_coins = 0

EXTERNAL HasInteracted()
EXTERNAL SetHasInteracted()
EXTERNAL PlayerCoins()

-> check_coins

=== check_coins ===
~ temp player_coins = PlayerCoins()

{ HasInteracted():
    #speaker: npc
    Good luck, traveler.
    -> END
- else:
    { player_coins < required_coins:
        #speaker: none
        You need {required_coins} coins to interact.
        -> END
    - else:
        ~ SetHasInteracted()
        -> story
    }
}


=== story ===
#speaker: npc
Hey, you look new around here.

#speaker: player
Yeah, I just arrived. I just need to pass through this forest. Anything I should know?

#speaker: npc
You're entering the Num Forest.

#speaker: npc
To pass through this forest, you must find 10 frogs and answer their questions.

#speaker: npc
They will give you 1 diamond if your answer is correct.

#speaker: npc
At the end of the forest, Master Num Frog will take your diamonds as a fee to pass through.

#speaker: player
Are the questions hard?

#speaker: npc
Well, it depends on the frog, so...

#speaker: player
I'll take the challenge.

#speaker: npc
Well, good luck, traveler. Oh, and remember...

#speaker: npc
There are spikes and pitfalls in the forest, so be careful.

#speaker: player
I'll keep that in mind. Thank you!

-> END
