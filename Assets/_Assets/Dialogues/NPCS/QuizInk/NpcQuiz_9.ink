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
Ah, a new traveler. Let’s see if you have the sharp mind needed for the journey ahead.

#speaker: player
I’m ready for whatever you throw at me.

#speaker: npc
Good! Here’s your test. Solve this correctly, and you may proceed.

-> math_question

=== math_question ===
#speaker: npc
What is 18 / 3 + 4?

+ [10]
    #speaker: player
    10
    
    #speaker: npc #reward: 10
    <b><color=green>Correct!</color></b> Impressive! Here are your 10 coins. Continue with wisdom.
    -> END
    
+ [12]
    #speaker: player
    12
    
    #speaker: npc
    <color=red>Incorrect.</color> That’s not the right answer. Better luck next time.
    -> END
    
+ [8]
    #speaker: player
    8
    
    #speaker: npc
    <color=red>Incorrect.</color> Try again, traveler. The answer lies within you.
    -> END
    
+ [14]
    #speaker: player
    14
    
    #speaker: npc
    <color=red>Incorrect.</color> Not quite. Think carefully next time!
    -> END
