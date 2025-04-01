EXTERNAL HasInteracted()
EXTERNAL SetHasInteracted()

-> check_interaction

=== check_interaction ===

{ HasInteracted():
    #speaker: npc
    You've already answered my question, traveler. Safe travels.
    -> END
- else:
    ~ SetHasInteracted()
    -> story
}

=== story ===
#speaker: npc
Ah, you look like someone ready for a little brain teaser!

#speaker: player
I’m always up for a challenge.

#speaker: npc
Here’s a small one, but beware—it’s a tricky one. Solve this correctly, and you may continue.

-> math_question

=== math_question ===
#speaker: npc
What is 3 + 3 * 0?

+ [6]
    #speaker: player
    6
    
    #speaker: npc
    <color=red>Incorrect.</color> Remember, the order of operations! Try again.
    -> END
    
+ [0]
    #speaker: player
    0
    
    #speaker: npc #reward: 10
    <b><color=green>Correct!</color></b> Well done! Here are your <color=\#FFD700>10 coins</color>. You may proceed.
    -> END
    
+ [3]
    #speaker: player
    3
    
    #speaker: npc
    <color=red>Incorrect.</color> Not quite. The multiplication happens first! Try again.
    -> END
    
+ [9]
    #speaker: player
    9
    
    #speaker: npc
    <color=red>Incorrect.</color> Think carefully about the order of operations.
    -> END
