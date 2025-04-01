EXTERNAL HasInteracted()
EXTERNAL SetHasInteracted()

-> check_interaction

=== check_interaction ===

{ HasInteracted():
    #speaker: npc
    You've already faced my challenge, traveler. Farewell and good luck.
    -> END
- else:
    ~ SetHasInteracted()
    -> story
}

=== story ===
#speaker: npc
Ah, I see you've arrived. Ready to prove your mind, traveler?

#speaker: player
I’m always ready for a test.

#speaker: npc
Let’s see if your intellect can handle this one.

-> math_question

=== math_question ===
#speaker: npc
What is (7 + 5) * 2?

+ [24]
    #speaker: player
    24
    
    #speaker: npc #reward: 10
    <b><color=green>Correct!</color></b> Well done! Here are your 10 coins. You may continue.
    -> END
    
+ [20]
    #speaker: player
    20
    
    #speaker: npc
    <color=red>Incorrect.</color> That's not quite right. Better luck next time.
    -> END
    
+ [28]
    #speaker: player
    28
    
    #speaker: npc
    <color=red>Incorrect.</color> Not quite. Keep trying, traveler.
    -> END
    
+ [18]
    #speaker: player
    18
    
    #speaker: npc
    <color=red>Incorrect.</color> That’s not the answer. Think carefully next time.
    -> END
