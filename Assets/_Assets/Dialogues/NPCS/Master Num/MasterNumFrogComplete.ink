EXTERNAL HasInteracted()
EXTERNAL SetHasInteracted()

-> check_interaction

=== check_interaction ===

{ HasInteracted():
    #speaker: npc
    You have already paid the toll, traveler. The path is open to you.
    -> END
- else:
    ~ SetHasInteracted()
    -> story
}

=== story ===
#speaker: npc
Welcome, traveler. I am Master Num Frog, You have answered 10 of my kin’s questions, but one final task remains.

#speaker: player
What must I do?

#speaker: npc
The toll to pass is all the <color=\#FFD700>coins</color>. Hand them over, and the path shall be yours.

#speaker: player
Here you go.

#speaker: npc #coinSpend: 100
The path is now open to you. Travel wisely.

-> player_questions

=== player_questions ===
#speaker: npc
Do you have any other questions before you go?

+ [Why do I need to pay coins?]
    #speaker: player
    Why do I need to pay <color=\#FFD700>coins</color>?
    
    #speaker: npc
    Passage through the Num Forest is a privilege, not a right. Knowledge and wisdom must be earned.
    -> player_questions

+ [What is beyond this path?]
    #speaker: player
    What is beyond this path?
    
    #speaker: npc
    A world of challenges and mysteries. But you must discover it yourself.
    -> player_questions

+ [Can I get my coins back?]
    #speaker: player
    Can I get my <color=\#FFD700>coins</color> back?
    
    #speaker: npc
    Ha! You are bold, but no. The toll is final.
    -> player_questions

+ [No more questions.]
    #speaker: player
    No more questions.
    
    #speaker: npc
    Then go, traveler. Your journey awaits.
    -> END
