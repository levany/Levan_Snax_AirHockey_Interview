Air Hockey Interview Project
by levan beniashvilli

tested on windows and android

------------------------------------
Getting started :
------------------------------------
for simplicity, everything is in one scene : "MASTER_SCENE"
its in the folder "Assets/_LevanInterview/_Main"

there is a script called "AirHockeyApp".
that is the root of everything.

------------------------------------------------------------------------
Disclamer
------------------------------------------------------------------------

Since this is an interview project - my goal was to demonstrate knowlage 
more than everything else,

For the sake of simplicity,
I Made assumptions and took liberties that I wouldnt do in a real world project.

for example - all classes are gameobjects with monobehaviours that sit in the main scene.
              this may cause unnecessary overhead, but its neglegable for this project.

              the benefit is that you can easily see and understand and even inspect 
              all the parts of this project right in front of you in the hirarchy.


More important - 

I split the app to 2 "package" folders : "App" and "Game".

I wanted to showcase different design approaches. so,
Everything that is "App" related (general flow, data, menues) - is designed in MVC (model-view-controller).
Everything that is "Game" related (game entities, etc) - is designed in a straight foward "Entity-Component" Hirarchy


------------------------------------------------------------------------
top 10 things to know:
------------------------------------------------------------------------

1. Main flow :

 - The "AirHockeyApp" singleton in the top of the hirarchy 
   is the main entry point and controller of the project.

 - the main flow of the app is : "Initialize everything" > "Show main menu" > "Run the game" > "Show Scores"   


2. General Architecture :

 - Thare is a cute little class called "Link".
 - it sits on the "AirHockeyApp" game object.
 - it collects references to all Controllers, Services, Models, and Views.
 - you can look at its inspector to kind of see what parts this project is made of.
   
 - evrything class the Link class to elegently access whatever it needs in the app.
   

3. the Game controller :

 - the Game controller handles the general game flow

 - it enables everything under the "Game" gameobject
   and this starts the game.


4. "Navigation" :

 - At any point in the app you can press "Esc" on PC, or the "back" button in android, 
   to stop and go back to the main menu.

 - All flows use async await, are straight-foward, and very orgenized and readable

 - Navigation between views and flows is done by just finishing the current running task, 
   and selecting the next one to continue.. very straigt foward.


5. Orgnization and predictability :

 - if you want to understand the code - I tried to make everything VERY predictible and self-explenitory.
   flows and actions happen in controllers,
   and ui stuff is presented in Views.

 - in the code, if you start typing "Controolers." or "Views." or "Events." 
   you will get an auto complete and see that all classes are very well orgnized in namespaces according to category.
   so its easy to "guess" were stuff is

 - MVC or not, Evrything that should be separated is separated.
  
 - Controlers make things happed, and dont know about UI.
 - Views show stuff, and dont do stuff.
 - Data is stored in models that don't do or show stuff.


6. Data :

 - for simplicity, 
   i made data classes scriptable objects to be able to easly view and edit in the inspector.
   But the files are only presets of course. They get cloned in runtime, when needed. 
   so changes to objects in runtime wont override data in files, only in runtime copies.
   it was really convenient 


7. Actual Gameplay :

 - when you click "Play" in the main menu - it enables the "Game" gameobject in the scene,
   this contains and enables the AirHockeyGameplayController object.
   
 - AirHockeyGameplayController runs the actual gameplay,
   it's aware of all the entities of AirHockey.
   

7.b. Game Entities : 


   - Paddle.
              - the thing you use to hit the Puck.
              - its a dynamic rigidbody with a primitive collider.
              - it has an InputController and an AI controller.

   - Puck.
              - its the "ball".
              - has a model and a primitive collier

   - Wall.
              - simple cubes with box colliders
   - Goal.
              - also simple imvisible cubes with box colliders.


   - "Player" 
               = human user. not in-game player. it has a name, points, and a color.


 - if we focus on reusibility, 
   the Entities them self should not be aware of each other,
   and, for example, comunicate using generic messages and common global services.
   that way we can copy-paste them to other projects or sections of the app, and everything just works.

 - I did implement generic messages using the Unibus library,
   and you get to see the general approach,
   but i didnt bother to make everything Super generic and separated, since its a small interview project..

8. Physics :
   
 - PaddleInputController manually moves the paddle to the pointer (mouse or touch) 

 - I Used "IBeginDragHandler, IEndDragHandler, IDragHandler" to get the pointer's position

 - to move the paddle I used unity's "FixedJoint". 
   it can be linked to a rigid body and move it thowards it in a way that is physically accurate, 
   taking colliders and physics into account.
   so if there is a wall between the paddle and the mouse pointer - it will get stuck on the wall. cleanly.

 - I also created a simple "Boundry" utility object to keep the paddles in their half of the table.


9. AI :

 - I Wrote Super simple AI. didnt bother to make anything fancy. (although there are many options for that)
   
 - it can  "Attack" = hit the puck fast,
          ,"Defend" = go to a defence position neer the goal
          ,and "Rest" = wait for a cooldown

 - it simply alternates between those states,
   when it finished doing on thing it does the other thing, and rests a little in between (and in the very beggining or a round)


10. Scoring :

 - there is a Score Controller that recives an event when you score a goal,
   and adds score to the player

 - the amount of points a goal is worth,
   and the amount of points you need to win the game,
   are configurable and are stored in a settings file (scriptable object) called "AppSettings"

 - there is also a score HUD - a world space canvas, on the game table.
 
 - there is also a ScoreView - a screen that pops when you win (or loose) 


11. Bonus : Its fun!

    - i really enjoy playing it.

    - where I come from you need 7 points to win at Air Hockey ! think you got what it takes ? :P

    - (you can always easly change that in the AppSettings)

------------------------------------	
Notes :
------------------------------------

I've worked in many systems - some simple, some complex,
and this prototype is just one example of how you can design things.

there are more approches, and this is a decent middle ground.

if you'd like to know more about my design i'd be happy to chit-chat :)

------------------------------------	
Documentation :
------------------------------------

- i wrote large comments when things might not be clear.

- in general - I didnt write comments for what is explained by the code it self.
- my approch was to write very readable and self explenetary code :)
  
- if all else failes and something is not clear,
  please feel free to ask any question, no matter how small or silly :)

------------------------------------	
Logs :
------------------------------------

- press the 0 in runtime to open a runtime log

- i used a very simple custom logger that i wrote sometime ago,
  it clearly shows the caller method for each line, and is helpfull to see thats going on.

- I Wrote a LOT of logs to be able to see EVERYTHING that is happening.
  it can be VERY helpfull :)

- there is a regular "Logger.Log(...)" log level, 

- there is a verbouse "Logger.Trace(...)" Log level that just logs everything,
  but its marked with a conditional attribute 
  and it will only show the deep logs if you have the relevent "LOGGER_TRACE" defined in the project

------------------------------------
Known Isses :
------------------------------------

- start playing, press esc to go back to main menu, and quickly press "Play" again.
  this someitmes causes wierd problems in the gameplay.


-----------------------------------------
Requirements Checklist (from the exercise email) :
-----------------------------------------

1. **Gameplay Mechanics**: yup
2. **3D & Physics**: yup
3. **Mouse Control**: yup
4. **AI Opponent**: yup - it doesnt aim a the opoonents goal - it just hits the puck at the right time.
5. **Scoring System**: yup
6. **User Interface**: yup
7. **Testing**: yup

-I also took the time to tweek the ai until it felt really fun and challanging.
-if you put you paddle in the center of the goal - its just wide enough for the puck to get through.
-if its the ai's turn and you just dont do anything - it will score a goal. and it can also defend itself well, that bastard! :P

----------------------------------------
Deliverables:
----------------------------------------

the git repo has the project files (assets, settings and packages).
and i sent you a google drive link to the latest builds for windows and android.

----------------------------------------
Thank you
----------------------------------------

Thank you :)



