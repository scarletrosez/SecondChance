-> day5kamar

VAR stat = 50

=== day5kamar
    Five days out... and still hurts and strange.
    
    // Periksa nilai stat untuk memilih jalur dialog yang tepat
    { stat > 50:
        -> statDiatas
    - else:
        -> statDibawah 
    }

=== statDiatas
    #Carlos
    Well, at least people recognize me as a better person, I guess.
    -> section2

=== statDibawah
    #Carlos
    Why do people still hate me? Just... why.
    -> section2

=== section2
    #Carlos
    I think i should rest today, something just fell off
    // Telepon berdering
    #Oscar
    Hey man, can you just.. come here, quick!!.
    
    +[Answer Chill]
        #Carlos
        Calm down, Man, tell me what you need
        #Oscar
        It’s a goddamn customer, they just chaotic now.
        #Carlos
        Fine, i’ll come

