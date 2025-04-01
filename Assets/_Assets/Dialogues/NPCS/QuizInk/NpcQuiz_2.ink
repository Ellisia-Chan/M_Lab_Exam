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
Halt, traveler! A quick test of wits stands before you.

#speaker: player
What kind of test?

#speaker: npc
A simple one. Solve this, and you may proceed.

-> math_question

=== math_question ===
#speaker: npc
What is 7 + 3?

+ [10]
    #speaker: player
    10
    
    #speaker: npc #reward: 10
    <b><color=green>Correct!</color></b> You have a sharp mind! Take these 10 coins and continue your journey.
    -> END
    
+ [9]
    #speaker: player
    9
    
    #speaker: npc
    <color=red>Incorrect.</color> That is not the correct answer. Be more careful next time.
    -> END
    
+ [11]
    #speaker: player
    11
    
    #speaker: npc
    <color=red>Incorrect.</color> That is not the right answer. Try thinking it through next time.
    -> END
    
+ [12]
    #speaker: player
    12
    
    #speaker: npc
    <color=red>Incorrect.</color> That is not the correct answer. Safe travels, traveler.
    -> END