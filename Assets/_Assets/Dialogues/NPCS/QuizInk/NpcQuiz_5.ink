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
Ah, another wanderer! Come closer. I’ve got a challenge for you.

#speaker: player
I’m ready for whatever test you’ve got.

#speaker: npc
Very well. Solve this, and you may continue.

-> math_question

=== math_question ===
#speaker: npc
What is 12 + 8 - 4?

+ [16]
    #speaker: player
    16
    
    #speaker: npc #reward: 10
    <b><color=green>Correct!</color></b> Excellent! Here are 10 coins. Proceed safely.
    -> END
    
+ [18]
    #speaker: player
    18
    
    #speaker: npc
    <color=red>Incorrect.</color> That’s not quite right. Better luck next time.
    -> END
    
+ [20]
    #speaker: player
    20
    
    #speaker: npc
    <color=red>Incorrect.</color> That’s not the correct answer. Don’t give up, though!
    -> END
    
+ [15]
    #speaker: player
    15
    
    #speaker: npc
    <color=red>Incorrect.</color> Not quite. You’ll get it next time!
    -> END
