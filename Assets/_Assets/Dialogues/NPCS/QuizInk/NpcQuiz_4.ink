EXTERNAL HasInteracted()
EXTERNAL SetHasInteracted()

-> check_interaction

=== check_interaction ===

{ HasInteracted():
    #speaker: npc
    You've already attempted my challenge, traveler. Best of luck on your journey.
    -> END
- else:
    ~ SetHasInteracted()
    -> story
}

=== story ===
#speaker: npc
Ah, another traveler seeking passage! But first, a test of numbers.

#speaker: player
If it’s a test, I’m ready.

#speaker: npc
Very well. Solve this, and you may continue.

-> math_question

=== math_question ===
#speaker: npc
What is 6 + 7?

+ [13]
    #speaker: player
    13
    
    #speaker: npc #reward: 10
    <b><color=green>Correct!</color></b> Impressive! Take these 10 coins and be on your way.
    -> END
    
+ [14]
    #speaker: player
    14
    
    #speaker: npc
    <color=red>Incorrect.</color> That is not the right answer. Perhaps next time.
    -> END
    
+ [12]
    #speaker: player
    12
    
    #speaker: npc
    <color=red>Incorrect.</color> That’s not quite right. Pay closer attention.
    -> END
    
+ [15]
    #speaker: player
    15
    
    #speaker: npc
    <color=red>Incorrect.</color> That is not correct. Safe travels regardless.
    -> END
