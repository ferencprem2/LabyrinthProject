const changeLang = (languageCode) => {
    document.documentElement.setAttribute("lang", languageCode);
};
function nyelvetvalt(){
    if(document.documentElement.getAttribute("lang")=="hu")
    {
    document.getElementById("Cim").innerHTML="Labirintus játék útmutató";
    document.getElementById("basic").innerHTML="Alap tudnivalók";
    document.getElementById("basic2").innerHTML="A 'w'/↑ és 's'/↓ billentyűkkel tudsz váltani a menüpontok közül választani pedig az 'Enter' billentyű lenyomásával tudsz.";
    document.getElementById("settings").innerHTML="2 nyelv és 5 szín közül választhatsz.";
    document.getElementById("load").innerHTML="A játék megkér hogy tölts be egy térképet. Nyomd meg az 'Entert' hogy tovább haladj.";
    document.getElementById("load2").innerHTML="Írd le a térkép nevét a 'Fájlnév:' után. Ezután nyomd meg az 'Enter'-t.";    
    document.getElementById("diff").innerHTML="Képes vagy nehézséget választani. A nehézség a térképen megtehető maximális lépéseidre van hatással.";
    document.getElementById("controls").innerHTML="Miután beléptél a játékba a 'w','a','s','d' billenytűkkel vagy a ↑ , ← , ↓ , → billentyűkkel tudsz mozogni a térképen.";
    document.getElementById("w").innerHTML="Nyomd meg a 'w' vagy a ↑ billentyűt hogy felfelé menj.";
    document.getElementById("a").innerHTML="Nyomd meg a 'a' vagy a ← billentyűt hogy balra menj.";
    document.getElementById("s").innerHTML="Nyomd meg a 's' vagy a ↓ billentyűt hogy lefelé menj.";
    document.getElementById("d").innerHTML="Nyomd meg a 'd' vagy a →  billentyűt hogy jobbra menj.";
    document.getElementById("treasure").innerHTML="Kincses termek";
    document.getElementById("treasure2").innerHTML="Kincses termeket találhatsz a térképen. A kincs megszerzéséhez csak be kell menned a terembe. A játék a jobb felső sarokban számolja hogy hágy kincset szereztél meg."
    document.getElementById("exit").innerHTML="Kijáratok";
    document.getElementById("exits").innerHTML="A képen egy példát mutatunk a kijáratokra. Ha ide mész a játék megkérdezi hogy biztosan elakarod-e hagyni az aktuális térképet.";
    document.getElementById("reallyexit").innerHTML="Ha tényleg el akarod hagyni a térképet nyomd meg az 'y' billentyűt. Ezután ezt fogod látni:";
    document.getElementById("save").innerHTML="Hogyan ments el egy térképet";
    document.getElementById("save").nextElementSibling.innerHTML="Nyomd meg az 'Esc' billentyűt és látni fogsz egy menüt.";
    document.getElementById("save2").innerHTML="Ha elakarod menteni a térképet válaszd a 'Mentés' opciót, a program megkér hogy írj be egy nevet a mentésnek.";

}
    else if (document.documentElement.getAttribute("lang")=="en"){
        document.getElementById("Cim").innerHTML="Labyrinth game guide";
        document.getElementById("basic").innerHTML="Basic things";
        document.getElementById("basic2").innerHTML="You can navigate beetween the menu points by pressing 'w'/↑ and 's'/↓ , to choose from them press the 'Enter' key on your keyboard.";
        document.getElementById("settings").innerHTML="You can choose form 2 languages and from 5 colors.";
        document.getElementById("load").innerHTML="The game will ask you to load a map. Press 'Enter' if you are on this window to continue.";
        document.getElementById("load2").innerHTML="Write the name of the map after the 'File name:'. After that press 'Enter'.";
        document.getElementById("diff").innerHTML="You will be able to choose difficulty. The difficulty has impact on how many steps can you make maximum on the map.";
        document.getElementById("controls").innerHTML="After you start the game and you are on the map you can move with the 'w','a','s','d' keys or the ↑ , ← , ↓, → keys on your keyboard.";
        document.getElementById("w").innerHTML="Press 'w'/↑ to move up.";
        document.getElementById("a").innerHTML="Press 'a'/← to move left.";
        document.getElementById("s").innerHTML="Press 's'/→ to move down.";
        document.getElementById("d").innerHTML="Press 'd'/↓ to move right.";
        document.getElementById("treasure").innerHTML="Treasure rooms";
        document.getElementById("treasure2").innerHTML="There are treasure rooms on the map. You can acquire the treasure if you just need to enter to the room. The game will count you the amount of tresures you obtained in the up right corner."
        document.getElementById("exit").innerHTML="Exits";
        document.getElementById("exits").innerHTML="On the picture we show you an example for an exit in the game. If you go out there the game will ask you if you really want to leave the map.";
        document.getElementById("reallyexit").innerHTML="If you really want to exit press 'y'. And then you will see this:";
        document.getElementById("save").innerHTML="How to save the map";
        document.getElementById("save").nextElementSibling.innerHTML="Press the 'Esc' key on your keyboard and you will see a menu.";
        document.getElementById("save2").innerHTML="If you choose the option 'Save' the program will ask you to enter a name for the save.";

    }
}