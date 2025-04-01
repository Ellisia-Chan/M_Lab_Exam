-> not_enough_frogs

=== not_enough_frogs ===
#speaker: master_frog
Hmm... I see you have not spoken to enough of my kin, traveler.

#speaker: player
How many more do I need?

#speaker: master_frog
You must answer the questions of 10 frogs before you may pass. You are not ready yet.

-> player_questions

=== player_questions ===
#speaker: npc
Do you have any other questions before you leave?

+ [Why do I need to answer the frogs?]
    #speaker: player
    Why do I need to answer the frogs?
    
    #speaker: master_frog
    Knowledge must be earned. Only those who respect the wisdom of the Num Forest may proceed.
    -> player_questions

+ [What happens if I answer all 10?]
    #speaker: player
    What happens if I answer all 10?
    
    #speaker: master_frog
    You will be granted passage. And maybe, just maybe, you will learn something valuable.
    -> player_questions

+ [Will you at least give me a hint?]
    #speaker: player
    Will you at least give me a hint?
    
    #speaker: master_frog
    Very well. Not all numbers are as simple as they seem. Think carefully before you answer.
    -> player_questions

+ [Can I pass anyway?]
    #speaker: player
    Can I pass anyway?
    
    #speaker: master_frog
    No. The path is only for those who prove their wisdom.
    -> END
