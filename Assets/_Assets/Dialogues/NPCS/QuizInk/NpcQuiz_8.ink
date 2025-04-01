EXTERNAL HasInteracted()
EXTERNAL SetHasInteracted()

-> check_interaction

=== check_interaction ===

{ HasInteracted():
    #speaker: npc
    You've already answered my question, traveler. May your path be safe.
    -> END
- else:
    ~ SetHasInteracted()
    -> story
}

=== story ===
#speaker: npc
Greetings, adventurer. Do you have the wits to pass my test?

#speaker: player
I’m ready. What is the challenge?

#speaker: npc
A simple one, but it will make you think. Solve this correctly, and you may proceed.

-> math_question

=== math_question ===
#speaker: npc
What is 15 - 7 + 4?

+ [12]
    #speaker: player
    12
    
    #speaker: npc #reward: 10
    <b><color=green>Correct!</color></b> Well done! You have earned 10 coins. Continue with caution, traveler.
    -> END
    
+ [10]
    #speaker: player
    10
    
    #speaker: npc
    <color=red>Incorrect.</color> That's not quite right. Try again next time.
    -> END
    
+ [14]
    #speaker: player
    14
    
    #speaker: npc
    <color=red>Incorrect.</color> Not the right answer. Better luck on your next try.
    -> END
    
+ [13]
    #speaker: player
    13
    
    #speaker: npc
    <color=red>Incorrect.</color> That’s not the answer. Keep thinking!
    -> END
