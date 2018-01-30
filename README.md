# Blue_Odyssey_Game
The application is written using Object Oriented C# using Unity. To play the game, download or clone repository and execute the MainBuild.exe file.

## CONTROLS
### Main Menu
  WASD for directions
  Enter for select
  MUST PRESS DONE ONCE YOU HAVE MADE PLAYER SELECTIONS\
  Press continue once everything is selected
### Game
  #### P1
  - Movement:   WASD
  - Fire Left:  'G'
  - Fire Right: 'H'
  #### P2
  - Movement:   Arrow Keys
  - Fire Left:  '-'
  - Fire Right: '='
### Game Over
  Enter to select Restart

## DESIGN PATTERN IMPLEMENTATION
Implements the Object Pool Design Pattern by instantiating and recycling the cannon balls since there will most likely be many instances of them. I chose this pattern because of the amount of create and destroys I see pop up on the project hierarchy and thought that this is probably really expensive in terms of efficiency to do. So I created an object pool script and an object pool for both shrapnels and cannons, where the object pool script takes in a Game Object to know which one it is pooling, and then instead of calling instantiate and destroy, I call the object pool’s functions nextobject and destroy to set objects active or inactive if they exist already. This method only instantiates new objects if all the objects in the pool are active.

## SCRIPTS
### GameOver
  - Able to reset the text values of the stats in the Game Over screen
  - Fetches information from the value retainer and uses them to display the stats
### WindowManager
  - Extends GenericWindow
  - Holds an array of windows that can be toggled/Opened by inputting the index values
### GenericWindow
  - Finds and stores the Event System
  - Focuses selection on specified game object
  - Abstract method to open window
  - Closes window
### Options
  - Holds functions to switch from P1 option mode to P2 options mode
  - Holds functions for button on-click  events in the options window
  - Updates the text of the options window in response to option selections and modifications
### Value Retainer
  - Holds a list of values when scenes are being transitioned from option to game, and game to gameover
  - Can set values to options values
  - Can set values to game values
### BarScale
  - Stores the starting value for some health/gun charging bar, and the current value that is to be passed in
  - Displays appropriately the current value on a bar on the game panels and shows the ratio of health in text on the game panel
### CameraFollow
  - Script for the camera to follow player and restrict movement according to how close the player is to the edge of the map
### CannonBalls
  - Holds information for instantiating a cannon ball
  - Sets some scatter value so that the dispersing of the cannon balls from the guns look more natural
  - Holds a range and travel speed of the cannon ball
  - Holds a value to know whether or not this cannon ball belongs to P1 or P2
  - Stores miss and hit shots to the relative parent ship that shot it
  - Resets its position and boolean values
  - Sets animation for the CannonBalls
  - Plays sound on instantiating and splashing if the cannon ball is out of range
### Flag
  - Makes the flag appear and disappear according to the health status of its parent ship
### Gun
  - Holds a boolean value to denote whose gun this belongs to
  - Holds a boolean value to denote whether it’s a left or right gun
  - Accesses the cannon ball object pool for instantiation and object recycling
  - Resets cannon ball values upon recycling and instantiating
  - Delays the firing so player cannot spam cannon balls
  - Checks to see if the corresponding input axis has a value in order to shoot appropriately
  - Sets animation for the guns
  - Sets the gun charge bar in each of the player panels as well as the “Ready” text
  - Hides the guns if the ship’s health is below or equal to 0
  - Interprets some of the options choices (crew and armament) to alter cannon ball and reload speed characteristics
### MiniMap
  - Finds both ships and its position relative to the map and renders the dots in the minimaps in the correct relative position
### Movement
  - Checks ships collisions with objects
  - Sets the animation of the ship respectively
  - Uses the values stored in the options screen to adjust speed, acceleration and health accordingly
  - Finds the respective healthbar of the parent ship
  - Calculates acceleration using mass and force
  - Caps movement speed
  - Describes the moving motion to simulate how a ship would move on water
  - Access shrapnel object pool and spawns shrapnels every time the parent ship is hit by a  cannon ball
  - Stores a number for the total amount of ammo left
  - Plays sounds
  - Decreases ship capabilities when damage is taken
### ObjectPool
  - Ability to instantiate or recycle some gameobject
  - Ability to destroy or set inactive some gameobject
### SceneManager
  - Loads a new scene into the game upon some conditions
### ShrapScript
  - Describes the movement of a shrapnel upon instantiation or recycling
  - Scatters the shrapnel to look natural
  - Destroy the shrapnel once a certain time limit has been reached



## GAME SCENE
### Ships(Pirate/Ship)
  - Trail rendering gameobject to render the trail appropriately and not clip outside the ship’s body
  - A set of Right and Left Guns that holds the Gun script and animator to set the animations
  - Holds 3 Pirate/Ship Flag Pole to set the location of the flags relative to the ship
    - The actual flag with the animations associated with it
### P1/P2 Canvas Holds the frame and display information of both the left and right screen
  - Left Frame renders the GUI
  - Holds a health bar with the BarScale script attached to it as well as a sprite renderer
  - Holds a Cannon Charge Left/Right bar to display appropriately the reload status of the guns on the left and right
  - Holds a MiniMap that includes the black dot for the pirate’s position and a red for the regular ship’s position along with the MiniMap script
  - Holds a health text to display health ratio
  - Holds a LGunTxt/RGunTxt to show if the reload is ready
  - Holds a label and the number of crews currently on the ship
  - Holds a label and the speed currently associated with the ship
  - Holds a label and the number of ammo currently on the ship
  - Holds the Map information
  - Holds the Quad with collision edges
  - Holds all the islands and its colliders
### EventSystem (Not Altered)
  - CannonObjPool to store and recycle instantiated cannon balls
  - ShrapnelObjPool to store and recycle instantiated shrapnels
  - Holds the Values gameobject to access options selected in the options window
  - Holds both P1 and P2 cameras



## OPTIONS SCENE
- Holds a Main Camera to show the options
- Option Canvas to hold all buttons and text associated with the options window which has the window manager script attached to it
  - Options Window that holds the options and has the options script attached to it
    - Holds the Title of the Game
    - Option window holds the title and P1/P2 Option buttons and related text
    - Has a start button with the Scene Manager within it
- Holds an Event System to hold the input modules for the selection mechanism in the options
- Holds a Values game object to store the information from the options to the actual game



## GAMEOVER SCENE
- Holds a Main Camera to show the Game Over screen
- Holds a GameOverCanvas to hold the window manager
  - Holds the Game Over Window with the Game Over script attached to it
    - Has the stats for P1 and P2 as well as a restart button
- EventSystem (Not Altered)
