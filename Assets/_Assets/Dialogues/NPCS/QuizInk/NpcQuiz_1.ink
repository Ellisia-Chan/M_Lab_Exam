EXTERNAL HasInteracted()
EXTERNAL SetHasInteracted()

-> check_interaction

=== check_interaction ===

{ HasInteracted():
    #speaker: npc
    You've already solved my challenge, traveler. Move along.
    -> END
- else:
    ~ SetHasInteracted()
    -> story
}

=== story ===
#speaker: npc
Ah, a traveler! If you wish to pass, you must answer my question correctly.

#speaker: player
I'm listening.

#speaker: npc
Very well. Here is your challenge.

-> math_question

=== math_question ===
#speaker: npc
What is (6 + 4) / 2?

+ [5]
    #speaker: player
    5
    
    #speaker: npc #reward: 10
    <b><color=green>Correct!</color></b> Here is your 10 coins.
    -> END
    
+ [10]
    #speaker: player
    10
    
    #speaker: npc
    <color=red>Incorrect.</color> That is not the right answer.  Sorry!
    -> END
    
+ [3]
    #speaker: player
    3
    
    #speaker: npc
    <color=red>Incorrect.</color> That is not the right answer.  Sorry!
    -> END
    
+ [4]
    #speaker: player
    4
    
    #speaker: npc
    <color=red>Incorrect.</color> That is not the right answer. Sorry!
    -> END
