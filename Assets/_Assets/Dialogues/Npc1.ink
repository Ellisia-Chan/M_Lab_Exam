VAR required_coins = 3
EXTERNAL PlayerCoins()

-> check_coins
=== check_coins ===
~ temp player_coins = PlayerCoins()

{ player_coins < required_coins:
    #speaker: none
    You need {required_coins} coins to interact.
    -> END
- else:
    -> story
}



=== story ===
#speaker: npc
Hey, you look new around here.

#speaker: player
Yeah, just arrived. Anything I should know?


#speaker: npc
That depends... What are you here for?

+ [Just exploring.]
    #speaker: player
     Just exploring. a break from the city.

    #speaker: npc
    You picked a strange time to visit.
    -> DONE

+ [Looking for someone.]
    #speaker: player
    I’m looking for someone. My brother came here weeks ago.

    #speaker: npc
    I might've seen someone like that. What’s his name?
    -> DONE

+ [Just passing through.]
    #speaker: player
    I’m just passing through. Won’t be here long.

    #speaker: npc
    Then you better stay on the main road.
    -> DONE
    
+ [None of your business.]
    #speaker: player
    That’s none of your business.

    #speaker: npc
    Fair enough. Just trying to be friendly.

#speaker: npc
Either way, keep your eyes open. This place has secrets.

-> END
