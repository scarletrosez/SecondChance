-> day5kamar

VAR stat = 50

=== day5kamar
    #speaker:Carlos #portrait:carlos_angry
    Five days out... and still hurts and strange.
    
    // Periksa nilai stat untuk memilih jalur dialog yang tepat
    { stat > 50:
        -> statDiatas
    - else:
        -> statDibawah 
    }

=== statDiatas
    #speaker:Carlos #portrait:carlos_happy
    Well, at least people recognize me as a better person, I guess.
    -> section2

=== statDibawah
    #speaker:Carlos #portrait:carlos_angry
    Why do people still hate me? Just... why.
    -> section2

=== section2
    #speaker:Carlos #portrait:carlos_neutral
    I think i should rest today, something just fell off
    // Telepon berdering
    #speaker:Oscar #portrait:Oscar_angry
    Hey man, can you just.. come here, quick!!.
    
    +[Answer Chill]
        #speaker:Carlos #portrait:carlos_happy
        Calm down, man, tell me what you need
        #speaker:Oscar #portrait:Oscar_angry
        It’s a goddamn customer, they just chaotic now.
        #speaker:Carlos #portrait:carlos_happy
        Fine, i’ll come
        ->section3
        
    +[Answer Harshly]
        #speaker:Carlos #portrait:carlos_angry
        What the hell Man, can you just act normal
        #speaker:Oscar #portrait:Oscar_angry
        No time for me to act normal OZUL! It’s a goddamn customer, they just chaotic now.
        #speaker:Carlos #portrait:carlos_neutral
        Fine.
        ->section3
        
=== section3
    //telepon berdering lagi
    #speaker:Carlos #portrait:carlos_neutral
    Ah shit not again.
    #speaker:Anthony #portrait:Anthony_angry
    Hi Carlos, please lend me your hand here, it just too much
    #speaker:Carlos #portrait:carlos_neutral
    what do you mean?
    #speaker:Anthony #portrait:Anthony_angry
    It’s the customer, they just bloating today
    #speaker:Carlos #portrait:carlos_happy
    Alright Anthony just... wait.
    #speaker:Carlos #portrait:carlos_neutral
    Fine, ill take one of them, that means i cant choose again it should be one of them
    ->DONE
    

