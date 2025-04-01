EXTERNAL HasInteracted()
EXTERNAL SetHasInteracted()

-> check_interaction

=== check_interaction ===

{ HasInteracted():
    #speaker: npc
    You've already taken my test, traveler. Best of luck on your journey.
    -> END
- else:
    ~ SetHasInteracted()
    -> story
}

=== story ===
#speaker: npc
Ah, a new face! I see you're ready for a challenge.

#speaker: player
You bet! What do you have for me?

#speaker: npc
Something simple, but challenging enough to test your wits.

-> math_question

=== math_question ===
#speaker: npc
What is 9 * (3 - 1)?
    
+ [22]
    #speaker: player
    22
    
    #speaker: npc
    <color=red>Incorrect.</color> That's not quite right. Better luck next time.
    -> END
    
+ [16]
    #speaker: player
    16
    
    #speaker: npc
    <color=red>Incorrect.</color> Oops! That's not correct. Try again, traveler.
    -> END
    
+ [18]
    #speaker: player
    18
    
    #speaker: npc #reward: 10
    <b><color=green>Correct!</color></b> Impressive! Here are your <color=\#FFD700>10 coins</color>. Good luck on your journey.
    -> END
    
+ [24]
    #speaker: player
    24
    
    #speaker: npc
    <color=red>Incorrect.</color> Not the right answer. Keep trying, and you'll get it!
    -> END
